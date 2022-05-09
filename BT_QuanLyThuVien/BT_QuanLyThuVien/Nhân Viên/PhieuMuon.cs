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
    public partial class PhieuMuon : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        public PhieuMuon()
        {
            InitializeComponent();
        }
        private void Load_PhieuMuon()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from PhieuMuon", conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            conn.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvPhieuMuon.DataSource = tb;
            grvPhieuMuon.Refresh();
        }
        private void Load_cbThuThu()
        {
            conn.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from Sach", conn);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            conn.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            DataRow r = tb.NewRow();
            r["MaSach"] = "--- Chọn Mã Sách ---";
            tb.Rows.InsertAt(r, 0);
            cbMaSach.DataSource = tb;
            cbMaSach.DisplayMember = "MaSach";
            cbMaSach.ValueMember = "MaSach";
        }
        private void cbMaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("select TenSach from Sach where MaSach='" + cbMaSach.SelectedValue.ToString() + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                lbMaSach.Text = reader.GetString(0).ToString();
            }
            reader.Close();
            cmd.Dispose();
            conn.Close();
        }
        private void Load_cbDocGia()
        {
            conn.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from DocGia", conn);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            conn.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            DataRow r = tb.NewRow();
            r["MaDocGia"] = "--- Chọn Mã Độc Giả ---";
            tb.Rows.InsertAt(r, 0);
            cbDocGia.DataSource = tb;
            cbDocGia.DisplayMember = "MaDocGia";
            cbDocGia.ValueMember = "MaDocGia";
        }
        private void cbDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("select TenDocGia from DocGia where MaDocGia='" + cbDocGia.SelectedValue.ToString() + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                lbDocGia.Text = reader.GetString(0).ToString();
            }
            reader.Close();
            cmd.Dispose();
            conn.Close();
        }
        private void PhieuMuon_Load(object sender, EventArgs e)
        {
            Load_PhieuMuon();
            Load_cbThuThu();
            Load_cbDocGia();
        }
        private void Them_Click(object sender, EventArgs e)
        {
            string P_maphieumuon = txtmaphieu.Text.Trim();
            string p_madocgia = cbMaSach.SelectedValue.ToString();
            string p_masach = cbMaSach.SelectedValue.ToString();
            DateTime p_ngaymuon = dateMuon.Value;
            DateTime p_ngaytra = dateTra.Value;
            string p_soluong = "1";
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("PhieuMuon_INS2", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhieuMuon", SqlDbType.NVarChar, 50).Value = P_maphieumuon;
            cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value = p_madocgia;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_masach;
            cmd.Parameters.Add("@NgayMuon", SqlDbType.Date).Value = p_ngaymuon;
            cmd.Parameters.Add("@NgayTra", SqlDbType.Date).Value = p_ngaytra;
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = p_soluong;                            //Giảm số lượng trong quản lý sách đi 1
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            Load_PhieuMuon();
            MessageBox.Show("Thêm mới phiếu mượn thành công");
        }
        private void Sua_Click(object sender, EventArgs e)
        {
            string P_maphieumuon = txtmaphieu.Text.Trim();
            string p_madocgia = cbDocGia.Text.Trim();
            string p_masach = cbMaSach.SelectedValue.ToString();
            DateTime p_ngaymuon = dateMuon.Value;
            DateTime p_ngaytra = dateTra.Value;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("PhieuMuon_UPDATE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhieuMuon", SqlDbType.NVarChar, 50).Value = P_maphieumuon;
            cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value = p_madocgia;
            cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_masach;
            cmd.Parameters.Add("@NgayMuon", SqlDbType.Date).Value = p_ngaymuon;
            cmd.Parameters.Add("@NgayTra", SqlDbType.Date).Value = p_ngaytra;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            Load_PhieuMuon();
            MessageBox.Show("Sửa phiếu mượn thành công");
            txtmaphieu.Enabled = true;
        }
        private void Xoa_Click(object sender, EventArgs e)
        {
            string P_maphieumuon = txtmaphieu.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("PhieuMuon_DELETE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhieuMuon", SqlDbType.NVarChar, 50).Value = P_maphieumuon;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            Load_PhieuMuon();
            MessageBox.Show("Xóa phiếu mượn thành công");
            txtmaphieu.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string P_tkmadocgia = txttimkiem_madocgia.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("PhieuMuon_find", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaDocGia", SqlDbType.NVarChar, 50).Value = P_tkmadocgia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvPhieuMuon.DataSource = tb;
            grvPhieuMuon.Refresh();
        }
        private void grvPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmaphieu.Text = grvPhieuMuon.Rows[i].Cells[0].Value.ToString();
            cbDocGia.Text = grvPhieuMuon.Rows[i].Cells[1].Value.ToString();
            cbMaSach.Text = grvPhieuMuon.Rows[i].Cells[2].Value.ToString();
            dateMuon.CustomFormat = grvPhieuMuon.Rows[i].Cells[3].Value.ToString();
            dateTra.CustomFormat = grvPhieuMuon.Rows[i].Cells[4].Value.ToString();
            txtmaphieu.Enabled = false;
        }
        private void Thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
    }
}
