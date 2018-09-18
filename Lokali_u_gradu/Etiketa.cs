using Lokali_u_gradu.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lokali_u_gradu
{

    [Serializable]
    public class Etiketa : INotifyPropertyChanged, ISerializable
    {
        private int id;
        private string opis;
        private List<Byte> argb;
        private SolidColorBrush boja;
    
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID",ID);
            info.AddValue("Opis", Opis);
            info.AddValue("ARGB", ARGB);
                    
        }

        public Etiketa(SerializationInfo info, StreamingContext context)
        {
            ID = (int)info.GetValue("ID", typeof(int));
            Opis = (string)info.GetValue("Opis", typeof(string));
            ARGB = (List<Byte>)info.GetValue("ARGB", typeof(List<Byte>));        

        }
        public Etiketa(int id, string opis, List<Byte> argb)
        {
            this.id = id;
            this.opis = opis;
            this.argb = argb;
            this.boja = new SolidColorBrush(Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
        }

        public List<Byte> ARGB
        {
            get
            {
                return argb;
            }
            set
            {
                if(value!=argb)
                {
                    argb = value;
                    OnPropertyChanged("ARGB");
                }
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if(value != id)
                {
                    id = value;
                    OnPropertyChanged("ID");
                }
            }                
        }

        public string Opis
        {
            get
            {
                return opis;
            }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }
       public SolidColorBrush Boja
        {
            get
            {
                return boja;
            }
            set
            {
                if (value != boja)
                {
                    boja = value;
                    OnPropertyChanged("Boja");
                }
            }
        }

    }
}
