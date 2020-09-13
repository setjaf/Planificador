using Planificador.VistaModelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Planificador.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Actividades : ContentPage
    {
        public bool EsVisible;
        public Actividades()
        {
            ViewModel = new ActividadesVistaModelo();
            EsVisible = true;
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                ViewModel.CambioEstadoActividadesCommand.Execute(null);
                var resultado = (DateTime.Now.Second % 10) != 0;
                if (!resultado)
                {
                    Timer10();
                }
                return resultado;
            });
            
        }

        public void Timer10()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                ViewModel.CambioEstadoActividadesCommand.Execute(null);
                var resultado = (DateTime.Now.Second % 60) != 0;
                if (!resultado)
                {
                    Timer60();
                }
                return resultado;
            });
        }

        public void Timer60()
        {
            Device.StartTimer(TimeSpan.FromSeconds(60), () => {
                ViewModel.CambioEstadoActividadesCommand.Execute(null);
                return true;
            });
        }

        public ActividadesVistaModelo ViewModel
        {
            get { return BindingContext as ActividadesVistaModelo; }
            set
            {
                BindingContext = value;
            }
        }

        protected override void OnAppearing()
        {
            ViewModel.CargarActividadesCommand.Execute(null);
            base.OnAppearing();
        }

        private async void lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var actividad = (ActividadVistaModelo)e.Item;
            var navPage = new NavigationPage(new ActividadDetalle(actividad))
            {
                BarBackgroundColor = Color.FromHex(actividad.BackgroundColor),
                BarTextColor = Color.FromHex(actividad.TextColor)
            };

            navPage.BindingContext = actividad;
            navPage.SetBinding(NavigationPage.BarBackgroundColorProperty, path: "BackgroundColor");
            navPage.SetBinding(NavigationPage.BarTextColorProperty, path: "TextColor");
            await Navigation.PushModalAsync(navPage);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync( new NavigationPage( new NuevaActividad( DateTime.Parse(ViewModel.DiaSeleccionado) ) ) );
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Eliminar actividad", "¿Estás seguro de eliminar la actividad?", "Aceptar", "Cancelar"))
            {
                ViewModel.EliminarActividadCommand.Execute(((MenuItem)sender).CommandParameter);
            }
        }
    }

}