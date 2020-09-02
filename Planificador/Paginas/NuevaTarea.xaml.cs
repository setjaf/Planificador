using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planificador.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Planificador.Modelos;

namespace Planificador.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevaTarea : ContentPage
    {
        public NuevaTarea()
        {
            InitializeComponent();

            this.BindingContext = new VistaModelo.NuevaTareaVistaModelo(Navigation);
        }
    }
}