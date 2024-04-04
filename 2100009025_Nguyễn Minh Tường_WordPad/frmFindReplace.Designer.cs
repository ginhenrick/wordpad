namespace _2100009025_Nguyễn_Minh_Tường_WordPad
{
    partial class frmFindReplace
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
            btnReplace = new Button();
            btnReplaceAll = new Button();
            btnFindNext = new Button();
            btnCancle = new Button();
            txtReplaceWith = new TextBox();
            txtFindWhat = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnReplace
            // 
            btnReplace.Location = new Point(313, 125);
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new Size(94, 29);
            btnReplace.TabIndex = 15;
            btnReplace.Text = "Replace";
            btnReplace.UseVisualStyleBackColor = true;
            btnReplace.Click += btnReplace_Click;
            // 
            // btnReplaceAll
            // 
            btnReplaceAll.Location = new Point(413, 125);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new Size(94, 29);
            btnReplaceAll.TabIndex = 14;
            btnReplaceAll.Text = "Replace All";
            btnReplaceAll.UseVisualStyleBackColor = true;
            btnReplaceAll.Click += btnReplaceAll_Click;
            // 
            // btnFindNext
            // 
            btnFindNext.Location = new Point(513, 125);
            btnFindNext.Name = "btnFindNext";
            btnFindNext.Size = new Size(94, 29);
            btnFindNext.TabIndex = 13;
            btnFindNext.Text = "Find Next";
            btnFindNext.UseVisualStyleBackColor = true;
            btnFindNext.Click += btnFindNext_Click;
            // 
            // btnCancle
            // 
            btnCancle.Location = new Point(613, 125);
            btnCancle.Name = "btnCancle";
            btnCancle.Size = new Size(94, 29);
            btnCancle.TabIndex = 12;
            btnCancle.Text = "Cancle";
            btnCancle.UseVisualStyleBackColor = true;
            btnCancle.Click += btnCancle_Click;
            // 
            // txtReplaceWith
            // 
            txtReplaceWith.Location = new Point(162, 79);
            txtReplaceWith.Name = "txtReplaceWith";
            txtReplaceWith.PlaceholderText = "Replace with";
            txtReplaceWith.Size = new Size(545, 27);
            txtReplaceWith.TabIndex = 11;
            // 
            // txtFindWhat
            // 
            txtFindWhat.Location = new Point(162, 33);
            txtFindWhat.Name = "txtFindWhat";
            txtFindWhat.PlaceholderText = "Find what";
            txtFindWhat.Size = new Size(545, 27);
            txtFindWhat.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 82);
            label2.Name = "label2";
            label2.Size = new Size(94, 20);
            label2.TabIndex = 9;
            label2.Text = "Replace with";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 36);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 8;
            label1.Text = "Find what";
            // 
            // frmFindReplace
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 190);
            Controls.Add(btnReplace);
            Controls.Add(btnReplaceAll);
            Controls.Add(btnFindNext);
            Controls.Add(btnCancle);
            Controls.Add(txtReplaceWith);
            Controls.Add(txtFindWhat);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmFindReplace";
            Text = "Find and Replace";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnReplace;
        private Button btnReplaceAll;
        private Button btnFindNext;
        private Button btnCancle;
        private TextBox txtReplaceWith;
        private TextBox txtFindWhat;
        private Label label2;
        private Label label1;
    }
}