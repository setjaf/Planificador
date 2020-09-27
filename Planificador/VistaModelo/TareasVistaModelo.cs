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
        private bool _isRefreshing;
        private TareasN _tareaNegocio;
        public ObservableCollection<TareaVistaModelo> ListaTareas { get; private set; }
        public ICommand CargarTareasCommand { get; private set; }
        public ICommand EliminarTareaCommand { get; private set; }

        public TareasVistaModelo(INavigation navigation)
        {
            _tareaNegocio = new TareasN();
            _isRefreshing = false;

            CargarTareasCommand = new Command(CargarTareas);
            EliminarTareaCommand = new Command(EliminarTarea);

            ListaTareas = new ObservableCollection<TareaVistaModelo>();
        }

        public void CargarTareas()
        {
            _isRefreshing = true;
            ListaTareas.Clear();
            foreach (var tarea in _tareaNegocio.listarTareas())
            {
                ListaTareas.Add(new TareaVistaModelo(tarea));
            }
            _isRefreshing = false;
            RaisePropertyChanged("IsRefreshing");
        }

        public void EliminarTarea(object idTarea)
        {
            _tareaNegocio.eliminarTarea((int)idTarea);
            CargarTareas();
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; RaisePropertyChanged(); }
        }
    }
}
