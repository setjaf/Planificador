using Planificador.VistaModelo;
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
    public partial class TareaDetalle : ContentPage
    {
        public TareaDetalle(TareaVistaModelo tareaVistaModelo)
        {
            ViewModel = new TareaDetalleVistaModelo( tareaVistaModelo);
            InitializeComponent();
        }

        public TareaDetalleVistaModelo ViewModel
        {
            get { return BindingContext as TareaDetalleVistaModelo; }
            set { BindingContext = value; }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            if(await DisplayAlert("Eliminar objetivo", "¿Estás seguro de eliminar el objetivo?","Sí", "No"))
            {
                ViewModel.EliminarObjetivoCommand.Execute(mi.CommandParameter);
                ViewModel.RefrescarObjetivosCommand.Execute(null);
            }
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var texto = await DisplayPromptAsync("Nuevo objetivo", "Escribe aquí el nuevo objetivo","Guardar","Cancelar",keyboard:Keyboard.Text);
            if(!String.IsNullOrEmpty(texto))
                ViewModel.AgregarObjetivoCommand.Execute(texto);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NuevaRecurrencia(ViewModel.TareaActual));
        }

        private async void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            if (await DisplayAlert("Eliminar recurrencia", "¿Estás seguro de eliminar la recurrencia?", "Aceptar", "Cancelar"))
                ViewModel.EliminarRecurrenciaCommand.Execute(((MenuItem)sender).CommandParameter);
        }
    }
}