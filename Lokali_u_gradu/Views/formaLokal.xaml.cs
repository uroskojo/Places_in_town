using Lokali_u_gradu.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Threading;
using static System.Resources.ResXFileRef;

namespace Lokali_u_gradu.Views
{
    /// <summary>
    /// Interaction logic for formaLokal.xaml
    /// </summary>
    public partial class formaLokal : System.Windows.Controls.UserControl
    {
        public Boolean[] flag = new Boolean[3];
        string datum;
        public bool zaIzmenu;
        public int tipID;
        public string putanjaIkonice;
        public string putanjaIkoniceTip;

        TipLokala tempTip; // za demo mod
        Lokal tempLok;
        DispatcherTimer tajmer;
        int i; // iterator kroz polja u demo modu

        //flag[0] je txtIme
        //flag[1] je txtOznaka
        //flag[2] je txtKapac


        public static List<String> statusAlc { get; set; }
        public static List<String> cene { get; set; }
        public static ObservableCollection<Etiketa> etikete { get; set; }
        public static List<string> tipoviLokala { get; set; }
    
        public formaLokal()
        {
            i = 0;
            tajmer = new DispatcherTimer();
            tajmer.Tick += new EventHandler(tajmer_Tick);

            InitializeComponent();

            etikete = new ObservableCollection<Etiketa>();
            statusAlc = new List<String>()
            {  "Ne služi se", "Služi se do 23h", "Služi se i kasno noću"  };
           
            cene = new List<String>()
            { "Niske cene", "Srednje cene", "Visoke cene", "Izuzetno visoke cene" };

            
            tipoviLokala = new List<string>();
            
            for (int i=0; i < MainWindow.instance.tipoviLokala.Count; i++)
             {
                 tipoviLokala.Add(MainWindow.instance.tipoviLokala[i].Ime);
             }
            for (int i = 0; i < MainWindow.instance.etikete.Count; i++)
            {
                listaEtiketa.Items.Add(MainWindow.instance.etikete[i].Opis);
             
            }   
        }


