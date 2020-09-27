using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class NuevaRecurrenciaVistaModelo : BaseVistaModelo
    {
        private readonly TareasN _tareasN;
        private int _dia;
        private TimeSpan _horaInicio;
        private string _duracion;
        private readonly INavigation _navigation;
        private readonly Page _page;
        private Dictionary<int, string> _tareas;
        private bool _pickerVisible;

        public TareaVistaModelo TareaActual { get; private set; }
        public ICommand GuardarRecurrenciaCommand { get; private set; }
        public ICommand EliminarRecurrenciaCommand { get; private set; }

        public NuevaRecurrenciaVistaModelo(TareaVistaModelo tarea, INavigation nav, Page page)
        {
            _duracion = "0";
            _page = page;
            TareaActual = tarea;
            _pickerVisible = true;
            if (TareaActual != null)
            {
                _pickerVisible = false;
                TareaActual.CargarRecurrencias();
            }
            _navigation = nav;
            _tareasN = new TareasN();
            GuardarRecurrenciaCommand = new Command(GuardarRecurrencia);
            EliminarRecurrenciaCommand = new Command(EliminarRecurrencia);
            _tareas = new Dictionary<int, string>();
            CargarTareas();
        }

        private async void GuardarRecurrencia()
        {
            var resultado = _tareasN.agregarRecurrenciaATarea(TareaActual.Id, _dia, _horaInicio, Convert.ToInt32(_duracion));
            if (resultado == null)
            {
                TareaActual.CargarRecurrencias();
            }
            else
                await _page.DisplayAlert("Error al registrar recurrencia", "La recurrencia tiene un traslape con la tarea " + _tareasN.consultarTarea(resultado.idTarea).titulo + "." ,"Aceptar");
                
        }

        public void CargarTareas()
        {
            _tareas.Clear();
            foreach (var tarea in _tareasN.listarTareas())
            {
                _tareas.Add(tarea.id, tarea.titulo);
            }
            if(TareaActual == null)
            {
                TareaSeleccionada = 0;
            }
            RaisePropertyChanged("ListaTareas");
        }

        private void EliminarRecurrencia(object idObjetivo)
        {
            _tareasN.eliminarRecurrecia((int)idObjetivo);
            TareaActual.CargarRecurrencias();
        }
        public int Dia
        {
            get { return _dia; }
            set
            {
                SetPropertyValue(ref _dia, value);
            }
        }

        public TimeSpan HoraInicio
        {
            get { return _horaInicio; }
            set
            {
                if (_horaInicio != value)
                {
                    _horaInicio = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Duracion
        {
            get { return _duracion; }
            set
            {
                SetPropertyValue(ref _duracion, value);
            }
        }

        public List<string> ListaTareas
        {
            get
            {
                var value = _tareas.Values.ToList();
                return value;
            }
        }

        public int TareaSeleccionada
        {
            get
            {
                if (TareaActual == null)
                {
                    return 0;
                }
                var value = _tareas.Keys.ToList().FindIndex(x => x == TareaActual.Id);
                return value;
            }
            set
            {
                TareaActual = new TareaVistaModelo(_tareasN.consultarTarea(_tareas.ElementAt((int)value).Key));
                TareaActual.CargarRecurrencias();               
                RaisePropertyChanged(nameof(TareaActual));
                RaisePropertyChanged();
            }
        }

        public bool pickerVisible
        {
            get
            {
                return _pickerVisible;
            }
        }
    }
}
