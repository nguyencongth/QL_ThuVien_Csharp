using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_QuanLyThuVien
{
    public partial class Manager_NguoiDung : Form
    {
        public Manager_NguoiDung()
        {
            InitializeComponent();
        }
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
        private void tìmSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimKiemSach GDKH_Sach = new TimKiemSach();
            GDKH_Sach.Show();
        }
        private void DoiMatKhau_Click(object sender, EventArgs e)
        {
            DoiMatKhau GDKH_DoiMK = new DoiMatKhau();
            GDKH_DoiMK.Show();
        }
        private void Manager_NguoiDung_Load(object sender, EventArgs e)
        {

        }
    }
}
