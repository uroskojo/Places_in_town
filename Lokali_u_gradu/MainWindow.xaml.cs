using Lokali_u_gradu.ViewModels;
using Lokali_u_gradu.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace Lokali_u_gradu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>   
    /// 

    public partial class MainWindow : Window
    {
        public static MainWindow instance;
        public static bool zaIzmenu; // indikator da je konstruktor formaLokal pozvan iz tabele; sluzi za izmenu podataka
        public Point startPoint = new Point();

        public ObservableCollection<TipLokala> tipoviLokala
        {
            get;
            set;
        }

        public ObservableCollection<Lokal> lokali
        {
            get;
            set;
        }
        public ObservableCollection<Etiketa> etikete
        {
            get;
            set;
        }
        public Dictionary<int, double[]> koo
        {

            get;
            set;
        }
        public Dictionary<int, List<int>> id_etiketa //kljuc je id lokala, a vrednost je lista id-jeva njegovih etiketa
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MapaViewModel();
            instance = this;

            pregledLokala.DataContext = this;

            tipoviLokala = new ObservableCollection<TipLokala>();
            lokali = new ObservableCollection<Lokal>();
            etikete = new ObservableCollection<Etiketa>();
            koo = new Dictionary<int, double[]>();
            id_etiketa = new Dictionary<int, List<int>>();


            etikete = procitaj_iz_fajla_Etikete();
            lokali = procitaj_iz_fajla_Lokal();
            tipoviLokala = procitaj_iz_fajla_Tip();
            koo = ucitaj_koordinate();

            for (int j = 0; j < tipoviLokala.Count; j++)
            {
                tipoviLokala[j].lokali = new ObservableCollection<Lokal>();
            }

            for (int i = 0; i < lokali.Count; i++)
            {
                lokali[i].etikete = new ObservableCollection<Etiketa>();

                for (int j = 0; j < tipoviLokala.Count; j++)
                {
                    if (lokali[i].OznakaTipa == tipoviLokala[j].ID)
                    {
                        tipoviLokala[j].lokali.Add(lokali[i]);
                    }
                }
            }

            List<int> tempList = new List<int>();
            int idx = 0;

            for (int i = 0; i < lokali.Count; i++)
            {
                for (int j = 0; j < lokali[i].ID_ETIKETA.Count; j++)
                {
                    for (int k = 0; k < etikete.Count; k++)
                    {
                        if (lokali[i].ID_ETIKETA[j] == etikete[k].ID)
                        {
                            lokali[i].etikete.Add(etikete[k]);
                            idx = i;
                            tempList.Add(lokali[i].ID_ETIKETA[j]);
                        }
                    }

                }
                id_etiketa.Add(lokali[idx].ID, tempList);
                tempList = new List<int>();
            }

            for (int i = 0; i < etikete.Count; i++)
            {
                etikete[i].Boja = new SolidColorBrush(Color.FromArgb(etikete[i].ARGB[0], etikete[i].ARGB[1], etikete[i].ARGB[2], etikete[i].ARGB[3]));
            }


        }

        public void sacuvaj_u_fajl_Lokal(ObservableCollection<Lokal> lokali)
        {
            Stream stream = File.Open("lokali.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, lokali);   // cuvam listu u fajl
            stream.Close();
        }


        public void sacuvaj_u_fajl_Tip(ObservableCollection<TipLokala> tipLokala)
        {
            Stream stream = File.Open("tipLokala.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, tipLokala);   // cuvam listu u fajl
            stream.Close();
        }

        public void sacuvaj_u_fajl_Etiketu(ObservableCollection<Etiketa> etikete)
        {
            Stream stream = File.Open("etikete.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, etikete);   // cuvam listu u fajl
            stream.Close();
        }

        public void sacuvaj_koordinate(Dictionary<int, double[]> koo)
        {
            Stream stream = File.Open("koordinate.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, koo);   // cuvam listu u fajl
            stream.Close();

        }

        public Dictionary<int, double[]> ucitaj_koordinate()
        {

            if (!File.Exists(@"D:\6. semestar- VEZBANJE\HCI\ProjekatPonovo\Lokali_u_gradu\Lokali_u_gradu\bin\Debug\koordinate.dat"))
            {
                return new Dictionary<int, double[]>();
            }

            Stream stream = File.Open("koordinate.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            koo = (Dictionary<int, double[]>)bf.Deserialize(stream);
            stream.Close();

            return koo;
        }

        public ObservableCollection<Etiketa> procitaj_iz_fajla_Etikete()
        {

            // binarni fajl
            if (!File.Exists(@"D:\6. semestar- VEZBANJE\HCI\ProjekatPonovo\Lokali_u_gradu\Lokali_u_gradu\bin\Debug\etikete.dat"))
            {
                return new ObservableCollection<Etiketa>();
            }

            Stream stream = File.Open("etikete.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            etikete = (ObservableCollection<Etiketa>)bf.Deserialize(stream);
            stream.Close();

            return etikete;

        }
        public ObservableCollection<Lokal> procitaj_iz_fajla_Lokal()
        {

            // binarni fajl
            //if 
            if(!File.Exists(@"lokali.dat"))
            {
                return new ObservableCollection<Lokal>();
            }

            Stream stream = File.Open("lokali.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            lokali = (ObservableCollection<Lokal>)bf.Deserialize(stream);
            stream.Close();

            return lokali;

        }

        public ObservableCollection<TipLokala> procitaj_iz_fajla_Tip()
        {
            // binarni fajl

            if (!File.Exists(@"D:\6. semestar- VEZBANJE\HCI\ProjekatPonovo\Lokali_u_gradu\Lokali_u_gradu\bin\Debug\tipLokala.dat"))
            {
                return new ObservableCollection<TipLokala>();
            }
            Stream stream = File.Open("tipLokala.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            tipoviLokala = (ObservableCollection<TipLokala>)bf.Deserialize(stream);
            stream.Close();


            return tipoviLokala;

        }

        public async void changeText(TextBlock txtBlock, String s)
        {
            txtBlock.Text = s;
            await Task.Delay(2000);
            txtBlock.Text = "";
        }

        public Boolean popunjeno(Boolean[] flag)
        {
            bool yes = true;

            for (int i = 0; i < flag.Count(); i++)
            {
                if (flag[i] == false)
                {
                    return false;
                }
            }
            return yes;
        }

        private void btnTabelaLokal_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new tabelaLokala();
            btnOcisti.Content = "MAPA";
        }

        public void postaviSliku(String putanja, Image ico)
        {
            PngBitmapDecoder decoderPNG;
            JpegBitmapDecoder decoderJPG;
            BitmapSource izvor = null;

            if (!putanja.Equals(""))
            {
                Uri myUri = new Uri(@putanja);
                int idx = putanja.LastIndexOf('.');
                string format = putanja.Substring(idx);


                if ((format.Equals(".png") || format.Equals(".PNG")))
                {
                    decoderPNG = new PngBitmapDecoder(myUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    izvor = decoderPNG.Frames[0];
                }

                if (format.Equals(".jpg") || format.Equals(".jpeg") || format.Equals(".JPG") || format.Equals(".JPEG"))
                {
                    decoderJPG = new JpegBitmapDecoder(myUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    izvor = decoderJPG.Frames[0];
                }

                ico.Source = izvor;
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void pregledLokala_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void pregledLokala_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(null);
            Vector diff = startPoint - mousePosition;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance
                ))
            {
                //uzmi povlacen element drveta
                TreeView tree = sender as TreeView;

                TreeViewItem treeViewItem = FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                if (treeViewItem == null)
                    return;

                if (treeViewItem.DataContext.ToString().Equals("Lokali_u_gradu.TipLokala"))
                    return;

                //nadji podatke "iza" TreeViewItem

                Lokal lokal = (Lokal)treeViewItem.DataContext;
                DataObject dragData = new DataObject("Lokal", lokal);
                DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
            }
        }

        private void btnOcisti_Click(object sender, RoutedEventArgs e)
        {
            resetPretrage();
            if (btnOcisti.Content.Equals("MAPA"))
            {
                DataContext = new MapaViewModel();
                btnOcisti.Content = "Očisti mapu";
                return;
            }

            Mapa.instance.mapa.Children.RemoveRange(2, Mapa.instance.mapa.Children.Count);
            List<int> kljucevi = new List<int>(MainWindow.instance.koo.Keys);

            for (int i = 0; i < kljucevi.Count; i++)
                koo.Remove(kljucevi[i]);

            sacuvaj_koordinate(koo);

        }

        //HELP
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mw = sender as MainWindow;

            if (mw is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)mw);
                if (str == null)
                    return;
                HelpProvider.ShowHelp(str, this);
            }
        }

        private void btnTabelaTipovi_Click(object sender, RoutedEventArgs e)
        {
            resetPretrage();
            DataContext = new tabelaTipoviView();
            btnOcisti.Content = "MAPA";
        }

        private void btnTabelaEtikete_Click(object sender, RoutedEventArgs e)
        {
            resetPretrage();
            DataContext = new tabelaEtiketeView();
            btnOcisti.Content = "MAPA";
        }

        public void resetPretrage()
        {

            if (tabelaLokala.instance != null && tabelaLokala.instance.cnt > 0)
            {
                tabelaLokala.instance.pomocni.Clear();
                for (int i = 0; i < tabelaLokala.instance.tempLokali.Count; i++)
                    tabelaLokala.instance.pomocni.Add(tabelaLokala.instance.tempLokali[i]);
            }

            if (tabelaTipoviView.instance != null && tabelaTipoviView.instance.cnt > 0)
            {
                tabelaTipoviView.instance.pomocni.Clear();
                for (int i = 0; i < tabelaTipoviView.instance.tempTipovi.Count; i++)
                    tabelaTipoviView.instance.pomocni.Add(tabelaTipoviView.instance.tempTipovi[i]);
            }

            if (tabelaEtiketeView.instance != null && tabelaEtiketeView.instance.cnt > 0)
            {
                tabelaEtiketeView.instance.pomocni.Clear();
                for (int i = 0; i < tabelaEtiketeView.instance.tempEtikete.Count; i++)
                    tabelaEtiketeView.instance.pomocni.Add(tabelaEtiketeView.instance.tempEtikete[i]);
            }
        }
    }
}
