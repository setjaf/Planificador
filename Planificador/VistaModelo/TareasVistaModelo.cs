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
        public ObservableCollection<TareaVistaModelo> ListaTareas { get; private set; }
        public ICommand CargarTareasCommand { get; private set; }
        public ICommand EliminarTareaCommand { get; private set; }

        public TareasVistaModelo(INavigation navigation)
        {
            _tareaNegocio = new TareasN();

            CargarTareasCommand = new Command(CargarTareas);
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

        public void EliminarTarea(object idTarea)
        {
            _tareaNegocio.eliminarTarea((int)idTarea);
            CargarTareas();
        }
    }
}
