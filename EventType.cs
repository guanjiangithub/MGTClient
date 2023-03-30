using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGTClient
{
    public enum EventType
    {
        LAUNCH_STEP = 0x0100,   // 启动，及以下正常步骤
        RUN_MSG = 0x0200,       // 运行时信息
        RUN_EVENT = 0x0300, // 运行中发生指定事件
        RUN_WARNING = 0x0400,       // 运行时警告
        RUN_DEBUG = 0x0500,// 运行时跟踪信息
        ATS_LOADING = 0x0600,   // ATS正在加载
        ATS_LOAD_ABORT = 0x0610,        // ATS加载不成功
        ATS_LOAD_OK = 0x0620,   // ATS加载成功
        ATS_STARTING = 0x0630,// ATS启动中
        ATS_STARTED = 0x0650,   // ATS已经启动
        ATS_ACTION = 0x0700,    // ATS正常程控动作
        ATS_ABORT = 0x0800,// ATS异常中止
        ATS_OVER = 0x0900,  // ATS结束
        API_DEBUG = 0x0A00,// 应用调用接口跟踪
        HOTLINE_DROPPED = 0x0B00,   // 通讯掉线
        HOTLINE_RESUMED = 0x0C00,   // 通讯恢复

        DOWNLOAD_STEP = 0x0D00,
        DOWNLOAD_ABORT = 0x1D00,
        DOWNLOAD_OK = 0x2D00,
        UPLOAD_STEP = 0x0E00,
        UPLOAD_ABORT = 0x1E00,
        UPLOAD_OK = 0x2E00,
        SYNC_STEP = 0x0F00,
        SYNC_NUMBER = 0x0F10,
        SYNC_WAIT = 0x0F20,
        SYNC_GO = 0x0F30,
        SYNC_ABORT = 0x1F00,
        SYNC_OK = 0x2F00,

        LAUNCH_ABORT = 0x1000,  // 启动异常中止
        LAUNCH_OK = 0x2000,// 启动正常完成
        CLOSE_STEP = 0x4000,    // 关闭，及以下步骤
        CLOSE_OK = 0x8000		// 关闭正常完成
    }
}
