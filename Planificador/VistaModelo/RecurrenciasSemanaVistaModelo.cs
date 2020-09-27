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
    public class RecurrenciasSemanaVistaModelo : BaseVistaModelo
    {
        private TareasN _tareasN;
        private int _horaInicio;
        private int _horaFin;
        private ObservableCollection<RecurrenciaVistaModelo> _recurrencias;

        public ICommand CargarRecurrenciasCommand { get; private set; }
        public ICommand EliminarRecurrenciaCommand { get; private set; }

        public RecurrenciasSemanaVistaModelo()
        {
            _tareasN = new TareasN();
            _recurrencias = new ObservableCollection<RecurrenciaVistaModelo>();
            CargarRecurrenciasCommand = new Command(CargarRecurrencias);
            EliminarRecurrenciaCommand = new Command(EliminarRecurrencia);

            CargarRecurrencias();
        }

        private void CargarRecurrencias()
        {
            _recurrencias.Clear();
            var recurrencias = _tareasN.consultarRecurrencias();
            _horaInicio = recurrencias.Count > 0 ? recurrencias[0].horaInicio.Hours : 0;
            foreach ( var recur in recurrencias )
            {
                _recurrencias.Add(new RecurrenciaVistaModelo(recur, _horaInicio * 60));
            }
            RaisePropertyChanged("Recurrencias");
        }

        private void EliminarRecurrencia(object idRecurrencia)
        {
            _tareasN.eliminarRecurrecia((int)idRecurrencia);
            CargarRecurrencias();
        }

        public ObservableCollection<RecurrenciaVistaModelo> Recurrencias
        {
            get { return _recurrencias; }
        }

        public int HoraInicio
        {
            get { return _horaInicio; }
        }

    }
}
