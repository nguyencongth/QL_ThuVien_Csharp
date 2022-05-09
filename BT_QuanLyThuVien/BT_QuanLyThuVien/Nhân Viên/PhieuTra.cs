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
    public partial class PhieuTra : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        public PhieuTra()
        {
            InitializeComponent();
        }
        private void Load_PhieuTra()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("select MaPhieuTra,PhieuTra.MaPhieuMuon,PhieuTra.MaThuThu, PhieuMuon.MaSach, NgayPhaiTra, TienPhat " +
                                            "from PhieuTra, PhieuMuon " +
                                            "where PhieuTra.MaPhieuMuon = PhieuMuon.MaPhieuMuon", conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            conn.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvPhieuTra.DataSource = tb;
            grvPhieuTra.Refresh();
        }
        private void Load_cbThuThu()
        {
            conn.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from ThuThu", conn);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            conn.Close();

            DataTable tb = new DataTable();
            da.Fill(tb);
            DataRow r = tb.NewRow();
            r["MaThuThu"] = "--- Chọn Mã Thủ Thư ---";
            tb.Rows.InsertAt(r, 0);
            cbMaThuThu.DataSource = tb;
            cbMaThuThu.DisplayMember = "MaThuThu";
            cbMaThuThu.ValueMember = "MaThuThu";
        }
        private void Load_cbPhieuMuon()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            //B2: Tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select * from PhieuMuon", conn);
            //B3:Đổ dl từ command vào dataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            conn.Close();
            DataTable tb = new DataTable();
            da.Fill(tb);
            DataRow r = tb.NewRow();
            r["MaPhieuMuon"] = "--- Chọn Mã Phiếu Mượn ---";
            tb.Rows.InsertAt(r, 0);
            cbMaPhieuMuon.DataSource = tb;
            cbMaPhieuMuon.DisplayMember = "MaPhieuMuon";
            cbMaPhieuMuon.ValueMember = "MaPhieuMuon";

        }
        private void CheckMaTrung(string p_maphieutra, ref int p_kq)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("CTPhieuTra_CheckMaTrung", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhieuTra", SqlDbType.NVarChar, 50).Value = p_maphieutra;
            SqlParameter kq = new SqlParameter("@kq", SqlDbType.Int);
            cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            p_kq = (int)kq.Value;
            cmd.Dispose();
            conn.Close();
        }
        private void PhieuTra1_Load(object sender, EventArgs e)
        {
            Load_PhieuTra();
            Load_cbThuThu();
            Load_cbPhieuMuon();
        }
        private void cbMaPhieuMuon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("select NgayMuon, NgayTra, MaDocGia, MaSach from PhieuMuon where MaPhieuMuon='" + cbMaPhieuMuon.SelectedValue.ToString() + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                dtNgayMuon.Text = reader.GetDateTime(0).ToShortDateString();
                dtNgayPhaiTra.Text = reader.GetDateTime(1).ToShortDateString();
                lbMaDocGia.Text = reader.GetString(2).ToString();
                lbMaSach.Text = reader.GetString(3).ToString();
            }
            reader.Close();
            cmd.Dispose();
            conn.Close();
        }
        private void cbMaThuThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("select TenThuThu from ThuThu where MaThuThu='" + cbMaThuThu.SelectedValue.ToString() + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                lbTenThuThu.Text = reader.GetString(0).ToString();
            }
            reader.Close();
            cmd.Dispose();
            conn.Close();
        }
        private void Them_Click(object sender, EventArgs e)
        {
            tinhtienphat();
            string P_maphieutra = txtmaphieutra.Text.Trim();
            string p_maphieumuon = cbMaPhieuMuon.SelectedValue.ToString();
            string p_mathuthu = cbMaThuThu.Text.Trim();
            DateTime p_ngaytra = dtNgayTra.Value;
            string p_TienPhat = txtTienNopPhat.Text.Trim();
            string p_soluong = "1";
            string p_masach = lbMaSach.Text.Trim();
            if (P_maphieutra == "")
            {
                txtmaphieutra.Focus();
                txtmaphieutra.BackColor = Color.Pink;
                MessageBox.Show("Mã phiếu trả không được rỗng", "Cảnh Báo");
            }
            int p_kq = 0;
            CheckMaTrung(P_maphieutra, ref p_kq);
            if (p_kq == 1)
            {
                txtmaphieutra.Focus();
                txtmaphieutra.BackColor = Color.Red;
                MessageBox.Show("Mã đã tồn tại. Nhập mã khác!", "Cảnh Báo");
            }
            else
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("CTPhieuTra_INS2", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaPhieuTra", SqlDbType.NVarChar, 50).Value = P_maphieutra;
                cmd.Parameters.Add("@MaPhieuMuon", SqlDbType.NVarChar, 50).Value = p_maphieumuon;
                cmd.Parameters.Add("@MaThuThu", SqlDbType.NVarChar, 50).Value = p_mathuthu;
                cmd.Parameters.Add("@TienPhat", SqlDbType.NVarChar, 50).Value = p_TienPhat;
                cmd.Parameters.Add("@NgayPhaiTra", SqlDbType.Date).Value = p_ngaytra;
                cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = p_soluong;
                cmd.Parameters.Add("@MaSach", SqlDbType.NVarChar, 50).Value = p_masach;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                loadlai();
                MessageBox.Show("Thêm mới phiếu trả thành công", "Thông Báo");
                txtmaphieutra.BackColor = Color.White;
            }
        }
        private void Sua_Click(object sender, EventArgs e)
        {
            string P_maphieutra = txtmaphieutra.Text.Trim();
            string p_maphieumuon = cbMaPhieuMuon.SelectedValue.ToString();
            string p_mathuthu = cbMaThuThu.Text.Trim();
            DateTime p_ngaytra = dtNgayTra.Value;
            string p_TienPhat = txtTienNopPhat.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("CTPhieuTra_UPDATE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhieuTra", SqlDbType.NVarChar, 50).Value = P_maphieutra;
            cmd.Parameters.Add("@MaPhieuMuon", SqlDbType.NVarChar, 50).Value = p_maphieumuon;
            cmd.Parameters.Add("@MaThuThu", SqlDbType.NVarChar, 50).Value = p_mathuthu;
            cmd.Parameters.Add("@TienPhat", SqlDbType.NVarChar, 50).Value = p_TienPhat;
            cmd.Parameters.Add("@NgayPhaiTra", SqlDbType.Date).Value = p_ngaytra; ;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            Load_PhieuTra();
            MessageBox.Show("Sửa phiếu trả thành công", "Cảnh Báo");
            txtmaphieutra.Enabled = true;
        }
        private void Xoa_Click(object sender, EventArgs e)
        {
            string P_maphieutra = txtmaphieutra.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("CTPhieuTra_DELETE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhieuTra", SqlDbType.NVarChar, 50).Value = P_maphieutra;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            Load_PhieuTra();
            MessageBox.Show("Xóa phiếu mượn thành công", "Thông Báo");
            txtmaphieutra.Enabled = true;
        }
        private void Thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }

        private void grvPhieuTra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmaphieutra.Text = grvPhieuTra.Rows[i].Cells[0].Value.ToString();
            cbMaPhieuMuon.SelectedValue = grvPhieuTra.Rows[i].Cells[1].Value.ToString();
            cbMaThuThu.SelectedValue = grvPhieuTra.Rows[i].Cells[2].Value.ToString();
            lbMaDocGia.Text = grvPhieuTra.Rows[i].Cells[3].Value.ToString();
            dtNgayPhaiTra.Text = grvPhieuTra.Rows[i].Cells[4].Value.ToString();
            txtTienNopPhat.Text = grvPhieuTra.Rows[i].Cells[5].Value.ToString();
            txtmaphieutra.Enabled = false;
        }
        private void tinhtienphat()
        {
            double songaymuon;
            double tienphaitra = 0;
            double gia = 0;
            if (dtNgayPhaiTra.Text == dtNgayTra.Text)
                songaymuon = 1;
            else
            {
                TimeSpan giatrio = dtNgayTra.Value - dtNgayPhaiTra.Value;
                songaymuon = Math.Round(giatrio.TotalDays, 0);
            }
            tienphaitra = 5000 * songaymuon;
            txtTienNopPhat.Text = tienphaitra.ToString();
        }
        private void dtNgayTra_ValueChanged(object sender, EventArgs e)
        {
            tinhtienphat();
        }
        private void TimKiem_madocgia_Click_1(object sender, EventArgs e)
        {
            string P_tkmadocgia = txttimkiem_madocgia.Text.Trim();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("CTPhieuTra_find", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaPhieuTra", SqlDbType.NVarChar, 50).Value = P_tkmadocgia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            grvPhieuTra.DataSource = tb;
            grvPhieuTra.Refresh();
        }
        private void loadlai()
        {
            txtmaphieutra.Clear();
            cbMaPhieuMuon.Refresh();
            lbTenThuThu.Text = "";
            txttimkiem_madocgia.Clear();
            cbMaPhieuMuon.Refresh();
            lbMaDocGia.Text = "";
            lbMaSach.Text = "";
            txtTienNopPhat.Clear();
            Load_PhieuTra();
            txtmaphieutra.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadlai();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
