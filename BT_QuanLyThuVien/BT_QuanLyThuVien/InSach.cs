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
    public partial class InSach : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0OT6Q1J\\SQLEXPRESS;Initial Catalog=BaiThi_QLThuVien;Integrated Security=True");
        private string p_masach;
        private string p_tensach;
        private string p_tacgia;
        private string p_nxb;
        public InSach(string p_masach, string p_tensach, string p_tacgia, string p_nxb) : this()
        {
            this.p_masach = p_masach;
            this.p_tensach = p_tensach;
            this.p_tacgia = p_tacgia;
            this.p_nxb = p_nxb;
        }
        
        public InSach()
        {
            InitializeComponent();
        }
        
        private void InSach_Load(object sender, EventArgs e)
        {
            CrystalReport1 rpt = new CrystalReport1();
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("Sach_TKS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TacGia", SqlDbType.NVarChar, 50).Value = p_tacgia;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            rpt.SetDataSource(tb);
            crtrbSach.ReportSource = rpt;
        }

        private void crtrbSach_Load(object sender, EventArgs e)
        {

        }
    }
}
