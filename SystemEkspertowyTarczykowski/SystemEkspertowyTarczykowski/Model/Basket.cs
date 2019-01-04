using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEkspertowyTarczykowski.Model
{
    class Basket : BaseNotificationClass
    {
        private string resultantClass;
        private string imageOK;
        private short countOfElementToCompare;
        public ObservableCollection<Mushroom> ListOfMushroom { get; set; }
        public Basket()
        {
            ListOfMushroom = new ObservableCollection<Mushroom>();
        }

        public string ResultantClass
        {
            get { return resultantClass; }
            set
            {
                resultantClass = value;
                OnPropertyChanged();
            }
        }

        public string ImageOK
        {
            get { return imageOK; }
            set
            {
                imageOK = value;
                OnPropertyChanged();
            }
        }

        public short CountOfElementToCompare
        {
            get { return countOfElementToCompare; }
            set
            {
                if(value == 3 || value == 5)
                {
                    countOfElementToCompare = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
