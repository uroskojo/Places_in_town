using Lokali_u_gradu.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lokali_u_gradu.Views
{
    /// <summary>
    /// Interaction logic for formaEtiketaView.xaml
    /// </summary>
    public partial class formaEtiketaView : System.Windows.Controls.UserControl
    {
        public Boolean[] flag = new Boolean[2];
        public bool zaIzmenu;
        public int boja;
        public List<Byte> ARGB;


        public formaEtiketaView()
        {
            InitializeComponent();
          
        }

        private void txtOznaka_LostFocus(object sender, RoutedEventArgs e)
        {
            
            flag[0] = true;

            if(String.IsNullOrEmpty(txtOznaka.Text))
            {
                flag[0] = false;
                txtOznaka.Background = System.Windows.Media.Brushes.LightPink;
            }

            if (txtOznaka.Text.Any(c => Char.IsLetter(c)))
            {
                flag[0] = false;
                MainWindow.instance.changeText(OznakaWarning, "Ne smeju se unositi slova!");
                txtOznaka.Background = System.Windows.Media.Brushes.LightPink;

            }
        }
        private void txtOpis_LostFocus(object sender, RoutedEventArgs e)
        {
            flag[1] = true;
            if(String.IsNullOrEmpty(txtOpis.Text))
            {
                flag[1] = false;
                txtOpis.Background = System.Windows.Media.Brushes.LightPink;
            }

        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {           

                int b = colorDialog.Color.ToArgb();
                boja = b;

                List<Byte> bytes = new List<Byte>();
                ARGB = new List<Byte>();
                bytes.Add(colorDialog.Color.A);
                bytes.Add(colorDialog.Color.R);
                bytes.Add(colorDialog.Color.G);
                bytes.Add(colorDialog.Color.B);
                ARGB = bytes;
                txtBoja.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
            }

            if(colorDialog.Color == null)   //mora da se izabere boja
            {
                MainWindow.instance.changeText(BojaWarning, "Mora se izabrati boja!");
                return;
            }

        }
      

        private void btnOdustani_Copy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.DataContext = new tabelaEtiketeView();

        }    

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {

            bool postojiEtiketa = false;
            
            if ((flag[0] && flag[1]) || zaIzmenu)
            {
                int id = int.Parse(txtOznaka.Text);
                Etiketa etiketa = new Etiketa(id, txtOpis.Text, ARGB);
               
                if (MainWindow.instance.etikete.Count == 0)
                {
                    MainWindow.instance.etikete.Add(etiketa);
                    MainWindow.instance.sacuvaj_u_fajl_Etiketu(MainWindow.instance.etikete);
                    zaIzmenu = false;
                    MainWindow.instance.DataContext = new tabelaEtiketeView();
                    return;
                }

                for (int i = 0; i < MainWindow.instance.etikete.Count; i++)
                {
                    if (zaIzmenu)
                    {
                        if (MainWindow.instance.etikete[i].ID == etiketa.ID)
                        {
                            MainWindow.instance.etikete[i] = etiketa;
                            zaIzmenu = false;
                            MainWindow.instance.DataContext = new tabelaEtiketeView();
                            MainWindow.instance.sacuvaj_u_fajl_Etiketu(MainWindow.instance.etikete);
                            return;
                        }
                    }
                    else
                    {
                        if (MainWindow.instance.etikete[i].ID == etiketa.ID)
                        {
                            postojiEtiketa = true;
                            MainWindow.instance.changeText(OznakaWarning, "Već postoji etiketa s unetim ID-jem!");
                            return;
                        }
                    }
                }
              
                if (!postojiEtiketa)
                {
                    MainWindow.instance.etikete.Add(etiketa);
                    MainWindow.instance.sacuvaj_u_fajl_Etiketu(MainWindow.instance.etikete);
                }

            }
            else return;

            zaIzmenu = false;
            MainWindow.instance.DataContext = new tabelaEtiketeView();
        }   
    }
}