        private void btnIkonica_Click(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|PNG Files (*.png)|*.png|*.jpeg";
            //|GIF Files (*.gif)|*.gif
            
            dlg.ShowDialog();
            putanjaIkonice = dlg.FileName;

            if (putanjaIkonice != null)
                MainWindow.instance.postaviSliku(putanjaIkonice, ico);
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;

            bool postojiLokal = false;
            int indexTipa = -1;

            string tip;

            string alc;
            string cene;
            int id, idTipa, kapac;


            if ((flag[0] && flag[1] && flag[2]) || zaIzmenu)
            {

                bool c = (bool)checkCigare.IsChecked;
                bool hendi = (bool)checkHendikep.IsChecked;
                bool rez = (bool)checkRezerv.IsChecked;

                id = int.Parse(txtOznaka.Text);
                kapac = int.Parse(txtKapacitet.Text);

                if (comboAlc.SelectedValue != null)
                    alc = comboAlc.SelectedValue.ToString();
                else
                    alc = "";

                if (comboCene.SelectedValue != null)
                    cene = comboCene.SelectedValue.ToString();
                else
                    cene = "";

                if (comboTipovi.SelectedValue != null)
                {
                    tip = comboTipovi.SelectedValue.ToString();
                    // vidim koji je tip selektovan i uzmem njegov index
                    for (int k = 0; k < MainWindow.instance.tipoviLokala.Count; k++)
                    {
                        if (MainWindow.instance.tipoviLokala[k].Ime.Equals(tip))
                        {
                            indexTipa = k;
                            break;
                        }
                    }

                }
                else
                {
                    TipWarning.FontSize = 15;
                    MainWindow.instance.changeText(TipWarning, "Mora se izabrati tip lokala!");
                    return;
                }


                idTipa = MainWindow.instance.tipoviLokala[indexTipa].ID;    // uzmem ID selektovanog tipa

                if (putanjaIkonice == null && MainWindow.instance.tipoviLokala[indexTipa].Ikonica == null)
                {
                    return;
                }
                else if (putanjaIkonice == null && MainWindow.instance.tipoviLokala[indexTipa].Ikonica != null)
                {
                    putanjaIkonice = MainWindow.instance.tipoviLokala[indexTipa].Ikonica;

                }

                Lokal lokal = new Lokal(id, idTipa, txtIme.Text, txtOpis.Text, rez, datum, kapac, c, hendi, alc, cene, putanjaIkonice, tip);
               
                List<int> tempList = new List<int>(); 
                MainWindow.instance.id_etiketa.TryGetValue(lokal.ID, out tempList);
               
                if (tempList == null)
                    tempList = new List<int>();
                else
                    lokal.ID_ETIKETA = tempList;

                if (zaIzmenu)
                    lokal.etikete = etikete;

                foreach (string opis in listaEtiketa.SelectedItems)
                {
                    for (int i = 0; i < MainWindow.instance.etikete.Count; i++)
                    {
                        if (MainWindow.instance.etikete[i].Opis.Equals(opis))
                        {
                         
                                if (lokal.etikete.Contains(MainWindow.instance.etikete[i]))
                                    continue;
                                else
                                {
                                    lokal.ID_ETIKETA.Add(MainWindow.instance.etikete[i].ID);
                                    lokal.etikete.Add(MainWindow.instance.etikete[i]);      
                                    MainWindow.instance.sacuvaj_u_fajl_Etiketu(MainWindow.instance.etikete);
                                 
                                }
                        }
                           
                    }
                }
                
                //ako vec postoji, ne moze se opet dodati
                for (int i = 0; i < MainWindow.instance.lokali.Count; i++)
                {
                    if (zaIzmenu)
                    {
                        if (MainWindow.instance.lokali[i].ID == lokal.ID && MainWindow.instance.lokali[i].OznakaTipa != idTipa)
                        {
                            // ako postoji i ako je IDtipa razlicit od ovog sada promenjenog ID tipa treba da iz starog tipa izbrisem lokal

                            postojiLokal = true;

                            for (int k = 0; k < MainWindow.instance.tipoviLokala.Count; k++)
                            {
                                if (MainWindow.instance.lokali[i].OznakaTipa == MainWindow.instance.tipoviLokala[k].ID)
                                {
                                    MainWindow.instance.tipoviLokala[k].lokali.Remove(MainWindow.instance.lokali[i]);
                                    //nakon brisanja lokala ako taj tip ne poseduje vise lokala izbrisati ga
                                    if (MainWindow.instance.tipoviLokala[k].lokali.Count == 0)
                                    {
                                        if (k < indexTipa)//ako je k index pre indexaTipa onda se indexTipa pomera za mesto u levo
                                            indexTipa--;
                                        MainWindow.instance.tipoviLokala.Remove(MainWindow.instance.tipoviLokala[k]);
                                    }

                                    break;
                                }
                            }
                            MainWindow.instance.lokali[i] = lokal;
                            MainWindow.instance.tipoviLokala[indexTipa].lokali.Add(lokal);
                            MainWindow.instance.sacuvaj_u_fajl_Tip(MainWindow.instance.tipoviLokala);
                            MainWindow.instance.sacuvaj_u_fajl_Lokal(MainWindow.instance.lokali);
                            break;

                        }else if(MainWindow.instance.lokali[i].ID == lokal.ID)
                        {
                            if(MainWindow.instance.tipoviLokala[indexTipa].lokali.Contains(MainWindow.instance.lokali[i]))
                            {
                               int idx =  MainWindow.instance.tipoviLokala[indexTipa].lokali.IndexOf(MainWindow.instance.lokali[i]);
                               MainWindow.instance.tipoviLokala[indexTipa].lokali[idx] = lokal;
                            }

                            MainWindow.instance.lokali[i] = lokal;
                            MainWindow.instance.sacuvaj_u_fajl_Tip(MainWindow.instance.tipoviLokala);
                            MainWindow.instance.sacuvaj_u_fajl_Lokal(MainWindow.instance.lokali);
                        }

                    }
                    else
                    {
                        if (MainWindow.instance.lokali[i].ID == lokal.ID)
                        {
                            postojiLokal = true;
                            MainWindow.instance.changeText(OznakaWarning, "Već postoji lokal s unetim ID-jem!");
                            return;
                        }
                    }
                }

                if (!postojiLokal && zaIzmenu == false)//ako ne postoji i nije za izmenu
                {
                    MainWindow.instance.lokali.Add(lokal);
                    MainWindow.instance.tipoviLokala[indexTipa].lokali.Add(lokal);
                    MainWindow.instance.sacuvaj_u_fajl_Lokal(MainWindow.instance.lokali);
                }

                zaIzmenu = false;
                MainWindow.instance.DataContext = new tabelaLokala();
            }
        }
        
        

