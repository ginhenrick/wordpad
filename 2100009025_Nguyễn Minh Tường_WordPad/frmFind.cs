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
    public partial class frmFind : Form
    {
        public event EventHandler<string> SearchRequested;
        public frmFind()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            // Lấy từ cần tìm kiếm từ TextBox
            string searchText = txtFind.Text;

            // Gửi yêu cầu tìm kiếm đến form cha
            SearchRequested?.Invoke(this, searchText);  
        }
    }
}
