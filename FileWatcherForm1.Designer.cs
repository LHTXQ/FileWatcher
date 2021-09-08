
namespace FileWatcher
{
    partial class FileWatcherForm1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.GroupBox groupBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileWatcherForm1));
            this.InputDelayTime = new System.Windows.Forms.TextBox();
            this.Delay = new System.Windows.Forms.CheckBox();
            this.WhetherIncludeSub = new System.Windows.Forms.CheckBox();
            this.WatchRename = new System.Windows.Forms.CheckBox();
            this.WhetherCopy = new System.Windows.Forms.CheckBox();
            this.WatchWrite = new System.Windows.Forms.CheckBox();
            this.Parameters = new System.Windows.Forms.RichTextBox();
            this.StatusSwitch = new System.Windows.Forms.CheckBox();
            this.Status = new System.Windows.Forms.RichTextBox();
            this.StatusTips = new System.Windows.Forms.Label();
            this.NowTime = new System.Windows.Forms.Label();
            this.SyncTimer = new System.Windows.Forms.Timer(this.components);
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.CausesValidation = false;
            label1.Location = new System.Drawing.Point(12, 18);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(29, 12);
            label1.TabIndex = 3;
            label1.Text = "参数";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.CausesValidation = false;
            label2.Location = new System.Drawing.Point(411, 18);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(29, 12);
            label2.TabIndex = 4;
            label2.Text = "状态";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.CausesValidation = false;
            label3.Enabled = false;
            label3.Font = new System.Drawing.Font("宋体", 7.5F);
            label3.Location = new System.Drawing.Point(717, 436);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(80, 10);
            label3.TabIndex = 7;
            label3.Text = "版本号：1.5.2.4";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.InputDelayTime);
            groupBox1.Controls.Add(this.Delay);
            groupBox1.Controls.Add(this.WhetherIncludeSub);
            groupBox1.Controls.Add(this.WatchRename);
            groupBox1.Controls.Add(this.WhetherCopy);
            groupBox1.Controls.Add(this.WatchWrite);
            groupBox1.Font = new System.Drawing.Font("宋体", 9F);
            groupBox1.Location = new System.Drawing.Point(14, 408);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(613, 38);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "设置";
            // 
            // InputDelayTime
            // 
            this.InputDelayTime.Location = new System.Drawing.Point(529, 12);
            this.InputDelayTime.Name = "InputDelayTime";
            this.InputDelayTime.Size = new System.Drawing.Size(44, 21);
            this.InputDelayTime.TabIndex = 11;
            this.InputDelayTime.Text = "3";
            this.InputDelayTime.TextChanged += new System.EventHandler(this.InputDelayTime_TextChanged);
            // 
            // Delay
            // 
            this.Delay.AutoSize = true;
            this.Delay.Checked = true;
            this.Delay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Delay.Location = new System.Drawing.Point(486, 13);
            this.Delay.Name = "Delay";
            this.Delay.Size = new System.Drawing.Size(108, 16);
            this.Delay.TabIndex = 10;
            this.Delay.Text = "延时        秒";
            this.Delay.UseVisualStyleBackColor = true;
            // 
            // WhetherIncludeSub
            // 
            this.WhetherIncludeSub.AutoSize = true;
            this.WhetherIncludeSub.Location = new System.Drawing.Point(56, 14);
            this.WhetherIncludeSub.Name = "WhetherIncludeSub";
            this.WhetherIncludeSub.Size = new System.Drawing.Size(84, 16);
            this.WhetherIncludeSub.TabIndex = 6;
            this.WhetherIncludeSub.Text = "包含子目录";
            this.WhetherIncludeSub.UseVisualStyleBackColor = true;
            // 
            // WatchRename
            // 
            this.WatchRename.AutoSize = true;
            this.WatchRename.Checked = true;
            this.WatchRename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WatchRename.Location = new System.Drawing.Point(389, 14);
            this.WatchRename.Name = "WatchRename";
            this.WatchRename.Size = new System.Drawing.Size(72, 16);
            this.WatchRename.TabIndex = 9;
            this.WatchRename.Text = "监视更名";
            this.WatchRename.UseVisualStyleBackColor = true;
            // 
            // WhetherCopy
            // 
            this.WhetherCopy.AutoSize = true;
            this.WhetherCopy.Checked = true;
            this.WhetherCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WhetherCopy.Location = new System.Drawing.Point(163, 14);
            this.WhetherCopy.Name = "WhetherCopy";
            this.WhetherCopy.Size = new System.Drawing.Size(108, 16);
            this.WhetherCopy.TabIndex = 5;
            this.WhetherCopy.Text = "复制被更改文件";
            this.WhetherCopy.UseVisualStyleBackColor = true;
            this.WhetherCopy.CheckedChanged += new System.EventHandler(this.WhetherCopy_CheckedChanged);
            // 
            // WatchWrite
            // 
            this.WatchWrite.AutoSize = true;
            this.WatchWrite.Checked = true;
            this.WatchWrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WatchWrite.Location = new System.Drawing.Point(295, 13);
            this.WatchWrite.Name = "WatchWrite";
            this.WatchWrite.Size = new System.Drawing.Size(72, 16);
            this.WatchWrite.TabIndex = 8;
            this.WatchWrite.Text = "监视写入";
            this.WatchWrite.UseVisualStyleBackColor = true;
            // 
            // Parameters
            // 
            this.Parameters.BackColor = System.Drawing.SystemColors.Window;
            this.Parameters.DetectUrls = false;
            this.Parameters.Location = new System.Drawing.Point(12, 36);
            this.Parameters.Name = "Parameters";
            this.Parameters.Size = new System.Drawing.Size(384, 366);
            this.Parameters.TabIndex = 0;
            this.Parameters.Text = "";
            this.Parameters.WordWrap = false;
            // 
            // StatusSwitch
            // 
            this.StatusSwitch.AutoSize = true;
            this.StatusSwitch.Location = new System.Drawing.Point(663, 421);
            this.StatusSwitch.Name = "StatusSwitch";
            this.StatusSwitch.Size = new System.Drawing.Size(48, 16);
            this.StatusSwitch.TabIndex = 1;
            this.StatusSwitch.Text = "开关";
            this.StatusSwitch.UseVisualStyleBackColor = true;
            this.StatusSwitch.CheckedChanged += new System.EventHandler(this.StatusSwitch_CheckedChanged);
            // 
            // Status
            // 
            this.Status.DetectUrls = false;
            this.Status.Location = new System.Drawing.Point(413, 36);
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Size = new System.Drawing.Size(375, 366);
            this.Status.TabIndex = 2;
            this.Status.Text = "";
            // 
            // StatusTips
            // 
            this.StatusTips.AutoSize = true;
            this.StatusTips.Enabled = false;
            this.StatusTips.Font = new System.Drawing.Font("宋体", 9F);
            this.StatusTips.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StatusTips.Location = new System.Drawing.Point(446, 18);
            this.StatusTips.Name = "StatusTips";
            this.StatusTips.Size = new System.Drawing.Size(311, 12);
            this.StatusTips.TabIndex = 11;
            this.StatusTips.Text = "(监视到 0 项更改，复制完成 0 个文件，遇到 0 个错误)";
            this.StatusTips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NowTime
            // 
            this.NowTime.AutoSize = true;
            this.NowTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.NowTime.Location = new System.Drawing.Point(47, 18);
            this.NowTime.Name = "NowTime";
            this.NowTime.Size = new System.Drawing.Size(215, 12);
            this.NowTime.TabIndex = 12;
            this.NowTime.Text = "(当前系统时间：yyyy-MM-dd HH:mm:ss)";
            this.NowTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SyncTimer
            // 
            this.SyncTimer.Enabled = true;
            this.SyncTimer.Interval = 1;
            this.SyncTimer.Tick += new System.EventHandler(this.SyncTimer_Tick);
            // 
            // FileWatcherForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NowTime);
            this.Controls.Add(this.StatusTips);
            this.Controls.Add(groupBox1);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.StatusSwitch);
            this.Controls.Add(this.Parameters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "FileWatcherForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件监视器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileWatcherForm1_FormClosing);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Parameters;
        private System.Windows.Forms.CheckBox StatusSwitch;
        private System.Windows.Forms.CheckBox WhetherCopy;
        private System.Windows.Forms.CheckBox WhetherIncludeSub;
        private System.Windows.Forms.CheckBox WatchWrite;
        private System.Windows.Forms.CheckBox WatchRename;
        private System.Windows.Forms.CheckBox Delay;
        private System.Windows.Forms.TextBox InputDelayTime;
        private System.Windows.Forms.Label StatusTips;
        private System.Windows.Forms.Label NowTime;
        internal System.Windows.Forms.RichTextBox Status;
        private System.Windows.Forms.Timer SyncTimer;
    }
}

