using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2100009025_Nguyễn_Minh_Tường_WordPad
{
    public partial class frmFindReplace : Form
    {
        private RichTextBox rtbDoc;
        private int currentIndex = 0; // Index hiện tại của từ đang tìm kiếm
        public frmFindReplace(RichTextBox rtbDoc)
        {
            InitializeComponent();
            this.rtbDoc = rtbDoc;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            string searchText = txtFindWhat.Text;
            string replaceText = txtReplaceWith.Text;
            int startIndex = currentIndex; // Thực hiện tìm kiếm từ vị trí hiện tại
            int index = rtbDoc.Text.IndexOf(searchText, startIndex);

            if (index != -1)
            {
                rtbDoc.Text = rtbDoc.Text.Remove(index, searchText.Length).Insert(index, replaceText);
                rtbDoc.Select(index, replaceText.Length);
                rtbDoc.Focus();
                currentIndex = index + replaceText.Length; // Cập nhật currentIndex
            }
            else
            {
                MessageBox.Show("Không tìm thấy.");
                currentIndex = 0; // Reset currentIndex nếu không tìm thấy từ
            }
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            string searchText = txtFindWhat.Text;
            int startIndex = currentIndex + 1; // Bắt đầu tìm kiếm từ vị trí hiện tại + 1
            int index = rtbDoc.Text.IndexOf(searchText, startIndex);

            if (index != -1)
            {
                rtbDoc.Select(index, searchText.Length);
                rtbDoc.Focus();
                currentIndex = index;
            }
            else
            {
                MessageBox.Show("Không tìm thấy.");
                currentIndex = 0; // Reset currentIndex nếu không tìm thấy từ
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            string searchText = txtFindWhat.Text;
            string replaceText = txtReplaceWith.Text;

            int count = 0;
            int index = rtbDoc.Text.IndexOf(searchText);
            while (index != -1)
            {
                rtbDoc.Text = rtbDoc.Text.Remove(index, searchText.Length).Insert(index, replaceText);
                count++;
                index = rtbDoc.Text.IndexOf(searchText, index + replaceText.Length);
            }

            MessageBox.Show($"Đã thay thế {count} từ.");
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
