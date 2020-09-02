﻿using Planificador.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planificador.VistaModelo
{
    public class RecurrenciaVistaModelo : BaseVistaModelo
    {
        readonly int _id;
        readonly int _idTarea;
        private int _dia;
        private TimeSpan _horaInicio;
        private int _duracion;

        public RecurrenciaVistaModelo(Recurrencia recurrencia)
        {
            _id = recurrencia.id;
            _idTarea = recurrencia.idTarea;
            _dia = recurrencia.dia;
            _horaInicio = recurrencia.horaInicio;
            _duracion = recurrencia.duracion;
        }

        public int Id
        {
            get { return _id; }
        }

        public int IdTarea
        {
            get { return _idTarea; }
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
                if(_horaInicio != value)
                {
                    _horaInicio = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TimeSpan HoraFin
        {
            get { return _horaInicio.Add(new TimeSpan(0, _duracion, 0)); }
        }

        public int Duracion
        {
            get { return _duracion; }
            set
            {
                SetPropertyValue(ref _duracion, value);
            }
        }
    }
}
