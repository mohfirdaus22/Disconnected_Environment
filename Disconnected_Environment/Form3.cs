using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class Form3 : Form
    {
        private string stringConnection = "data source=Jorengezzz\\THEPASHTER;" + "database=Activity6;User ID=sa;password=Salahtompo22";
        private SqlConnection koneksi;
        private string NIM, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource customerBindingSource = new BindingSource();


        //bikin
        private void Prodicbx()
        {
            koneksi.Open();
            string str = "select Id_prodi, nama_prodi from dbo.Prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxProdi.DisplayMember = "nama_prodi";
            cbxProdi.ValueMember = "Id_prodi";
            cbxProdi.DataSource = ds.Tables[0];
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNIM.Text = "";
            txtNama.Text = "";
            txtAlamat.Text = "";
            dtTanggalLahir.Value = DateTime.Today;
            txtNIM.Enabled = true;
            txtNama.Enabled = true;
            cbxJenisKelamin.Enabled = true;
            txtAlamat.Enabled = true;
            dtTanggalLahir.Enabled = true;
            cbxProdi.Enabled = true;
            Prodicbx();
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnAdd.Enabled = false;

        }

        //bikin
        private void refreshform()
        {
            txtNIM.Enabled = false;
            txtNama.Enabled = false;
            cbxJenisKelamin.Enabled = false;
            txtAlamat.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
            DataMahasiswa_Load();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NIM = txtNIM.Text;
            nama = txtNama.Text;
            jk = cbxJenisKelamin.Text;
            alamat = txtAlamat.Text;
            tgl = dtTanggalLahir.Value;
            prodi = cbxProdi.SelectedValue.ToString();
            koneksi.Open();
            string strs = "select Id_prodi from dbo.prodi where nama_prodi = @dd";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@dd", prodi));
            SqlDataReader dr = cm.ExecuteReader();
            dr.Close();
            string str = "insert into dbo.Mahasiswa (NIM, Nama_Mahasiswa, Jenis_Kelamin, Alamat, Tgl_Lahir, Id_prodi)" +
                "values(@NIM, @Nm, @Jk, @Al, @Tgll, @Idp)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@NIM", NIM));
            cmd.Parameters.Add(new SqlParameter("@Nm", nama));
            cmd.Parameters.Add(new SqlParameter("@Jk", jk));
            cmd.Parameters.Add(new SqlParameter("@AL", alamat));
            cmd.Parameters.Add(new SqlParameter("@Tgll", tgl));
            cmd.Parameters.Add(new SqlParameter("@Idp", prodi));
            cmd.ExecuteNonQuery();

            koneksi.Close();

            MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshform();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1_ f1 = new Form1_();
            f1.Show();
            this.Hide();
        }

        public Form3()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            this.bindingNavigator1.BindingSource = this.customerBindingSource;
            refreshform();
        }

        //bikin

        private void DataMahasiswa_Load()
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("select m.NIM, m.Nama_Mahasiswa, "
                + "m.Alamat, m.Jenis_Kelamin, m.Tgl_Lahir,p.nama_prodi From dbo.Mahasiswa m "
                + "join dbo.prodi p on m.Id_prodi = p.Id_prodi", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.customerBindingSource.DataSource = ds.Tables[0];
            this.txtNIM.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "NIM", true));
            this.txtNama.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "Nama_Mahasiswa", true));
            this.txtAlamat.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "Alamat", true));
            this.cbxJenisKelamin.DataBindings.Add(new Binding("Text", this.customerBindingSource,
                "Jenis_Kelamin", true));
            this.dtTanggalLahir.DataBindings.Add(new Binding("Text", this.customerBindingSource, "Tgl_Lahir",
                true));
            this.cbxProdi.DataBindings.Add(new Binding("Text", this.customerBindingSource, "nama_prodi", true));
            koneksi.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            refreshform();
        }


        //bikin
        private void clearBinding()
        {
            this.txtNIM.DataBindings.Clear();
            this.txtNama.DataBindings.Clear();
            this.txtAlamat.DataBindings.Clear();
            this.cbxJenisKelamin.DataBindings.Clear();
            this.dtTanggalLahir.DataBindings.Clear();
            this.cbxProdi.DataBindings.Clear();
        }


    }
}
