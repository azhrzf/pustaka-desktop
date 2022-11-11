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

        private void Update_TextChanged(object sender, TextChangedEventArgs e)
        {
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

                if (!String.IsNullOrEmpty(this.NamaPenulis2_TextBox.Text) && !String.IsNullOrEmpty(this.NamaPenulis3_TextBox.Text) && !String.IsNullOrEmpty(this.NamaPenulis4_TextBox.Text))
                {
                    return $"{inisialNama} dkk";
                }
                else if (!String.IsNullOrEmpty(this.NamaPenulis2_TextBox.Text) && !String.IsNullOrEmpty(this.NamaPenulis3_TextBox.Text))
                {
                    return $"{inisialNama}. {this.NamaPenulis2_TextBox.Text}., dan {this.NamaPenulis3_TextBox.Text}";
                }
                else if (!String.IsNullOrEmpty(this.NamaPenulis2_TextBox.Text)) {
                    return $"{inisialNama} dan {this.NamaPenulis2_TextBox.Text}";
                }

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
                else
                {
                    return "";
                }
            }

            if (String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(this.TahunTerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.JudulBuku_TextBox.Text) || String.IsNullOrEmpty(this.NamaPenerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.TempatTerbit_TextBox.Text))
            {
                PustakaSemua.Foreground = Brushes.Red;
            }
            else
            {
                PustakaSemua.Foreground = Brushes.Black;
                ButtonTambah.Visibility = Visibility.Visible;
            }

            this.PustakaNamaPenulis.Text = tentukanPustaka(inisialNama(this.NamaPenulis_TextBox.Text), "titik");
            this.PustakaTahunTerbit.Text = tentukanPustaka(this.TahunTerbit_TextBox.Text, "titik");
            this.PustakaJudulBuku.Text = tentukanPustaka(this.JudulBuku_TextBox.Text, "titik");
            this.PustakaEdisiBuku.Text = tentukanPustaka(this.EdisiBuku_TextBox.Text, "titik");
            this.PustakaTempatTerbit.Text = tentukanPustaka(this.TempatTerbit_TextBox.Text, "colon");
            this.PustakaNamaPenerbit.Text = tentukanPustaka(this.NamaPenerbit_TextBox.Text, "titikStop");
        }

        private void TambahPenulisButton1_Click(object sender, RoutedEventArgs e)
        {
            if (NamaPenulis_TextBox.Visibility == Visibility.Visible && NamaPenulis2_TextBox.Visibility == Visibility.Visible && NamaPenulis3_TextBox.Visibility == Visibility.Visible)
            {
                if (String.IsNullOrEmpty(NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(NamaPenulis2_TextBox.Text) || String.IsNullOrEmpty(NamaPenulis3_TextBox.Text))
                {
                    MessageBox.Show("Lengkapi Nama Penulis Terlebih Dahulu!");
                }
                else
                {
                    NamaPenulis2_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis2_TextBox.Visibility = Visibility.Visible;
                    HapusPenulis2_Button.Visibility = Visibility.Visible;

                    NamaPenulis3_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis3_TextBox.Visibility = Visibility.Visible;
                    HapusPenulis3_Button.Visibility = Visibility.Visible;

                    NamaPenulis4_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis4_TextBox.Visibility = Visibility.Visible;
                    HapusPenulis4_Button.Visibility = Visibility.Visible;

                    TambahPenulisButton1.Visibility = Visibility.Collapsed;
                }
            }
            else if (NamaPenulis2_TextBox.Visibility == Visibility.Visible)
            {
                if (String.IsNullOrEmpty(NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(NamaPenulis2_TextBox.Text))
                {
                    MessageBox.Show("Lengkapi Nama Penulis Terlebih Dahulu!");
                }
                else
                {
                    NamaPenulis2_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis2_TextBox.Visibility = Visibility.Visible;
                    HapusPenulis2_Button.Visibility = Visibility.Visible;

                    NamaPenulis3_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis3_TextBox.Visibility = Visibility.Visible;
                    HapusPenulis3_Button.Visibility = Visibility.Visible;

                    NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                    NamaPenulis4_TextBox.Visibility = Visibility.Collapsed;
                    HapusPenulis4_Button.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(NamaPenulis_TextBox.Text))
                {
                    MessageBox.Show("Lengkapi Nama Penulis Terlebih Dahulu!");
                }
                else
                {
                    NamaPenulis2_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis2_TextBox.Visibility = Visibility.Visible;
                    HapusPenulis2_Button.Visibility = Visibility.Visible;

                    NamaPenulis3_TextBlock.Visibility = Visibility.Collapsed;
                    NamaPenulis3_TextBox.Visibility = Visibility.Collapsed;
                    HapusPenulis3_Button.Visibility = Visibility.Collapsed;

                    NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                    NamaPenulis4_TextBox.Visibility = Visibility.Collapsed;
                    HapusPenulis4_Button.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void HapusPenulis2_Button_Click(object sender, RoutedEventArgs e)
        {
            if (NamaPenulis3_TextBlock.Visibility == Visibility.Visible && NamaPenulis4_TextBlock.Visibility == Visibility.Visible)
            {
                NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis4_TextBox.Visibility = Visibility.Collapsed;
                HapusPenulis4_Button.Visibility = Visibility.Collapsed;

                NamaPenulis2_TextBox.Text = NamaPenulis3_TextBox.Text;
                NamaPenulis3_TextBox.Text = NamaPenulis4_TextBox.Text;
                NamaPenulis4_TextBox.Text = null;
            }
            else if (NamaPenulis3_TextBlock.Visibility == Visibility.Visible && NamaPenulis4_TextBlock.Visibility == Visibility.Collapsed)
            {
                NamaPenulis3_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis3_TextBox.Visibility = Visibility.Collapsed;
                HapusPenulis3_Button.Visibility = Visibility.Collapsed;

                NamaPenulis2_TextBox.Text = NamaPenulis3_TextBox.Text;
                NamaPenulis3_TextBox.Text = null;
                NamaPenulis4_TextBox.Text = null;
            }
            else
            {
                NamaPenulis2_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis2_TextBox.Visibility = Visibility.Collapsed;
                HapusPenulis2_Button.Visibility = Visibility.Collapsed;
                
                NamaPenulis2_TextBox.Text = null;
            }
            TambahPenulisButton1.Visibility = Visibility.Visible;
        }

        private void HapusPenulis3_Button_Click(object sender, RoutedEventArgs e)
        {
            if (NamaPenulis4_TextBlock.Visibility == Visibility.Visible)
            {
                NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis4_TextBox.Visibility = Visibility.Collapsed;
                HapusPenulis4_Button.Visibility = Visibility.Collapsed;

                NamaPenulis3_TextBox.Text = NamaPenulis4_TextBox.Text;
                NamaPenulis4_TextBox.Text = null;
            }
            else
            {
                NamaPenulis3_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis3_TextBox.Visibility = Visibility.Collapsed;
                HapusPenulis3_Button.Visibility = Visibility.Collapsed;
                
                NamaPenulis3_TextBox.Text = null;
            }
            TambahPenulisButton1.Visibility = Visibility.Visible;
        }

        private void HapusPenulis4_Button_Click(object sender, RoutedEventArgs e)
        {
            NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
            NamaPenulis4_TextBox.Visibility = Visibility.Collapsed;
            HapusPenulis4_Button.Visibility = Visibility.Collapsed;

            NamaPenulis4_TextBox.Text = null;
            TambahPenulisButton1.Visibility = Visibility.Visible;
        }
    }
}
