using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MGTClient
{
    public class MGTSDK
    {
        private const string Path = "mgt.dll";

        public delegate void  MultiLogCallback (uint hSrc, int msgId, int ms, string msg);
        public delegate void  MultiMsgInCallback (uint hSrc, int hMsgIn);

        /// <summary>
        /// 获得MGT库版本号
        /// </summary>
        /// <returns></returns>
        [DllImport(Path, CallingConvention=CallingConvention.StdCall)]
        public extern static uint GtGetDllVer();

        /// <summary>
        /// 获得控制设备数量
        /// </summary>
        /// <returns></returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int  GtNumber();

        /// <summary>
        /// 启动控制器
        /// </summary>
        /// <param name="cindex"></param>
        /// <param name="fpLog"></param>
        /// <param name="fpClientMsgIn"></param>
        /// <returns></returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static uint GtLaunch(int cindex, MultiLogCallback fpLog, MultiMsgInCallback  fpClientMsgIn);

        /// <summary>
        /// 结束运行，断开与代龙控制器的连接
        /// </summary>
        /// <param name="hDyna"></param>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static void GtClose(uint hDyna);

        /// <summary>
        /// 获得MGT运行日志事件
        /// </summary>
        /// <param name="hDyna"></param>
        /// <param name="msgBuf"></param>
        /// <param name="bufLen"></param>
        /// <returns></returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int GtGetLogEvent(uint hDyna, byte[] msgBuf, int bufLen);

        /// <summary>
        /// 获得产品信息
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="pInfo">存放名录的缓存区</param>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static void GtGetProductInfo(uint hDyna, IntPtr pInfo);

        /// <summary>
        /// 获得全部测量对象Vin的名录
        /// </summary>
        /// <param name="hDyna"></param>
        /// <param name="tagList"></param>
        /// <returns>测量对象的数量</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int GtGetVinList(uint hDyna, byte[] tagList);


        /// <summary>
        /// 获得全部数字输入对象Din的名录
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="tagList">存放名录的缓存区</param>
        /// <returns>数字输入对象的数量</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int  GtGetDinList(uint hDyna, byte[] tagList);

        /// <summary>
        /// 获得全部开关输出对象Dout的名录
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="tagList">存放名录的缓存区</param>
        /// <returns>开关输出对象的数量</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int  GtGetDoutList(uint hDyna, byte[] tagList);

        /// <summary>
        /// 订阅Vin对象
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="vinTag">Vin对象名称</param>
        /// <returns>表示Vin对象的句柄。0表示不存在。</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int GtSubscribeForVin(uint hDyna, string vinTag);

        /// <summary>
        /// 获得测量通道的最大满量程
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="hVin">Vin对象句柄</param>
        /// <returns>DVB格式表示的量程数</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static uint GtGetMaxScale(uint hDyna, int hVin);

        /// <summary>
        /// 获得测量单位码
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="hVin">Vin对象句柄</param>
        /// <returns></returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int  GtGetUnitCode(uint hDyna, int hVin);


        /// <summary>
        /// 示值清零
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="hVin">Vin对象句柄</param>
        /// <param name="vled">指定显示值</param>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static void GtClearToZero(uint hDyna, int hVin, double vled);


        /// <summary>
        /// 订阅Din对象
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="dinTag">Din对象名称</param>
        /// <returns>表示Din对象的句柄。0表示不存在。</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int GtSubscribeForDin(uint hDyna,string dinTag);

        /// <summary>
        /// 快照MGT实时测量数据到用户线程缓存区
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <returns>数据点的数量</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]

        public extern static int GtTakeSnapshot(uint hDyna);


        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int GtGetTimeBlock(uint hDyna, byte[] tBuf);

        /// <summary>
        /// 获得快照区测量数据块
        /// </summary>
        /// <param name="hDyna">动态控制器句柄</param>
        /// <param name="hVin">指定测量通道Vin对象句柄</param>
        /// <param name="vBuf">存放测量数据块的缓存区</param>
        /// <returns>实际获得的数据个数</returns>
        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public extern static int GtGetVinBlock(uint hDyna, int hVin, uint[] vBuf);


    }

    public class MGTData
    {
        private const string Path = "data.dll";
        [DllImport(Path)]
        public static extern int Data_GetVer();

        [DllImport(Path)]
        public static extern int Data_DvbIsPositive(uint dvb);

        [DllImport(Path)]
        public static extern int Data_DvbIsInteger(uint dvb);

        [DllImport(Path)]
        public static extern int Data_DotOfDvb(uint dvb);

        [DllImport(Path)]
        public static extern int Data_Dvb2Int(uint dvb);

        [DllImport(Path)]
        public static extern uint Data_Int2Dvb(int iv);

        [DllImport(Path)]
        public static extern double Data_Dvb2Float(uint dvb);


        [DllImport(Path)]
        public static extern uint Data_Float2Dvb(double fv, int dot);

        [DllImport(Path)]
        public static extern uint Data_FreeFloat2Dvb(double fv);

        [DllImport(Path)]
        public static extern uint Data_DvbReform(uint dvb, int dot);

        [DllImport(Path)]
        public static extern int Data_DvbCompare(uint dvb1, uint dvb2);

        [DllImport(Path)]
        public static extern void Data_Dvb2String(char[] buf, uint dvb);

        [DllImport(Path)]
        public static extern uint Data_String2Dvb(string str);

        [DllImport(Path)]
        public static extern int Data_GetDvbCount(uint[] dvbs);

        [DllImport(Path)]
        public static extern int Data_Dvbs2String(char[] buf, uint[] dvbs);

        [DllImport(Path)]
        unsafe public static extern int Data_String2Dvbs(uint[] dvbs, char* str);

        [DllImport(Path)]
        public static extern short Data_Int2Bcd(int iv);

        [DllImport(Path)]
        public static extern int Data_Bcd2Int(short bcd);

        [DllImport(Path)]
        public static extern void Data_Bcd2String(char[] buf, short bcd);

        [DllImport(Path)]
        unsafe public static extern short Data_String2Bcd(char* str);
    }
}
