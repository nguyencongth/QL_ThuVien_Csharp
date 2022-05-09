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
    public partial class QuanLySach : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        public QuanLySach()
        {
            InitializeComponent();
        }
        private void Load_grvSach()
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
            grvSach.DataSource = tb;
            grvSach.Refresh();
        }
        private void QuanLySach_Load(object sender, EventArgs e)
        {
            Load_grvSach();
        }
        private bool KtraDinhDanglaSo(string text)
        {
            //hàm trả về giá trị là True nếu text là số, False nấu text là chữ
            int kq;
            return int.TryParse(text, out kq);
        }
        private void Check_maSachtrung(string p_MaSach, ref int p_kq)
        {
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("Check_trungmaSach", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_MaSach;
            SqlParameter kq = new SqlParameter("@kq", SqlDbType.Int);
            cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            p_kq = (int)kq.Value;
            cmd.Dispose();
            con.Close();
        }
        private void btThemSach_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaSach = txtMaSach.Text.Trim();
            string p_TenSach = txtTenSach.Text.Trim();
            string p_TacGia = txtTacGia.Text.Trim();
            string p_NXB = txtNXB.Text.Trim();
            string p_ChuyenMuc = txtChuyenMuc.Text.Trim();
            string p_ViTri = txtViTri.Text.Trim();
            int p_SoLuong = int.Parse(txtSoLuong.Text.Trim());
            //Ktra định dạng là số
            if (KtraDinhDanglaSo(txtGia.Text.Trim()) == false)
            {
                txtGia.Focus();
                txtGia.BackColor = Color.Green;
                MessageBox.Show("Hãy nhập số!");
            }
            else
            {
                long p_Gia = long.Parse(txtGia.Text.Trim());
                //Ktra dữ liệu rỗng
                if (p_MaSach == "")
                {
                    txtMaSach.Focus();
                    txtMaSach.BackColor = Color.Pink;
                    MessageBox.Show("Mã Thủ Thư không được rỗng");
                }
                else
                {
                    //Ktra trùng mã Sách (p_kq = 1 là mã lớp trùng)
                    int p_kq = 0;
                    Check_maSachtrung(p_MaSach, ref p_kq);
                    if (p_kq == 1)
                    {
                        txtMaSach.Focus();
                        txtMaSach.BackColor = Color.Red;
                        MessageBox.Show("Mã đã tồn tại. Nhập mã khác!");
                    }
                    else
                    {
                        //B2: Tạo kết nối DB
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        //B3: Tạo đối tượng Command
                        SqlCommand cmd = new SqlCommand("Sach_Ins", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_MaSach;
                        cmd.Parameters.Add("@TenSach", SqlDbType.NVarChar, 50).Value = p_TenSach;
                        cmd.Parameters.Add("@TacGia", SqlDbType.NVarChar, 50).Value = p_TacGia;
                        cmd.Parameters.Add("@NXB", SqlDbType.NVarChar, 50).Value = p_NXB;
                        cmd.Parameters.Add("@Gia", SqlDbType.BigInt).Value = p_Gia;
                        cmd.Parameters.Add("@ChuyenMuc", SqlDbType.NVarChar, 50).Value = p_ChuyenMuc;
                        cmd.Parameters.Add("@ViTri", SqlDbType.NVarChar, 50).Value = p_ViTri;
                        cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = p_SoLuong;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        con.Close();
                        Load_grvSach();
                        MessageBox.Show("Thêm mới thành công","Thông Báo");
                        LamMoi();
                    }}}
        }
        private void btSuaThongTin_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaSach = txtMaSach.Text.Trim();
            string p_TenSach = txtTenSach.Text.Trim();
            string p_TacGia = txtTacGia.Text.Trim();
            string p_NXB = txtNXB.Text.Trim();
            long p_Gia = long.Parse(txtGia.Text.Trim());
            string p_ChuyenMuc = txtChuyenMuc.Text.Trim();
            string p_ViTri = txtViTri.Text.Trim();
            int p_SoLuong = int.Parse(txtSoLuong.Text.Trim());
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("Sach_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaSach",   SqlDbType.NVarChar, 50).Value = p_MaSach;
            cmd.Parameters.Add("@TenSach",  SqlDbType.NVarChar, 50).Value = p_TenSach;
            cmd.Parameters.Add("@TacGia",   SqlDbType.NVarChar, 50).Value = p_TacGia;
            cmd.Parameters.Add("@NXB",      SqlDbType.NVarChar, 50).Value = p_NXB;
            cmd.Parameters.Add("@Gia",      SqlDbType.BigInt).Value = p_Gia;
            cmd.Parameters.Add("@ChuyenMuc",SqlDbType.NVarChar, 50).Value = p_ChuyenMuc;
            cmd.Parameters.Add("@ViTri",    SqlDbType.NVarChar, 50).Value = p_ViTri;
            cmd.Parameters.Add("@SoLuong",  SqlDbType.Int).Value = p_SoLuong;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvSach();
            MessageBox.Show("Sửa thành công","Thông Báo");
            LamMoi();
        }
        private void btXoaSach_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaSach = txtMaSach.Text.Trim();
            //B2: Tạo kết nối DB\
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("Sach_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_MaSach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvSach();
            MessageBox.Show("Xóa thành công","Thông Báo");
            txtMaSach.Enabled = true;
        }
        private void btTimKiemThongTinSach_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_TimKiem = txtTimKiem.Text.Trim();
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("Sach_Find", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_TimKiem;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            //B4: Tạo dataAdapter để đọc dữ liệu từ cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //B5: Tạo dataTable để lấy dữ liệu từ DA
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvSach.DataSource = tb;
            grvSach.Refresh();
        }
        private void grvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaSach.Text =    grvSach.Rows[i].Cells[0].Value.ToString();
            txtTenSach.Text =   grvSach.Rows[i].Cells[1].Value.ToString();
            txtTacGia.Text =    grvSach.Rows[i].Cells[2].Value.ToString();
            txtNXB.Text =       grvSach.Rows[i].Cells[3].Value.ToString();
            txtGia.Text =       grvSach.Rows[i].Cells[4].Value.ToString();
            txtChuyenMuc.Text = grvSach.Rows[i].Cells[5].Value.ToString();
            txtViTri.Text =     grvSach.Rows[i].Cells[6].Value.ToString();
            txtSoLuong.Text =   grvSach.Rows[i].Cells[7].Value.ToString();
            txtMaSach.Enabled = false;
        }
        private void btThoat_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
        private void LamMoi()
        {
            txtMaSach.Clear();
            txtMaSach.Enabled = true;
            txtMaSach.Focus();
            txtTenSach.Clear();
            txtTacGia.Clear();
            txtNXB.Clear();
            txtGia.Clear();
            txtChuyenMuc.Clear();
            txtViTri.Clear();
            txtSoLuong.Clear();
        }
        private void btLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            Load_grvSach();
        }
    }
}
