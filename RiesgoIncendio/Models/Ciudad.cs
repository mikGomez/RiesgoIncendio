using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiesgoIncendio.Models
{
    using System.ComponentModel;

    namespace WPF_NeighborhoodCommunity.Models
    {
        class Ciudad : INotifyPropertyChanged
        {
            #region VARIABLES
            public event PropertyChangedEventHandler PropertyChanged;

            private double _temperatura;
            private double _riegoHumedad;
            #endregion

            #region OBJETOS
            public double Temperatura
            {
                get { return _temperatura; }
                set
                {
                    _temperatura = value;
                    OnPropertyChanged("Temperatura");
                }
            }

            public double RiegoHumedad
            {
                get { return _riegoHumedad; }
                set
                {
                    _riegoHumedad = value;
                    OnPropertyChanged("RiegoHumedad");
                }
            }
            #endregion

            // Método que se encarga de actualizar las propiedades en cada cambio
            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
