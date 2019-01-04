using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEkspertowyTarczykowski.Model
{
    class Mushroom : BaseNotificationClass
    {
        private double diameterOfTheHat { get; set; }
        private double lamella { get; set; }
        private double toxins { get; set; }
        public string Edibility { get; set; }
        public double Suitable { get; set; }

        public double DiameterOfTheHat
        {
            get { return diameterOfTheHat; }
            set
            {
                diameterOfTheHat = value;
                OnPropertyChanged();
            }
        }

        public double Lamella
        {
            get { return lamella; }
            set
            {
                lamella = value;
                OnPropertyChanged();
            }
        }

        public double Toxins
        {
            get { return toxins; }
            set
            {
                toxins = value;
                OnPropertyChanged();
            }
        }
    }
}
