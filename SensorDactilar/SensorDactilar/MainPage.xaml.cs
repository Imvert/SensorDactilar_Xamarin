using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace SensorDactilar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void btnlogin_Clicked(object sender, EventArgs e)
        {

            bool verificarhuella = await CrossFingerprint.Current.IsAvailableAsync(false);

            if (!verificarhuella)
            {
                await DisplayAlert(
                    "Alerta",
                    "El sensor de huella no esta habilitada",
                    "ok");
                return;
            }

            AuthenticationRequestConfiguration conf = new AuthenticationRequestConfiguration("Autentificacion",
                "Autentificar acceso");

            var result = await CrossFingerprint.Current.AuthenticateAsync(conf);

            if (result.Authenticated)
            {
                await DisplayAlert(
                   "Bienvenido",
                   "Autentificacion correcta",
                   "ok");
                return;
            }
            else 
            {
                await DisplayAlert(
                   "Error",
                   "Acceso denegado",
                   "ok");
                return;
            }
        }
    }
}
