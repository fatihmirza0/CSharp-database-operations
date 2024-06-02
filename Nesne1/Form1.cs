using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Nesne1
{
    public partial class Form1 : Form
    {
        OleDbConnection baglan;
        OleDbDataAdapter verial, da2;
        DataTable dt;
        OleDbCommand komut, komut2;
        BindingSource bs = new BindingSource();
        BindingSource bs2 = new BindingSource();

        void Listele()//Her prosedür içinden çağırabileceğimiz metot tanımladık.
        {
            string baglanti, sorgu;
            baglanti = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + "\\ogrenci1.mdb";
            sorgu = "select * from genel";
            baglan = new OleDbConnection(baglanti);
            verial = new OleDbDataAdapter(sorgu, baglan);
            dt = new DataTable();
            verial.Fill(dt);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }
        void listele2()
        {
            baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + "\\ogrenci1.mdb");
            baglan.Open();
            da2 = new OleDbDataAdapter("Select * From ders", baglan);
            DataTable tablo2 = new DataTable();
            da2.Fill(tablo2);
            bs2.DataSource = tablo2;
            dataGridView2.DataSource = bs2;
            baglan.Close();
        }
        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        void temizle2()
        {
            textBox6.Text = "";
            textBox5.Text = "";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="" && textBox2.Text!="" && textBox3.Text !="" && textBox4.Text != "") {
                komut = new OleDbCommand("Insert into genel (ad,soyad,sınıf,numara) values (@ad,@soyad,@sınıf,@numara)", baglan);
                komut.Parameters.AddWithValue("@ad", textBox1.Text);
                komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                komut.Parameters.AddWithValue("@sınıf", textBox3.Text);
                komut.Parameters.AddWithValue("@numara", textBox4.Text);
                baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Tebrikler veri girişiniz başarılı.", "Başarılı.");
            }
            else {
                MessageBox.Show("Lütfen bütün verileri giriniz.", "Eksik veri girildi!");
            }
            Listele();
            temizle();
            textBox1.Focus();

        }
        private void Form1_Load(object sender, EventArgs e)
        {   
            Listele();
            listele2();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand("Delete from genel where ad=@ad", baglan);
            komut.Parameters.AddWithValue("@ad", dataGridView1.CurrentRow.Cells[0].Value);
            baglan.Open();
            komut.ExecuteNonQuery();
            baglan.Close();
            Listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand("Update genel Set ad=@ad,soyad=@soyad,sınıf=@sınıf Where numara=@numara", baglan);
            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            komut.Parameters.AddWithValue("@soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@sınıf", textBox3.Text);
            komut.Parameters.AddWithValue("@numara", textBox4.Text);
            baglan.Open();
            komut.ExecuteNonQuery();
            baglan.Close();
            Listele();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            komut2 = new OleDbCommand("Update ders Set dersad=@dersad where derskod=@derskod", baglan);
            komut2.Parameters.AddWithValue("@dersad", textBox6.Text);
            komut2.Parameters.AddWithValue("derskod", textBox5.Text);
            baglan.Open();
            komut2.ExecuteNonQuery();
            baglan.Close();
            listele2();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
            bs2.MoveFirst();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
            bs2.MoveLast();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
            bs2.MovePrevious();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
            bs2.MoveNext();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            komut2 = new OleDbCommand("Delete from ders where derskod=@derskod", baglan);
            komut2.Parameters.AddWithValue("derskod", dataGridView2.CurrentRow.Cells[0].Value);
            baglan.Open();
            komut2.ExecuteNonQuery();
            baglan.Close();
            listele2();
            Listele();
            MessageBox.Show("Seçtiğiniz ders silindi.", "Ders SİLİNDİ!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox5.Text!= "" && textBox6.Text != "")
            {
                komut2 = new OleDbCommand("Insert into ders(derskod,dersad) values(@derskod,@dersad)", baglan);
                komut2.Parameters.AddWithValue("@derskod", Convert.ToInt32(textBox5.Text));
                komut2.Parameters.AddWithValue("@dersad", textBox6.Text);
                baglan.Open();
                komut2.ExecuteNonQuery();
                baglan.Close();
                listele2();
                temizle2();
                MessageBox.Show("Tebrikler başarıyla kaydedildi.", "Kayıt Başarılı");
                textBox5.Focus();
            }
            else
            {
                MessageBox.Show("Lütfen Ders kodunu ve ders adını giriniz", "Boşlukları dolduralım.");
                temizle2();
            }
        }
    }
}