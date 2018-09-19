using Lokali_u_gradu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Lokali_u_gradu.Help
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        MainWindow prozor;
        tabelaLokala tl;
        tabelaTipoviView tt;
        tabelaEtiketeView te;
        public JavaScriptControlHelper(Object w)
        {
            string s = ((MainWindow)w).DataContext.ToString();
            HelpViewer.instance.izKogJeKlikNazad = s;
            if (s.Equals("Lokali_u_gradu.ViewModels.MapaViewModel"))
            {
                prozor = (MainWindow)w;
                return;
            } else if (s.Equals("Lokali_u_gradu.Views.tabelaLokala"))
            {
                tl = new tabelaLokala();
                return;
            }else if(s.Equals("Lokali_u_gradu.Views.tabelaTipoviView"))
            {
                tt = new tabelaTipoviView();
                return;
            }else if(s.Equals("Lokali_u_gradu.Views.tabelaEtiketeView"))
            {
                te = new tabelaEtiketeView();
                return;            
            }
        }
     
    }
}
