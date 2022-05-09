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
    public partial class TimKiemSach : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        public TimKiemSach()
        {
            InitializeComponent();
        }
        private void TimKiemSach_Load(object sender, EventArgs e)
        {
            Load_grvSach();
        }
        private void Load_grvSach()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from Sach", conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            conn.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvSach.DataSource = tb;
            grvSach.Refresh();
        }
        private void rbmasach_CheckedChanged(object sender, EventArgs e)
        {
            string p_masach = Nhapthongtin_timkiem.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Sach_TimKiemMaSach", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_masach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvSach.DataSource = tb;
            grvSach.Refresh();
        }
        private void rbtensach_CheckedChanged(object sender, EventArgs e)
        {
            string p_tensach = Nhapthongtin_timkiem.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Sach_TimKiemTenSach", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenSach", SqlDbType.NVarChar, 50).Value = p_tensach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvSach.DataSource = tb;
            grvSach.Refresh();
        }
        private void rbtacgia_CheckedChanged(object sender, EventArgs e)
        {
            string p_tacgia = Nhapthongtin_timkiem.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Sach_TimKiemTacGia", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TacGia", SqlDbType.NVarChar, 50).Value = p_tacgia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvSach.DataSource = tb;
            grvSach.Refresh();
        }
        private void btLamMoi_Click(object sender, EventArgs e)
        {
            Load_grvSach();
            rbmasach.Checked = false;
            rbtacgia.Checked = false;
            rbtensach.Checked = false;
            Nhapthongtin_timkiem.Clear();
        }
        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
    }
}
