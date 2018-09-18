using Lokali_u_gradu.ViewModels;
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

namespace Lokali_u_gradu.Views
{
    /// <summary>
    /// Interaction logic for formaTipLokalaView.xaml
    /// </summary>
    public partial class formaTipLokalaView : UserControl
    {
        public Boolean[] flag = new Boolean[2];
        public string putanjaIkoniceTip;
        public bool zaIzmenu;
        public int pomocniIDtipa;
        
        //flag[0] je txtImeTipa
        //flag[1] je txtOznakaTipa
        //flag[2] je ikonica


        public formaTipLokalaView()
        {
            InitializeComponent();
       
        }


        private void txtImeTipaL_LostFocus(object sender, RoutedEventArgs e)
        {
            flag[0] = true;

            if (String.IsNullOrEmpty(txtImeTipaL.Text))
            {
                flag[0] = false;
                txtImeTipaL.Background = Brushes.LightPink;
            }

            if (txtImeTipaL.Text.Any(c => char.IsNumber(c)))
            {
                flag[0] = false;
                MainWindow.instance.changeText(imeTipaWarning, "Ne smeju se unositi brojevi!");

            }
        }

        private void txtOznakaTipaL_LostFocus(object sender, RoutedEventArgs e)
        {
            flag[1] = true;
            if (String.IsNullOrEmpty(txtOznakaTipaL.Text))
            {
                flag[1] = false;
                txtOznakaTipaL.Background = Brushes.LightPink;
            }

            if (txtOznakaTipaL.Text.Any(c => char.IsLetter(c)))
            {
                flag[1] = false;
                MainWindow.instance.changeText(OznakaTipaWarning, "Ne smeju se unositi slova!");
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.DataContext = new tabelaTipoviView();
          
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            bool dodaj = true;
                           
            if (putanjaIkoniceTip == null)
            {
                MainWindow.instance.changeText(ikonicaWarning, "Morate izabrati ikonicu!");
                return;
            }

            int idTipa = int.Parse(txtOznakaTipaL.Text);

            if ((flag[0] & flag[1]) || zaIzmenu)
            {
                TipLokala tipL = new TipLokala(idTipa, txtImeTipaL.Text, txtOpisTipaL.Text, putanjaIkoniceTip);

                //ako ne postoji ni jedan tip lokala, kreiracu ga bez bilo kakvih provera
                if (MainWindow.instance.tipoviLokala.Count == 0)
                {
                    MainWindow.instance.tipoviLokala.Add(tipL);
                    MainWindow.instance.DataContext = new tabelaTipoviView();

                    
                    MainWindow.instance.sacuvaj_u_fajl_Tip(MainWindow.instance.tipoviLokala);
                    return;
                }

                //prolazim kroz unete tipove lokala i ako postoji tip s ID-jem koji pokusavam da unesem, zabranicu
                for (int i = 0; i < MainWindow.instance.tipoviLokala.Count; i++)
                {
                        if(zaIzmenu)
                        {
                            if (MainWindow.instance.tipoviLokala[i].ID == idTipa)
                            {
                                tipL.lokali = MainWindow.instance.tipoviLokala[i].lokali;
                                MainWindow.instance.tipoviLokala[i] = tipL;
                                MainWindow.instance.DataContext = new tabelaTipoviView();
                                zaIzmenu = false;
                                                        
                            MainWindow.instance.sacuvaj_u_fajl_Tip(MainWindow.instance.tipoviLokala);


                            for(int j=0; j<MainWindow.instance.lokali.Count; j++)
                            {
                                if (MainWindow.instance.lokali[j].OznakaTipa == MainWindow.instance.tipoviLokala[i].ID)
                                {
                                    MainWindow.instance.lokali[j].Tip = MainWindow.instance.tipoviLokala[i].Ime;
                                    MainWindow.instance.sacuvaj_u_fajl_Lokal(MainWindow.instance.lokali);
                                }
                                    
                            }

                            return;
                            }
                           
                        }
                        else
                        {
                            if (MainWindow.instance.tipoviLokala[i].ID == idTipa)
                            {
                                dodaj = false;
                                postojiWarning.FontSize = 20;
                                MainWindow.instance.changeText(postojiWarning, "Već postoji tip lokala s unetom oznakom!");
                                return;
                            }
                           
                        }
 
                }
                
                if(dodaj)
                {
                    MainWindow.instance.tipoviLokala.Add(tipL);
                    MainWindow.instance.DataContext = new tabelaTipoviView();
                                       
                    MainWindow.instance.sacuvaj_u_fajl_Tip(MainWindow.instance.tipoviLokala);
                }
            }

        
        }
        
        private void btnIkonicaTip_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png";
            //JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif

            dlg.ShowDialog();
            putanjaIkoniceTip = dlg.FileName;
            
            if (putanjaIkoniceTip!=null)
                MainWindow.instance.postaviSliku(putanjaIkoniceTip, ico);
        }

    }
}
