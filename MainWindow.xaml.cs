using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
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
using static System.Net.Mime.MediaTypeNames;

namespace RGB
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

        private void KeverCsuszka(double rVal, double gVal, double bVal)
        {
            byte R = Convert.ToByte(rVal);
            byte G = Convert.ToByte(gVal);
            byte B = Convert.ToByte(bVal);
            eredmeny.Fill = new SolidColorBrush(Color.FromRgb(R, G, B));
            txtPiros.Content = R.ToString();
            txtZold.Content = G.ToString();
            txtKek.Content = B.ToString();
            eredmenyRGB.Content = $"rgb({R}, {G}, {B})";

            // google segítség
            string r16 = R.ToString("X");
            string g16 = G.ToString("X");
            string b16 = B.ToString("X");
            if (r16.Length == 1)
            {
                r16 = "0" + r16;
            }
            if (g16.Length == 1)
            {
                g16 = "0" + g16;
            }

            if (b16.Length == 1)
            {
                b16 = "0" + b16;
            }
            eredmenyHex.Content = $"#{r16}{g16}{b16}";
        }


        private void sliPiros_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            KeverCsuszka(sliPiros.Value, sliZold.Value, sliKek.Value);
        }


        private void sliZold_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            KeverCsuszka(sliPiros.Value, sliZold.Value, sliKek.Value);
        }


        private void sliKek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            KeverCsuszka(sliPiros.Value, sliZold.Value, sliKek.Value);
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            String szinAdatok = $"{Convert.ToByte(sliPiros.Value)}; {Convert.ToByte(sliZold.Value)}; {Convert.ToByte(sliKek.Value)}";
            if (lbSzinek.Items.Contains(szinAdatok))
            {
                MessageBox.Show("Ez a színkombináció már szerepel a listában!");
            }
            else
            {
                lbSzinek.Items.Add(szinAdatok);
            }
        }

        private void btnTorol_Click(object sender, RoutedEventArgs e)
        {
            if (lbSzinek.SelectedIndex >= 0)
            {
                lbSzinek.Items.RemoveAt(lbSzinek.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Válassz ki egy elemet a törléshez!");
            }
        }

        private void btnUrit_Click(object sender, RoutedEventArgs e)
        {
            lbSzinek.Items.Clear();
        }

        private void lbSzinek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSzinek.SelectedItem != null)
            {
                string[] tagok = lbSzinek.SelectedItem.ToString().Split(';');
                KeverCsuszka(double.Parse(tagok[0]), double.Parse(tagok[1]), double.Parse(tagok[2]));
                sliPiros.Value = byte.Parse(tagok[0]);
                sliZold.Value = byte.Parse(tagok[1]);
                sliKek.Value = byte.Parse(tagok[2]);
            }
        }

        private void eredmenyHex_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // google segítség
            Clipboard.SetText(eredmenyHex.Content.ToString());
        }

        private void eredmenyRGB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // google segítség
            Clipboard.SetText(eredmenyRGB.Content.ToString());
        }


        private void eredmenyRGB_MouseEnter(object sender, MouseEventArgs e)
        {
            eredmenyRGB.Cursor = Cursors.Cross;
        }

        private void eredmenyHex_MouseEnter(object sender, MouseEventArgs e)
        {
            eredmenyHex.Cursor = Cursors.Cross;
        }
    }
}
