using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lokali_u_gradu.Views
{
    /// <summary>
    /// Interaction logic for Mapa.xaml
    /// </summary>
    public partial class Mapa : UserControl
    {
        Point startPointCanvas;
        Boolean kantaCoo = false;
        public static Mapa instance;
        
        public Mapa()
        {
            InitializeComponent();
            postaviMapu();
            inicijalizacijaMape();
            instance = this;
            
        }

       public void inicijalizacijaMape()
        {

            List<int> kljucevi = new List<int>(MainWindow.instance.koo.Keys);
            double[] xy = new double[2];
            kanta.ToolTip = "Ukloni lokal sa mape";

            foreach (int kljuc in kljucevi)
            {
                for (int j = 0; j < MainWindow.instance.lokali.Count; j++)
                {
                    if (kljuc == MainWindow.instance.lokali[j].ID)
                    {
                        Label name = new Label();
                        Image icon = new Image();
                        icon.Width = 28;
                        icon.Height = 28;
                       
                        name.Content = MainWindow.instance.lokali[j].Ime;
                      
                        MainWindow.instance.postaviSliku(MainWindow.instance.lokali[j].Ikonica, icon);

                        Canvas dete = new Canvas();
                        dete.Children.Add(icon);
                        dete.Children.Add(name);
                        dete.Name = "L" + (MainWindow.instance.lokali[j].ID).ToString();
                        
                        Canvas.SetLeft(name, 23);
                        Canvas.SetTop(name, -2);

                        mapa.Children.Add(dete);
                        MainWindow.instance.koo.TryGetValue(kljuc, out xy);
                        Canvas.SetTop(dete, xy[1]);
                        Canvas.SetLeft(dete, xy[0]);

                        inicijalizujToolTip(dete, MainWindow.instance.lokali[j]);
                    }
                }
            }
        }

        public void postaviMapu()
        {
            Uri myUri = new Uri(@"D:\6. semestar- VEZBANJE\HCI\ProjekatPonovo\Lokali_u_gradu\Lokali_u_gradu\Resursi\mapa.jpg");
            //PngBitmapDecoder decoder = new PngBitmapDecoder(myUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            // BitmapSource izvor = decoder.Frames[0];

            JpegBitmapDecoder decoder = new JpegBitmapDecoder(myUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource izvor = decoder.Frames[0];
            slikaMape.Source = izvor;
          
        }

        private void kanta_DragEnter(object sender, DragEventArgs e)
        {

            if (!e.Data.GetDataPresent("deteKanvas")) // || sender == e.Source
            {
                e.Effects = DragDropEffects.None;
            }
        }



        private void pregledLokala_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("Lokal") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public Boolean preklapanjeIkonica(List<double[]> existsCoo, Point currentPosition)
        {
            for (int i = 0; i < existsCoo.Count; i++)
            {
                if (Math.Abs((existsCoo[i])[0] - currentPosition.X) < 18 && Math.Abs((existsCoo[i])[1] - currentPosition.Y) < 18)
                {
                    return true;
                }
            }
            return false;
        }
        private void pregledLokala_Drop(object sender, DragEventArgs e)
        {
            Point currentPosition = e.GetPosition(mapa);
            TextBlock blok = new TextBlock();
            Label name = new Label();
            Image icon = new Image();
            icon.Width = 28;
            icon.Height = 28;
            double[] koo = new double[2];
            List<double[]> existsCoo = MainWindow.instance.koo.Values.ToList();

            if (e.Data.GetDataPresent("Lokal"))
            {
                Lokal lokal = e.Data.GetData("Lokal") as Lokal;

                if (naKanti())
                    return;
                   
                if (MainWindow.instance.koo.ContainsKey(lokal.ID))
                    return;
                
                 //ne dozvoli da se preklope
                if (preklapanjeIkonica(existsCoo, currentPosition)) 
                    return;

                MainWindow.instance.postaviSliku(lokal.Ikonica, icon);
                name.Content = lokal.Ime;
                
                Canvas dete = new Canvas();
                dete.Children.Add(icon);
                dete.Children.Add(name);

                dete.Name = "L"+ (lokal.ID).ToString();


                //OVDE postavi etikete kao tooltipove
                inicijalizujToolTip(dete, lokal);


                Canvas.SetLeft(name, 23);
                Canvas.SetTop(name, -2);

                var elementLeft = currentPosition.X;
                var elementTop = currentPosition.Y;

                koo = new double[2] { currentPosition.X, currentPosition.Y };             
               
                MainWindow.instance.koo.Add(lokal.ID, koo);
                mapa.Children.Add(dete);

                Canvas.SetTop(dete, elementTop);
                Canvas.SetLeft(dete, elementLeft);

                MainWindow.instance.sacuvaj_koordinate(MainWindow.instance.koo);

            }else if (e.Data.GetDataPresent("deteKanvas"))
            {
                Canvas deteKanv = e.Data.GetData("deteKanvas") as Canvas; //previousArrangeRect su prethodne koordinate

                if (deteKanv.Name.Equals("mapa"))   //ako ne povuce ikonicu
                    return;

                if (preklapanjeIkonica(existsCoo, currentPosition))
                    return;

                if (naKanti())
                    return;

                Canvas.SetLeft(deteKanv, currentPosition.X);
                Canvas.SetTop(deteKanv, currentPosition.Y);

                string tempID = deteKanv.Name;
                int idLokala = int.Parse(tempID.Substring(1));

                koo = new double[2] { currentPosition.X, currentPosition.Y };

                if (MainWindow.instance.koo.ContainsKey(idLokala))
                {
                    MainWindow.instance.koo.Remove(idLokala);
                    MainWindow.instance.koo.Add(idLokala, koo);
                    MainWindow.instance.sacuvaj_koordinate(MainWindow.instance.koo);

                }

            }

        }

        private void mapa_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.Source == mapa)
                return;
            startPointCanvas = e.GetPosition(mapa);
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
        private void mapa_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(mapa);
            Vector diff = startPointCanvas - mousePosition;

            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance
                ))
            {
                //prevlacenje
                Canvas c = sender as Canvas;
                Canvas deteKanvas = FindAncestor<Canvas>((DependencyObject)e.OriginalSource); //internal aray. Ovo je dobro, nadje ime i ikonicu

                DataObject dragData = new DataObject("deteKanvas", deteKanvas);
                DragDrop.DoDragDrop(mapa,dragData, DragDropEffects.Move);

            }
        }

        private void kanta_Drop(object sender, DragEventArgs e)
        {
            kantaCoo = true;
            Canvas deteKanv = e.Data.GetData("deteKanvas") as Canvas;

            if (deteKanv == null)  
                return;


            string tempID = deteKanv.Name;
            int idLokala = int.Parse(tempID.Substring(1));
            MainWindow.instance.koo.Remove(idLokala);
            MainWindow.instance.sacuvaj_koordinate(MainWindow.instance.koo);
            mapa.Children.Remove(deteKanv);
           

        }

        public Boolean naKanti()
        {
            if(kantaCoo)
            {
                kantaCoo = false; //resetujem
                return true;
            }
            return false;
        }

        public void inicijalizujToolTip(Canvas dete, Lokal lokal)
        {
            //tooltip etikete

            bool addNewLine = true;

            TextBlock blockParent = new TextBlock();
            blockParent.MinWidth = 130;
            blockParent.MaxWidth = 220;
            ///
            Image slikaTT = new Image();
            MainWindow.instance.postaviSliku(lokal.Ikonica, slikaTT);
            slikaTT.Stretch = Stretch.Fill;
            blockParent.Inlines.Add(slikaTT);

            if(lokal.etikete.Count > 0)
                blockParent.Inlines.Add("\n");
            ///
            for (int i = 0; i < lokal.etikete.Count; i++)
            {
                if (i == lokal.etikete.Count - 1)
                    addNewLine = false;
                 
                 
                    TextBlock blockEtiketa = new TextBlock();
                    blockEtiketa.Text = lokal.etikete[i].Opis;
                    blockEtiketa.FontSize = 17;

                    blockEtiketa.Width = 220;
                    blockEtiketa.Foreground = Brushes.White;
                               
                    blockEtiketa.Background = new SolidColorBrush(Color.FromArgb(lokal.etikete[i].ARGB[0], lokal.etikete[i].ARGB[1], lokal.etikete[i].ARGB[2], lokal.etikete[i].ARGB[3])); 
                    blockParent.Inlines.Add(blockEtiketa);
                    
                    if(addNewLine)
                        blockParent.Inlines.Add("\n");
                
            }

            dete.ToolTip = blockParent;
        }
    }
}