        private void txtIme_LostFocus(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;

            flag[0] = true;
            if (String.IsNullOrEmpty(txtIme.Text))
            {
                flag[0] = false;
                txtIme.Background = Brushes.LightPink;
            }

            if (txtIme.Text.Any(c => Char.IsNumber(c)))
            {
                flag[0] = false;
                MainWindow.instance.changeText(imeWarning, "Ne smeju se unositi brojevi!");
                txtIme.Background = Brushes.LightPink;
               
            }

        }

        private void txtOznaka_LostFocus(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;

            flag[1] = true;
            if(String.IsNullOrEmpty(txtOznaka.Text))
            {
                flag[1] = false;
                txtOznaka.Background = Brushes.LightPink;
            }
            if (txtOznaka.Text.Any(c => char.IsLetter(c)))
            {
                flag[1] = false;
                MainWindow.instance.changeText(OznakaWarning, "Ne smeju se unositi slova!");

            }

        }

        private void txtKapacitet_LostFocus(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;

            flag[2] = true;
            if (txtKapacitet.Text.Any(c => char.IsLetter(c)))
            {
                flag[2] = false;
                MainWindow.instance.changeText(kapacitetWarning, "Ne smeju se unositi slova!");
               
            }

            if(String.IsNullOrEmpty(txtKapacitet.Text))
            {
                flag[2] = false;
                txtKapacitet.Background = Brushes.LightPink;
                
            }
        }

