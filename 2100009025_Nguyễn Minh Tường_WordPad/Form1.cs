using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;



namespace _2100009025_Nguyễn_Minh_Tường_WordPad
{
    public partial class Form1 : Form
    {
        private frmFind findForm;

        private frmFindReplace findReplaceForm;

        internal TextBox txtReplaceWith { get; set; }
        internal TextBox txtFindWhat { get; set; }

        public Form1()
        {
            InitializeComponent();
            InitializeFindForm();
            rtbDoc.Click += rtbDoc_Click; // Attach event handler for RichTextBox click
            rtbDoc.DragEnter += RtbDoc_DragEnter; // Attach event handler for DragEnter event
            rtbDoc.DragDrop += RtbDoc_DragDrop; // Attach event handler for DragDrop event
        }

        private void InitializeFindForm()
        {
            findForm = new frmFind();
            findForm.SearchRequested += FindForm_SearchRequested;
        }

        private void FindForm_SearchRequested(object sender, string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                // Đặt vị trí con trỏ ở đầu văn bản để bắt đầu tìm kiếm
                rtbDoc.SelectionStart = 0;
                rtbDoc.SelectionLength = 0;

                int count = 0;
                int index = rtbDoc.Find(searchText, rtbDoc.SelectionStart + rtbDoc.SelectionLength, RichTextBoxFinds.None);
                // Loại bỏ highlight từ cũ nếu có
                RemoveOldHighlights();

                // Tìm kiếm và highlight tất cả các từ cần tìm
                while (index != -1)
                {
                    rtbDoc.SelectionStart = index;
                    rtbDoc.SelectionLength = searchText.Length;
                    rtbDoc.SelectionBackColor = Color.Yellow;

                    // Tìm kiếm từ tiếp theo từ vị trí kết thúc của từ hiện tại
                    index = rtbDoc.Find(searchText, index + searchText.Length, RichTextBoxFinds.None);
                    count++;
                }

                // Scroll đến vị trí con trỏ hiện tại
                rtbDoc.ScrollToCaret();

                // Hiển thị hộp thoại thông báo với số lần từ được tìm thấy
                MessageBox.Show($"Found {count} occurrences of '{searchText}'.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter text to search.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoveOldHighlights()
        {
            int originalSelectionStart = rtbDoc.SelectionStart;
            int originalSelectionLength = rtbDoc.SelectionLength;

            // Loại bỏ highlight từ cũ
            for (int i = 0; i < rtbDoc.TextLength; i++)
            {
                rtbDoc.Select(i, 1);
                rtbDoc.SelectionBackColor = rtbDoc.BackColor;
            }

            // Khôi phục vị trí con trỏ và chiều dài được chọn ban đầu
            rtbDoc.Select(originalSelectionStart, originalSelectionLength);
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
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                if (extension == ".txt")
                {
                    rtbDoc.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
                else if (extension == ".rtf")
                {
                    rtbDoc.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    // Xử lý trường hợp người dùng chọn định dạng khác (không bắt buộc)
                    MessageBox.Show("Invalid file format. Please choose .txt or .rtf");
                }
            }
        }


        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Hiển thị hộp thoại Page Setup
            PageSetupDialog pageSetupDialog = new PageSetupDialog();

            // Thiết lập cài đặt trang mặc định (có thể sửa đổi)
            pageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();

            // Thiết lập hệ thống in mặc định (có thể sửa đổi)
            pageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

            // Hiển thị hộp thoại Page Setup và kiểm tra xem người dùng có nhấp vào OK không
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy các thuộc tính trang được cài đặt từ hộp thoại Page Setup
                System.Drawing.Printing.PageSettings pageSettings = pageSetupDialog.PageSettings;

                // Áp dụng các cài đặt trang vào vùng RichTextBox (rtbDoc)
                rtbDoc.SelectionAlignment = HorizontalAlignment.Left; // Thiết lập căn lề trái (ví dụ)
                rtbDoc.SelectionColor = Color.Blue; // Thiết lập màu chữ (ví dụ)

                // Điều chỉnh vị trí lề phù hợp
                rtbDoc.SelectionIndent = pageSettings.Margins.Left; // Lề trái
                rtbDoc.SelectionRightIndent = pageSettings.Margins.Right; // Lề phải
                                                                          // Áp dụng các cài đặt khác tùy theo nhu cầu

                // Lấy kích thước giấy đã chọn từ hộp thoại Page Setup
                float paperWidth = pageSettings.PaperSize.Width; // Chiều rộng của giấy (đơn vị: 1/100 inch)
                float paperHeight = pageSettings.PaperSize.Height; // Chiều cao của giấy (đơn vị: 1/100 inch)

                // Chuyển đổi kích thước giấy từ đơn vị 1/100 inch sang pixel (đơn vị được sử dụng bởi RichTextBox)
                int paperWidthInPixels = (int)(paperWidth * rtbDoc.ZoomFactor * 100 / 254); // Chuyển đổi sang pixel
                int paperHeightInPixels = (int)(paperHeight * rtbDoc.ZoomFactor * 100 / 254); // Chuyển đổi sang pixel

                // Đặt lại kích thước của RichTextBox để phù hợp với kích thước giấy
                rtbDoc.Width = paperWidthInPixels;
                rtbDoc.Height = paperHeightInPixels;
                // Ghi nhận các cài đặt trang mới (nếu cần)
                // Có thể lưu các cài đặt này để sử dụng lại trong tương lai

                // Hiển thị thông báo xác nhận cho người dùng (tuỳ chọn)
                MessageBox.Show("Page setup applied successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tạo PrintPreviewDialog
            var printPreviewDialog = new PrintPreviewDialog();

            // Thiết lập PrintPreviewDialog
            printPreviewDialog.Document = printDocument1;

            // Hiển thị PrintPreviewDialog
            printPreviewDialog.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại PrintDialog
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                // In tài liệu
                printDocument1.Print();
            }

        }
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            // Vẽ nội dung RichTextBox lên trang in
            var graphics = e.Graphics;
            graphics.DrawString(rtbDoc.Text, rtbDoc.Font, Brushes.Black, e.MarginBounds);
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
            findForm.Show();
        }






        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFindReplace findReplaceForm = new frmFindReplace(rtbDoc);
            findReplaceForm.ShowDialog();

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
            // Open the file dialog to select an image file
            openFileDialog1.ShowDialog();

