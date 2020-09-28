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
    public partial class NotasPorTarea : ContentPage
    {
        public NotasPorTarea(int idTarea)
        {
            ViewModel = new NotasPorTareaVistaModelo(idTarea);
            InitializeComponent();
        }

        public NotasPorTareaVistaModelo ViewModel
        {
            get { return BindingContext as NotasPorTareaVistaModelo; }
            set { BindingContext = value; }
        }
    }
}