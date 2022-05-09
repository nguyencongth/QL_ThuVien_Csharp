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
    public partial class HoaDon : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        
        public HoaDon()
        {
            InitializeComponent();
        }
        private void Load_cbThuThu()
        {
            con.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from ThuThu", con);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            con.Close();

            DataTable tb = new DataTable();
            da.Fill(tb);
            DataRow r = tb.NewRow();
            r["MaThuThu"] = "--- Chọn Mã Thủ Thư ---";
            tb.Rows.InsertAt(r, 0);
            cbMaThuThu.DataSource = tb;
            cbMaThuThu.DisplayMember = "MaThuThu";
            cbMaThuThu.ValueMember = "MaThuThu";
            
        }
        private void Load_cbDocGia()
        {
            con.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from DocGia", con);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            con.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            DataRow r = tb.NewRow();
            r["MaDocGia"] = "--- Chọn Mã Độc Giả ---";
            tb.Rows.InsertAt(r, 0);
            cbMaDocGia.DataSource = tb;
            cbMaDocGia.DisplayMember = "MaDocGia";
            cbMaDocGia.ValueMember = "MaDocGia";

        }
        private void Load_cbSach()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from Sach", con);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            con.Close();
            //B4: Đổ dữ liệu từ DataAdapter vào datatable
            DataTable tb = new DataTable();
            da.Fill(tb);
            DataRow r = tb.NewRow();
            r["MaSach"] = "--- Chọn Mã Sách ---";
            tb.Rows.InsertAt(r, 0);
            cbMaSach.DataSource = tb;
            cbMaSach.DisplayMember = "MaSach";
            cbMaSach.ValueMember = "MaSach";
        }
        private void TestHoaDon_Load(object sender, EventArgs e)
        {
            this.Load_cbThuThu();
            Load_cbDocGia();
            Load_cbSach();
            load_grvHoaDon();
            
        }
        private void cbMaThuThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("select TenThuThu from ThuThu where MaThuThu='" + cbMaThuThu.SelectedValue.ToString() + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                lbTenThuThu.Text = reader.GetString(0).ToString();
            }
            reader.Close();
            cmd.Dispose();
            con.Close();
        }
        private void cbMaDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("select TenDocGia,GioiTinh,DiaChi from DocGia where MaDocGia='" + cbMaDocGia.SelectedValue.ToString() + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                lbTenDocGia.Text = reader.GetString(0).ToString();
                lbGioiTinh.Text = reader.GetString(1).ToString();
                lbDiaChi.Text = reader.GetString(2).ToString();
            }
            reader.Close();
            cmd.Dispose();
            con.Close();
        }
        private void cbMaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("select TenSach,Gia from Sach where MaSach='" + cbMaSach.SelectedValue.ToString() + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                txtTenSach.Text = reader.GetString(0).ToString();
                txtGia.Text = reader.GetInt64(1).ToString();
            }
            reader.Close();
            cmd.Dispose();
            con.Close();
        }
        private void load_grvHoaDon()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ChiTietHoaDon", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            con.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvHoaDon.DataSource = tb;
            grvHoaDon.Refresh();
        }
        private void btThem_Click_1(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaHoaDon = txtMaHoaDon.Text.Trim();
            string p_MaSach = cbMaSach.SelectedValue.ToString();
            string p_TenSach = txtTenSach.Text.Trim();
            long p_Gia = long.Parse(txtGia.Text.Trim());
            int p_SoLuong = int.Parse(txtSoLuong.Text.Trim());
            long p_ThanhTien = long.Parse(lbThanhTien.Text.Trim());
            DateTime p_NgayBan = dtNgayBan.Value;
            //B2:
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("ChiTietHoaDon_Ins", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaHoaDon", SqlDbType.NVarChar, 50).Value = p_MaHoaDon;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_MaSach;
            cmd.Parameters.Add("@TenSach", SqlDbType.NVarChar, 50).Value = p_TenSach;
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = p_SoLuong;
            cmd.Parameters.Add("@Gia", SqlDbType.BigInt).Value = p_Gia;
            cmd.Parameters.Add("@ThanhTien", SqlDbType.BigInt).Value = p_ThanhTien;
            cmd.Parameters.Add("@NgayBan", SqlDbType.Date).Value = p_NgayBan;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            load_grvHoaDon();
            MessageBox.Show("Thêm mới thành công", "Thông Báo");
        }
        private void btHuy_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaSach = cbMaSach.SelectedValue.ToString();
            //B2: Tạo kết nối DB\
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("ChiTietHoaDon_deleteall", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_MaSach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            load_grvHoaDon();
            MessageBox.Show("Hủy hóa đơn thành công", "Thông Báo");
        }
        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
        private void btTinh_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(txtSoLuong.Text);
            int b = Convert.ToInt32(txtGia.Text);
            int kq = a * b;
            lbThanhTien.Text = kq.ToString();
        }
    }
}
