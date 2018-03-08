namespace ReceiveMailTest {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.receiveMail = new System.Windows.Forms.Button();
			this.sonul_label = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(17, 40);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(231, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "FetchAll";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(98, 71);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(69, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "plain text";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(173, 71);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "html text";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(17, 71);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "xml text";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(17, 158);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(231, 23);
			this.button5.TabIndex = 4;
			this.button5.Text = "delete message";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(16, 187);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(232, 23);
			this.button6.TabIndex = 5;
			this.button6.Text = "save and load from .eml";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(17, 100);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(231, 23);
			this.button7.TabIndex = 6;
			this.button7.Text = "okunmamışları getir";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(17, 129);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(231, 23);
			this.button8.TabIndex = 7;
			this.button8.Text = "delete with message id";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button8);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button7);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button6);
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.button5);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(292, 232);
			this.panel1.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(110, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Test İşlemleri";
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.sonul_label);
			this.panel2.Controls.Add(this.receiveMail);
			this.panel2.Location = new System.Drawing.Point(310, 12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(325, 236);
			this.panel2.TabIndex = 9;
			// 
			// receiveMail
			// 
			this.receiveMail.Location = new System.Drawing.Point(21, 13);
			this.receiveMail.Name = "receiveMail";
			this.receiveMail.Size = new System.Drawing.Size(75, 23);
			this.receiveMail.TabIndex = 0;
			this.receiveMail.Text = "Mail Al";
			this.receiveMail.UseVisualStyleBackColor = true;
			this.receiveMail.Click += new System.EventHandler(this.receiveMail_Click);
			// 
			// sonul_label
			// 
			this.sonul_label.AutoSize = true;
			this.sonul_label.Location = new System.Drawing.Point(18, 51);
			this.sonul_label.Name = "sonul_label";
			this.sonul_label.Size = new System.Drawing.Size(127, 13);
			this.sonul_label.TabIndex = 1;
			this.sonul_label.Text = "sonuc burada yer alacak.";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(647, 260);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label sonul_label;
		private System.Windows.Forms.Button receiveMail;
	}
}

