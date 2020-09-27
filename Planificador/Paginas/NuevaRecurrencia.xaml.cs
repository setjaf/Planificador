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
    public partial class NuevaRecurrencia : ContentPage
    {
        public NuevaRecurrencia(TareaVistaModelo tareaActual)
        {
            ViewModel = new NuevaRecurrenciaVistaModelo(tareaActual, Navigation, this);
            if (tareaActual == null)
            {
                ViewModel.TareaSeleccionada = 0;
            }
            InitializeComponent();
        }

        public NuevaRecurrenciaVistaModelo ViewModel
        {
            get { return BindingContext as NuevaRecurrenciaVistaModelo; }
            set { BindingContext = value; }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Eliminar recurrencia", "¿Estás seguro de eliminar la recurrencia?", "Aceptar", "Cancelar"))
                ViewModel.EliminarRecurrenciaCommand.Execute(((MenuItem)sender).CommandParameter);
        }
    }
}