        public Boolean demoAktivan()
        {
            ////////////////////////////////////////////////////
            if (lblDemo.Content.Equals("DEMO MOD AKTIVIRAN"))
            {
                tajmer.Stop();
                i = 0;

                var converter = new System.Windows.Media.BrushConverter();
                lblDemo.Foreground = Brushes.Blue;
                lblDemo.FontSize = 18;
                lblDemo.Content = "Demo";
                lblDemo.ToolTip = "Klikni da aktiviraš demo mod";
                lblDemoStop.Visibility = Visibility.Hidden;
                this.Background = (Brush)converter.ConvertFromString("White");
                resetujFormu();
                MainWindow.instance.DataContext = this;
                return true;
                /////////////////////////////////////////////
            }
            return false;
        }


        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;
            MainWindow.instance.DataContext = new tabelaLokala();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
          
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.datum = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.datum = date.Value.ToShortDateString();
            }
        }
       
        private void lblDemo_MouseLeftClick(object sender, MouseButtonEventArgs e)
        {// DEMO MOD
            var converter = new System.Windows.Media.BrushConverter();
            lblDemo.Foreground = (Brush)converter.ConvertFromString("#53C617");
            lblDemo.FontSize = 23;
            lblDemo.ToolTip = null;
            lblDemo.Content = "DEMO MOD AKTIVIRAN";
            lblDemoStop.Visibility = Visibility.Visible;
            this.Background = (Brush)converter.ConvertFromString("#D6C9C9");
            ico.Visibility = Visibility.Hidden; //sakrijem ikonicu
            tajmer.Interval = TimeSpan.FromSeconds(0.5);
            tajmer.Start();
             
        }

        private void tajmer_Tick(object sender, EventArgs e)
        {
            string[] sadrzaj = new string[7] { "Gallery", "1111", "80", "Neki opis lokala", "Ne služi se", "Niske cene", "Kafic" };
            string datum = "5/8/2018";
            string putanjaIkonice = "D:\\6. semestar- VEZBANJE\\HCI\\ProjekatPonovo\\Lokali_u_gradu\\Lokali_u_gradu\\Resursi\\lokali\\DSC_0028a-1-1000x500.jpg";
            int idTipa = 100;
            bool hendi = false;
            bool cigarete = true;
            bool rezerv = true;
            

            if (i == 7)
            {
                MainWindow.instance.lokali.Remove(tempLok);

                if(MainWindow.instance.tipoviLokala.Contains(tempTip))
                {
                    int idx = MainWindow.instance.tipoviLokala.IndexOf(tempTip);

                    if(MainWindow.instance.tipoviLokala[idx].lokali.Contains(tempLok))
                       MainWindow.instance.tipoviLokala[idx].lokali.Remove(tempLok);
                }

                i = 0;
                resetujFormu();           
                MainWindow.instance.DataContext = this;
                return;
            }
                
            if(txtIme.Text.Equals(""))
            {
                txtIme.Text = sadrzaj[i];
                i++;
                return;
            }
                       
            if(txtOznaka.Text.Equals(""))
            {
                txtOznaka.Text = sadrzaj[i];
                i++;

                return;
            }
            
            if(txtKapacitet.Text.Equals(""))
            {
                txtKapacitet.Text = sadrzaj[i];
                i++;
                return;
            }
            
            if(txtOpis.Text.Equals(""))
            {
                txtOpis.Text = sadrzaj[i];
                i++;
                return;
            }

            if (comboAlc.SelectedValue == null)
            {
                comboAlc.SelectedValue = sadrzaj[i];
                i++;
                return;
            }

            if (comboCene.SelectedValue == null)
            {
                comboCene.SelectedValue = sadrzaj[i];
                i++;
                return;
            }

            if (comboTipovi.SelectedValue == null)
            {
                comboTipovi.SelectedValue = sadrzaj[i];// i = 6
                return;
            }

            if(checkRezerv.IsChecked == false)
            {
                checkRezerv.IsChecked = rezerv;
                return;
            }

            if(checkCigare.IsChecked == false)
            {
                checkCigare.IsChecked = cigarete;
                return;
            }

            if (DatumIzbor.SelectedDate == null)
            {
                DatumIzbor.SelectedDate = DateTime.Parse(datum);
                return;
            }
            if(ico.Visibility == Visibility.Hidden)
            {
                MainWindow.instance.postaviSliku(putanjaIkonice, ico);
                ico.Visibility = Visibility.Visible;
                return;
            }
               
            tempLok = new Lokal(int.Parse(sadrzaj[1]) , idTipa, txtIme.Text, txtOpis.Text, rezerv, datum, int.Parse(sadrzaj[2]), cigarete, hendi, sadrzaj[4], sadrzaj[5], putanjaIkonice, sadrzaj[6]);

            MainWindow.instance.lokali.Add(tempLok); 
            
            for(int i=0; i<MainWindow.instance.tipoviLokala.Count; i++)
            {
                if (MainWindow.instance.tipoviLokala[i].ID == idTipa)
                {
                    MainWindow.instance.tipoviLokala[i].lokali.Add(tempLok);
                    tempTip = MainWindow.instance.tipoviLokala[i];
                }
            }

            MainWindow.instance.DataContext = new tabelaLokala();
            
        }

        private void lblDemoStop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tajmer.Stop();
            i = 0;

            var converter = new System.Windows.Media.BrushConverter();
            lblDemo.Foreground = Brushes.Blue;
            lblDemo.FontSize = 18;
            lblDemo.Content = "Demo";
            lblDemo.ToolTip = "Klikni da aktiviraš demo mod";
            lblDemoStop.Visibility = Visibility.Hidden;
            this.Background = (Brush)converter.ConvertFromString("White");
            resetujFormu();
            MainWindow.instance.DataContext = this;
        }

        private void resetujFormu()
        {
            txtIme.Text = "";
            txtOznaka.Text = "";
            txtKapacitet.Text = "";
            txtOpis.Text = "";
            comboAlc.SelectedValue = null;
            comboCene.SelectedValue = null;
            comboTipovi.SelectedValue = null;
            checkRezerv.IsChecked = false;
            checkCigare.IsChecked = false;
            DatumIzbor.SelectedDate = null;
            ico.Visibility = Visibility.Hidden;
            
        }

        private void checkRezerv_Click(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;
        }

        private void checkHendikep_Click(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;
        }

        private void checkCigare_Click(object sender, RoutedEventArgs e)
        {
            if (demoAktivan())
                return;
        }

    }
}
