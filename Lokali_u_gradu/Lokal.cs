using Lokali_u_gradu.ViewModels;
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
    public class Lokal : INotifyPropertyChanged, ISerializable
    {
        private int id;
        private int oznakaTipa;
        private string nazivTipa;
        private string ikonica;
        private string ime;
        private string opis;
        private string alc;
        private string cene;
        private bool dostupanHendikep;
        private bool dozvoljenoPusenje;
        private bool rezervacije;
        private string datumOtvaranja;
        private int kapacitet;
     
       
        private List<int> id_etiketa;
        public ObservableCollection<Etiketa> etikete
        {
            get;
            set;
        }
        public event PropertyChangedEventHandler PropertyChanged;

      
        public Lokal(int id, int oTipa, string ime, string opis, bool rezerv, string datum, int kapac, bool cigare, bool hendi, string alc, string cene, string ikonica, string tip)
        {
            this.id = id;
            this.oznakaTipa = oTipa;
            this.ime = ime;
            this.opis = opis;
            this.rezervacije = rezerv;
            this.datumOtvaranja = datum;
            this.kapacitet = kapac;
            this.dozvoljenoPusenje = cigare;
            this.dostupanHendikep = hendi;
            this.alc = alc;
            this.cene = cene;
            this.ikonica = ikonica;
            this.nazivTipa = tip;
            etikete = new ObservableCollection<Etiketa>();
            id_etiketa = new List<int>();
        }

        public List<int> ID_ETIKETA
        {

            get
            {
                return id_etiketa;
            }
            set
            {
                if (value != id_etiketa)
                {
                    id_etiketa = value;
                    OnPropertyChanged("ID_ETIKETA");
                }

            }
        }
        public string Tip
        {

            get
            {
                return nazivTipa;
            }
            set
            {
                if (value != nazivTipa)
                {
                    nazivTipa = value;
                    OnPropertyChanged("Tip");
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

        public int OznakaTipa
        {
            get
            {
                return oznakaTipa;
            }
            set
            {
                if(value != oznakaTipa)
                {
                    oznakaTipa = value;
                    OnPropertyChanged("OznakaTipa");
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
                if(value != ikonica)
                {
                    ikonica = value;
                    OnPropertyChanged("Ikonica");
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
                    OnPropertyChanged("Ime");
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

        public string Alc
        {
            get
            {
                return alc;
            }
            set
            {
                if (value != alc)
                {
                    alc = value;
                    OnPropertyChanged("Alc");
                }
            }
        }

        public string Cene
        {
            get
            {
                return cene;
            }
            set
            {
                if (value != cene)
                {
                    cene = value;
                    OnPropertyChanged("Cene");
                }
            }
        }
        public bool Hendikep
        {
            get
            {
                return dostupanHendikep;
            }
            set
            {
                if (value != dostupanHendikep)
                {
                    dostupanHendikep = value;
                    OnPropertyChanged("Hendikep");
                }
            }
        }

        public bool Pusenje
        {
            get
            {
                return dozvoljenoPusenje;
            }
            set
            {
                if (value != dozvoljenoPusenje)
                {
                    dozvoljenoPusenje = value;
                    OnPropertyChanged("Pusenje");
                }
            }
        }

        public bool Rezervacije
        {
            get
            {
                return rezervacije;
            }
            set
            {
                if (value != rezervacije)
                {
                    rezervacije = value;
                    OnPropertyChanged("Rezervacije");
                }
            }
        }

        public string Datum
        {
            get
            {
                return datumOtvaranja;
            }
            set
            {
                if (value != datumOtvaranja)
                {
                    datumOtvaranja = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        public int Kapacitet
        {
            get
            {
                return kapacitet;
            }
            set
            {
                if (value != kapacitet)
                {
                    kapacitet = value;
                    OnPropertyChanged("Kapacitet");
                }
            }
        }

       
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
           info.AddValue("OznakaTipa", OznakaTipa);
           info.AddValue("Tip", Tip);
           info.AddValue("Ikonica", ikonica);
           info.AddValue("Alc", Alc);
           info.AddValue("Cene", Cene);
           info.AddValue("Hendikep", Hendikep);
           info.AddValue("Pusenje", Pusenje);
           info.AddValue("Rezervacije", Rezervacije);
           info.AddValue("Datum", Datum);
           info.AddValue("Kapacitet", Kapacitet);

            info.AddValue("ID_ETIKETA", ID_ETIKETA);


       }

       public Lokal(SerializationInfo info, StreamingContext context)
       {
           Ime = (string)info.GetValue("Ime", typeof(string));
           Opis = (string)info.GetValue("Opis", typeof(string));
           ID = (int)info.GetValue("Oznaka", typeof(int));
           OznakaTipa = (int)info.GetValue("OznakaTipa", typeof(int));
           Tip = (string)info.GetValue("Tip", typeof(string));
           Ikonica = (string)info.GetValue("Ikonica", typeof(string));
           Alc = (string)info.GetValue("Alc", typeof(string));
           Cene = (string)info.GetValue("Cene", typeof(string));
           Hendikep = (bool)info.GetValue("Hendikep", typeof(bool));
           Pusenje = (bool)info.GetValue("Pusenje", typeof(bool));
           Rezervacije = (bool)info.GetValue("Rezervacije", typeof(bool));
           Datum = (string)info.GetValue("Datum", typeof(string));
           Kapacitet = (int)info.GetValue("Kapacitet", typeof(int));

            ID_ETIKETA = (List<int>)info.GetValue("ID_ETIKETA", typeof(List<int>));
       }

        public Lokal()
        {
        }

    }



}
