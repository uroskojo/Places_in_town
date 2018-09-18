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
    /// Interaction logic for tabelaTipoviView.xaml
    /// </summary>
    public partial class tabelaTipoviView : UserControl, INotifyPropertyChanged
    {
        public tabelaTipoviView()
        {
            InitializeComponent();
            this.DataContext = this;
            instance = this;
            TipoviLokala = MainWindow.instance.tipoviLokala;
            cnt = 0;
        }

        public static tabelaTipoviView instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public int cnt; //brojac ulazaka u pretragu
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public ObservableCollection<TipLokala> TipoviLokala
        {
            get;
            set;
        }
        public List<TipLokala> tempTipovi // za pretragu
        {
            get;
            set;
        }
        public ObservableCollection<TipLokala> pomocni // za pretragu
        {
            get;
            set;
        }

        private void txtPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            String tempIme1, tempIme2;
            cnt++;

            if (cnt == 1)
                inicijalizujTipove(tableGridTipL.ItemsSource);

            pomocni = (ObservableCollection<TipLokala>)tableGridTipL.ItemsSource;

            tempIme2 = txtPretraga.Text.ToLower();


            if (tempIme2.Equals(""))
            {
                pomocni.Clear();
                for (int i = 0; i < tempTipovi.Count; i++)
                    pomocni.Add(tempTipovi[i]);
            }

            for (int i = 0; i < tempTipovi.Count; i++)
            {
                tempIme1 = tempTipovi[i].Ime.ToLower();

                if (!tempIme1.Contains(tempIme2))
                {
                    if (pomocni.Contains(tempTipovi[i]))
                    {
                        pomocni.Remove(tempTipovi[i]);
                    }
                }
                else
                {
                    if (!pomocni.Contains(tempTipovi[i]))
                    {
                        pomocni.Add(tempTipovi[i]);
                    }
                }
            }

        }

        private void inicijalizujTipove(IEnumerable itemsSource)
        {
            tempTipovi = ((ObservableCollection<TipLokala>)itemsSource).ToList();
        }

        private void btnDodajTipL_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.DataContext = new formaTipLokalaView();
        }
        private void btnIzmeniTipL_Click(object sender, RoutedEventArgs e)
        {
            if (tableGridTipL.SelectedItem == null)
                return;

            TipLokala tipLokala = (TipLokala)tableGridTipL.SelectedItem;

            formaTipLokalaView ftlv = new formaTipLokalaView();

            ftlv.txtImeTipaL.Text = tipLokala.Ime;
            ftlv.txtOznakaTipaL.Text = tipLokala.ID.ToString();
            ftlv.txtOznakaTipaL.IsEnabled = false;
            ftlv.txtOpisTipaL.Text = tipLokala.Opis;

            if (tipLokala.Ikonica != null)
            {
                ftlv.putanjaIkoniceTip = tipLokala.Ikonica;
                MainWindow.instance.postaviSliku(ftlv.putanjaIkoniceTip, ftlv.ico);
            }


            tipLokala.zaIzmenu = true;
            ftlv.zaIzmenu = true;
            ftlv.pomocniIDtipa = tipLokala.ID;
            MainWindow.instance.DataContext = ftlv;


        }

        private void btnObrisiTipL_Click(object sender, RoutedEventArgs e)
        {
            if (tableGridTipL.SelectedItems == null)
                return;

            MessageBoxResult dr = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);

            if (dr == MessageBoxResult.Yes)
            {
                List<TipLokala> tipoviZaBrisanje = tableGridTipL.SelectedItems.Cast<TipLokala>().ToList();

                for (int i = 0; i < MainWindow.instance.tipoviLokala.Count; i++)
                {
                    for (int j = 0; j < tipoviZaBrisanje.Count; j++)
                    {
                        if (MainWindow.instance.tipoviLokala[i].ID == tipoviZaBrisanje[j].ID)
                        {
                            for (int k = 0; k < MainWindow.instance.tipoviLokala[i].lokali.Count; k++)
                            {
                                for (int m = 0; m < MainWindow.instance.lokali.Count; m++)
                                {
                                    if (MainWindow.instance.tipoviLokala[i].lokali[k].ID == MainWindow.instance.lokali[m].ID)
                                        MainWindow.instance.lokali.Remove(MainWindow.instance.tipoviLokala[i].lokali[k]);

                                }
                            }
                            MainWindow.instance.tipoviLokala.Remove(MainWindow.instance.tipoviLokala[i]);//obrisem tip, ali treba da obrisem i lokale koji su bili pod tim tipom

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

    }
}
