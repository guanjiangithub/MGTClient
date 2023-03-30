using MGTClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MGTClient
{
    public partial class Form1 : Form
    {
        private uint m_hand;
        private List<VinModel> m_VinModels = new List<VinModel>();
        public Form1()
        {
            InitializeComponent();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int index = MGTSDK.GtNumber();
            for (int i = 0; i < index; i++)
            {
                MGTSDK.MultiMsgInCallback multiMsgInCallback = new MGTSDK.MultiMsgInCallback(MultiMsgInCallback);
                MGTSDK.MultiLogCallback multiLogCallback = new MGTSDK.MultiLogCallback(MultiLogCallback);
                m_hand = MGTSDK.GtLaunch(i, multiLogCallback, multiMsgInCallback);
            }
        }

        private void MultiMsgInCallback(uint hSrc, int hMsgIn)
        {
            string str = "hSrc:" + hSrc + " hMsgIn:" + hMsgIn;
            log(str);
        }

        private void MultiLogCallback(uint hSrc, int msgId, int ms, string msg)
        {
            
            string str = "hSrc:0x" + hSrc.ToString("x") + " msgId:" + msgId + " ms:" + ms + " msg:" + msg;
            log(str);
            EventType eventType = (EventType)msgId;
            switch (eventType)
            {
                case EventType.LAUNCH_STEP:// 启动，及以下正常步骤
                    {

                    }
                    break;
                case EventType.RUN_MSG:// 运行时信息
                    {
                    }
                    break;
                case EventType.RUN_EVENT:// 运行中发生指定事件
                    {

                    }
                    break;
                case EventType.RUN_WARNING:       // 运行时警告
                    {

                    }
                    break;
                case EventType.RUN_DEBUG:// 运行时跟踪信息
                    {

                    }
                    break;
                case EventType.ATS_LOADING: // ATS正在加载
                    {

                    }
                    break;
                case EventType.ATS_LOAD_ABORT:// ATS加载不成功
                    { }
                    break;
                case EventType.ATS_LOAD_OK:   // ATS加载成功
                    { }
                    break;
                case EventType.ATS_STARTING:// ATS启动中
                    {

                    }
                    break;
                case EventType.ATS_STARTED:   // ATS已经启动
                    {

                    }
                    break;
                case EventType.ATS_ACTION:   // ATS正常程控动作
                    { }
                    break;
                case EventType.ATS_ABORT:// ATS异常中止
                    { }
                    break;
                case EventType.ATS_OVER:  // ATS结束
                    { }
                    break;
                case EventType.API_DEBUG:// 应用调用接口跟踪
                    { }
                    break;

                case EventType.HOTLINE_DROPPED: // 通讯掉线
                    { }
                    break;
                case EventType.HOTLINE_RESUMED:  // 通讯恢复
                    { }
                    break;

                case EventType.DOWNLOAD_STEP:
                    { }
                    break;
                case EventType.DOWNLOAD_ABORT:
                    { }
                    break;
                case EventType.DOWNLOAD_OK:
                    { }
                    break;
                case EventType.UPLOAD_STEP:
                    { }
                    break;
                case EventType.UPLOAD_ABORT:
                    { }
                    break;
                case EventType.UPLOAD_OK:
                    { }
                    break;
                case EventType.SYNC_STEP:
                    { }
                    break;
                case EventType.SYNC_NUMBER:
                    {

                    }
                    break;
                case EventType.SYNC_WAIT:
                    {

                    }
                    break;
                case EventType.SYNC_GO:
                    { }
                    break;
                case EventType.SYNC_ABORT:
                    { }
                    break;
                case EventType.SYNC_OK:
                    { }
                    break;

                case EventType.LAUNCH_ABORT:  // 启动异常中止
                    { }
                    break;
                case EventType.LAUNCH_OK:// 启动正常完成
                    {

                        SubscribeChannels();
                    }
                    break;
                case EventType.CLOSE_STEP:    // 关闭，及以下步骤
                    { }
                    break;
                case EventType.CLOSE_OK:// 关闭正常完成
                    { 
                        
                    }
                    break;
            }

        }

        private void log(string dd)
        {
            this.Invoke((EventHandler)delegate
            {
                richTextBox1.AppendText(dd+"\r\n");
            });
        }

        private void SubscribeChannels()
        {
            byte[] buffer = new byte[1024];
            int number=  MGTSDK.GtGetVinList(m_hand, buffer);
            string str = Encoding.Default.GetString(buffer).Trim();
            string[] names = str.Split(',');
            for (int i = 0; i < number; i++)
            {
               int intper= MGTSDK.GtSubscribeForVin(m_hand, names[i]);
                if (intper > 0)
                {
                    VinModel vinModel = new VinModel();
                    vinModel.Name = names[i];
                    vinModel.ChannelHandle = intper;
                    m_VinModels.Add(vinModel);
                }
            }
            this.Invoke((EventHandler)delegate
            {
                for (int i = 0; i < m_VinModels.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["ChannelName"].Value = m_VinModels[i].Name;
                }
                timer1.Enabled = true;
            });
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int n = MGTSDK.GtTakeSnapshot(m_hand);
            if (n >0)
            {
                foreach (var item in m_VinModels)
                {
                    uint[] loadBuffer = new uint[n];
                    int p= MGTSDK.GtGetVinBlock(m_hand, item.ChannelHandle, loadBuffer);
                    double nv = 0;
                    int dot = 2;
                    for (int i = 0; i < n; i++)
                    {
                        nv = MGTData.Data_Dvb2Float(loadBuffer[i]);
                        if (i == 0)
                        {
                            dot = MGTData.Data_DotOfDvb(loadBuffer[i]);
                        }
                        if (item.IsWaitZero)
                        {
                            item.IsWaitZero = false;
                            item.Zero = nv;
                        }
                    }
                    item.Dot = (uint)dot;
                    item.Value= nv - item.Zero;
                    IEnumerable<DataGridViewRow> enumerableList = this.dataGridView1.Rows.Cast<DataGridViewRow>();
                     List<DataGridViewRow>  lists = (from items in enumerableList
                                                  where items.Cells["ChannelName"].Value.ToString() == item.Name.Trim()
                                                  select items).ToList();
                    if (lists.Count > 0)
                    {
                        foreach (var list in lists)
                        {
                            list.Cells["ChannelValue"].Value = item.Value;
                        }
                    }

                    //dataGridView1.Rows.Insert(0, item);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            timer1.Enabled = false;
            m_VinModels.Clear();
            
            MGTSDK.GtClose(m_hand);
            m_hand = 0;
        }
    }
}
