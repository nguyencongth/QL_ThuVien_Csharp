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
    
    public partial class ThanhVien : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        public ThanhVien()
        {
            InitializeComponent();
        }
        private void Load_grvThuThu()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from ThuThu", con);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            con.Close();
            //B4: Đổ dữ liệu từ DataAdapter vào datatable
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvThuThu.DataSource = tb;
            grvThuThu.Refresh();
        }
        private void ThanhVien_Load(object sender, EventArgs e)
        {
            Load_grvThuThu();
        }
        private void Check_maThuThutrung(string p_MaThuThu, ref int p_kq)
        {
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("Check_trungmaThuThu", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaThuThu", SqlDbType.NVarChar, 50).Value = p_MaThuThu;
            SqlParameter kq = new SqlParameter("@kq", SqlDbType.Int);
            cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            p_kq = (int)kq.Value;
            cmd.Dispose();
            con.Close();
        }

        private void btThemThuThu_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaThuThu = txtMaThuThu.Text.Trim();
            string p_TenThuThu = txtTenThuThu.Text.Trim();
            DateTime p_NamSinh = dtNamSinh.Value;
            string p_GioiTinh = "";                                         // Tạo biến gender => Để chứa giá trị Male hoặc Female
            if (this.rdMale.Checked == true)
            {
                p_GioiTinh = "Nam";                                         // Nếu RadioButton Male được check => Lưu Male vào gender
            }
            else if (this.rdFemale.Checked == true)
            {
                p_GioiTinh = "Nữ";                                          // Nếu RadioButton Female được check => Lưu Female vào gender
            }
            else
            {
                p_GioiTinh = "Khác";
            }
            string p_ChucVu = txtChucVu.Text.Trim();
            string p_DiaChi = txtDiaChi.Text.Trim();
            string p_MatKhau = txtMatKhau.Text.Trim();
            //Ktra dữ liệu rỗng
            if (p_MaThuThu == "")
            {
                txtMaThuThu.Focus();
                txtMaThuThu.BackColor = Color.Pink;
                MessageBox.Show("Mã Thủ Thư không được rỗng", "Thông Báo");
            }
            else
            {
                //Ktra trùng mã lớp (p_kq = 1 là mã lớp trùng)
                int p_kq = 0;
                Check_maThuThutrung(p_MaThuThu, ref p_kq);
                if (p_kq == 1)
                {
                    txtMaThuThu.Focus();
                    txtMaThuThu.BackColor = Color.Red;
                    MessageBox.Show("Mã đã tồn tại. Nhập mã khác!", "Thông Báo");
                }
                else
                {
                    //B2: Tạo kết nối DB
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    //B3: Tạo đối tượng Command
                    SqlCommand cmd = new SqlCommand("ThuThu_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaThuThu", SqlDbType.NVarChar, 50).Value = p_MaThuThu;
                    cmd.Parameters.Add("@TenThuThu", SqlDbType.NVarChar, 50).Value = p_TenThuThu;
                    cmd.Parameters.Add("@NamSinh", SqlDbType.Date).Value = p_NamSinh;
                    cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 50).Value = p_GioiTinh;
                    cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 50).Value = p_ChucVu;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50).Value = p_DiaChi;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 50).Value = p_MatKhau;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    Load_grvThuThu();
                    MessageBox.Show("Thêm mới thành công", "Thông Báo");
                }
            }
        }

        private void btSuaThongTin_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaThuThu = txtMaThuThu.Text.Trim();
            string p_TenThuThu = txtTenThuThu.Text.Trim();
            DateTime p_NamSinh = dtNamSinh.Value;
            string p_GioiTinh = "";                                         // Tạo biến gender => Để chứa giá trị Male hoặc Female
            if (this.rdMale.Checked == true)
            {
                p_GioiTinh = "Nam";                                         // Nếu RadioButton Male được check => Lưu Male vào gender
            }
            else if (this.rdFemale.Checked == true)
            {
                p_GioiTinh = "Nữ";                                          // Nếu RadioButton Female được check => Lưu Female vào gender
            }
            else
            {
                p_GioiTinh = "Khác";
            }
            string p_ChucVu = txtChucVu.Text.Trim();
            string p_DiaChi = txtDiaChi.Text.Trim();
            string p_MatKhau = txtMatKhau.Text.Trim();
            txtMaThuThu.Enabled = false;
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("ThuThu_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaThuThu", SqlDbType.NVarChar, 50).Value = p_MaThuThu;
            cmd.Parameters.Add("@TenThuThu", SqlDbType.NVarChar, 50).Value = p_TenThuThu;
            cmd.Parameters.Add("@NamSinh", SqlDbType.Date).Value = p_NamSinh;
            cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 50).Value = p_GioiTinh;
            cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 50).Value = p_ChucVu;
            cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50).Value = p_DiaChi;
            cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 50).Value = p_MatKhau;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvThuThu();
            MessageBox.Show("Sửa thành công","Thông Báo");
            txtMaThuThu.Enabled = true;
        }

        private void btXoaThuThu_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaThuThu = txtMaThuThu.Text.Trim();
            //B2: Tạo kết nối DB\
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("ThuThu_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaThuThu", SqlDbType.NVarChar, 50).Value = p_MaThuThu;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvThuThu();
            MessageBox.Show("Xóa thành công","Thông Báo");
            txtMaThuThu.Enabled = true;
        }

        private void btTimKiemThuThu_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_TimKiem = txtTimKiem.Text.Trim();
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("ThuThu_Find1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaThuThu", SqlDbType.NVarChar, 50).Value = p_TimKiem;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            //B4: Tạo dataAdapter để đọc dữ liệu từ cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //B5: Tạo dataTable để lấy dữ liệu từ DA
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvThuThu.DataSource = tb;
            grvThuThu.Refresh();
        }

        private void grvThuThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaThuThu.Text = grvThuThu.Rows[i].Cells[0].Value.ToString();
            txtTenThuThu.Text = grvThuThu.Rows[i].Cells[1].Value.ToString();
            dtNamSinh.Text = grvThuThu.Rows[i].Cells[2].Value.ToString();
            rdMale.Text = "Nam";
            rdFemale.Text = "Nữ";
            radKhac.Text = "Khác";
            if (grvThuThu.SelectedCells[0].Value.ToString() == "Nữ")
            {
                rdFemale.Checked = true;
            }
            else if (grvThuThu.SelectedCells[0].Value.ToString() == "Nam")
            {
                rdMale.Checked = true;
            }
            else
            {
                radKhac.Checked = true;
            }
            txtChucVu.Text = grvThuThu.Rows[i].Cells[4].Value.ToString();
            txtDiaChi.Text = grvThuThu.Rows[i].Cells[5].Value.ToString();
            txtMatKhau.Text = grvThuThu.Rows[i].Cells[6].Value.ToString();
            txtMaThuThu.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void LamMoi()
        {
            txtMaThuThu.Clear();
            txtMaThuThu.Enabled = true;
            txtMaThuThu.Focus();
            txtTenThuThu.Clear();
            txtMatKhau.Clear();
            rdFemale.Checked = false;
            rdMale.Checked = false;
            radKhac.Checked = false;
            rdMale.Refresh();
            rdFemale.Refresh();
            radKhac.Refresh();
            txtChucVu.Clear();
            txtDiaChi.Clear();
        }
        private void btLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            Load_grvThuThu();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
