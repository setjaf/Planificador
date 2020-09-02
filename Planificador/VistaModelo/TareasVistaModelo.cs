using Planificador.Modelos;
using Planificador.Negocio;
using Planificador.Paginas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class TareasVistaModelo : BaseVistaModelo
    {
        private TareasN _tareaNegocio;
        private INavigation _navigation;
        public ObservableCollection<TareaVistaModelo> ListaTareas { get; private set; }
        public ICommand AbrirNuevaTareaCommand { get; private set; }
        public ICommand CargarTareasCommand { get; private set; }
        public ICommand EliminarTareaCommand { get; private set; }

        public TareasVistaModelo(INavigation navigation)
        {
            _tareaNegocio = new TareasN();

            _navigation = navigation;

            CargarTareasCommand = new Command(CargarTareas);
            AbrirNuevaTareaCommand = new Command(AbrirNuevaTarea);
            EliminarTareaCommand = new Command(EliminarTarea);

            ListaTareas = new ObservableCollection<TareaVistaModelo>();
        }

        public void CargarTareas()
        {
            ListaTareas.Clear();
            foreach (var tarea in _tareaNegocio.listarTareas())
            {
                ListaTareas.Add(new TareaVistaModelo(tarea));
            } 
        }

        public void AbrirNuevaTarea()
        {
            _navigation.PushModalAsync(new NuevaTarea());
        }

        public void EliminarTarea(object idTarea)
        {
            _tareaNegocio.eliminarTarea((int)idTarea);
            CargarTareas();
        }
    }
}
