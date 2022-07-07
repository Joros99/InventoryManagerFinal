
namespace RaktarKezeloDiploma
{
    partial class ListReceipts
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textCode = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.maskedFrom = new System.Windows.Forms.MaskedTextBox();
            this.maskedUntil = new System.Windows.Forms.MaskedTextBox();
            this.btn_Content = new System.Windows.Forms.Button();
            this.btn_byQR = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(593, 250);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(49, 6);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(100, 23);
            this.textName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Név:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mettől:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Meddig:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(394, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Számla kód";
            // 
            // textCode
            // 
            this.textCode.Location = new System.Drawing.Point(468, 5);
            this.textCode.Name = "textCode";
            this.textCode.Size = new System.Drawing.Size(56, 23);
            this.textCode.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(530, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Keresés";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // maskedFrom
            // 
            this.maskedFrom.Location = new System.Drawing.Point(199, 7);
            this.maskedFrom.Mask = "0000-00-00";
            this.maskedFrom.Name = "maskedFrom";
            this.maskedFrom.Size = new System.Drawing.Size(68, 23);
            this.maskedFrom.TabIndex = 10;
            this.maskedFrom.ValidatingType = typeof(System.DateTime);
            // 
            // maskedUntil
            // 
            this.maskedUntil.Location = new System.Drawing.Point(319, 7);
            this.maskedUntil.Mask = "0000-00-00";
            this.maskedUntil.Name = "maskedUntil";
            this.maskedUntil.Size = new System.Drawing.Size(69, 23);
            this.maskedUntil.TabIndex = 11;
            this.maskedUntil.ValidatingType = typeof(System.DateTime);
            // 
            // btn_Content
            // 
            this.btn_Content.Location = new System.Drawing.Point(617, 7);
            this.btn_Content.Name = "btn_Content";
            this.btn_Content.Size = new System.Drawing.Size(75, 40);
            this.btn_Content.TabIndex = 12;
            this.btn_Content.Text = "A rendelés tartalma";
            this.btn_Content.UseVisualStyleBackColor = true;
            this.btn_Content.Click += new System.EventHandler(this.btn_Content_Click);
            // 
            // btn_byQR
            // 
            this.btn_byQR.Location = new System.Drawing.Point(617, 53);
            this.btn_byQR.Name = "btn_byQR";
            this.btn_byQR.Size = new System.Drawing.Size(75, 53);
            this.btn_byQR.TabIndex = 13;
            this.btn_byQR.Text = "Számla keresése QR alapján";
            this.btn_byQR.UseVisualStyleBackColor = true;
            this.btn_byQR.Click += new System.EventHandler(this.btn_byQR_Click);
            // 
            // ListReceipts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 298);
            this.Controls.Add(this.btn_byQR);
            this.Controls.Add(this.btn_Content);
            this.Controls.Add(this.maskedUntil);
            this.Controls.Add(this.maskedFrom);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ListReceipts";
            this.Text = "Számlák listázása";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.MaskedTextBox maskedFrom;
        private System.Windows.Forms.MaskedTextBox maskedUntil;
        private System.Windows.Forms.Button btn_Content;
        private System.Windows.Forms.Button btn_byQR;
    }
}