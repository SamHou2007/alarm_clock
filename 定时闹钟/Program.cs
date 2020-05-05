using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace 定时闹钟
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process current = Process.GetCurrentProcess();//获取当前进程名
            Process[] processes = Process.GetProcessesByName(current.ProcessName);//获取同名进程
            foreach (Process process in processes)//遍历所有同名进程
            {
                if (process.Id != current.Id)//忽略当前进程
                {
                    if (process.MainModule.FileName == current.MainModule.FileName)//比对文件路径
                    {
                        return;
                    }
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
