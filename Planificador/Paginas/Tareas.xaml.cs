using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planificador.Negocio;
using Planificador.Modelos;
using Planificador.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Planificador.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class Tareas : ContentPage
    {
        public Tareas()
        {

            ViewModel = new TareasVistaModelo(Navigation);

            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            ViewModel.CargarTareasCommand.Execute(null);
            base.OnAppearing();
        }

        public TareasVistaModelo ViewModel
        {
            get { return BindingContext as TareasVistaModelo; }
            set { BindingContext = value; }
        }

        private async void tareasList_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {
            TareaVistaModelo tarea = (TareaVistaModelo) e.Item;
            var navPage = new NavigationPage(new TareaDetalle(tarea))
            {
                BarBackgroundColor = Color.FromHex(tarea.BackgroundColor),
                BarTextColor = Color.FromHex(tarea.TextColor)
            };

            navPage.BindingContext = tarea;
            navPage.SetBinding(NavigationPage.BarBackgroundColorProperty, path: "BackgroundColor");
            navPage.SetBinding(NavigationPage.BarTextColorProperty, path: "TextColor");
            await Navigation.PushModalAsync(navPage);
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Eliminar tarea", "¿Estás seguro de eliminar la tarea?", "Aceptar", "Cancelar"))
            {
                ViewModel.EliminarTareaCommand.Execute(((MenuItem)sender).CommandParameter);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new NuevaTarea()));
        }
    }
}