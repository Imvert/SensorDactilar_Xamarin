using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SensorDactilar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async  void btn_login_Clicked(object sender, EventArgs e)
        {
            var usuario = txtuser.Text;
            var password = txtpass.Text;

            if (string.IsNullOrEmpty(usuario))
            {
                await DisplayAlert("Error", "Debe ingresar su usuario", "OK");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Debe ingresar su password", "OK");
                return;
            }

            if (usuario == "admin" && password == "1234")
            {

                await DisplayAlert("Correcto", "Bienvenido " + usuario, "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Incorrecto", "Datos erroneos", "OK");
                return;

            }


        }
        private async void  btn_biometrica_Clicked(object sender, EventArgs e)
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
            CrossLocalNotifications.Current.Show("Activando sensor dactilar", "Validando huella", 1);

            var result = await CrossFingerprint.Current.AuthenticateAsync(conf);

            if (result.Authenticated)
            {
                await DisplayAlert(
                   "Bienvenido",
                   "Autentificacion correcta",
                   "ok");
                CrossLocalNotifications.Current.Show("Acceso permitido", "huella validada", 2);
                await Navigation.PushAsync(new MainPage());
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