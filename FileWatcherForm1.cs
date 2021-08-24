using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using System.Diagnostics;

namespace FileWatcher
{
    public partial class FileWatcherForm1 : Form
    {
        public FileWatcherForm1()
        {
            InitializeComponent();
            Status.Text = "\t\t\t使用说明\n\n\n1、本程序可监视目录及文件的写入与更名并复制被更改文件；\n\n2、在左侧文本框中填入目录，一行填写一个目录，不可有空行。第一行目录为复制文件的目标写入目录，余下的目录为需监视的目录；\n\n目录填写示例（监视“\\\\192.168.1.1\\示例目录1”和“\\\\192.168.1.1\\示例目录2”并将更改写入“C:\\示例目录”）：\n\nC:\\示例目录\n\\\\192.168.1.1\\示例目录\n\\\\192.168.1.1\\示例目录2\n\n4、底部设置项中“包含子目录”仅在开始监视前可设置，“复制被更改文件”、“监视写入”、“监视重命名”和“延时”在开始监视前后均可设置。\n\n5、若同时设置复制文件和延时，则文件复制操作将在监视到文件更改数秒后执行以避免文件被占用产生错误。设置完成后点击“开关”复选框切换监视状态。\n\n\n\n\t\tlhtxq@live.com\t刘汉涛\t版权所有";
            NowTime.Text = "(当前系统时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ")";
        }

        internal static string FilePath;//目标写入目录
        internal static string[] PathArr;//目录数组
        internal static int ChangedCount = 0;//文件更改统计
        internal static int WritedCount = 0;//写入文件数统计
        internal static int ErrorCount = 0;//错误统计
        FileSystemWatcher[] WatcherArr;
        private void Run()
        {
            FilePath = this.TargetPath.Text;
            PathArr = FilePath.Split('\n');
            WatcherArr = new FileSystemWatcher[PathArr.Count()];
            if (PathArr.Count() > 1)
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
                Status.Select(Status.TextLength, 0); 
                Status.ScrollToCaret();
                try
                {
                    for (int i = 1; i < PathArr.Count(); i++)
                    {
                        if (PathArr[i].Length > 0)
                        {
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
                            WatcherArr[i].EnableRaisingEvents = true;
                        }
                        else
                        {
                            FolderErrorTips();
                            break;
                        }
                    }
                }
                catch
                {
                    FolderErrorTips();
                }
            }
            else
            {
                FolderErrorTips();
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
            string FileName;//复制的文件名
            string ChangedObject;
            if (WatchWrite.Checked==true)
            {
                FileName = e.Name;
                //
                if (e.Name.Contains("\\"))
                {
                    FileName = e.Name.Split('\\')[e.Name.Split('\\').Length - 1];
                }
                //用于提取子目录改变时的末级目录内文件名
                if (e.FullPath.Substring(0, e.FullPath.Length- FileName.Length - 1) != PathArr[0])
                {
                    if (Directory.Exists(e.FullPath))
                    {
                        ChangedObject = "目录";
                    }
                    else
                    {
                        ChangedObject = "文件";
                    }
                    ChangedCount++;
                    StatusTips.Text = StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
                    Status.Text = this.Status.Text + "\n" + ChangedCount + "、写入" + ChangedObject + "(" + DateTime.Now.ToString() + ")\t" + e.FullPath + "\n";
                    Status.Select(Status.TextLength, 0);
                    Status.ScrollToCaret();
                    if (WhetherCopy.Checked == true && !Directory.Exists(e.FullPath))
                    {
                        if (Delay.Checked == false)
                        {
                            WriteFiles NewWrite = new WriteFiles();
                            NewWrite.WriteWithoutDelay(e.FullPath, FileName);
                        }
                        else
                        {
                            WriteFiles NewWrite = new WriteFiles();
                            NewWrite.WriteWithDelay(e.FullPath, FileName);
                        }
                    }
                }
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            string FileName;//复制的文件名
            string ChangedObject;
            if (WatchRename.Checked==true)
            {
                FileName = e.Name;
                //
                if (e.Name.Contains("\\"))
                {
                    FileName = e.Name.Split('\\')[e.Name.Split('\\').Length - 1];
                }
                //用于提取子目录改变时的末级目录内文件名
                if (e.FullPath.Substring(0, e.FullPath.Length - FileName.Length - 1) != PathArr[0])
                {
                    if (Directory.Exists(e.FullPath))
                    {
                        ChangedObject = "目录";
                    }
                    else
                    {
                        ChangedObject = "文件";
                    }
                    ChangedCount++;
                    StatusTips.Text = "(监视到 " + ChangedCount + " 项更改，复制完成 " + WritedCount + " 个文件，遇到 " + ErrorCount + " 个错误)";
                    Status.Text = this.Status.Text + "\n" + ChangedCount + "、更名" + ChangedObject + "(" + DateTime.Now.ToString() + ")\t" + e.OldFullPath + " >>> " + e.FullPath + "\n";
                    Status.Select(Status.TextLength, 0); 
                    Status.ScrollToCaret();
                    if (WhetherCopy.Checked==true && !Directory.Exists(e.FullPath))
                    {
                        if (Delay.Checked == false)
                        {
                            WriteFiles NewWrite = new WriteFiles();
                            NewWrite.WriteWithoutDelay(e.FullPath, FileName);
                        }
                        else
                        {
                            WriteFiles NewWrite = new WriteFiles();
                            NewWrite.WriteWithDelay(e.FullPath, FileName);
                        }
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
                try
                {
                    this.Run();
                }
                catch
                {
                    StatusSwitch.Checked = false;
                    this.Status.Text = this.Status.Text + "\n→→→程序遇到错误！(" + DateTime.Now.ToString() + ")\t无法创建文件监视实例！\n\n";
                }
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
                    this.Status.Text = this.Status.Text + "\n→→→程序遇到错误！(" + DateTime.Now.ToString() + ")\t无法销毁文件监视实例！请重新打开软件。\n\n";
                    //FolderErrorTips();
                }
                Status.Text="已结束监视，结束监视的时间：" + DateTime.Now.ToString() + "\n\n*************************************\n\n" + Status.Text+ "\n\n******************结束******************\n";
                Status.Select(0, 0);  
                Status.ScrollToCaret();
                StatusTips.Enabled = false;
                WhetherIncludeSub.Enabled = true;
                TargetPath.Enabled = true;
            }
            
        }

        internal void FolderErrorTips()
        {
            this.Status.Text = this.Status.Text + "\n\n→→→程序遇到错误！(" + DateTime.Now.ToString() + ")\t请检查目录填写是否正确并确保软件拥有目标目录访问权限！\n";
            Status.Select(Status.TextLength, 0);
            Status.ScrollToCaret();
            StatusSwitch.Checked = false;
        }

        internal void WriteErrorTips()
        {
            this.Status.Text = this.Status.Text + "\n→→→程序遇到错误！(" + DateTime.Now.ToString() + ")\t请检查目录权限或文件有效性！\n";
            Status.Select(Status.TextLength, 0);
            Status.ScrollToCaret();
        }

        internal static float DelayTime =3.0F;
        private void InputDelayTime_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(InputDelayTime.Text, out DelayTime);
            if(DelayTime<0.001)
            {
                DelayTime = 0.001F;
                InputDelayTime.Text = DelayTime.ToString();
            }
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            NowTime.Text = "(当前系统时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ")";
            if (WriteFiles.WriteError==true)
            {
                WriteFiles.WriteError = false;
                WriteErrorTips();
            }
            if (StatusSwitch.Checked==true)
            {
                StatusTips.Text = "(监视到 " + FileWatcherForm1.ChangedCount + " 项更改，复制完成 " + FileWatcherForm1.WritedCount + " 个文件，遇到 " + FileWatcherForm1.ErrorCount + " 个错误)";
            }
        }

