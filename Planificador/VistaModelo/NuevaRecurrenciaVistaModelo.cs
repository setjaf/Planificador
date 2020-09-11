using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class NuevaRecurrenciaVistaModelo : BaseVistaModelo
    {
        private readonly TareasN _tareasN;
        private int idTarea;
        private int _dia;
        private TimeSpan _horaInicio;
        private string _duracion;
        private readonly INavigation _navigation;
        private readonly Page _page;

        public TareaVistaModelo TareaActual { get; private set; }
        public ICommand GuardarRecurrenciaCommand { get; private set; }
        public ICommand EliminarRecurrenciaCommand { get; private set; }

        public NuevaRecurrenciaVistaModelo(TareaVistaModelo tarea, INavigation nav, Page page)
        {
            _duracion = "0";
            _page = page;
            TareaActual = tarea;
            TareaActual.CargarRecurrencias();
            _navigation = nav;
            _tareasN = new TareasN();
            GuardarRecurrenciaCommand = new Command(GuardarRecurrencia);
            EliminarRecurrenciaCommand = new Command(EliminarRecurrencia);
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
    }
}
