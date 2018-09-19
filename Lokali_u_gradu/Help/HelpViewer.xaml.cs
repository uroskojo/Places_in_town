using Lokali_u_gradu.ViewModels;
using Lokali_u_gradu.Views;
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

namespace Lokali_u_gradu.Help
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : UserControl
    {
        public string izKogJeKlikNazad;
        public static HelpViewer instance;
        private JavaScriptControlHelper ch;
        public HelpViewer(string key, Object originator)
        {
            InitializeComponent();

            instance = this;

            string curDir = Directory.GetCurrentDirectory();
            string path = String.Format("{0}/Help/{1}.html", curDir, key);
            if (!File.Exists(path))
            {
                key = "error";
            }

            Uri u = new Uri(String.Format("file:///{0}/Help/{1}.html", curDir, key));
            ch = new JavaScriptControlHelper(originator);
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);
        }



        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (izKogJeKlikNazad.Equals("Lokali_u_gradu.ViewModels.MapaViewModel"))
                MainWindow.instance.DataContext = new MapaViewModel();
            else if (izKogJeKlikNazad.Equals("Lokali_u_gradu.Views.tabelaLokala"))
                MainWindow.instance.DataContext = new tabelaLokala();
            else if (izKogJeKlikNazad.Equals("Lokali_u_gradu.Views.tabelaTipoviView"))
                MainWindow.instance.DataContext = new tabelaTipoviView();    
            else if (izKogJeKlikNazad.Equals("Lokali_u_gradu.Views.tabelaEtiketeView"))
                MainWindow.instance.DataContext = new tabelaEtiketeView();
            else if (izKogJeKlikNazad.Equals("Lokali_u_gradu.Views.formaLokal"))
            {
                formaLokal fl = new formaLokal();
                fl.lblDemo.Content = "Demo";
                fl.lblDemo.Visibility = Visibility.Visible;
                MainWindow.instance.DataContext = fl;

            }
                
            else if (izKogJeKlikNazad.Equals("Lokali_u_gradu.Views.formaTipLokalaView"))
                MainWindow.instance.DataContext = new formaTipLokalaView();
            else if (izKogJeKlikNazad.Equals("Lokali_u_gradu.Views.formaEtiketaView"))
                MainWindow.instance.DataContext = new formaEtiketaView();
        }
      
    }
}
