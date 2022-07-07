
namespace RaktarKezeloDiploma
{
    partial class AddWithQRCODE
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboDevices = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textProduct = new System.Windows.Forms.TextBox();
            this.comboNumber = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(352, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Location = new System.Drawing.Point(12, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(415, 256);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Kamera:";
            // 
            // comboDevices
            // 
            this.comboDevices.FormattingEnabled = true;
            this.comboDevices.Location = new System.Drawing.Point(66, 8);
            this.comboDevices.Name = "comboDevices";
            this.comboDevices.Size = new System.Drawing.Size(265, 23);
            this.comboDevices.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textProduct
            // 
            this.textProduct.Location = new System.Drawing.Point(12, 304);
            this.textProduct.Name = "textProduct";
            this.textProduct.Size = new System.Drawing.Size(286, 23);
            this.textProduct.TabIndex = 8;
            // 
            // comboNumber
            // 
            this.comboNumber.FormattingEnabled = true;
            this.comboNumber.Location = new System.Drawing.Point(304, 303);
            this.comboNumber.Name = "comboNumber";
            this.comboNumber.Size = new System.Drawing.Size(41, 23);
            this.comboNumber.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(351, 303);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Hozzáadás";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // AddWithQRCODE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 338);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.comboNumber);
            this.Controls.Add(this.textProduct);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboDevices);
            this.Name = "AddWithQRCODE";
            this.Text = "Hozzáadás QR kód alapján";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddWithQRCODE_FormClosed);
            this.Load += new System.EventHandler(this.AddWithQRCODE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboDevices;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textProduct;
        private System.Windows.Forms.ComboBox comboNumber;
        private System.Windows.Forms.Button btnAdd;
    }
}