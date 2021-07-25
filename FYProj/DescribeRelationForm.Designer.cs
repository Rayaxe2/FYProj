
namespace FYProj
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.RelationSelCancelBtn = new System.Windows.Forms.Button();
            this.RelationSelDoneBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(40, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Multiplicity";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(40, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Participation";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Many To Many",
            "One To Many",
            "Many To One",
            "One To One"});
            this.comboBox1.Location = new System.Drawing.Point(40, 71);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(182, 33);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Manditory-Manditory",
            "Manditory-Optional",
            "Optional-Manditory",
            "Optional-Optional"});
            this.comboBox2.Location = new System.Drawing.Point(40, 174);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(182, 33);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // RelationSelCancelBtn
            // 
            this.RelationSelCancelBtn.Location = new System.Drawing.Point(136, 251);
            this.RelationSelCancelBtn.Name = "RelationSelCancelBtn";
            this.RelationSelCancelBtn.Size = new System.Drawing.Size(112, 62);
            this.RelationSelCancelBtn.TabIndex = 4;
            this.RelationSelCancelBtn.Text = "Cancel";
            this.RelationSelCancelBtn.UseVisualStyleBackColor = true;
            this.RelationSelCancelBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // RelationSelDoneBtn
            // 
            this.RelationSelDoneBtn.Location = new System.Drawing.Point(12, 251);
            this.RelationSelDoneBtn.Name = "RelationSelDoneBtn";
            this.RelationSelDoneBtn.Size = new System.Drawing.Size(112, 62);
            this.RelationSelDoneBtn.TabIndex = 5;
            this.RelationSelDoneBtn.Text = "Done";
            this.RelationSelDoneBtn.UseVisualStyleBackColor = true;
            this.RelationSelDoneBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form2
            // 
            this.AcceptButton = this.RelationSelDoneBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.RelationSelCancelBtn;
            this.ClientSize = new System.Drawing.Size(260, 325);
            this.ControlBox = false;
            this.Controls.Add(this.RelationSelDoneBtn);
            this.Controls.Add(this.RelationSelCancelBtn);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Describe Relationship";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button RelationSelCancelBtn;
        private System.Windows.Forms.Button RelationSelDoneBtn;
    }
}