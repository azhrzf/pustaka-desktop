using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;

namespace Daftar_Pustaka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool bahasaIND = true;

        private void GantiBahasa_Click(object sender, RoutedEventArgs e)
        {
            if (bahasaIND)
            {
                GantiBahasa.Content = "ENG (Ganti)";
                bahasaIND = false;
            }
            else
            {
                GantiBahasa.Content = "IND (Switch)";
                bahasaIND = true;
            }
            this.TahunTerbit_TextBox.Text += " ";
            this.TahunTerbit_TextBox.Text = this.TahunTerbit_TextBox.Text.Substring(0, this.TahunTerbit_TextBox.Text.Length - 1);
        }

        string inisialNama(string nama)
        {
            if (String.IsNullOrEmpty(nama))
            {
                return "";
            }

            string inisialNama = "";
            string buatInisial = nama.Trim();
            string[] inisialBaru = Regex.Split(buatInisial, @"[\u0020\u0009\u000D\u00A0]+");

            for (int i = 0; i < inisialBaru.Length; i++)
            {
                inisialBaru[i] = $"{inisialBaru[i][0].ToString()}{inisialBaru[i].Substring(1).ToLower()}";
            }

            if (inisialBaru.Length > 1)
            {
                //inisialNama = $"{inisialBaru[inisialBaru.Length - 1]}, {inisialBaru[0][0]}, ";
                inisialNama = $"{inisialBaru[inisialBaru.Length - 1]},";
                for (int i = 0; i < inisialBaru.Length - 1; i++)
                {
                    inisialNama = inisialNama + $" {inisialBaru[i]}";
                }
            }
            else
            {
                inisialNama = $"{inisialBaru[0]}";
            }

            if (tambahPenulis.Count > 1)
            {
                for (int i = 0; i < tambahPenulis.Count - 1; i++)
                {
                    inisialNama += $", {tambahPenulis[i]}";
                }
                if (String.IsNullOrEmpty(tambahPenulis[tambahPenulis.Count - 1]))
                {
                    string[] koma = inisialNama.Split(",");
                    string pengganti = "";
                    for (int i = 0; i < koma.Length - 1; i++)
                    {
                        pengganti += $"{koma[i]},";
                    }
                    inisialNama = pengganti.Trim() + $" {(bahasaIND ? "dan" : "and")}{koma[koma.Length - 1]}";
                }
                else
                {
                    inisialNama = $"{inisialNama.Trim()}, {(bahasaIND ? "dan" : "and")} {tambahPenulis[tambahPenulis.Count - 1]}";
                }
            }

            if (tambahPenulis.Count == 1 && !(String.IsNullOrEmpty(tambahPenulis[0])))
            {
                inisialNama = $"{inisialNama}, {(bahasaIND ? "dan" : "and")} {tambahPenulis[tambahPenulis.Count - 1]}";
            }

            if (inisialNama.Substring(inisialNama.Length - 1) == ".")
            {
                inisialNama = inisialNama.Substring(0, inisialNama.Length - 1);
            }

            return inisialNama;
        }

        string clearNama(string nama)
        {
            if (String.IsNullOrEmpty(nama))
            {
                return "";
            }

            string inisialNama = "";
            string buatInisial = nama.Trim();
            string[] inisialBaru = Regex.Split(buatInisial, @"[\u0020\u0009\u000D\u00A0]+");

            if (inisialBaru.Length > 0)
            {
                for (int i = 0; i < inisialBaru.Length; i++)
                {
                    inisialBaru[i] = $"{inisialBaru[i][0]}{inisialBaru[i].Substring(1).ToLower()}";
                    inisialNama += $" {inisialBaru[i]}";
                }
            }
            return inisialNama.Trim();
        }

        string unInisialNama(string nama)
        {
            string[] koma = nama.Split(",");
            int manyKoma = koma.Length;

            string inisialNama = "";

            if (manyKoma == 0)
            {
                return nama;
            }

            else if (manyKoma > 1)
            {
                nama = $"{koma[0]}, {koma[1]}";
            }

            string[] buatInisial = nama.Split(" ");

            for (int i = 1; i < buatInisial.Length; i++)
            {
                inisialNama += $"{buatInisial[i]} ";
            }

            inisialNama = ($"{inisialNama}{buatInisial[0].Substring(0, buatInisial[0].Length - 1)}").Trim();

            return inisialNama;
        }

        static string tentukanPustaka(string namaPustaka, string next)
        {
            if (!String.IsNullOrEmpty(namaPustaka) && next == "titik")
            {
                return $"{namaPustaka.Trim()}. ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "titikStop")
            {
                return $"{namaPustaka.Trim()}.";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "colon")
            {
                return $"{namaPustaka.Trim()}: ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "kurung")
            {
                return $"({namaPustaka.Trim()}), ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "colonBulan")
            {
                return $"({namaPustaka.Trim()}): ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "artikel")
            {
                return $"\"{namaPustaka.Trim()}.\" ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "spasi")
            {
                return $"{namaPustaka.Trim()} ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "koma")
            {
                return $"{namaPustaka.Trim()}, ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "nomorJurnal")
            {
                return $"no. {namaPustaka.Trim()} ";
            }
            else
            {
                return "";
            }
        }

        List<String> tambahPenulis = new List<String>();
        bool bolehTambah = true;

        private void TambahPenulisButton1_Click(object sender, RoutedEventArgs e)
        {
            foreach (string penulis in tambahPenulis)
            {
                if (String.IsNullOrEmpty(penulis))
                {
                    bolehTambah = false;
                    break;
                }
                bolehTambah = true;
            }

            if (!bolehTambah || String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text))
            {
                MessageBox.Show("Lengkapi Nama Penulis Terlebih Dahulu!");
            }

            else
            {
                tambahPenulis.Add("");
                NextPenulis.Children.Clear();
                TextBlock[] textBlockTambah = new TextBlock[tambahPenulis.Count];
                TextBox[] textBoxTambah = new TextBox[tambahPenulis.Count];
                Button[] buttonHapus = new Button[tambahPenulis.Count];

                for (int i = 0; i < tambahPenulis.Count; i++)
                {
                    var textBlockTambahSet = new TextBlock();
                    textBlockTambah[i] = textBlockTambahSet;
                    textBlockTambahSet.Text = $"Nama Penulis {i + 2}";
                    textBlockTambahSet.Name = $"NamaPenulis{i}";
                    textBlockTambahSet.FontWeight = FontWeights.Bold;
                    Thickness margin = textBlockTambahSet.Margin;
                    margin.Top = 10;
                    textBlockTambahSet.Margin = margin;
                    NextPenulis.Children.Add(textBlockTambah[i]);

                    var textBoxTambahSet = new TextBox();
                    textBoxTambah[i] = textBoxTambahSet;
                    textBoxTambahSet.Name = $"TambahPenulis{i}";
                    textBoxTambahSet.TextChanged += Update_TextChanged;
                    textBoxTambahSet.Text = tambahPenulis[i];
                    NextPenulis.Children.Add(textBoxTambah[i]);

                    var buttonHapusSet = new Button();
                    buttonHapus[i] = buttonHapusSet;
                    buttonHapusSet.Content = "Hapus";
                    buttonHapusSet.Name = $"HapusPenulis{i}";
                    buttonHapusSet.Margin = margin;
                    NextPenulis.Children.Add(buttonHapus[i]);
                    buttonHapusSet.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonHapusPenulis_Click));
                }
            }
        }

        void ButtonHapusPenulis_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string index = button.Name;
            int remove = Convert.ToInt32(index.Substring(index.Length - 1));

            tambahPenulis.RemoveAt(remove);
            NextPenulis.Children.Clear();

            TextBlock[] textBlockTambah = new TextBlock[tambahPenulis.Count];
            TextBox[] textBoxTambah = new TextBox[tambahPenulis.Count];
            Button[] buttonHapus = new Button[tambahPenulis.Count];

            for (int i = 0; i < tambahPenulis.Count; i++)
            {
                var textBlockTambahSet = new TextBlock();
                textBlockTambah[i] = textBlockTambahSet;
                textBlockTambahSet.Text = $"Nama Penulis {i + 2}";
                textBlockTambahSet.Name = $"NamaPenulis{i}";
                textBlockTambahSet.FontWeight = FontWeights.Bold;
                Thickness margin = textBlockTambahSet.Margin;
                margin.Top = 10;
                textBlockTambahSet.Margin = margin;
                NextPenulis.Children.Add(textBlockTambah[i]);

                var textBoxTambahSet = new TextBox();
                textBoxTambah[i] = textBoxTambahSet;
                textBoxTambahSet.Name = $"TambahPenulis{i}";
                textBoxTambahSet.TextChanged += Update_TextChanged;
                textBoxTambahSet.Text = tambahPenulis[i];
                NextPenulis.Children.Add(textBoxTambah[i]);

                var buttonHapusSet = new Button();
                buttonHapus[i] = buttonHapusSet;
                buttonHapusSet.Content = "Hapus";
                buttonHapusSet.Name = $"HapusPenulis{i}";
                buttonHapusSet.Margin = margin;
                NextPenulis.Children.Add(buttonHapus[i]);
                buttonHapusSet.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonHapusPenulis_Click));
            }
        }

        string name = "buku";
        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            name = button.Name;

            if (name == "tesis")
            {
                TambahPenulisButton1.Visibility = Visibility.Collapsed;
            }
            else
            {
                TambahPenulisButton1.Visibility = Visibility.Visible;
            }

            this.TahunTerbit_TextBox.Text += " ";
            this.TahunTerbit_TextBox.Text = this.TahunTerbit_TextBox.Text.Substring(0, this.TahunTerbit_TextBox.Text.Length - 1);
        }

        private void Update_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            string check = textBox.Name;

            if (check.Substring(0, check.Length - 1) == "TambahPenulis")
            {
                int index = Convert.ToInt32(check.Substring(check.Length - 1));
                tambahPenulis[index] = clearNama(textBox.Text.Trim());
            }

            if (name == "buku")
            {
                NamaPenulis_TextBlock.Visibility = Visibility.Visible;
                TahunTerbit_TextBlock.Visibility = Visibility.Visible;
                Artikel_TextBlock.Visibility = Visibility.Collapsed;
                JudulBuku_TextBlock.Visibility = Visibility.Visible;
                EdisiBuku_TextBlock.Visibility = Visibility.Visible;
                NomorJurnal_TextBlock.Visibility = Visibility.Collapsed;
                BulanTerbit_TextBlock.Visibility = Visibility.Collapsed;
                Halaman_TextBlock.Visibility = Visibility.Collapsed;
                TempatTerbit_TextBlock.Visibility = Visibility.Visible;
                NamaPenerbit_TextBlock.Visibility = Visibility.Visible;
                PlatformPenerbit_TextBlock.Visibility = Visibility.Collapsed;
                Link_TextBlock.Visibility = Visibility.Collapsed;

                JudulBuku_TextBlock.Text = "Judul Buku";
                EdisiBuku_TextBlock.Text = "Edisi Buku (Opsional)";
                NamaPenerbit_TextBlock.Text = "Nama Penerbit";
                TempatTerbit_TextBlock.Text = "Tempat Terbit";

                NamaPenulis_TextBox.Visibility = Visibility.Visible;
                TahunTerbit_TextBox.Visibility = Visibility.Visible;
                Artikel_TextBox.Visibility = Visibility.Collapsed;
                JudulBuku_TextBox.Visibility = Visibility.Visible;
                EdisiBuku_TextBox.Visibility = Visibility.Visible;
                NomorJurnal_TextBox.Visibility = Visibility.Collapsed;
                BulanTerbit_TextBox.Visibility = Visibility.Collapsed;
                Halaman_TextBox.Visibility = Visibility.Collapsed;
                TempatTerbit_TextBox.Visibility = Visibility.Visible;
                NamaPenerbit_TextBox.Visibility = Visibility.Visible;
                PlatformPenerbit_TextBox.Visibility = Visibility.Collapsed;
                Link_TextBox.Visibility = Visibility.Collapsed;

                this.PustakaArtikel.Text = "";
                this.PustakaNomorJurnal.Text = "";
                this.PustakaBulanTerbit.Text = "";
                this.PustakaHalaman.Text = "";
                this.PustakaPlatformPenerbit.Text = "";
                this.PustakaLink.Text = "";

                if (String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(this.TahunTerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.JudulBuku_TextBox.Text) || String.IsNullOrEmpty(this.NamaPenerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.TempatTerbit_TextBox.Text))
                {
                    PustakaSemua.Foreground = Brushes.Red;
                    ButtonTambah.Visibility = Visibility.Hidden;
                }
                else
                {
                    PustakaSemua.Foreground = Brushes.Black;
                    ButtonTambah.Visibility = Visibility.Visible;
                }
                this.PustakaNamaPenerbit.Text = tentukanPustaka(this.NamaPenerbit_TextBox.Text, "titikStop");
                this.PustakaJudulBuku.Text = tentukanPustaka(this.JudulBuku_TextBox.Text, "titik");
                this.PustakaEdisiBuku.Text = tentukanPustaka(this.EdisiBuku_TextBox.Text, "titik");
                this.PustakaTempatTerbit.Text = tentukanPustaka(this.TempatTerbit_TextBox.Text, "colon");
            }

            else if (name == "ebooks")
            {
                NamaPenulis_TextBlock.Visibility = Visibility.Visible;
                TahunTerbit_TextBlock.Visibility = Visibility.Visible;
                Artikel_TextBlock.Visibility = Visibility.Collapsed;
                JudulBuku_TextBlock.Visibility = Visibility.Visible;
                EdisiBuku_TextBlock.Visibility = Visibility.Visible;
                NomorJurnal_TextBox.Visibility = Visibility.Collapsed;
                BulanTerbit_TextBlock.Visibility = Visibility.Collapsed;
                Halaman_TextBlock.Visibility = Visibility.Collapsed;
                TempatTerbit_TextBlock.Visibility = Visibility.Visible;
                NamaPenerbit_TextBlock.Visibility = Visibility.Visible;
                PlatformPenerbit_TextBlock.Visibility = Visibility.Visible;
                Link_TextBlock.Visibility = Visibility.Collapsed;

                JudulBuku_TextBlock.Text = "Judul Buku";
                EdisiBuku_TextBlock.Text = "Edisi Buku (Opsional)";
                NamaPenerbit_TextBlock.Text = "Nama Penerbit";
                PlatformPenerbit_TextBlock.Text = "Platform Penerbit";
                TempatTerbit_TextBlock.Text = "Tempat Terbit";

                NamaPenulis_TextBox.Visibility = Visibility.Visible;
                TahunTerbit_TextBox.Visibility = Visibility.Visible;
                Artikel_TextBox.Visibility = Visibility.Collapsed;
                JudulBuku_TextBox.Visibility = Visibility.Visible;
                EdisiBuku_TextBox.Visibility = Visibility.Visible;
                NomorJurnal_TextBox.Visibility = Visibility.Collapsed;
                BulanTerbit_TextBox.Visibility = Visibility.Collapsed;
                Halaman_TextBox.Visibility = Visibility.Collapsed;
                TempatTerbit_TextBox.Visibility = Visibility.Visible;
                NamaPenerbit_TextBox.Visibility = Visibility.Visible;
                PlatformPenerbit_TextBox.Visibility = Visibility.Visible;
                Link_TextBox.Visibility = Visibility.Collapsed;

                //sini
                this.PustakaArtikel.Text = "";
                this.PustakaNomorJurnal.Text = "";
                this.PustakaBulanTerbit.Text = "";
                this.PustakaHalaman.Text = "";
                this.PustakaLink.Text = "";

                if (String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(this.TahunTerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.JudulBuku_TextBox.Text) || String.IsNullOrEmpty(this.NamaPenerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.TempatTerbit_TextBox.Text) || String.IsNullOrEmpty(this.PlatformPenerbit_TextBox.Text))
                {
                    PustakaSemua.Foreground = Brushes.Red;
                    ButtonTambah.Visibility = Visibility.Hidden;
                }
                else
                {
                    PustakaSemua.Foreground = Brushes.Black;
                    ButtonTambah.Visibility = Visibility.Visible;
                }
                this.PustakaNamaPenerbit.Text = tentukanPustaka(this.NamaPenerbit_TextBox.Text, "titik");
                this.PustakaPlatformPenerbit.Text = tentukanPustaka(this.PlatformPenerbit_TextBox.Text, "titikStop");
                this.PustakaJudulBuku.Text = tentukanPustaka(this.JudulBuku_TextBox.Text, "titik");
                this.PustakaEdisiBuku.Text = tentukanPustaka(this.EdisiBuku_TextBox.Text, "titik");
                this.PustakaTempatTerbit.Text = tentukanPustaka(this.TempatTerbit_TextBox.Text, "colon");
            }

            else if (name == "jurnal")
            {
                NamaPenulis_TextBlock.Visibility = Visibility.Visible;
                TahunTerbit_TextBlock.Visibility = Visibility.Visible;
                Artikel_TextBlock.Visibility = Visibility.Visible;
                JudulBuku_TextBlock.Visibility = Visibility.Visible;
                EdisiBuku_TextBlock.Visibility = Visibility.Visible;
                NomorJurnal_TextBlock.Visibility = Visibility.Visible;
                BulanTerbit_TextBlock.Visibility = Visibility.Visible;
                Halaman_TextBlock.Visibility = Visibility.Visible;
                TempatTerbit_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenerbit_TextBlock.Visibility = Visibility.Collapsed;
                PlatformPenerbit_TextBlock.Visibility = Visibility.Collapsed;
                Link_TextBlock.Visibility = Visibility.Visible;

                JudulBuku_TextBlock.Text = "Judul Jurnal";
                EdisiBuku_TextBlock.Text = "Edisi Jurnal";
                Link_TextBlock.Text = "Link dengan DOI/Database tanpa DOI (Opsional)";

                NamaPenulis_TextBox.Visibility = Visibility.Visible;
                TahunTerbit_TextBox.Visibility = Visibility.Visible;
                Artikel_TextBox.Visibility = Visibility.Visible;
                JudulBuku_TextBox.Visibility = Visibility.Visible;
                EdisiBuku_TextBox.Visibility = Visibility.Visible;
                NomorJurnal_TextBox.Visibility = Visibility.Visible;
                BulanTerbit_TextBox.Visibility = Visibility.Visible;
                Halaman_TextBox.Visibility = Visibility.Visible;
                TempatTerbit_TextBox.Visibility = Visibility.Collapsed;
                NamaPenerbit_TextBox.Visibility = Visibility.Collapsed;
                PlatformPenerbit_TextBox.Visibility = Visibility.Collapsed;
                Link_TextBox.Visibility = Visibility.Visible;

                this.PustakaTempatTerbit.Text = "";
                this.PustakaNamaPenerbit.Text = "";
                this.PustakaPlatformPenerbit.Text = "";

                if (String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(this.TahunTerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.JudulBuku_TextBox.Text) || String.IsNullOrEmpty(this.Halaman_TextBox.Text) ||
                String.IsNullOrEmpty(this.BulanTerbit_TextBox.Text) || String.IsNullOrEmpty(this.EdisiBuku_TextBox.Text) ||
                String.IsNullOrEmpty(this.Artikel_TextBox.Text) || String.IsNullOrEmpty(this.NomorJurnal_TextBox.Text))
                {
                    PustakaSemua.Foreground = Brushes.Red;
                    ButtonTambah.Visibility = Visibility.Hidden;
                }
                else
                {
                    PustakaSemua.Foreground = Brushes.Black;
                    ButtonTambah.Visibility = Visibility.Visible;
                }
                this.PustakaArtikel.Text = tentukanPustaka(this.Artikel_TextBox.Text, "artikel");
                this.PustakaJudulBuku.Text = tentukanPustaka(this.JudulBuku_TextBox.Text, "spasi");
                this.PustakaEdisiBuku.Text = tentukanPustaka(this.EdisiBuku_TextBox.Text, "koma");
                this.PustakaNomorJurnal.Text = tentukanPustaka(this.NomorJurnal_TextBox.Text, "nomorJurnal");
                this.PustakaLink.Text = tentukanPustaka(this.Link_TextBox.Text, "titikStop");
                this.PustakaHalaman.Text = tentukanPustaka(this.Halaman_TextBox.Text, "titik");
                this.PustakaBulanTerbit.Text = tentukanPustaka(this.BulanTerbit_TextBox.Text, "colonBulan");
            }

            else if (name == "tesis")
            {
                NamaPenulis_TextBlock.Visibility = Visibility.Visible;
                TahunTerbit_TextBlock.Visibility = Visibility.Visible;
                Artikel_TextBlock.Visibility = Visibility.Visible;
                JudulBuku_TextBlock.Visibility = Visibility.Collapsed;
                EdisiBuku_TextBlock.Visibility = Visibility.Visible;
                NomorJurnal_TextBlock.Visibility = Visibility.Collapsed;
                BulanTerbit_TextBlock.Visibility = Visibility.Collapsed;
                Halaman_TextBlock.Visibility = Visibility.Collapsed;
                TempatTerbit_TextBlock.Visibility = Visibility.Visible;
                NamaPenerbit_TextBlock.Visibility = Visibility.Visible;
                PlatformPenerbit_TextBlock.Visibility = Visibility.Visible;
                Link_TextBlock.Visibility = Visibility.Visible;

                Artikel_TextBlock.Text = "Judul Tesis";
                EdisiBuku_TextBlock.Text = "Tipe Tesis";
                TempatTerbit_TextBlock.Text = "Perguruan Tinggi";
                NamaPenerbit_TextBlock.Text = "Database Tesis";
                PlatformPenerbit_TextBlock.Text = "Nomor Database";
                Link_TextBlock.Text = "Link";

                NamaPenulis_TextBox.Visibility = Visibility.Visible;
                TahunTerbit_TextBox.Visibility = Visibility.Visible;
                Artikel_TextBox.Visibility = Visibility.Visible;
                JudulBuku_TextBox.Visibility = Visibility.Collapsed;
                EdisiBuku_TextBox.Visibility = Visibility.Visible;
                NomorJurnal_TextBox.Visibility = Visibility.Collapsed;
                BulanTerbit_TextBox.Visibility = Visibility.Collapsed;
                Halaman_TextBox.Visibility = Visibility.Collapsed;
                TempatTerbit_TextBox.Visibility = Visibility.Visible;
                NamaPenerbit_TextBox.Visibility = Visibility.Visible;
                PlatformPenerbit_TextBox.Visibility = Visibility.Visible;
                Link_TextBox.Visibility = Visibility.Visible;

                this.PustakaJudulBuku.Text = "";
                this.PustakaNomorJurnal.Text = "";
                this.PustakaBulanTerbit.Text = "";
                this.PustakaHalaman.Text = "";

                if (String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(this.TahunTerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.Artikel_TextBox.Text) || String.IsNullOrEmpty(this.EdisiBuku_TextBox.Text) ||
                String.IsNullOrEmpty(this.TempatTerbit_TextBox.Text) || String.IsNullOrEmpty(this.NamaPenerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.PlatformPenerbit_TextBox.Text) || String.IsNullOrEmpty(this.Link_TextBox.Text))
                {
                    PustakaSemua.Foreground = Brushes.Red;
                    ButtonTambah.Visibility = Visibility.Hidden;
                }
                else
                {
                    PustakaSemua.Foreground = Brushes.Black;
                    ButtonTambah.Visibility = Visibility.Visible;
                }
                // balik

                this.PustakaArtikel.Text = tentukanPustaka(this.Artikel_TextBox.Text, "artikel");
                this.PustakaEdisiBuku.Text = tentukanPustaka(this.EdisiBuku_TextBox.Text, "koma");
                this.PustakaNamaPenerbit.Text = tentukanPustaka(this.NamaPenerbit_TextBox.Text, "koma");
                this.PustakaEdisiBuku.Text = tentukanPustaka(this.EdisiBuku_TextBox.Text, "koma");
                this.PustakaTempatTerbit.Text = tentukanPustaka(this.TempatTerbit_TextBox.Text, "koma");
                this.PustakaNamaPenerbit.Text = tentukanPustaka(this.NamaPenerbit_TextBox.Text, "spasi");
                this.PustakaPlatformPenerbit.Text = tentukanPustaka(this.PlatformPenerbit_TextBox.Text, "kurung");
                this.PustakaLink.Text = this.Link_TextBox.Text.Trim();
            }

            this.PustakaNamaPenulis.Text = tentukanPustaka(inisialNama((this.NamaPenulis_TextBox.Text).Trim()), "titik");
            this.PustakaTahunTerbit.Text = tentukanPustaka(this.TahunTerbit_TextBox.Text, "titik");
        }

        class newPustaka
        {
            public string namaPenulis { get; set; }
            public string tahunTerbit { get; set; }
            public string artikel { get; set; }
            public string judulBuku { get; set; }
            public string edisiBuku { get; set; }
            public string nomorJurnal { get; set; }
            public string bulanTerbit { get; set; }
            public string halaman { get; set; }
            public string tempatTerbit { get; set; }
            public string namaPenerbit { get; set; }
            public string platformPenerbit { get; set; }
            public string link { get; set; }
        }

        List<newPustaka> pustaka2 = new List<newPustaka>();
        List<String> pustaka = new List<String>();

        private void ButtonTambah_Click(object sender, RoutedEventArgs e)
        {
            pustaka2.Add(new newPustaka
            {
                namaPenulis = inisialNama(this.NamaPenulis_TextBox.Text),
                tahunTerbit = this.TahunTerbit_TextBox.Text,
                artikel = (name == "buku" || name == "ebooks") ? "" : this.Artikel_TextBox.Text,
                judulBuku = (name == "tesis") ? "" : this.JudulBuku_TextBox.Text,
                edisiBuku = this.EdisiBuku_TextBox.Text,
                nomorJurnal = !(name == "jurnal") ? "" : this.NomorJurnal_TextBox.Text,
                bulanTerbit = (name == "buku" || name == "ebooks" || name == "tesis") ? "" : this.BulanTerbit_TextBox.Text,
                halaman = (name == "buku" || name == "ebooks" || name == "tesis") ? "" : this.Halaman_TextBox.Text,
                tempatTerbit = this.TempatTerbit_TextBox.Text,
                namaPenerbit = this.NamaPenerbit_TextBox.Text,
                platformPenerbit = (name == "buku" || name == "jurnal") ? "" : this.PlatformPenerbit_TextBox.Text,
                link = (name == "buku" || name == "ebooks") ? "" : this.Link_TextBox.Text
            });

            pustaka.Add(this.PustakaSemua.Text);
            pustaka2 = pustaka2.OrderBy(x => x.namaPenulis + x.tahunTerbit + (String.IsNullOrEmpty(x.artikel) ? x.judulBuku : x.artikel) + x.judulBuku).ToList();
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();
            ButtonEditSatu.Children.Clear();

            Run[] namaPenulisRun = new Run[pustaka.Count];
            Run[] tahunTerbitRun = new Run[pustaka.Count];
            Run[] artikelRun = new Run[pustaka.Count];
            Run[] judulBukuRun = new Run[pustaka.Count];
            Run[] edisiBukuRun = new Run[pustaka.Count];
            Run[] nomorJurnalRun = new Run[pustaka.Count];
            Run[] bulanTerbitRun = new Run[pustaka.Count];
            Run[] halamanRun = new Run[pustaka.Count];
            Run[] tempatTerbitRun = new Run[pustaka.Count];
            Run[] namaPenerbitRun = new Run[pustaka.Count];
            Run[] platformPenerbitRun = new Run[pustaka.Count];
            Run[] linkRun = new Run[pustaka.Count];

            Paragraph[] arrayParagraph = new Paragraph[pustaka.Count];
            Button[] arrayButton = new Button[pustaka.Count];
            Button[] arrayButton2 = new Button[pustaka.Count];

            for (int i = 0; i < pustaka.Count; i++)
            {

                var namaPenulisRunSet = new Run();
                var tahunTerbitRunSet = new Run();
                var artikelRunSet = new Run();
                var judulBukuRunSet = new Run();
                var edisiBukuRunSet = new Run();
                var nomorJurnalRunSet = new Run();
                var bulanTerbitRunSet = new Run();
                var halamanRunSet = new Run();
                var tempatTerbitRunSet = new Run();
                var namaPenerbitRunSet = new Run();
                var platformPenerbitRunSet = new Run();
                var linkRunSet = new Run();

                namaPenulisRun[i] = namaPenulisRunSet;
                tahunTerbitRun[i] = tahunTerbitRunSet;
                artikelRun[i] = artikelRunSet;
                judulBukuRun[i] = judulBukuRunSet;
                edisiBukuRun[i] = edisiBukuRunSet;
                nomorJurnalRun[i] = nomorJurnalRunSet;
                bulanTerbitRun[i] = bulanTerbitRunSet;
                halamanRun[i] = halamanRunSet;
                tempatTerbitRun[i] = tempatTerbitRunSet;
                platformPenerbitRun[i] = platformPenerbitRunSet;
                namaPenerbitRun[i] = namaPenerbitRunSet;

                linkRun[i] = linkRunSet;

                // loncat

                namaPenulisRunSet.Text = tentukanPustaka(pustaka2[i].namaPenulis, "titik");
                tahunTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tahunTerbit, "titik");
                artikelRunSet.Text = tentukanPustaka(pustaka2[i].artikel, "artikel");
                judulBukuRunSet.Text = tentukanPustaka(pustaka2[i].judulBuku, (name == "jurnal" ? "spasi" : "titik"));
                edisiBukuRunSet.Text = tentukanPustaka(pustaka2[i].edisiBuku, (name == "jurnal" || name == "tesis" ? "koma" : "titik"));
                nomorJurnalRunSet.Text = tentukanPustaka(pustaka2[i].nomorJurnal, "nomorJurnal");
                bulanTerbitRunSet.Text = tentukanPustaka(pustaka2[i].bulanTerbit, "colonBulan");
                halamanRunSet.Text = tentukanPustaka(pustaka2[i].halaman, "titik");
                tempatTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tempatTerbit, (name == "tesis" ? "koma" : "colon"));
                namaPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].namaPenerbit, (name == "ebooks" || name == "jurnal" ? "titik" : (name == "tesis" ? "spasi" : "titikStop")));
                platformPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].platformPenerbit, (name == "tesis" ? "kurung" : "titikStop"));
                linkRunSet.Text = name == "jurnal" ? tentukanPustaka(pustaka2[i].link, "titikStop") : pustaka2[i].link;

                judulBukuRunSet.FontStyle = FontStyles.Italic;

                var tes = new Paragraph();
                arrayParagraph[i] = tes;
                tes.LineHeight = 2;

                tes.Inlines.Add(namaPenulisRun[i]);
                tes.Inlines.Add(tahunTerbitRun[i]);
                tes.Inlines.Add(artikelRun[i]);
                tes.Inlines.Add(judulBukuRun[i]);
                tes.Inlines.Add(edisiBukuRun[i]);
                tes.Inlines.Add(nomorJurnalRun[i]);
                tes.Inlines.Add(bulanTerbitRun[i]);
                tes.Inlines.Add(halamanRun[i]);
                tes.Inlines.Add(tempatTerbitRun[i]);
                tes.Inlines.Add(namaPenerbitRun[i]);
                tes.Inlines.Add(platformPenerbitRun[i]);
                tes.Inlines.Add(linkRun[i]);

                DaftarPustaka.Document.Blocks.Add(arrayParagraph[i]);

                Button newButton = new Button();
                var txt2 = new Button();
                arrayButton[i] = txt2;
                txt2.Content = "Hapus";
                txt2.Name = $"Btn{i}";
                ButtonHapusSatu.Children.Add(arrayButton[i]);
                txt2.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnSatu_Click));

                Button newButton2 = new Button();
                var txt4 = new Button();
                arrayButton2[i] = txt4;
                txt4.Content = "Edit";
                txt4.Name = $"Hps{i}";
                ButtonEditSatu.Children.Add(arrayButton2[i]);
                txt4.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnEdit_Click));
            }
        }

        void BtnSatu_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string index = button.Name;
            int remove = Convert.ToInt32(index.Substring(index.Length - 1));

            pustaka.RemoveAt(remove);
            pustaka2.RemoveAt(remove);
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();
            ButtonEditSatu.Children.Clear();

            pustaka2 = pustaka2.OrderBy(x => x.namaPenulis + x.tahunTerbit + (String.IsNullOrEmpty(x.artikel) ? x.judulBuku : x.artikel) + x.judulBuku).ToList();

            Run[] namaPenulisRun = new Run[pustaka.Count];
            Run[] tahunTerbitRun = new Run[pustaka.Count];
            Run[] artikelRun = new Run[pustaka.Count];
            Run[] judulBukuRun = new Run[pustaka.Count];
            Run[] edisiBukuRun = new Run[pustaka.Count];
            Run[] nomorJurnalRun = new Run[pustaka.Count];
            Run[] bulanTerbitRun = new Run[pustaka.Count];
            Run[] halamanRun = new Run[pustaka.Count];
            Run[] tempatTerbitRun = new Run[pustaka.Count];
            Run[] namaPenerbitRun = new Run[pustaka.Count];
            Run[] platformPenerbitRun = new Run[pustaka.Count];
            Run[] linkRun = new Run[pustaka.Count];

            Paragraph[] arrayParagraph = new Paragraph[pustaka.Count];
            Button[] arrayButton = new Button[pustaka.Count];
            Button[] arrayButton2 = new Button[pustaka.Count];

            for (int i = 0; i < pustaka.Count; i++)
            {

                var namaPenulisRunSet = new Run();
                var tahunTerbitRunSet = new Run();
                var artikelRunSet = new Run();
                var judulBukuRunSet = new Run();
                var edisiBukuRunSet = new Run();
                var nomorJurnalRunSet = new Run();
                var bulanTerbitRunSet = new Run();
                var halamanRunSet = new Run();
                var tempatTerbitRunSet = new Run();
                var namaPenerbitRunSet = new Run();
                var platformPenerbitRunSet = new Run();
                var linkRunSet = new Run();

                namaPenulisRun[i] = namaPenulisRunSet;
                tahunTerbitRun[i] = tahunTerbitRunSet;
                artikelRun[i] = artikelRunSet;
                judulBukuRun[i] = judulBukuRunSet;
                edisiBukuRun[i] = edisiBukuRunSet;
                nomorJurnalRun[i] = nomorJurnalRunSet;
                bulanTerbitRun[i] = bulanTerbitRunSet;
                halamanRun[i] = halamanRunSet;
                tempatTerbitRun[i] = tempatTerbitRunSet;
                platformPenerbitRun[i] = platformPenerbitRunSet;
                namaPenerbitRun[i] = namaPenerbitRunSet;

                linkRun[i] = linkRunSet;

                namaPenulisRunSet.Text = tentukanPustaka(pustaka2[i].namaPenulis, "titik");
                tahunTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tahunTerbit, "titik");
                artikelRunSet.Text = tentukanPustaka(pustaka2[i].artikel, "artikel");
                judulBukuRunSet.Text = tentukanPustaka(pustaka2[i].judulBuku, (name == "jurnal" ? "spasi" : "titik"));
                edisiBukuRunSet.Text = tentukanPustaka(pustaka2[i].edisiBuku, (name == "jurnal" || name == "tesis" ? "koma" : "titik"));
                nomorJurnalRunSet.Text = tentukanPustaka(pustaka2[i].nomorJurnal, "nomorJurnal");
                bulanTerbitRunSet.Text = tentukanPustaka(pustaka2[i].bulanTerbit, "colonBulan");
                halamanRunSet.Text = tentukanPustaka(pustaka2[i].halaman, "titik");
                tempatTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tempatTerbit, (name == "tesis" ? "koma" : "colon"));
                namaPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].namaPenerbit, (name == "ebooks" || name == "jurnal" ? "titik" : (name == "tesis" ? "spasi" : "titikStop")));
                platformPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].platformPenerbit, (name == "tesis" ? "kurung" : "titikStop"));
                linkRunSet.Text = name == "jurnal" ? tentukanPustaka(pustaka2[i].link, "titikStop") : pustaka2[i].link;

                judulBukuRunSet.FontStyle = FontStyles.Italic;

                var tes = new Paragraph();
                arrayParagraph[i] = tes;
                tes.LineHeight = 2;

                tes.Inlines.Add(namaPenulisRun[i]);
                tes.Inlines.Add(tahunTerbitRun[i]);
                tes.Inlines.Add(artikelRun[i]);
                tes.Inlines.Add(judulBukuRun[i]);
                tes.Inlines.Add(edisiBukuRun[i]);
                tes.Inlines.Add(nomorJurnalRun[i]);
                tes.Inlines.Add(bulanTerbitRun[i]);
                tes.Inlines.Add(halamanRun[i]);
                tes.Inlines.Add(tempatTerbitRun[i]);
                tes.Inlines.Add(namaPenerbitRun[i]);
                tes.Inlines.Add(platformPenerbitRun[i]);
                tes.Inlines.Add(linkRun[i]);

                DaftarPustaka.Document.Blocks.Add(arrayParagraph[i]);

                Button newButton = new Button();
                var txt2 = new Button();
                arrayButton[i] = txt2;
                txt2.Content = "Hapus";
                txt2.Name = $"Btn{i}";
                ButtonHapusSatu.Children.Add(arrayButton[i]);
                txt2.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnSatu_Click));

                Button newButton2 = new Button();
                var txt4 = new Button();
                arrayButton2[i] = txt4;
                txt4.Content = "Edit";
                txt4.Name = $"Hps{i}";
                ButtonEditSatu.Children.Add(arrayButton2[i]);
                txt4.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnEdit_Click));
            }
        }

        void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string index = button.Name;
            int remove = Convert.ToInt32(index.Substring(index.Length - 1));

            NamaPenulis_TextBox.Text = unInisialNama(pustaka2[remove].namaPenulis);
            TahunTerbit_TextBox.Text = pustaka2[remove].tahunTerbit;
            Artikel_TextBox.Text = pustaka2[remove].artikel;
            JudulBuku_TextBox.Text = pustaka2[remove].judulBuku;
            EdisiBuku_TextBox.Text = pustaka2[remove].edisiBuku;
            BulanTerbit_TextBox.Text = pustaka2[remove].bulanTerbit;
            Halaman_TextBox.Text = pustaka2[remove].halaman;
            TempatTerbit_TextBox.Text = pustaka2[remove].tempatTerbit;
            NamaPenerbit_TextBox.Text = pustaka2[remove].namaPenerbit;
            PlatformPenerbit_TextBox.Text = pustaka2[remove].platformPenerbit;
            Link_TextBox.Text = pustaka2[remove].link;

            pustaka.RemoveAt(remove);
            pustaka2.RemoveAt(remove);
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();
            ButtonEditSatu.Children.Clear();

            pustaka2 = pustaka2.OrderBy(x => x.namaPenulis + x.tahunTerbit + (String.IsNullOrEmpty(x.artikel) ? x.judulBuku : x.artikel) + x.judulBuku).ToList();

            Run[] namaPenulisRun = new Run[pustaka.Count];
            Run[] tahunTerbitRun = new Run[pustaka.Count];
            Run[] artikelRun = new Run[pustaka.Count];
            Run[] judulBukuRun = new Run[pustaka.Count];
            Run[] edisiBukuRun = new Run[pustaka.Count];
            Run[] nomorJurnalRun = new Run[pustaka.Count];
            Run[] bulanTerbitRun = new Run[pustaka.Count];
            Run[] halamanRun = new Run[pustaka.Count];
            Run[] tempatTerbitRun = new Run[pustaka.Count];
            Run[] namaPenerbitRun = new Run[pustaka.Count];
            Run[] platformPenerbitRun = new Run[pustaka.Count];
            Run[] linkRun = new Run[pustaka.Count];

            Paragraph[] arrayParagraph = new Paragraph[pustaka.Count];
            Button[] arrayButton = new Button[pustaka.Count];
            Button[] arrayButton2 = new Button[pustaka.Count];

            for (int i = 0; i < pustaka.Count; i++)
            {

                var namaPenulisRunSet = new Run();
                var tahunTerbitRunSet = new Run();
                var artikelRunSet = new Run();
                var judulBukuRunSet = new Run();
                var edisiBukuRunSet = new Run();
                var nomorJurnalRunSet = new Run();
                var bulanTerbitRunSet = new Run();
                var halamanRunSet = new Run();
                var tempatTerbitRunSet = new Run();
                var namaPenerbitRunSet = new Run();
                var platformPenerbitRunSet = new Run();
                var linkRunSet = new Run();

                namaPenulisRun[i] = namaPenulisRunSet;
                tahunTerbitRun[i] = tahunTerbitRunSet;
                artikelRun[i] = artikelRunSet;
                judulBukuRun[i] = judulBukuRunSet;
                edisiBukuRun[i] = edisiBukuRunSet;
                nomorJurnalRun[i] = nomorJurnalRunSet;
                bulanTerbitRun[i] = bulanTerbitRunSet;
                halamanRun[i] = halamanRunSet;
                tempatTerbitRun[i] = tempatTerbitRunSet;
                platformPenerbitRun[i] = platformPenerbitRunSet;
                namaPenerbitRun[i] = namaPenerbitRunSet;

                linkRun[i] = linkRunSet;

                namaPenulisRunSet.Text = tentukanPustaka(pustaka2[i].namaPenulis, "titik");
                tahunTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tahunTerbit, "titik");
                artikelRunSet.Text = tentukanPustaka(pustaka2[i].artikel, "artikel");
                judulBukuRunSet.Text = tentukanPustaka(pustaka2[i].judulBuku, (name == "jurnal" ? "spasi" : "titik"));
                edisiBukuRunSet.Text = tentukanPustaka(pustaka2[i].edisiBuku, (name == "jurnal" || name == "tesis" ? "koma" : "titik"));
                nomorJurnalRunSet.Text = tentukanPustaka(pustaka2[i].nomorJurnal, "nomorJurnal");
                bulanTerbitRunSet.Text = tentukanPustaka(pustaka2[i].bulanTerbit, "colonBulan");
                halamanRunSet.Text = tentukanPustaka(pustaka2[i].halaman, "titik");
                tempatTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tempatTerbit, (name == "tesis" ? "koma" : "colon"));
                namaPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].namaPenerbit, (name == "ebooks" || name == "jurnal" ? "titik" : (name == "tesis" ? "spasi" : "titikStop")));
                platformPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].platformPenerbit, (name == "tesis" ? "kurung" : "titikStop"));
                linkRunSet.Text = name == "jurnal" ? tentukanPustaka(pustaka2[i].link, "titikStop") : pustaka2[i].link;

                judulBukuRunSet.FontStyle = FontStyles.Italic;

                var tes = new Paragraph();
                arrayParagraph[i] = tes;
                tes.LineHeight = 2;

                tes.Inlines.Add(namaPenulisRun[i]);
                tes.Inlines.Add(tahunTerbitRun[i]);
                tes.Inlines.Add(artikelRun[i]);
                tes.Inlines.Add(judulBukuRun[i]);
                tes.Inlines.Add(edisiBukuRun[i]);
                tes.Inlines.Add(nomorJurnalRun[i]);
                tes.Inlines.Add(bulanTerbitRun[i]);
                tes.Inlines.Add(halamanRun[i]);
                tes.Inlines.Add(tempatTerbitRun[i]);
                tes.Inlines.Add(namaPenerbitRun[i]);
                tes.Inlines.Add(platformPenerbitRun[i]);
                tes.Inlines.Add(linkRun[i]);

                DaftarPustaka.Document.Blocks.Add(arrayParagraph[i]);

                Button newButton = new Button();
                var txt2 = new Button();
                arrayButton[i] = txt2;
                txt2.Content = "Hapus";
                txt2.Name = $"Btn{i}";
                ButtonHapusSatu.Children.Add(arrayButton[i]);
                txt2.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnSatu_Click));

                Button newButton2 = new Button();
                var txt4 = new Button();
                arrayButton2[i] = txt4;
                txt4.Content = "Edit";
                txt4.Name = $"Hps{i}";
                ButtonEditSatu.Children.Add(arrayButton2[i]);
                txt4.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnEdit_Click));
            }
        }

        private void HapusDaftarPustaka_Click(object sender, RoutedEventArgs e)
        {
            pustaka.Clear();
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();
            ButtonEditSatu.Children.Clear();
            HapusDaftarPustaka.Visibility = Visibility.Collapsed;
        }

        private void DaftarPustaka_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(new TextRange(DaftarPustaka.Document.ContentStart, DaftarPustaka.Document.ContentEnd).Text))
            {
                HapusDaftarPustaka.Visibility = Visibility.Collapsed;
            }
            else
            {
                HapusDaftarPustaka.Visibility = Visibility.Visible;
            }
        }

        private void ButtonContoh_Click(object sender, RoutedEventArgs e)
        {
            tambahPenulis.Clear();
            NextPenulis.Children.Clear();

            if (name == "tesis")
            {
                this.PustakaJudulBuku.Text = "";
                this.PustakaNomorJurnal.Text = "";
                this.PustakaBulanTerbit.Text = "";
                this.PustakaHalaman.Text = "";

                NamaPenulis_TextBox.Text = "Benjamin J Beller";
                TahunTerbit_TextBox.Text = "2014";
                Artikel_TextBox.Text = "Fire History of the Peron Peninsula, Shark Bay, Western Australia Based on Remote Sensing Dendrochronology, and Anecdotal Evidence";
                EdisiBuku_TextBox.Text = "M.S. thesis";
                TempatTerbit_TextBox.Text = "University of Life";
                NamaPenerbit_TextBox.Text = "ProQuest Dissertations and Theses Global";
                PlatformPenerbit_TextBox.Text = "1564281";
                Link_TextBox.Text = "http://proquest/928578237";
            }

            else if (name == "jurnal")
            {

                this.PustakaTempatTerbit.Text = "";
                this.PustakaNamaPenerbit.Text = "";
                this.PustakaPlatformPenerbit.Text = "";

                NamaPenulis_TextBox.Text ="X. J. Meng";

                tambahPenulis.Add("B. Wiseman");
                tambahPenulis.Add("F. Elvinger");
                tambahPenulis.Add("D. K. Guenette");
                tambahPenulis.Add("T. E. Toth");
                tambahPenulis.Add("R. E. Engle");
                tambahPenulis.Add("S. U. Emerson");
                tambahPenulis.Add("R. H. Purcell");

                TextBlock[] textBlockTambah = new TextBlock[tambahPenulis.Count];
                TextBox[] textBoxTambah = new TextBox[tambahPenulis.Count];
                Button[] buttonHapus = new Button[tambahPenulis.Count];

                for (int i = 0; i < tambahPenulis.Count; i++)
                {
                    var textBlockTambahSet = new TextBlock();
                    textBlockTambah[i] = textBlockTambahSet;
                    textBlockTambahSet.Text = $"Nama Penulis {i + 2}";
                    textBlockTambahSet.Name = $"NamaPenulis{i}";
                    textBlockTambahSet.FontWeight = FontWeights.Bold;
                    Thickness margin = textBlockTambahSet.Margin;
                    margin.Top = 10;
                    textBlockTambahSet.Margin = margin;
                    NextPenulis.Children.Add(textBlockTambah[i]);

                    var textBoxTambahSet = new TextBox();
                    textBoxTambah[i] = textBoxTambahSet;
                    textBoxTambahSet.Name = $"TambahPenulis{i}";
                    textBoxTambahSet.TextChanged += Update_TextChanged;
                    textBoxTambahSet.Text = tambahPenulis[i];
                    NextPenulis.Children.Add(textBoxTambah[i]);

                    var buttonHapusSet = new Button();
                    buttonHapus[i] = buttonHapusSet;
                    buttonHapusSet.Content = "Hapus";
                    buttonHapusSet.Name = $"HapusPenulis{i}";
                    buttonHapusSet.Margin = margin;
                    NextPenulis.Children.Add(buttonHapus[i]);
                    buttonHapusSet.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonHapusPenulis_Click));
                }

                TahunTerbit_TextBox.Text = "2002";
                Artikel_TextBox.Text = "Prevalence of Antibodies to Hepatitis E Virus in Veterinarians Working with Swine and in Normal Blood Donors in the United States and Other Countries";
                JudulBuku_TextBox.Text = "Journal of Clinical Microbiology";
                EdisiBuku_TextBox.Text = "40";
                NomorJurnal_TextBox.Text = "1";
                BulanTerbit_TextBox.Text = "January";
                Halaman_TextBox.Text = "117-122";
                Link_TextBox.Text = "https://doi.org/10.1128/JCM.40.1.117-122.2002";
            }

            else if (name == "ebooks")
            {
                this.PustakaArtikel.Text = "";
                this.PustakaNomorJurnal.Text = "";
                this.PustakaBulanTerbit.Text = "";
                this.PustakaHalaman.Text = "";
                this.PustakaLink.Text = "";

                NamaPenulis_TextBox.Text = "Len Bass";

                tambahPenulis.Add("Paul Clements");
                tambahPenulis.Add("Rick Kazman");

                TextBlock[] textBlockTambah = new TextBlock[tambahPenulis.Count];
                TextBox[] textBoxTambah = new TextBox[tambahPenulis.Count];
                Button[] buttonHapus = new Button[tambahPenulis.Count];

                for (int i = 0; i < tambahPenulis.Count; i++)
                {
                    var textBlockTambahSet = new TextBlock();
                    textBlockTambah[i] = textBlockTambahSet;
                    textBlockTambahSet.Text = $"Nama Penulis {i + 2}";
                    textBlockTambahSet.Name = $"NamaPenulis{i}";
                    textBlockTambahSet.FontWeight = FontWeights.Bold;
                    Thickness margin = textBlockTambahSet.Margin;
                    margin.Top = 10;
                    textBlockTambahSet.Margin = margin;
                    NextPenulis.Children.Add(textBlockTambah[i]);

                    var textBoxTambahSet = new TextBox();
                    textBoxTambah[i] = textBoxTambahSet;
                    textBoxTambahSet.Name = $"TambahPenulis{i}";
                    textBoxTambahSet.TextChanged += Update_TextChanged;
                    textBoxTambahSet.Text = tambahPenulis[i];
                    NextPenulis.Children.Add(textBoxTambah[i]);

                    var buttonHapusSet = new Button();
                    buttonHapus[i] = buttonHapusSet;
                    buttonHapusSet.Content = "Hapus";
                    buttonHapusSet.Name = $"HapusPenulis{i}";
                    buttonHapusSet.Margin = margin;
                    NextPenulis.Children.Add(buttonHapus[i]);
                    buttonHapusSet.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonHapusPenulis_Click));
                }

                TahunTerbit_TextBox.Text = "2003";
                JudulBuku_TextBox.Text = "Software Architecture in Practice";
                EdisiBuku_TextBox.Text = "2nd ed";
                TempatTerbit_TextBox.Text = "Reading, MA";
                NamaPenerbit_TextBox.Text = "Addison Wesley";
                PlatformPenerbit_TextBox.Text = "Safari e-book";
            }

            else if (name == "buku")
            {
                this.PustakaArtikel.Text = "";
                this.PustakaNomorJurnal.Text = "";
                this.PustakaBulanTerbit.Text = "";
                this.PustakaHalaman.Text = "";
                this.PustakaLink.Text = "";
                this.PustakaPlatformPenerbit.Text = "";

                NamaPenulis_TextBox.Text = "F. W. Nicholas";
                TahunTerbit_TextBox.Text = "2010";
                JudulBuku_TextBox.Text = "Introduction to Veterinary Genetics";
                EdisiBuku_TextBox.Text = "3rd ed";
                TempatTerbit_TextBox.Text = "Oxford";
                NamaPenerbit_TextBox.Text = "Wiley-Blackwell";
            }
        }
    }
}