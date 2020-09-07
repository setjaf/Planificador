using Planificador.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Planificador.VistaModelo
{
    public class RecurenciasPorDiaVistaModelo : BaseVistaModelo
    {
        private readonly ObservableCollection<RecurrenciaVistaModelo> _lunes;
        private readonly ObservableCollection<RecurrenciaVistaModelo> _martes;
        private readonly ObservableCollection<RecurrenciaVistaModelo> _miercoles;
        private readonly ObservableCollection<RecurrenciaVistaModelo> _jueves;
        private readonly ObservableCollection<RecurrenciaVistaModelo> _viernes;
        private readonly ObservableCollection<RecurrenciaVistaModelo> _sabado;
        private readonly ObservableCollection<RecurrenciaVistaModelo> _domingo;

        public RecurenciasPorDiaVistaModelo()
        {
            _lunes = new ObservableCollection<RecurrenciaVistaModelo>();
            _martes = new ObservableCollection<RecurrenciaVistaModelo>();
            _miercoles = new ObservableCollection<RecurrenciaVistaModelo>();
            _jueves = new ObservableCollection<RecurrenciaVistaModelo>();
            _viernes = new ObservableCollection<RecurrenciaVistaModelo>();
            _sabado = new ObservableCollection<RecurrenciaVistaModelo>();
            _domingo = new ObservableCollection<RecurrenciaVistaModelo>();
        }

        public ObservableCollection<RecurrenciaVistaModelo> Lunes
        {
            get { return _lunes; }
        }

        public ObservableCollection<RecurrenciaVistaModelo> Martes
        {
            get { return _martes; }
        }

        public ObservableCollection<RecurrenciaVistaModelo> Miercoles
        {
            get { return _miercoles; }
        }
        public ObservableCollection<RecurrenciaVistaModelo> Jueves
        {
            get { return _jueves; }
        }
        public ObservableCollection<RecurrenciaVistaModelo> Viernes
        {
            get { return _viernes; }
        }
        public ObservableCollection<RecurrenciaVistaModelo> Sabado
        {
            get { return _sabado; }
        }
        public ObservableCollection<RecurrenciaVistaModelo> Domingo
        {
            get { return _domingo; }
        }

        public void CargarRecurrencias(List<Recurrencia> recurrencias)
        {
            LimpiarDias();
            foreach (var recurrencia in recurrencias)
            {
                switch (recurrencia.dia)
                {
                    case (int)DayOfWeek.Monday:
                        _lunes.Add(new RecurrenciaVistaModelo(recurrencia));
                        break;
                    case (int)DayOfWeek.Tuesday:
                        _martes.Add(new RecurrenciaVistaModelo(recurrencia));
                        break;
                    case (int)DayOfWeek.Wednesday:
                        _miercoles.Add(new RecurrenciaVistaModelo(recurrencia));
                        break;
                    case (int)DayOfWeek.Thursday:
                        _jueves.Add(new RecurrenciaVistaModelo(recurrencia));
                        break;
                    case (int)DayOfWeek.Friday:
                        _viernes.Add(new RecurrenciaVistaModelo(recurrencia));
                        break;
                    case (int)DayOfWeek.Saturday:
                        _sabado.Add(new RecurrenciaVistaModelo(recurrencia));
                        break;
                    case (int)DayOfWeek.Sunday:
                        _domingo.Add(new RecurrenciaVistaModelo(recurrencia));
                        break;
                    default:
                        break;
                }
            }
            RaiseAllPropertiesChanged();
        }
        public void LimpiarDias()
        {
            _lunes.Clear();
            _martes.Clear();
            _miercoles.Clear();
            _jueves.Clear();
            _viernes.Clear();
            _sabado.Clear();
            _domingo.Clear();
        }

    }
}
