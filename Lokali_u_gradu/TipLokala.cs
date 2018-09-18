using Lokali_u_gradu.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Lokali_u_gradu
{
    [Serializable]
    public class TipLokala : INotifyPropertyChanged, ISerializable
    {
        private int id;
        private string ime;
        private string opis;
        private string ikonica;
        public bool zaIzmenu;

        public ObservableCollection<Lokal> lokali
        {
            get;
            set;
        }

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
            info.AddValue("Ime", Ime);
            info.AddValue("Opis", Opis);
            info.AddValue("Oznaka", ID);
            info.AddValue("Ikonica", Ikonica);
        }

        public TipLokala(SerializationInfo info, StreamingContext context)
        {
            Ime = (string)info.GetValue("Ime", typeof(string));
            Opis = (string)info.GetValue("Opis", typeof(string));
            ID = (int)info.GetValue("Oznaka", typeof(int));
            Ikonica = (string)info.GetValue("Ikonica", typeof(string));

        }
        public TipLokala(int id, string ime, string opis, string ikonica)
        {
            this.id = id;
            this.ime = ime;
            this.opis = opis;
            this.ikonica = ikonica;
            lokali = new ObservableCollection<Lokal>();
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;

                }
            }
        }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (value != ime)
                {
                    ime = value;

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

                }
            }
        }

        public string Ikonica
        {
            get
            {
                return ikonica;
            }
            set
            {
                if (value != ikonica)
                {
                    ikonica = value;

                }
            }
        }
    }
}
