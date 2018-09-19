using Lokali_u_gradu.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lokali_u_gradu.Views;

namespace Lokali_u_gradu
{
    class HelpProvider
    {
        public static string GetHelpKey(DependencyObject obj)
        {
            string s = ((MainWindow)obj).DataContext.ToString();
            if (s.Equals("Lokali_u_gradu.ViewModels.MapaViewModel"))
                return "pocetna";
            else if (s.Equals("Lokali_u_gradu.Views.tabelaLokala"))
                return "tabelaLokala";
            else if (s.Equals("Lokali_u_gradu.Views.tabelaTipoviView"))
                return "tabelaTipova";
            else if (s.Equals("Lokali_u_gradu.Views.tabelaEtiketeView"))
                return "tabelaEtiketa";
            else if (s.Equals("Lokali_u_gradu.Views.formaLokal"))
                return "formaLokalHelp";
            else if (s.Equals("Lokali_u_gradu.Views.formaTipLokalaView"))
                return "formaTipHelp";
            else if (s.Equals("Lokali_u_gradu.Views.formaEtiketaView"))
                return "formaEtiketaHelp";

            return null;
        }

        public static void SetHelpKey(DependencyObject obj, string value)
        {
            obj.SetValue(HelpKeyProperty, value);
        }

        public static readonly DependencyProperty HelpKeyProperty =
            DependencyProperty.RegisterAttached("HelpKey", typeof(string), typeof(HelpProvider), new PropertyMetadata("index", HelpKey));
        private static void HelpKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //NOOP
        }

        
        public static void ShowHelp(string key, Object originator)
        {
            HelpViewer hh = new HelpViewer(key, originator);
            MainWindow.instance.DataContext = hh;
        }
    }
}
