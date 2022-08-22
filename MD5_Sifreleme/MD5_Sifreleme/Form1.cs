using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MD5_Sifreleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_sifrele_Click(object sender, EventArgs e)
        {         
            lbl_sonuc.Text = MD5Sifrele(txt_sifrelenecek.Text);
        }

        public static string MD5Sifrele(string sifrelenecekMetin)
        {

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);

            //dizinin hash'ini hesaplattık.
            dizi = md5.ComputeHash(dizi);

            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            StringBuilder sb = new StringBuilder();

            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.
            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            DosyayaYaz(sifrelenecekMetin, sb.ToString());
            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
        }

        
        public static void DosyayaYaz(string metin, string sifreliMetin)
        {
        DosyayaYaz:
            string dosyaYolu = Application.StartupPath + "history.txt";

            if (!File.Exists(dosyaYolu))
            {
                // Dosya oluştur
                FileStream fs = File.Create(dosyaYolu);
                fs.Close();
                StreamWriter sw = File.AppendText(dosyaYolu);
                sw.WriteLine("ORJİNAL METİN" + " -> " + "ŞİFRELİ METİN");
                sw.WriteLine("");
                sw.Close();
                
                MessageBox.Show("Yeni Dosya Oluşturuldu");
                goto DosyayaYaz;
            }

            else
            {
                // Dosyaya Yaz
                StreamWriter sw = File.AppendText(dosyaYolu);    
                sw.WriteLine(metin + " -> " + sifreliMetin);
                sw.Close();
                MessageBox.Show("Şifreleme kaydedildi.");
            }

        }

    }

}

