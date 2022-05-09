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
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        //kiểm tra xem người dùng đã nhập đầy đủ thông tin hay chưa
        public bool KiemTra()
        {
            if (txtTaiKhoan.Text == "")
            {
                lbThongBao.ForeColor = System.Drawing.Color.Red;
                lbThongBao.Text = "Vui lòng nhập tên tài khoản !!";
                txtTaiKhoan.Focus();
                return false;
            }
            else  if (txtMatKhau.Text == "")
            {
                lbThongBao.ForeColor = System.Drawing.Color.Red;
                lbThongBao.Text = "Vui lòng nhập mật khẩu hiện tại !!";
                txtMatKhau.Focus();
                return false;
            }
            else if (txtMatKhauMoi.Text == "")
            {
                lbThongBao.ForeColor = System.Drawing.Color.Red;
                lbThongBao.Text = "Vui lòng nhập mật khẩu mới !!";
                txtMatKhauMoi.Focus();
                return false;
            }
            else if (txtNhapLai.Text == "")
            {
                lbThongBao.ForeColor = System.Drawing.Color.Red;
                lbThongBao.Text = "Vui lòng xác nhận mật khẩu !!";
                txtNhapLai.Focus();
                return false;
            }
            else if (txtMatKhauMoi.Text != txtNhapLai.Text)
            {
                lbThongBao.ForeColor = System.Drawing.Color.Red;
                lbThongBao.Text = "Mật khẩu mới và mật khẩu xác nhận không trùng khớp";
                txtNhapLai.Focus();
                txtNhapLai.SelectAll();
                return false;
            }
            return true;
        }
        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            
        }
        private void btDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (KiemTra())
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_Update_Pass";
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value =     txtTaiKhoan.Text;
                    cmd.Parameters.Add("@OldPass", SqlDbType.NVarChar).Value =  txtMatKhau.Text;
                    cmd.Parameters.Add("@NewPass", SqlDbType.NVarChar).Value =  txtMatKhauMoi.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.GetInt32(0) == 1)
                    {
                        lbThongBao.ForeColor = System.Drawing.Color.Blue;
                        lbThongBao.Text = dr.GetString(1);
                        MessageBox.Show("Thay Đổi Mật Khẩu Thành Công!!!", "Thông Báo");
                        Close();
                    }
                    else
                    {
                        lbThongBao.ForeColor = System.Drawing.Color.Red;
                        lbThongBao.Text = dr.GetString(1);
                        txtMatKhau.Focus();
                        txtMatKhau.SelectAll();
                    }
                    dr.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienThiMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = (char)0;
                txtMatKhauMoi.PasswordChar = (char)0;
                txtNhapLai.PasswordChar = (char)0;
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
                txtMatKhauMoi.PasswordChar = '*';
                txtNhapLai.PasswordChar = '*';
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
    }
}
