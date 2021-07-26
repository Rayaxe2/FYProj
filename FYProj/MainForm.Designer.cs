
namespace FYProj
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.BackColor = System.Drawing.Color.MistyRose;
            this.button1.Location = new System.Drawing.Point(143, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 92);
            this.button1.TabIndex = 0;
            this.button1.Text = "New Table";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.button6);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button12);
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.button10);
            this.flowLayoutPanel1.Controls.Add(this.button5);
            this.flowLayoutPanel1.Controls.Add(this.button7);
            this.flowLayoutPanel1.Controls.Add(this.button8);
            this.flowLayoutPanel1.Controls.Add(this.button9);
            this.flowLayoutPanel1.Controls.Add(this.button11);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 736);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1674, 98);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Coral;
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(3, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(134, 92);
            this.button6.TabIndex = 3;
            this.button6.Text = "ER Diagram Starter [Assistant]";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Tomato;
            this.button4.Location = new System.Drawing.Point(420, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 92);
            this.button4.TabIndex = 3;
            this.button4.Text = "Remove Table";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Azure;
            this.button2.Location = new System.Drawing.Point(560, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 92);
            this.button2.TabIndex = 2;
            this.button2.Text = "Add Relationship";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PaleTurquoise;
            this.button3.Location = new System.Drawing.Point(700, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 92);
            this.button3.TabIndex = 2;
            this.button3.Text = "Remove Relationship";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Aquamarine;
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(840, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(134, 92);
            this.button10.TabIndex = 4;
            this.button10.Text = "NF1 Check";
            this.button10.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Aquamarine;
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(980, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(131, 92);
            this.button5.TabIndex = 2;
            this.button5.Text = "NF2 Check";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Aquamarine;
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(1117, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(134, 92);
            this.button7.TabIndex = 4;
            this.button7.Text = "NF3 Check";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Aquamarine;
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(1257, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(134, 92);
            this.button8.TabIndex = 4;
            this.button8.Text = "NF4 Check";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Aquamarine;
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(1397, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(134, 92);
            this.button9.TabIndex = 4;
            this.button9.Text = "NF5 Check";
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(1537, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(134, 92);
            this.button11.TabIndex = 5;
            this.button11.Text = "Proceed to conceptual design";
            this.button11.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Salmon;
            this.button12.Location = new System.Drawing.Point(280, 3);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(134, 92);
            this.button12.TabIndex = 4;
            this.button12.Text = "Rename Table";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1675, 837);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "ER Model Maker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
    }
}

