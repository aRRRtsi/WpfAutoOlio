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
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace WpfAutoOlio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Autot
        Car Auto1 = new Car();
        Car Auto2 = new Car();

        // Ajastin
        public DispatcherTimer AutonKiihdytin = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            // Vaihteisto
            cbVaihteisto.Items.Add("Automaatti");
            cbVaihteisto.Items.Add("Manuaali");
            cbVaihteisto.Items.Add("Robotti");
            cbVaihteisto2.ItemsSource = cbVaihteisto.Items;

            // Ajastin
            AutonKiihdytin.Tick += AutonKiihdytin_Tick; // Kutsutaan ajastimen tick rutiinia
            AutonKiihdytin.Interval = TimeSpan.FromMilliseconds(500); // 0,5 sekunnin välein
        }

        // AutonKiihdytin_Tick
        private void AutonKiihdytin_Tick(object sender, EventArgs e)
        {
            if (Auto1.Nopeus < Auto1.haeHuippunopeus())
            {
                btnKiihdyta.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            } else
            {
                AutonKiihdytin.Stop();
            }
        }

        // Auto 1
        private void BtnNaytaAutonTiedot_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Auton yksi tiedot");
            Auto1.Merkki = txtMerkki.Text;
            Auto1.Malli = txtMalli.Text;
            Auto1.Vari = txtVari.Text;
            Auto1.Vaihteisto = cbVaihteisto.Text;

            if (IsNumeric(txtHevosvoimat.Text) && (txtHevosvoimat.Text != ""))
            {
                Auto1.Hevosvoimat = int.Parse(txtHevosvoimat.Text);
            }
            else
            {
                MessageBox.Show("Syötä hevosvoimat numeroina!");
                txtHevosvoimat.Text = "";
                txtHevosvoimat.Focus();
            }
            
            if (IsAllDigits(txtHuippunopeus.Text) && (txtHuippunopeus.Text != ""))
            {
                try
                {
                    Auto1.asetaHuippunopeus(int.Parse(txtHuippunopeus.Text));
                    NopeusMittari.MaxValue = Auto1.haeHuippunopeus();
                }
                catch (Exception)
                {
                    MessageBox.Show("Syötä huippunopeus 1-400km/h väliltä!");
                    txtHuippunopeus.Focus();
                }
                
            } else
            {
                MessageBox.Show("Syötä huippunopeus numeroina!");
                txtHuippunopeus.Text = "";
                txtHuippunopeus.Focus();
            }

            try
            {
                Auto1.VaihteidenMaara = int.Parse(txtVaihteidenMaara.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Syötä vaihteiden määrä numeroina välillä 4-9!");
                txtVaihteidenMaara.Text = "";
                txtVaihteidenMaara.Focus();
            }
            
            Auto1.Nopeus = int.Parse(txtNopeus.Text);

            ShowCarInfo(Auto1);
        }

        // Auto 2
        private void BtnNaytaAutonTiedot2_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Auton kaksi tiedot");
            Auto2.Merkki = txtMerkki2.Text;
            Auto2.Malli = txtMalli2.Text;
            Auto2.Vari = txtVari2.Text;
            Auto2.Vaihteisto = cbVaihteisto2.Text;

            Auto2.Hevosvoimat = int.Parse(txtHevosvoimat2.Text);

            try
            {
                Auto2.Huippunopeus = int.Parse(txtHuippunopeus2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Syötä huippunopeus numeroina 1-400km/h väliltä!");
                txtHuippunopeus2.Focus();
            }

            try
            {
                Auto2.VaihteidenMaara = int.Parse(txtVaihteidenMaara2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Syötä vaihteiden määrä numeroina välillä 4-9!");
                txtVaihteidenMaara2.Text = "";
                txtVaihteidenMaara2.Focus();
            }

            Auto2.Nopeus = int.Parse(txtNopeus2.Text);

            ShowCarInfo(Auto2);
        }

        // Näytä auton tiedot
        public void ShowCarInfo(Car auto)
        {
            string Message =    "Merkki: " + auto.Merkki + "\r\n" +
                                "Malli: " + auto.Malli + "\r\n" +
                                "Väri: " + auto.Vari + "\r\n" +
                                "Vaihteisto: " + auto.Vaihteisto + "\r\n" +
                                "Hevosvoimat: " + auto.Hevosvoimat.ToString() + " HP" + "\r\n" +
                                "Huippunopeus: " + auto.haeHuippunopeus().ToString() + " KM/H" + "\r\n" +
                                "Vaihteet: " + auto.VaihteidenMaara.ToString() + "\r\n" +
                                "Nopeus: " + auto.Nopeus.ToString() + " KM/H" + "\r\n" +
                                "Moottori käynnissä?: " + auto.Kaynnissa + "\r\n";

            MessageBox.Show(Message);
            //MessageBox.Show(auto.ToString());
        }

        // Auton käynnistys
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnStart)
            {
                Auto1.KaynnistaMoottori();
                if (Auto1.Kaynnissa == true)
                {
                    btnIndicator.Content = "R";
                    btnIndicator.Background = Brushes.Green;
                }
            } else if (sender == btnStart2)
            {
                Auto2.KaynnistaMoottori();
                if (Auto2.Kaynnissa == true)
                {
                    btnIndicator2.Content = "R";
                    btnIndicator2.Background = Brushes.Green;
                }
            }
        }

        // Auton sammutus
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnStop)
            {
                Auto1.SammutaMoottori();
                if (Auto1.Kaynnissa == false)
                {
                    btnIndicator.Content = "";
                    btnIndicator.Background = Brushes.Red;
                }
            } else if (sender == btnStop2)
            {
                Auto2.SammutaMoottori();
                if (Auto2.Kaynnissa == false)
                {
                    btnIndicator2.Content = "";
                    btnIndicator2.Background = Brushes.Red;
                }
            }
        }

        // Kiihdytys
        private void BtnKiihdyta_Click(object sender, RoutedEventArgs e)
        {
            if (!AutonKiihdytin.IsEnabled)
            {
                AutonKiihdytin.Start();
            }
            Auto1.Kiihdyta();
            txtNopeus.Text = Auto1.Nopeus.ToString();
            NopeusMittari.CurrentValue = Auto1.Nopeus;
        }

        private void BtnJarruta_Click(object sender, RoutedEventArgs e)
        {
            AutonKiihdytin.Stop();
            Auto1.Jarruta();
            txtNopeus.Text = Auto1.Nopeus.ToString();
            NopeusMittari.CurrentValue = Auto1.Nopeus;
        }

        private void BtnKiihdyta2_Click(object sender, RoutedEventArgs e)
        {
            Auto2.Kiihdyta();
            txtNopeus2.Text = Auto2.Nopeus.ToString();
        }

        private void BtnJarruta2_Click(object sender, RoutedEventArgs e)
        {
            Auto2.Jarruta();
            txtNopeus2.Text = Auto2.Nopeus.ToString();
        }

        // Tarkistukset
        public static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, "^[0-9]+$");
        }

        // Tyhjennys
        private void BtnTyhjennys_Click(object sender, RoutedEventArgs e)
        {
            // Voit käyttää myös txtEsim.Clear();
            txtMerkki.Text = "";
            txtMalli.Text = "";
            txtVari.Text = "";
            cbVaihteisto.Text = "";
            txtHevosvoimat.Text = "";
            txtHuippunopeus.Text = "";
            txtVaihteidenMaara.Text = "";
            txtNopeus.Text = "0";

            Auto1.Merkki = "";
            Auto1.Malli = "";
            Auto1.Vari = "";
            Auto1.Vaihteisto = "";
            Auto1.Hevosvoimat = 0;
            Auto1.asetaHuippunopeus(0);
            Auto1.VaihteidenMaara = 0;
            Auto1.Nopeus = 0;
        }
    }
}
