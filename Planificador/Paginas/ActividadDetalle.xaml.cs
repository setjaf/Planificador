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
    public partial class ActividadDetalle : ContentPage
    {
        public ActividadDetalle( ActividadVistaModelo actividad)
        {
            ViewModel = new ActividadDetalleVistaModelo(actividad, Navigation);
            InitializeComponent();
        }

        public ActividadDetalleVistaModelo ViewModel
        {
            get { return BindingContext as ActividadDetalleVistaModelo; }
            set
            {
                BindingContext = value;
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            var resultado = await DisplayPromptAsync("Editar título", "Escribe aquí en nuevo título para la actividad", "Guardar", "Cancelar",keyboard:Keyboard.Text,initialValue:(string)((Button)sender).CommandParameter);
            if (!String.IsNullOrWhiteSpace(resultado))
                ViewModel.EditarTituloCommand.Execute(resultado);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var resultado = await DisplayActionSheet("¿Estas seguro de querer eliminar la actividad?", "Cancelar", "Eliminar");
            var resBool = resultado.Equals("Eliminar");
            if (resBool)
            {
                ViewModel.EliminarActividadCommand.Execute(null);
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var idTarea = (int)((ToolbarItem)sender).CommandParameter;
            Navigation.PushAsync(new NotasPorTarea(idTarea));
        }
    }
}