            // Check if any file is selected
            if (openFileDialog1.FileNames.Length > 0)
            {
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    // Read the image file
                    Image image = Image.FromFile(fileName);

                    // Insert the image at the cursor position
                    InsertImageAtCursorPosition(image);
                }
            }
        }

        private void InsertImageAtCursorPosition(Image image)
        {
            // Get the cursor position in the RichTextBox
            int cursorIndex = rtbDoc.SelectionStart;

            // Resize the image to fit within RichTextBox width
            if (image.Width > rtbDoc.Width)
            {
                float aspectRatio = (float)image.Height / image.Width;
                int newWidth = rtbDoc.Width - 20; // Reduce 20px for margins
                int newHeight = (int)(newWidth * aspectRatio);
                image = new Bitmap(image, new Size(newWidth, newHeight));
            }

            // Insert the image at the cursor position
            Clipboard.SetImage(image);
            rtbDoc.Paste();
        }

        private void RtbDoc_DragEnter(object sender, DragEventArgs e)
        {
            // Allow dropping of files onto the RichTextBox
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void RtbDoc_DragDrop(object sender, DragEventArgs e)
        {
            // Get the list of file paths dropped onto the RichTextBox
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

            // Check if any file is dropped
            if (fileNames.Length > 0)
            {
                foreach (string fileName in fileNames)
                {
                    // Check if the dropped file is an image
                    if (IsImageFile(fileName))
                    {
                        // Read the image file
                        Image image = Image.FromFile(fileName);

                        // Insert the image at the cursor position
                        InsertImageAtCursorPosition(image);
                    }
                }
            }
        }

        // Function to check if a file is an image file
        private bool IsImageFile(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp";
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'O' && Control.ModifierKeys == Keys.Control)
            {
                XuliOpenFile();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo PrintDocument
            printDocument1 = new PrintDocument();
            printDocument1.DocumentName = "My Document";
            printDocument1.PrintPage += PrintPageHandler;

            // Khởi tạo PrintDialog
            printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

        }



        private void rtbDoc_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void rtbDoc_Click(object sender, EventArgs e)
        {

            // Set the cursor position to where the user clicked
            Point cursorPosition = rtbDoc.PointToClient(Cursor.Position);
            rtbDoc.SelectionStart = rtbDoc.GetCharIndexFromPosition(cursorPosition);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutDialog();
        }

        private void ShowAboutDialog()
        {
            string aboutMessage = "THÀNH VIÊN NHÓM\n\n";
            aboutMessage += "Họ và tên                      MSSV                   Tỷ lệ hoàn thành Task\n";
            aboutMessage += "Nguyễn Minh Tường      2100009025    Hoành thành 7/7 chức năng\n";
            aboutMessage += "Nguyễn Đức Duy            2100012116    Hoàn thành  6/8 chức năng\n";
            aboutMessage += "Trịnh Văn Nguyên           2100004998    Hoàn thành  3/9 chức năng\n";
            aboutMessage += "Vương Trọng Tín             1911549016     Hoàn thành  10/10 chức năng\n";
            MessageBox.Show(aboutMessage, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            findForm.Show();
        }
    }
}
