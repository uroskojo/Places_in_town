using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Lokali_u_gradu.Views
{
    /// <summary>
    /// Interaction logic for tabelaLokala.xaml
    /// </summary>
    public partial class tabelaLokala : UserControl, INotifyPropertyChanged
    {
        
        public static tabelaLokala instance;
        public int cnt; //brojac ulazaka u pretragu
       

        public tabelaLokala()
        {
            InitializeComponent();
            this.DataContext = this;
            instance = this;///////////////
            Lokali = MainWindow.instance.lokali;
            cnt = 0;
            

        }
        public ObservableCollection<Lokal> pomocni //za pretragu
        {
            get;
            set;
        }
        public ObservableCollection<Lokal> Lokali
        {
            get;
            set;
        }
        public List<Lokal> tempLokali
        {
            get;
            set;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {/*
            MainWindow mw = sender as MainWindow;
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (mw is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)mw);
                HelpProvider.ShowHelpTabelaLok(str, this);
            }*/
        }

        private void btnDodajLokal_Click(object sender, RoutedEventArgs e)
        {
            formaLokal fl = new formaLokal();
            fl.lblDemo.Visibility = Visibility.Visible;
            MainWindow.instance.DataContext = fl;
            
        }

        private void btnIzmeniLokal_Click(object sender, RoutedEventArgs e)
        {

            if (tableGridLokali.SelectedItem == null)
                return;

            Lokal lokal = (Lokal)tableGridLokali.SelectedItem;
            formaLokal fl = new formaLokal();

            MainWindow.zaIzmenu = true;

            fl.txtIme.Text = lokal.Ime;
            fl.txtOznaka.Text = lokal.ID.ToString();
            fl.txtOznaka.IsEnabled = false;

            fl.txtOpis.Text = lokal.Opis;
            fl.txtKapacitet.Text = lokal.Kapacitet.ToString();
            fl.checkHendikep.IsChecked = lokal.Hendikep;
            fl.checkCigare.IsChecked = lokal.Pusenje;
            fl.checkRezerv.IsChecked = lokal.Rezervacije;

            if (lokal.Datum != null)
                fl.DatumIzbor.SelectedDate = DateTime.Parse(lokal.Datum);

            fl.putanjaIkonice = lokal.Ikonica;

            if (lokal.Cene != null)
                fl.comboCene.SelectedValue = lokal.Cene;

            if (lokal.Alc != null)
                fl.comboAlc.SelectedValue = lokal.Alc;

            if (lokal.Ikonica != null)
            {
                fl.putanjaIkoniceTip = lokal.Ikonica;
                MainWindow.instance.postaviSliku(fl.putanjaIkoniceTip, fl.ico);
            }

            for (int i = 0; i < MainWindow.instance.tipoviLokala.Count; i++)
            {
                if (MainWindow.instance.tipoviLokala[i].ID == lokal.OznakaTipa)
                {
                    fl.putanjaIkoniceTip = MainWindow.instance.tipoviLokala[i].Ikonica; //pamti ikonicu tipa
                    break;
                }

            }
            fl.zaIzmenu = true;
            fl.tipID = lokal.OznakaTipa;
            fl.comboTipovi.SelectedValue = lokal.Tip;

            formaLokal.etikete = lokal.etikete;

            MainWindow.instance.DataContext = fl;
        }

        private void btnObrisiLokal_Click(object sender, RoutedEventArgs e)
        {
            if (tableGridLokali.SelectedItems == null)
                return;

            MessageBoxResult dr = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);

            if (dr == MessageBoxResult.Yes)
            {

                List<Lokal> lokaliZaBrisanje = tableGridLokali.SelectedItems.Cast<Lokal>().ToList();

                for (int i = 0; i < MainWindow.instance.lokali.Count; i++)
                {
                    for (int j = 0; j < lokaliZaBrisanje.Count; j++)
                    {
                        if (MainWindow.instance.lokali[i].ID == lokaliZaBrisanje[j].ID)
                        {
                            if (MainWindow.instance.koo.ContainsKey(MainWindow.instance.lokali[i].ID))
                            {
                                MainWindow.instance.koo.Remove(MainWindow.instance.lokali[i].ID);
                                MainWindow.instance.sacuvaj_koordinate(MainWindow.instance.koo);
                            }

                            MainWindow.instance.lokali.Remove(MainWindow.instance.lokali[i]);
                        }
                    }
                }

                for (int i = 0; i < MainWindow.instance.tipoviLokala.Count; i++)
                {
                    for (int j = 0; j < lokaliZaBrisanje.Count; j++)
                    {
                        if (MainWindow.instance.tipoviLokala[i].ID == lokaliZaBrisanje[j].OznakaTipa)
                        {
                            MainWindow.instance.tipoviLokala[i].lokali.Remove(lokaliZaBrisanje[j]);
                        }
                    }
                }
                MainWindow.instance.sacuvaj_u_fajl_Etiketu(MainWindow.instance.etikete);
                MainWindow.instance.sacuvaj_u_fajl_Lokal(MainWindow.instance.lokali);
                MainWindow.instance.sacuvaj_u_fajl_Tip(MainWindow.instance.tipoviLokala);
            }
            else
                return;
        }

        private void txtPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            String tempIme1, tempIme2;
            cnt++;

            if (cnt == 1)
              inicijalizujLokale(tableGridLokali.ItemsSource);

           pomocni = (ObservableCollection<Lokal>)tableGridLokali.ItemsSource;
            
            tempIme2 = txtPretraga.Text.ToLower();
      

            if (tempIme2.Equals(""))
            {
                pomocni.Clear();
                for (int i = 0; i < tempLokali.Count; i++)
                    pomocni.Add(tempLokali[i]);
            }   

            for (int i = 0; i < tempLokali.Count; i++)
            {
                tempIme1 = tempLokali[i].Ime.ToLower();

                if (!tempIme1.Contains(tempIme2))
                {
                   if(pomocni.Contains(tempLokali[i]))
                    {
                        pomocni.Remove(tempLokali[i]);
                    }
                }else
                {
                    if(!pomocni.Contains(tempLokali[i]))
                    {
                        pomocni.Add(tempLokali[i]);
                    }
                }
            }

        }

        private void inicijalizujLokale(IEnumerable itemsSource)
        {
            tempLokali = ((ObservableCollection<Lokal>)itemsSource).ToList();
        }
    }
}