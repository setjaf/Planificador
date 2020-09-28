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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var cp = (string)((ToolbarItem)sender).CommandParameter;
            var texto = await DisplayPromptAsync("Editar título", "Escribe aquí el nuevo título", "Guardar", "Cancelar", keyboard: Keyboard.Text, initialValue:cp);
            if (!String.IsNullOrEmpty(texto))
                ViewModel.EditarTituloCommand.Execute(texto);
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            var cp = (string)((ToolbarItem)sender).CommandParameter;
            var texto = await DisplayPromptAsync("Editar descripción", "Escribe aquí la nueva descripción", "Guardar", "Cancelar", keyboard: Keyboard.Text, initialValue: cp);
            if (!String.IsNullOrEmpty(texto))
                ViewModel.EditarDescripcionCommand.Execute(texto);
        }

        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            var cp = (string)((ToolbarItem)sender).CommandParameter;
            var texto = await DisplayPromptAsync("Editar color", "Escribe aquí el nuevo color", "Guardar", "Cancelar", keyboard: Keyboard.Text, maxLength:6, initialValue: cp);
            if (!String.IsNullOrEmpty(texto) && texto.Length == 6)
            {
                ViewModel.EditarColorCommand.Execute(texto);
            }                
            else
                await DisplayAlert("Error al guardar el color", "El texto que indica el color debe contener 6 caracteres que representan el color en hexadecimal", "Aceptar");
        }

        private void ToolbarItem_Clicked_3(object sender, EventArgs e)
        {
            var idTarea = (int)((ToolbarItem)sender).CommandParameter;
            Navigation.PushAsync(new NotasPorTarea(idTarea));
        }
    }
}