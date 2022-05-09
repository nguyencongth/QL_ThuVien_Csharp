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

    public partial class DocGia : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        public DocGia()
        {
            InitializeComponent();
        }
        private void Load_grvDocGia()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from DocGia", con);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            con.Close();
            //B4: Đổ dữ liệu từ DataAdapter vào datatable
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvDocGia.DataSource = tb;
            grvDocGia.Refresh();
        }

        private void ThemTheDocGia_Load(object sender, EventArgs e)
        {
            Load_grvDocGia();
        }
        private void Check_maDocGiatrung(string p_MaDocGia, ref int p_kq)
        {
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("Check_trungmaDocGIa", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value = p_MaDocGia;
            SqlParameter kq = new SqlParameter("@kq", SqlDbType.Int);
            cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            p_kq = (int)kq.Value;
            cmd.Dispose();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaDocGia = txtMaDocGia.Text.Trim();
            string p_MatKhau = txtMatKhau.Text.Trim();
            string p_TenDocGia = txtTenDocGia.Text.Trim();
            DateTime p_NamSinh = dtNgaySinh.Value;
            string p_gioitinh = "";                                         // Tạo biến gender => Để chứa giá trị Male hoặc Female
            if (this.rdMale.Checked == true)
            {
                p_gioitinh = "Nam";                                        // Nếu RadioButton Male được check => Lưu Male vào gender
            }
            else if (this.rdFemale.Checked == true)
                {
                p_gioitinh = "Nữ";                                  // Nếu RadioButton Female được check => Lưu Female vào gender
                }
            else
            {
                p_gioitinh = "Khác";
            }
            string p_DiaChi = txtDiaChi.Text.Trim();
            string p_SoDienThoai = txtSoDienThoai.Text.Trim();
            DateTime p_NgayMuaThe = dtMuaThe.Value;
            DateTime p_NgayHetHan = dtHetHan.Value;
            //Ktra dữ liệu rỗng
            if (p_MaDocGia == "")
            {
                txtMaDocGia.Focus();
                txtMaDocGia.BackColor = Color.Pink;
                MessageBox.Show("Mã Độc Giả không được rỗng");
            }
            else
            {
                //Ktra trùng mã lớp (p_kq = 1 là mã lớp trùng)
                int p_kq = 0;
                Check_maDocGiatrung(p_MaDocGia, ref p_kq);
                if (p_kq == 1)
                {
                    txtMaDocGia.Focus();
                    txtMaDocGia.BackColor = Color.Red;
                    MessageBox.Show("Mã đã tồn tại. Nhập mã khác!");
                }
                else
                {
                    //B2: Tạo kết nối DB
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    //B3: Tạo đối tượng Command
                    SqlCommand cmd = new SqlCommand("DocGia_Ins2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value = p_MaDocGia;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 50).Value = p_MatKhau;
                    cmd.Parameters.Add("@TenDocGia", SqlDbType.NVarChar, 50).Value = p_TenDocGia;
                    cmd.Parameters.Add("@NamSinh", SqlDbType.Date).Value = p_NamSinh;
                    cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 50).Value = p_gioitinh;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50).Value = p_DiaChi;
                    cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar, 50).Value = p_SoDienThoai;
                    cmd.Parameters.Add("@NgayMuaThe", SqlDbType.Date).Value = p_NgayMuaThe;
                    cmd.Parameters.Add("@NgayHetHan", SqlDbType.Date).Value = p_NgayHetHan;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    Load_grvDocGia();
                    MessageBox.Show("Thêm mới thành công");
                    ClearTextBox();
             }}
        }
        private void ClearTextBox()
        {
            txtMaDocGia.Clear();
            txtMaDocGia.Focus();                                                //sau khi xoá đưa con trỏ chuột về Mã Độc Giả
            txtMaDocGia.Enabled = true;
            txtMatKhau.Clear();
            txtTenDocGia.Clear();
            txtDiaChi.Clear();
            txtNgheNghiep.Clear();
            dtNgaySinh.Text = DateTime.Now.ToString();
            txtSoDienThoai.Clear();
            rdFemale.Checked = false;
            rdMale.Checked = false;
            rdMale.Refresh();
            rdFemale.Refresh();
            dtMuaThe.Text = DateTime.Now.ToString();
            dtHetHan.Text = DateTime.Now.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            this.groupBox1.BackColor = Color.Transparent;
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }

        private void Them_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaDocGia = txtMaDocGia.Text.Trim();
            string p_MatKhau = txtMatKhau.Text.Trim();
            string p_TenDocGia = txtTenDocGia.Text.Trim();
            DateTime p_NamSinh = dtNgaySinh.Value;
            string p_gioitinh = "";                                         // Tạo biến gender => Để chứa giá trị Male hoặc Female
            if (this.rdMale.Checked == true)
            {
                p_gioitinh = "Nam";                                        // Nếu RadioButton Male được check => Lưu Male vào gender
            }
            else if (this.rdFemale.Checked == true)
                {
                    p_gioitinh = "Nữ";                                  // Nếu RadioButton Female được check => Lưu Female vào gender
                }
            else
            {
                p_gioitinh = "Khác";
            }
            string p_NgheNghiep = txtNgheNghiep.Text.Trim();
            string p_DiaChi = txtDiaChi.Text.Trim();
            string p_SoDienThoai = txtSoDienThoai.Text.Trim();
            DateTime p_NgayMuaThe = dtMuaThe.Value;
            DateTime p_NgayHetHan = dtHetHan.Value;
            //Ktra dữ liệu rỗng
            if (p_MaDocGia == "")
            {
                txtMaDocGia.Focus();
                txtMaDocGia.BackColor = Color.Pink;
                MessageBox.Show("Mã Độc Giả không được rỗng","Thông báo");
            }
            else
            {
                //Ktra trùng mã lớp (p_kq = 1 là mã lớp trùng)
                int p_kq = 0;
                Check_maDocGiatrung(p_MaDocGia, ref p_kq);
                if (p_kq == 1)
                {
                    txtMaDocGia.Focus();
                    txtMaDocGia.BackColor = Color.Red;
                    MessageBox.Show("Mã đã tồn tại. Nhập mã khác!","Thông báo");
                }
                else
                {
                    //B2: Tạo kết nối DB
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    //B3: Tạo đối tượng Command
                    SqlCommand cmd = new SqlCommand("DocGia_Ins2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value =     p_MaDocGia;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 50).Value =      p_MatKhau;
                    cmd.Parameters.Add("@TenDocGia", SqlDbType.NVarChar, 50).Value =    p_TenDocGia;
                    cmd.Parameters.Add("@NamSinh", SqlDbType.Date).Value =              p_NamSinh;
                    cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 50).Value =     p_gioitinh;
                    cmd.Parameters.Add("@NgheNghiep", SqlDbType.NVarChar, 50).Value =   p_NgheNghiep;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50).Value =       p_DiaChi;
                    cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar, 50).Value =  p_SoDienThoai;
                    cmd.Parameters.Add("@NgayMuaThe", SqlDbType.Date).Value =           p_NgayMuaThe;
                    cmd.Parameters.Add("@NgayHetHan", SqlDbType.Date).Value =           p_NgayHetHan;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    Load_grvDocGia();
                    MessageBox.Show("Thêm mới thành công", "Thông báo ");
                    ClearTextBox();
                }
            }
        }
        private void Sua_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaDocGia = txtMaDocGia.Text.Trim();
            string p_MatKhau = txtMatKhau.Text.Trim();
            string p_TenDocGia = txtTenDocGia.Text.Trim();
            DateTime p_NamSinh = dtNgaySinh.Value;
            string p_gioitinh = "";                                         // Tạo biến gender => Để chứa giá trị Male hoặc Female
            if (this.rdMale.Checked == true)
            {
                p_gioitinh = "Nam";                                         // Nếu RadioButton Male được check => Lưu Male vào gender
            }
            else if (this.rdFemale.Checked == true)
                {
                    p_gioitinh = "Nữ";                                      // Nếu RadioButton Female được check => Lưu Female vào gender
                }
            else
            {
                p_gioitinh = "Khác";
            }
            string p_NgheNghiep = txtNgheNghiep.Text.Trim();
            string p_DiaChi = txtDiaChi.Text.Trim();
            string p_SoDienThoai = txtSoDienThoai.Text.Trim();
            DateTime p_NgayMuaThe = dtMuaThe.Value;
            DateTime p_NgayHetHan = dtHetHan.Value;
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("DocGia_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value =     p_MaDocGia;
            cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 50).Value =      p_MatKhau;
            cmd.Parameters.Add("@TenDocGia", SqlDbType.NVarChar, 50).Value =    p_TenDocGia;
            cmd.Parameters.Add("@NamSinh", SqlDbType.Date).Value =              p_NamSinh;
            cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 50).Value =     p_gioitinh;
            cmd.Parameters.Add("@NgheNghiep", SqlDbType.NVarChar, 50).Value =   p_NgheNghiep;
            cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50).Value =       p_DiaChi;
            cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar, 50).Value =  p_SoDienThoai;
            cmd.Parameters.Add("@NgayMuaThe", SqlDbType.Date).Value =           p_NgayMuaThe;
            cmd.Parameters.Add("@NgayHetHan", SqlDbType.Date).Value =           p_NgayHetHan;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvDocGia();
            MessageBox.Show("Sửa thành công");
            txtMaDocGia.Enabled = true;
        }
        private void grvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaDocGia.Text = grvDocGia.Rows[i].Cells[0].Value.ToString();
            txtMatKhau.Text = grvDocGia.Rows[i].Cells[1].Value.ToString();
            txtTenDocGia.Text = grvDocGia.Rows[i].Cells[2].Value.ToString();
            dtNgaySinh.Text = grvDocGia.Rows[i].Cells[3].Value.ToString();
            rdMale.Text = "Nam";
            rdFemale.Text = "Nữ";
            rdKhac.Text = "Khác";
            if (grvDocGia.SelectedCells[0].Value.ToString() == "Nữ")
            {
                rdFemale.Checked = true;
            }
            else if (grvDocGia.SelectedCells[0].Value.ToString() == "Nam")
            {
                rdMale.Checked = true;
            }
            else
            {
                rdKhac.Checked = true;
            }
            txtNgheNghiep.Text = grvDocGia.Rows[i].Cells[4].Value.ToString();
            txtDiaChi.Text = grvDocGia.Rows[i].Cells[6].Value.ToString();
            txtSoDienThoai.Text = grvDocGia.Rows[i].Cells[7].Value.ToString();
            dtMuaThe.Text = grvDocGia.Rows[i].Cells[8].Value.ToString();
            dtHetHan.Text = grvDocGia.Rows[i].Cells[9].Value.ToString();
            txtMaDocGia.Enabled = false;
        }
        private void Xoa_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaDocGia = txtMaDocGia.Text.Trim();
            //B2: Tạo kết nối DB\
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("DocGia_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value = p_MaDocGia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvDocGia();
            MessageBox.Show("Xóa thành công");
            txtMaDocGia.Enabled = true;
            
        }
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            //B1: lấy dữ liệu từ các control
            string p_MaDocGia = txttimkiem_madocgia.Text.Trim();
            //B2: Tạo kết nối DB
            if (con.State != ConnectionState.Open)
                con.Open();
            //B3: Tạo đối tượng Command
            SqlCommand cmd = new SqlCommand("DocGia_Find", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value = p_MaDocGia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            //B4: Tạo dataAdapter để đọc dữ liệu từ cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //B5: Tạo dataTable để lấy dữ liệu từ DA
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvDocGia.DataSource = tb;
            grvDocGia.Refresh();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Load_grvDocGia();
            ClearTextBox();
        }
    }
}
