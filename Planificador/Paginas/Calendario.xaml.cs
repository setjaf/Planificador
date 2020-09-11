﻿using Planificador.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Planificador.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendario : ContentPage
    {
        public Calendario()
        {

            ViewModel = new CalendarioVistaModelo();
            InitializeComponent();
        }

        public CalendarioVistaModelo ViewModel
        {
            get { return BindingContext as CalendarioVistaModelo; }
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var actividad = (ActividadVistaModelo)((TappedEventArgs)e).Parameter;
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

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            var hola = e;
            Console.WriteLine("Piched");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NuevaActividad()));
        }
    }
}