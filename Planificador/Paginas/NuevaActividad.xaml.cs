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
    public partial class NuevaActividad : ContentPage
    {
        public NuevaActividad(DateTime diaSeleccionado)
        {
            ViewModel = new NuevaActividadVistaModelo(Navigation, this, diaSeleccionado);
            InitializeComponent();
        }

        public NuevaActividadVistaModelo ViewModel
        {
            get { return BindingContext as NuevaActividadVistaModelo; }
            set { BindingContext = value; }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}