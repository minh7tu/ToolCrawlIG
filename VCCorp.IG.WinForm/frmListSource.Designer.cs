
namespace VCCorp.IG.WinForm
{
    partial class frmListSource
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPost = new System.Windows.Forms.Button();
            this.btnHashtag = new System.Windows.Forms.Button();
            this.btnLocation = new System.Windows.Forms.Button();
            this.rtxtDisplayResult = new System.Windows.Forms.RichTextBox();
            this.btnOriginAudio = new System.Windows.Forms.Button();
            this.txtOptions = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetSourceId = new System.Windows.Forms.Button();
            this.btnLoginIG = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCrawlerPost = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnCrawlerComment = new System.Windows.Forms.Button();
            this.pnlCefsharp = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblError = new System.Windows.Forms.Label();
            this.lblOk = new System.Windows.Forms.Label();
            this.lblSum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResutlUrl = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(22, 19);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(74, 37);
            this.btnPost.TabIndex = 0;
            this.btnPost.Text = "Profile";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnHashtag
            // 
            this.btnHashtag.Location = new System.Drawing.Point(115, 19);
            this.btnHashtag.Name = "btnHashtag";
            this.btnHashtag.Size = new System.Drawing.Size(74, 37);
            this.btnHashtag.TabIndex = 2;
            this.btnHashtag.Text = "Hashtag";
            this.btnHashtag.UseVisualStyleBackColor = true;
            this.btnHashtag.Click += new System.EventHandler(this.btnHashtag_Click);
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(22, 69);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(74, 37);
            this.btnLocation.TabIndex = 3;
            this.btnLocation.Text = "Location";
            this.btnLocation.UseVisualStyleBackColor = true;
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // rtxtDisplayResult
            // 
            this.rtxtDisplayResult.Location = new System.Drawing.Point(564, 172);
            this.rtxtDisplayResult.Name = "rtxtDisplayResult";
            this.rtxtDisplayResult.Size = new System.Drawing.Size(480, 343);
            this.rtxtDisplayResult.TabIndex = 4;
            this.rtxtDisplayResult.Text = "";
            // 
            // btnOriginAudio
            // 
            this.btnOriginAudio.Location = new System.Drawing.Point(115, 69);
            this.btnOriginAudio.Name = "btnOriginAudio";
            this.btnOriginAudio.Size = new System.Drawing.Size(74, 37);
            this.btnOriginAudio.TabIndex = 5;
            this.btnOriginAudio.Text = "OriginAudio";
            this.btnOriginAudio.UseVisualStyleBackColor = true;
            this.btnOriginAudio.Click += new System.EventHandler(this.btnOriginAudio_Click);
            // 
            // txtOptions
            // 
            this.txtOptions.Location = new System.Drawing.Point(129, 28);
            this.txtOptions.Name = "txtOptions";
            this.txtOptions.Size = new System.Drawing.Size(100, 20);
            this.txtOptions.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nhập vào trạng thái :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnAuto);
            this.groupBox1.Controls.Add(this.btnGetSourceId);
            this.groupBox1.Controls.Add(this.btnLoginIG);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtOptions);
            this.groupBox1.Location = new System.Drawing.Point(30, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 160);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Điều kiện";
            // 
            // btnGetSourceId
            // 
            this.btnGetSourceId.Location = new System.Drawing.Point(129, 68);
            this.btnGetSourceId.Name = "btnGetSourceId";
            this.btnGetSourceId.Size = new System.Drawing.Size(95, 38);
            this.btnGetSourceId.TabIndex = 11;
            this.btnGetSourceId.Text = "Cập nhập Id_Source Null";
            this.btnGetSourceId.UseVisualStyleBackColor = true;
            this.btnGetSourceId.Click += new System.EventHandler(this.btnGetSourceId_Click);
            // 
            // btnLoginIG
            // 
            this.btnLoginIG.Location = new System.Drawing.Point(19, 68);
            this.btnLoginIG.Name = "btnLoginIG";
            this.btnLoginIG.Size = new System.Drawing.Size(83, 38);
            this.btnLoginIG.TabIndex = 10;
            this.btnLoginIG.Text = "Đặng nhập IG";
            this.btnLoginIG.UseVisualStyleBackColor = true;
            this.btnLoginIG.Click += new System.EventHandler(this.btnLoginIG_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPost);
            this.groupBox2.Controls.Add(this.btnOriginAudio);
            this.groupBox2.Controls.Add(this.btnHashtag);
            this.groupBox2.Controls.Add(this.btnLocation);
            this.groupBox2.Location = new System.Drawing.Point(291, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 134);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách nguồn";
            // 
            // btnCrawlerPost
            // 
            this.btnCrawlerPost.Location = new System.Drawing.Point(24, 22);
            this.btnCrawlerPost.Name = "btnCrawlerPost";
            this.btnCrawlerPost.Size = new System.Drawing.Size(169, 41);
            this.btnCrawlerPost.TabIndex = 10;
            this.btnCrawlerPost.Text = "Si_Demand_Source";
            this.btnCrawlerPost.UseVisualStyleBackColor = true;
            this.btnCrawlerPost.Click += new System.EventHandler(this.btnCrawlerPost_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCrawlerPost);
            this.groupBox3.Location = new System.Drawing.Point(510, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 134);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Crawl Post";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(129, 117);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(95, 37);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Dừng";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(19, 117);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(83, 37);
            this.btnAuto.TabIndex = 12;
            this.btnAuto.Text = "Tự động";
            this.btnAuto.UseVisualStyleBackColor = true;
            // 
            // btnCrawlerComment
            // 
            this.btnCrawlerComment.Location = new System.Drawing.Point(15, 22);
            this.btnCrawlerComment.Name = "btnCrawlerComment";
            this.btnCrawlerComment.Size = new System.Drawing.Size(158, 37);
            this.btnCrawlerComment.TabIndex = 11;
            this.btnCrawlerComment.Text = "Si_Demand_Source_Post";
            this.btnCrawlerComment.UseVisualStyleBackColor = true;
            this.btnCrawlerComment.Click += new System.EventHandler(this.btnCrawlerComment_Click);
            // 
            // pnlCefsharp
            // 
            this.pnlCefsharp.Location = new System.Drawing.Point(30, 225);
            this.pnlCefsharp.Name = "pnlCefsharp";
            this.pnlCefsharp.Size = new System.Drawing.Size(474, 290);
            this.pnlCefsharp.TabIndex = 12;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblError);
            this.groupBox4.Controls.Add(this.lblOk);
            this.groupBox4.Controls.Add(this.lblSum);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(932, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(142, 134);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Kết quả bóc tách";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(103, 69);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(13, 13);
            this.lblError.TabIndex = 14;
            this.lblError.Text = "0";
            // 
            // lblOk
            // 
            this.lblOk.AutoSize = true;
            this.lblOk.Location = new System.Drawing.Point(103, 46);
            this.lblOk.Name = "lblOk";
            this.lblOk.Size = new System.Drawing.Size(13, 13);
            this.lblOk.TabIndex = 13;
            this.lblOk.Text = "0";
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(103, 22);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(13, 13);
            this.lblSum.TabIndex = 11;
            this.lblSum.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Lỗi:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Xong và lưu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tổng:";
            // 
            // txtResutlUrl
            // 
            this.txtResutlUrl.Location = new System.Drawing.Point(30, 187);
            this.txtResutlUrl.Name = "txtResutlUrl";
            this.txtResutlUrl.Size = new System.Drawing.Size(474, 20);
            this.txtResutlUrl.TabIndex = 14;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.btnCrawlerComment);
            this.groupBox5.Location = new System.Drawing.Point(736, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(190, 134);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Crawl Comment";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 37);
            this.button1.TabIndex = 12;
            this.button1.Text = "Si_Crawl_Data_Excel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmListSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 538);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.txtResutlUrl);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.rtxtDisplayResult);
            this.Controls.Add(this.pnlCefsharp);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmListSource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VCCorp-Instagram";
            this.Load += new System.EventHandler(this.frmListSource_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnHashtag;
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.RichTextBox rtxtDisplayResult;
        private System.Windows.Forms.Button btnOriginAudio;
        private System.Windows.Forms.TextBox txtOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCrawlerPost;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCrawlerComment;
        private System.Windows.Forms.Button btnGetSourceId;
        private System.Windows.Forms.Button btnLoginIG;
        private System.Windows.Forms.Panel pnlCefsharp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblOk;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResutlUrl;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
    }
}