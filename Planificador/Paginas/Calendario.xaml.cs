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
    }
}