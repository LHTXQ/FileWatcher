using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileWatcher
{
    public partial class FileWatcherForm1 : Form
    {
        public FileWatcherForm1()
        {
            InitializeComponent();
            Status.Text = "\t\t\t使用说明\n\n\n1、本程序可监视文件夹及文件的创建与重命名并复制文件更改；\n\n2、在左侧文本框中填入目录，一行填写一个目录，不可有空行。第一行目录为复制文件时的目标写入目录，余下的目录为需监视的目录；\n\n目录填写示例（监视“\\\\192.168.1.1\\示例目录”并将更改写入“C:\\示例目录”）：\n\nC:\\示例目录\n \\\\192.168.1.1\\示例目录\n\n4、底部设置项中“包含子文件夹”仅在开始监视前可设置，“复制被更改对象”、“监视写入”、“监视重命名”和“延时”在开始监视前后均可设置。若同时设置复制文件和延时，则文件复制操作将在监视到文件更改数秒后执行以避免文件被占用产生错误。设置完成后点击“开关”复选框切换监视状态。\n\n5、目前存在且不打算解决的问题：监视目录中有目录更改时会产生错误；在延时时间内监视到多项更改时仅会复制其中一个文件。\n\n\n\n\n\t\tlhtxq@live.com\t刘汉涛\t版权所有";
            NowTime.Text = "(当前系统时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ")";
        }

        string FilePath;//目标写入目录
        string[] PathArr;//目录数组
        int ChangedCount = 0;//文件更改统计
        int WritedCount = 0;//写入文件数统计
        int ErrorCount = 0;//错误统计
        FileSystemWatcher[] WatcherArr;
        private void Run()
        {
            FilePath = this.TargetPath.Text;
            PathArr = FilePath.Split('\n');
            WatcherArr = new FileSystemWatcher[PathArr.Count()];
            if (PathArr.Count() >= 2)
            {
                long num = PathArr.GetUpperBound(0);
                for (int i = 0; i <= num; i++)
                {
                    if (PathArr[i].Substring(PathArr[i].Length - 1) == "\\")
                    {
                        PathArr[i] = PathArr[i].Substring(0, PathArr[i].Length - 1);
                    }
                    if (i == 0)
                    {
                        Status.Text = this.Status.Text + "\n\n*************************************\n\n目标写入目录：\n" + PathArr[i] + "\n\n*************************************\n\n监视的目录：";
                    }
                    else
                    {
                        Status.Text = this.Status.Text + "\n" + PathArr[i];
                    }
                }
                Status.Text = this.Status.Text + "\n\n*************************************\n\n监视到的文件更改：\n";
                //Status.Focus();
                //设置光标的位置到文本尾   
                Status.Select(Status.TextLength, 0);
                //滚动到控件光标处   
                Status.ScrollToCaret();
                //Status.AppendText(Text);
                //FileSystemWatcher[] WatcherArr = new FileSystemWatcher[PathArr.Count()];
                try
                {
                    for (int i = 1; i < PathArr.Count(); i++)
                    {
                        if (PathArr[i].Length > 0)
                        {
                            //if (i>=1)
                            //{
                            WatcherArr[i] = new FileSystemWatcher();
                            WatcherArr[i].Path = PathArr[i];
                            if (WhetherIncludeSub.Checked == true)
                            {
                                WatcherArr[i].IncludeSubdirectories = true;
                            }
                            //WatcherArr[i].NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                            //                            | NotifyFilters.Size
                            //                            | NotifyFilters.FileName
                            //                            | NotifyFilters.DirectoryName;
                            //WatcherArr[i].Changed += OnChanged;//new FileSystemEventHandler(OnChanged);
                            WatcherArr[i].Created += OnCreated; //new FileSystemEventHandler(OnCreated);
                            //WatcherArr[i].Deleted += new FileSystemEventHandler(OnDeleted);
                            WatcherArr[i].Renamed += OnRenamed;
                            //WatcherArr[i].Error += OnError;
                            //}
                            WatcherArr[i].EnableRaisingEvents = true;
                        }
                        else
                        {
                            ErrorTips();
                            break;
                        }
                    }
                }
                catch
                {
                    ErrorTips();
                }
            }
            else
            {
                ErrorTips();
            }
        }
        /*
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                    if (WatchWrite.Checked == true)
                    {
                            if(e.FullPath==PathArr[0])
                    {
                        return;
                    }            
                    ChangedCount++;
                    StatusTips.Text = "（监视到 " + ChangedCount + " 个更改，遇到 " + ErrorCount + " 个错误）";
                    Status.Text = this.Status.Text + "\n" + ChangedCount + "、" + e.ChangeType + "(" + DateTime.Now.ToString() + ")\t" + e.FullPath + "\n";
                    //Status.Focus();
                    //设置光标的位置到文本尾   
                    Status.Select(Status.TextLength, 0);
                    //滚动到控件光标处   
                    Status.ScrollToCaret();
                    //Status.AppendText(Text);
                    CopyFullPath = e.FullPath;
                    FileName = e.Name;
                    WriteWithDelay(CopyFullPath, FileName);
                    //if (WhetherCopy.Checked == true)
                    //{
                    //    try
                    //    {
                    //        File.Copy(e.FullPath, PathArr[0] + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + e.Name, false);
                    //    }
                    //    catch
                    //    {
                    //        ErrorTips();
                    //    }
                    //}
                }
            }
        }
        */

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if(WatchWrite.Checked==true)
            {
                string InfluencedName;
                InfluencedName = e.Name;
                if (e.Name.Contains("\\"))
                {
                    InfluencedName = e.Name.Split('\\')[e.Name.Split('\\').Length - 1];
                }
                if (e.FullPath.Substring(0, e.FullPath.Length- InfluencedName.Length - 1) != PathArr[0])
                {
                    ChangedCount++;
                    StatusTips.Text = StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
                    Status.Text = this.Status.Text + "\n" + ChangedCount + "、" + e.ChangeType + "(" + DateTime.Now.ToString() + ")\t" + e.FullPath + "\n";
                    //Status.Focus();
                    //设置光标的位置到文本尾   
                    Status.Select(Status.TextLength, 0);
                    //滚动到控件光标处   
                    Status.ScrollToCaret();
                    //Status.AppendText(Text);
                    CopyFullPath = e.FullPath;
                    FileName = InfluencedName;
                    WriteWithDelay(CopyFullPath, FileName);
                    //if (WhetherCopy.Checked == true)
                    //{
                    //    try
                    //    {
                    //        File.Copy(e.FullPath, PathArr[0] + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + e.Name, false);
                    //    }
                    //    catch
                    //    {
                    //        ErrorTips();
                    //    }
                    //}
                }
            }
        }
        
        string CopyFullPath;//文件复制来源
        string FileName;//复制的文件名
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if(WatchRename.Checked==true)
            {
                string InfluencedName;
                InfluencedName = e.Name;
                if (e.Name.Contains("\\"))
                {
                    InfluencedName = e.Name.Split('\\')[e.Name.Split('\\').Length - 1];
                }
                if (e.FullPath.Substring(0, e.FullPath.Length - InfluencedName.Length - 1) != PathArr[0])
                {
                    ChangedCount++;
                    StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
                    Status.Text = this.Status.Text + "\n" + ChangedCount + "、" + e.ChangeType + "(" + DateTime.Now.ToString() + ")\t" + e.FullPath + "\n";
                    //Status.Focus();
                    //设置光标的位置到文本尾   
                    Status.Select(Status.TextLength, 0);
                    //滚动到控件光标处   
                    Status.ScrollToCaret();
                    //Status.AppendText(Text);
                    CopyFullPath = e.FullPath;
                    FileName = InfluencedName;
                    WriteWithDelay(CopyFullPath, FileName);
                    //if (WhetherCopy.Checked == true)
                    //{
                    //    try
                    //    {
                    //        File.Copy(e.FullPath, PathArr[0] + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + e.Name, false);
                    //    }
                    //    catch
                    //    {
                    //        ErrorTips();
                    //    }
                    //}
                }
            }
        }
        Boolean DelayWrite = false;
        private void WriteWithDelay(string S, string N)
        {
            if (WhetherCopy.Checked == true)
            {
                if(Delay.Checked==true)
                {
                    DelayWrite = true;
                }
                else
                {
                    try
                    {
                        //Status.Text = S + "\n" + PathArr[0];
                        //if (S.Substring(0,2)=="\\\\")
                        //{
                        //    S = "\\\\" + S.Substring(2).Replace("\\","\\\\");
                        //}
                        //if (PathArr[0].Substring(0, 2) == "\\\\")
                        //{
                        //    PathArr[0] = "\\" + PathArr[0].Substring(2).Replace("\\","\\\\");
                        //}
                        //Status.Text = Status.Text+ "\n"+S + "\n" + PathArr[0];
                        File.Copy(S, PathArr[0] + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + N, false);
                        WritedCount++;
                        StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
                    }
                    catch
                    {
                        ErrorTips();
                    }
                }

            }
        }
        private void StatusSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.StatusSwitch.Checked==true )
            {
                WhetherIncludeSub.Enabled = false;
                TargetPath.Enabled = false;
                this.Status.Text = "开始监视的时间：" + DateTime.Now.ToString();
                StatusTips.Enabled = true;
                ChangedCount = 0;
                ErrorCount = 0;
                WritedCount = 0;
                StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
                try
                {
                    this.Run();
                }
                catch
                {
                    ErrorTips();
                }
                //this.Run();
            }
            else
            {
                try
                {
                    for (int i = 1; i < PathArr.Count(); i++)
                    {
                        WatcherArr[i].Dispose();
                    }
                }
                catch
                {
                    ErrorTips();
                }
                Status.Text="已结束监视，结束监视的时间：" + DateTime.Now.ToString() + "\n\n*************************************\n\n" + Status.Text+ "\n\n******************结束******************\n";
                StatusTips.Enabled = false;
                WhetherIncludeSub.Enabled = true;
                TargetPath.Enabled = true;
            }
            
        }
        private void ErrorTips()
        {
            this.Status.Text = this.Status.Text + "\n→→→程序遇到错误！(" + DateTime.Now.ToString() + ")\n→→→请检查目录权限或目录填写是否正确（包括目录有效性及正确性）！\n\n";
            //Status.Focus();
            //设置光标的位置到文本尾   
            Status.Select(Status.TextLength, 0);
            //滚动到控件光标处   
            Status.ScrollToCaret();
            //Status.AppendText(Text);
            ErrorCount++;
            StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
            //MessageBox.Show("请检查目录权限或目录填写是否正确！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //MessageBox.Show(DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void WriteWithoutDelay(string S, string N)
        {
            try
            {
                File.Copy(S, PathArr[0] + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + N, false);
                WritedCount++;
                StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
            }
            catch
            {
                ErrorTips();
            }
        }

        string Delay_CopyFullPath;
        string Delay_Filename;
        int DelayTime=3;
        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            if(DelayTimer.Interval.Equals(DelayTime * 1000))
            {
                WriteWithoutDelay(Delay_CopyFullPath, Delay_Filename);
                DelayWrite = false;
                DelayTimer.Interval = 1;
            }
            if (DelayWrite == true)
            {
                Delay_CopyFullPath = CopyFullPath;
                Delay_Filename = FileName;
                DelayTimer.Interval = DelayTime * 1000;
            }

            //Status.Text = this.Status.Text + "!";
        }

        private void InputDelayTime_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(InputDelayTime.Text, out DelayTime);
            if(DelayTime<=0)
            {
                DelayTime = 1;
                InputDelayTime.Text = DelayTime.ToString();
            }
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            NowTime.Text = "(当前系统时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ")";
        }
    }
}
