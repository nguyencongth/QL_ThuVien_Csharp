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
    public partial class Manager_QuanLy : Form
    {
        public Manager_QuanLy()
        {
            InitializeComponent();
        }
        private void lapHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon hoaDon = new HoaDon();
            hoaDon.Show();
        }
        private void thànhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThanhVien thanhVien = new ThanhVien();
            thanhVien.Show();
        }
        private void phiếuMượnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhieuMuon phieuMuon = new PhieuMuon();
            phieuMuon.Show();
        }
        private void sáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLySach quanLySach = new QuanLySach();
            quanLySach.Show();
        }
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
        private void phiếuTrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhieuTra phieutra = new PhieuTra();
            phieutra.Show();
        }
        private void độcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocGia docgia = new DocGia();
            docgia.Show();
        }
        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKeSach thongKe = new ThongKeSach();
            thongKe.Show();
        }
        private void Manager_QuanLy_Load(object sender, EventArgs e)
        {

        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
