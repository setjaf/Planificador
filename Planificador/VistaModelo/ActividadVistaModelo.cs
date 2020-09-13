using Planificador.Modelos;
using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class ActividadVistaModelo : BaseVistaModelo
    {
        private int _id;
        private int? _idTarea;
        private DateTime _dia;
        private TimeSpan _horaInicio;
        private int _duracion;
        private string? _titulo;
        private string? _descripcion;
        private string? _color;
        private TareasN _tareasN;
        private ObservableCollection<ObjetivoVistaModelo> _objetivos;

        private double tsAEntero( TimeSpan hora)
        {
            return hora.Hours + (hora.Minutes / 60);
        }

        public ActividadVistaModelo(Actividad actividad)
        {
            _tareasN = new TareasN();
            _id = actividad.id;

            _dia = actividad.dia;
            _horaInicio = actividad.horaInicio;
            _duracion = actividad.duracion;
            
            _idTarea = actividad.idTarea;

            if (_idTarea != null)
            {
                Tarea tarea = _tareasN.consultarTarea((int)_idTarea);
                _titulo = tarea.titulo;
                _descripcion = tarea.descripcion;
                _color = tarea.color;
                _objetivos = new ObservableCollection<ObjetivoVistaModelo>();
                CargarObjetivos();
            }
            else
            {
                _titulo = actividad.titulo;
                _descripcion = actividad.descripcion;
                _color = actividad.color;
            }

        }

        public int Id
        {
            get { return _id; }
        }

        public int? IdTarea
        {
            get { return _idTarea; }
            set { 
                if (SetPropertyValue(ref _idTarea, value)) 
                    RaisePropertyChanged("EsTarea"); 
            }
        }

        public DateTime Dia
        {
            get { return _dia; }
            set
            {
                if(_dia != value)
                {
                    _dia = value;
                    RaisePropertyChanged();
                }
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
                    RaisePropertyChanged("Posicion");
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
                RaisePropertyChanged("Posicion");
            }
        }

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                SetPropertyValue(ref _titulo, value);
            }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                SetPropertyValue(ref _descripcion, value);
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                if(SetPropertyValue(ref _color, value))
                {
                    RaisePropertyChanged("TextColor");
                    RaisePropertyChanged("BackgroundColor");
                    if(_id != 0)
                        new ActividadesN().modificarActividad(_id, _horaInicio, _dia, _duracion, color: _color);
                }
            }
        }

        public Rectangle Posicion
        {
            //get { return String.Format("{0},{1},{2},{3}", 0, (int)(tsAEntero(_horaInicio) * 60), 1, _duracion); }
            get { return new Rectangle(0, (int)(tsAEntero(_horaInicio) * 60), 1, _duracion); }
        }

        public string TextColor
        {
            get { return String.Format("#{0}", "FAFAFA"); }
        }

        public string BackgroundColor
        {
            get { return String.Format("#{0}", _color);}
            // return String.Format("#59{0}",_color); 
        }
                
        public bool EsTarea
        {
            get
            {
                if (_idTarea == null)
                    return false;
                return true;
            }
        }

        public ObservableCollection<ObjetivoVistaModelo> Objetivos
        {
            get
            {
                return _objetivos;
            }
        }

        public void CargarObjetivos()
        {
            _objetivos.Clear();
            foreach (var objetivo in new TareasN().consultarObjetivos((int)_idTarea))
            {
                _objetivos.Add(new ObjetivoVistaModelo(objetivo));
            }
            RaisePropertyChanged(nameof(Objetivos));
        }

    }
}