        private void WhetherCopy_CheckedChanged(object sender, EventArgs e)
        {
            if(WhetherCopy.Checked==true)
            {
                Delay.Enabled = true;
            }
            else
            {
                Delay.Enabled = false;
            }
        }
    }

    public class WriteFiles
    {
        internal static Boolean WriteError = false;
        internal void WriteWithoutDelay(string S, string N)
        {
            try
            {
                File.Copy(S, FileWatcherForm1.PathArr[0] + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + N, false);
                FileWatcherForm1.WritedCount++;
            }
            catch
            {
                FileWatcherForm1.ErrorCount++;
                WriteError = true;
            }
        }

        private string DelayCopyFullPath;
        private string DelayFilename;
        private System.Timers.Timer DelayTimer;
        internal void WriteWithDelay(string S, string N)
        {
            DelayCopyFullPath = S;
            DelayFilename = N;
            DelayTimer = new System.Timers.Timer();
            DelayTimer.Interval = FileWatcherForm1.DelayTime * 1000;
            DelayTimer.Elapsed += new System.Timers.ElapsedEventHandler(DelayTimer_Tick);
            DelayTimer.AutoReset = false;
            DelayTimer.Enabled = true;
            DelayTimer.Start();
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            DelayTimer.Stop();
            DelayTimer.Dispose();
            WriteWithoutDelay(DelayCopyFullPath,DelayFilename);
        }
    }
}
