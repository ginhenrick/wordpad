using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace _2100009025_Nguyễn_Minh_Tường_WordPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Clear();
        }

        private void openCtrlOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XuliOpenFile();
        }

        private void XuliOpenFile()
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "open file";
            ofd.Filter = "Text|*.txt|Word Pad |*.rtf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(ofd.FileName);
                if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                {
                    rtbDoc.Text = File.ReadAllText(ofd.FileName);
                }
                else if (Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
                {
                    rtbDoc.LoadFile(ofd.FileName);
                }
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XuLySaveFile();
        }

        private void XuLySaveFile()
        {
            var sfd = new SaveFileDialog();
            sfd.Title = "Luu file";
            sfd.Filter = "Text|*.txt|Word Pad |*.rtf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(sfd.FileName);
                if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                {
                    File.WriteAllText(sfd.FileName, rtbDoc.Text);
                }
                else if (Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                {
                    rtbDoc.SaveFile(sfd.FileName);
                }
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XuLySaveAs();
        }

        private void XuLySaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text|*.txt|Word Pad |*.rtf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                rtbDoc.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
            }

        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Redo();
        }



        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Paste();
        }

        private void insertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở dialog chọn file
            openFileDialog1.ShowDialog();

            // Kiểm tra nếu file được chọn
            // Kiểm tra nếu có file được chọn
            if (openFileDialog1.FileNames.Length > 0)
            {
                // Duyệt qua các file ảnh được chọn
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    // Đọc file ảnh
                    Image image = Image.FromFile(fileName);

                    // Tạo một object PictureBox
                    PictureBox pictureBox = new PictureBox();

                    // Thiết lập các thuộc tính cho PictureBox
                    pictureBox.Image = image;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Dock = DockStyle.Fill;

                    // Thêm PictureBox vào RichTextBox
                    rtbDoc.Controls.Add(pictureBox);
                }
            }
        }

        private void selectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XuLySelectFont();
        }

        private void XuLySelectFont()
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                // Áp dụng Font chữ cho RichTextBox
                rtbDoc.Font = fontDialog1.Font;
            }
        }
        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            cd.FullOpen = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                // Áp dụng màu sắc cho văn bản được chọn
                rtbDoc.SelectionColor = cd.Color;
            }
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bold();
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Italic();
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Underline();
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Xóa toàn bộ kiểu áp dụng
            rtbDoc.SelectionFont = new Font(rtbDoc.SelectionFont, FontStyle.Regular);
            rtbDoc.SelectionColor = SystemColors.WindowText;
        }

        private void pageColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại chọn màu
            cd.FullOpen = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                // Áp dụng màu sắc cho Background của RichTextBox
                rtbDoc.BackColor = cd.Color;
            }
        }

        private void identToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void alignToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void removeBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionBullet = false;
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionIndent = 0;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bold()
        {
            // Get the current font style
            FontStyle style = rtbDoc.SelectionFont.Style;

            // Toggle Bold style
            if ((style & FontStyle.Bold) == FontStyle.Bold)
            {
                // Bold is currently applied, so remove it
                style &= ~FontStyle.Bold;
            }
            else
            {
                // Bold is not applied, so add it
                style |= FontStyle.Bold;
            }

            // Apply the updated style to the selection
            rtbDoc.SelectionFont = new Font(rtbDoc.SelectionFont, style);
        }

        private void Italic()
        {
            // Get the current font style
            FontStyle style = rtbDoc.SelectionFont.Style;

            // Toggle Italic style
            if ((style & FontStyle.Italic) == FontStyle.Italic)
            {
                // Italic is currently applied, so remove it
                style &= ~FontStyle.Italic;
            }
            else
            {
                // Italic is not applied, so add it
                style |= FontStyle.Italic;
            }

            // Apply the updated style to the selection
            rtbDoc.SelectionFont = new Font(rtbDoc.SelectionFont, style);
        }

        private void Underline()
        {
            // Get the current font style
            FontStyle style = rtbDoc.SelectionFont.Style;

            // Toggle Underline style
            if ((style & FontStyle.Underline) == FontStyle.Underline)
            {
                // Underline is currently applied, so remove it
                style &= ~FontStyle.Underline;
            }
            else
            {
                // Underline is not applied, so add it
                style |= FontStyle.Underline;
            }

            // Apply the updated style to the selection
            rtbDoc.SelectionFont = new Font(rtbDoc.SelectionFont, style);
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Bold();
            boldToolStripMenuItem.Enabled = true;
        }

        private void tstUnderline_Click(object sender, EventArgs e)
        {
            Underline();
            underlineToolStripMenuItem.Enabled = true;
        }

        private void tstItalic_Click(object sender, EventArgs e)
        {
            Italic();
            italicToolStripMenuItem.Enabled = true;
        }

        private void Indent5ptsMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionIndent = 50;
        }

        private void Indent10ptsMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionIndent = 100;
        }

        private void Indent15ptsMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionIndent = 150;
        }

        private void Indent20ptsMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionIndent = 200;
        }

        private void AlignLeftMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void AlignCenterMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void AlignRightMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void addBulletsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            rtbDoc.SelectionBullet = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            XuliOpenFile();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            XuLySaveFile();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rtbDoc.Clear();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            XuLySelectFont();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            cd.FullOpen = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                // Áp dụng màu sắc cho văn bản được chọn
                rtbDoc.SelectionColor = cd.Color;
            }
        }
    }
}
