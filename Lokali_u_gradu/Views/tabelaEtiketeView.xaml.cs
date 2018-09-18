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
    /// Interaction logic for tabelaEtiketeView.xaml
    /// </summary>
    public partial class tabelaEtiketeView : UserControl, INotifyPropertyChanged
    {
        public int cnt; //brojac ulazaka u pretragu
        public static tabelaEtiketeView instance;

        public tabelaEtiketeView()
        {
            InitializeComponent();
            this.DataContext = this;
            instance = this;
            Etikete = MainWindow.instance.etikete;
            cnt = 0;
        }
      
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        public ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }
        public List<Etiketa> tempEtikete // za pretragu
        {
            get;
            set;
        }
        public ObservableCollection<Etiketa> pomocni // za pretragu
        {
            get;
            set;
        }
        private void txtPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            String tempIme1, tempIme2;
            cnt++;

            if (cnt == 1)
                inicijalizujTipove(tableGridEtikete.ItemsSource);

            pomocni = (ObservableCollection<Etiketa>)tableGridEtikete.ItemsSource;

            tempIme2 = txtPretraga.Text.ToLower();


            if (tempIme2.Equals(""))
            {
                pomocni.Clear();
                for (int i = 0; i < tempEtikete.Count; i++)
                    pomocni.Add(tempEtikete[i]);
            }

            for (int i = 0; i < tempEtikete.Count; i++)
            {
                tempIme1 = tempEtikete[i].Opis.ToLower();

                if (!tempIme1.Contains(tempIme2))
                {
                    if (pomocni.Contains(tempEtikete[i]))
                    {
                        pomocni.Remove(tempEtikete[i]);
                    }
                }
                else
                {
                    if (!pomocni.Contains(tempEtikete[i]))
                    {
                        pomocni.Add(tempEtikete[i]);
                    }
                }
            }
        }

        private void inicijalizujTipove(IEnumerable itemsSource)
        {
            tempEtikete = ((ObservableCollection<Etiketa>)itemsSource).ToList();
        }

        private void btnDodajEtiketu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.DataContext = new formaEtiketaView();
        }

        private void btnIzmeniEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if (tableGridEtikete.SelectedItem == null)
                return;

            Etiketa etiketa = (Etiketa)tableGridEtikete.SelectedItem;

            formaEtiketaView fev = new formaEtiketaView();
            fev.txtOznaka.Text = etiketa.ID.ToString();
            fev.txtOznaka.IsEnabled = false;
            fev.txtOpis.Text = etiketa.Opis;
            fev.txtBoja.Background = new SolidColorBrush(Color.FromArgb(etiketa.ARGB[0], etiketa.ARGB[1], etiketa.ARGB[2], etiketa.ARGB[3]));
            fev.ARGB = etiketa.ARGB;


            fev.zaIzmenu = true;
            MainWindow.instance.DataContext = fev;
        }

        private void btnObrisiEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if (tableGridEtikete.SelectedItems == null)
                return;

            MessageBoxResult dr = MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButton.YesNo);

            if (dr == MessageBoxResult.Yes)
            {
                List<Etiketa> etiketeZaBrisanje = tableGridEtikete.SelectedItems.Cast<Etiketa>().ToList();

                for (int s = 0; s < MainWindow.instance.etikete.Count; s++)
                {
                    for (int o = 0; o < etiketeZaBrisanje.Count; o++)
                    {
                        if (MainWindow.instance.etikete[s].ID == etiketeZaBrisanje[o].ID)
                        {
                            MainWindow.instance.etikete.Remove(etiketeZaBrisanje[o]);
                        }
                    }
                }

                for (int s = 0; s < MainWindow.instance.lokali.Count; s++)
                {
                    for (int k = 0; k < MainWindow.instance.lokali[s].ID_ETIKETA.Count; k++)
                    {
                        for (int o = 0; o < etiketeZaBrisanje.Count; o++)
                        {
                            if (MainWindow.instance.lokali[s].ID_ETIKETA[k] == etiketeZaBrisanje[o].ID)
                            {
                                MainWindow.instance.lokali[s].ID_ETIKETA.Remove(etiketeZaBrisanje[o].ID);
                            }
                        }
                    }
                }

                for (int s = 0; s < MainWindow.instance.lokali.Count; s++)
                {
                    for (int k = 0; k < MainWindow.instance.lokali[s].etikete.Count; k++)
                    {
                        for (int o = 0; o < etiketeZaBrisanje.Count; o++)
                        {
                            if (MainWindow.instance.lokali[s].etikete[k].ID == etiketeZaBrisanje[o].ID)
                            {
                                MainWindow.instance.lokali[s].etikete.Remove(etiketeZaBrisanje[o]);
                            }
                        }
                    }
                }

            }
            else if (dr == MessageBoxResult.No)
            {
                return;
            }

            MainWindow.instance.sacuvaj_u_fajl_Etiketu(MainWindow.instance.etikete);
            MainWindow.instance.sacuvaj_u_fajl_Lokal(MainWindow.instance.lokali);
        }
    }

}

