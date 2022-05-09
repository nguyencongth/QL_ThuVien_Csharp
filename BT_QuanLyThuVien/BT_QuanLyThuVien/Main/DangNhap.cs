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
    public partial class DangNhap : Form
    {
        class AccessData
        {
            public SqlConnection GetConnection()
            {
                return new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
            }
            public void ExcuteNonQuery(string sql)
            {
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            public SqlDataReader ExecuteReader(string sql)
            {
                SqlConnection conn = GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
        }
        public DangNhap()
        {
            InitializeComponent();
        }
        class KiemTraDocGia
        {
            public int CheckLogin(string username, string password)
            {
                AccessData acc = new AccessData();
                SqlDataReader reader = acc.ExecuteReader("SELECT MaDocGia, MatKhau FROM DocGia");
                while (reader.Read())
                {
                    if (reader[0].ToString() == username && reader[1].ToString() == password)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }
        class KiemTraThuThu
        {
            public int CheckLogin(string username, string password)
            {
                AccessData acc = new AccessData();
                SqlDataReader reader = acc.ExecuteReader("SELECT MaThuThu, MatKhau FROM ThuThu");
                while (reader.Read())
                {
                    if (reader[0].ToString() == username && reader[1].ToString() == password)
                    {
                        // reader[0] tương ứng cho textbox Username
                        // reader[1] tương ứng cho textbox Password
                        return 1;
                    }
                }
                return 0;
            }
        }
        private void DangNhap1_Load(object sender, EventArgs e)
        {

        }
        private void btQuanLy_Click(object sender, EventArgs e)
        {
            KiemTraThuThu dn = new KiemTraThuThu();
            if (dn.CheckLogin(txtTaiKhoan.Text, txtMatKhau.Text) == 1)          // Kiểm tra data từ TextBox và data trong database
            {
                Manager_QuanLy hd = new Manager_QuanLy();
                MessageBox.Show("Đăng Nhập Thành Công","Thông Báo");
                this.Hide();                                                    // Form Đăng Nhập sẽ ẩn đi => MainForm sẽ load lên
                hd.ShowDialog();
            }
            else
            {
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                lblThongBao.Text = "Sai mã thủ thư hoặc mật khẩu. Vui lòng nhập lại !!!";
                txtTaiKhoan.Clear();
                txtMatKhau.Clear();
            }
        }
        private void btNguoiDung_Click(object sender, EventArgs e)
        {
            KiemTraDocGia dn = new KiemTraDocGia();
            Manager_NguoiDung GDKH = new Manager_NguoiDung();
            if (dn.CheckLogin(txtTaiKhoan.Text, txtMatKhau.Text) == 1)          // Kiểm tra data từ TextBox và data trong database
            {
                MessageBox.Show("Đăng Nhập Thành Công","Thông Báo");
                this.Hide();                                                    // Form Đăng Nhập sẽ ẩn đi => MainForm sẽ load lên
                GDKH.ShowDialog();
            }
            else
            {
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                lblThongBao.Text = "Sai mã độc giả hoặc mật khẩu. Vui lòng nhập lại !!!";
                txtTaiKhoan.Clear();
                txtMatKhau.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
        private void cbHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienThiMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = (char)0;
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }
    }
}
