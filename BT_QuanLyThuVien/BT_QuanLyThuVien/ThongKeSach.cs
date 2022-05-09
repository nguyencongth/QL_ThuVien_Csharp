using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace BT_QuanLyThuVien
{
    public partial class ThongKeSach : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        public ThongKeSach()
        {
            InitializeComponent();
        }
        private void load_ThongKeSach()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select MaSach, TenSach, TacGia, NXB, SoLuong from Sach", con);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            con.Close();
            //B4: Đổ dữ liệu từ DataAdapter vào datatable
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvTKSach.DataSource = tb;
            grvTKSach.Refresh();
        }
        private void ThongKeSach_Load(object sender, EventArgs e)
        {
            load_ThongKeSach();
        }

        private void xuatDanhSach_Click(object sender, EventArgs e)
        {
            string p_masach = txtmasach.Text.Trim();
            string p_tensach = txttensach.Text.Trim();
            string p_tacgia = txttacgia.Text.Trim();
            string p_nxb = txtNXB.Text.Trim();
            long p_soluong = long.Parse(txtsoluong.Text.Trim());
            Form f = new InSach(p_masach, p_tensach, p_tacgia, p_nxb);
            f.Show();
        }
        private void timkiem_Click(object sender, EventArgs e)
        {
            string p_timkiemTacGia = txtTKtacgia.Text.Trim();
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("Sach_TKS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TacGia", SqlDbType.NVarChar, 50).Value = p_timkiemTacGia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            //B4: Tạo dataAdapter để đọc dữ liệu từ cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //B5: Tạo dataTable để lấy dữ liệu từ DA
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvTKSach.DataSource = tb;
            grvTKSach.Refresh();
        }
        private void thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
        private void grvTKSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                int i = e.RowIndex;
                txtmasach.Text = grvTKSach.Rows[i].Cells[0].Value.ToString();
                txttensach.Text = grvTKSach.Rows[i].Cells[1].Value.ToString();
                txttacgia.Text = grvTKSach.Rows[i].Cells[2].Value.ToString();
                txtNXB.Text = grvTKSach.Rows[i].Cells[3].Value.ToString();
                txtsoluong.Text = grvTKSach.Rows[i].Cells[4].Value.ToString();
            txtmasach.Enabled = false;
        }
        private void btXoa_Click(object sender, EventArgs e)
        {
            string p_masach = txtmasach.Text.Trim();
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("Sach_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_masach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            load_ThongKeSach();
            MessageBox.Show("Xóa thành công", "Thông Báo");
            txtmasach.Enabled = true;
        }

        private void LamMoi_Click(object sender, EventArgs e)
        {
            load_ThongKeSach();
        }
    }
}
