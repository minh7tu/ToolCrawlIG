
namespace VCCorp.IG.WinForm
{
    partial class frmMain
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
            this.btnGetNewSource = new System.Windows.Forms.Button();
            this.btnGetSource = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetNewSource
            // 
            this.btnGetNewSource.Location = new System.Drawing.Point(259, 112);
            this.btnGetNewSource.Name = "btnGetNewSource";
            this.btnGetNewSource.Size = new System.Drawing.Size(143, 46);
            this.btnGetNewSource.TabIndex = 0;
            this.btnGetNewSource.Text = "Get New Source";
            this.btnGetNewSource.UseVisualStyleBackColor = true;
            this.btnGetNewSource.Click += new System.EventHandler(this.btnGetNewSource_Click);
            // 
            // btnGetSource
            // 
            this.btnGetSource.Location = new System.Drawing.Point(430, 112);
            this.btnGetSource.Name = "btnGetSource";
            this.btnGetSource.Size = new System.Drawing.Size(143, 46);
            this.btnGetSource.TabIndex = 1;
            this.btnGetSource.Text = "Get Sourced";
            this.btnGetSource.UseVisualStyleBackColor = true;
            this.btnGetSource.Click += new System.EventHandler(this.btnGetSource_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 511);
            this.Controls.Add(this.btnGetSource);
            this.Controls.Add(this.btnGetNewSource);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách chức năng";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetNewSource;
        private System.Windows.Forms.Button btnGetSource;
    }
}

