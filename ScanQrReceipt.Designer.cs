
namespace RaktarKezeloDiploma
{
    partial class ScanQrReceipt
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
            this.components = new System.ComponentModel.Container();
            this.btn_start = new System.Windows.Forms.Button();
            this.pb_Camera = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboDevices = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Camera)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(352, 14);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 7;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // pb_Camera
            // 
            this.pb_Camera.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pb_Camera.Location = new System.Drawing.Point(12, 43);
            this.pb_Camera.Name = "pb_Camera";
            this.pb_Camera.Size = new System.Drawing.Size(415, 256);
            this.pb_Camera.TabIndex = 6;
            this.pb_Camera.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Kamera:";
            // 
            // comboDevices
            // 
            this.comboDevices.FormattingEnabled = true;
            this.comboDevices.Location = new System.Drawing.Point(66, 14);
            this.comboDevices.Name = "comboDevices";
            this.comboDevices.Size = new System.Drawing.Size(265, 23);
            this.comboDevices.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ScanQrReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 314);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.pb_Camera);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboDevices);
            this.Name = "ScanQrReceipt";
            this.Text = "Számla keresése QR kód alapján";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanQrReceipt_FormClosing);
            this.Load += new System.EventHandler(this.ScanQrReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Camera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.PictureBox pb_Camera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboDevices;
        private System.Windows.Forms.Timer timer1;
    }
}