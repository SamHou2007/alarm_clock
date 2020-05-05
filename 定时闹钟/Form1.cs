using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace 定时闹钟
{
    public partial class Form1 : Form
    {
        bool a = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.AppendAllText(@"C:\Users\public\log.txt", "Close...\n");
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            File.AppendAllText(@"C:\Users\public\log.txt", "Start...\n");
            if (File.Exists(@"C:\Users\public\time.txt"))
            {
                string[] s = File.ReadAllLines(@"C:\Users\public\time.txt");
                hour.Value = Convert.ToDecimal(s[0]);
                minute.Value = Convert.ToDecimal(s[1]);
                second.Value = Convert.ToDecimal(s[2]);
                File.AppendAllText(@"C:\Users\public\log.txt", "Get data!\n");
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
            backgroundWorker1.RunWorkerAsync();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            a = false;
            File.Delete(@"C:\Users\public\time.txt");
            string[] result = new string[]
            {
                Convert.ToString(hour.Value),
                Convert.ToString(minute.Value),
                Convert.ToString(second.Value)
            }; //输入文本
            File.AppendAllLines(@"C:\Users\public\time.txt", result);
            File.AppendAllLines(@"C:\Users\public\log.txt", result);
            File.AppendAllText(@"C:\Users\public\log.txt", "Close...\n");
            MessageBox.Show("保存成功！程序即将关闭，请重启程序！");
            Environment.Exit(0);
        }

        private void help_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"设置好时间，点击保存即可！保存后窗体将不再自动显示！可以在任务栏右键手动显示！请允许程序开机启动！运行记录保存在C: \Users\public\log.txt，可以定期删除！C:\Users\public\time.txt请勿删除！否则会导致设置丢失！卸载前先点击清除闹钟，会自动删除time.txt！", "帮助");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] s = File.ReadAllLines(@"C:\Users\public\time.txt");
            while (true)
            {
                while (a)
                {
                    if (s[0] == DateTime.Now.Hour.ToString() && s[1] == DateTime.Now.Minute.ToString() && s[2] == DateTime.Now.Second.ToString())
                    {
                        for (int i = 0; i < 10; i++)
                            System.Media.SystemSounds.Beep.Play();
                        MessageBox.Show("你设定的时间到了！");

                    }
                    Thread.Sleep(10);
                }
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            a = false;
            File.Delete(@"C:\Users\public\time.txt");
            File.AppendAllText(@"C:\Users\public\log.txt", "Delete time.txt\n");
            File.AppendAllText(@"C:\Users\public\log.txt", "Close...\n");
            MessageBox.Show("删除成功！程序即将退出！");
            Environment.Exit(0);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }
    }
}
