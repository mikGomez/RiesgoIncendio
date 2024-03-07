using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RiesgoIncendio.Models.WPF_NeighborhoodCommunity.Models;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RiesgoIncendio.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ciudad ciu = new Ciudad();
        int resu;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ciu;
        }
        // boton para obtener la informacion de OpenWeatherMap, donde he tenido que hacerme una sesion y te dan un apyKey
        private async void ObtenerInformacion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // esta es mi apiKey
                string apiKey = "94c80625625fb76980431219a931d98f";
                // Ciudad donde quiero recoger la info, se la pasamos por texto
                string city = ciudad.Text.ToString();

                if (string.IsNullOrEmpty(city))
                {
                    ciudad.Text = "Por favor, introduce una ciudad";
                    return;
                }
                // Aqui tenemos que poner la url con la ciudad y la apikey que tenemos, esto lo da la propia pagina tambien aunque tendremos que adaptarla a la ciudad que queramos
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

                // Tenemos  que haacernos un incio de  cliente de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // obetenemos con el get los datos de la url
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Tenemos que leer el contenido en forma de caracteres
                        string responseBody = await response.Content.ReadAsStringAsync();

                        try
                        {
                           // Cogemos el JSON y lo tenemos que convertir en un objeto JObject

                            JObject json = JObject.Parse(responseBody);

                            // Tenemos que irnos a OpenWeatherMap donde nos viene un ejemplo del json con lo que contiene, lo que necesitamos es lo siguiente(Se podrian coger mas cosas)
                            // Cogeremos el dato que esta dentro de main, temp (Temperatura) y el otro para humedad
                            string temp = json["main"]["temp"].ToString();
                            string humedad = json["main"]["humidity"].ToString();

                            ciu.Temperatura = Convert.ToDouble(temp);
                            ciu.RiegoHumedad = Convert.ToInt32(humedad);

                        }
                        catch (JsonReaderException ex)
                        {
                            MessageBox.Show("Error en el JSON: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error en HTTP", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonComprobar(object sender, RoutedEventArgs e)
        {
            if (ValidarHumedad(ciu.RiegoHumedad))
            {

                resu = CalcularCBI(ciu.Temperatura, ciu.RiegoHumedad);
                resultado.Text = resu.ToString();
            }
            else
            {
                MessageBox.Show("La humedad relativa debe ser mayor que cero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static bool ValidarHumedad(double RH)
        {
            return RH >= 0;
        }

        public static int CalcularCBI(double T, double RH)
        {
            double cbi = (((110 - 1.373 * RH) - 0.54 * (10.20 - T)) * (124 * Math.Pow(10, (-0.0142 * RH)))) / 60;
            int cbiRedondeado = (int)Math.Round(cbi);
            return cbiRedondeado;
        }
    }
}