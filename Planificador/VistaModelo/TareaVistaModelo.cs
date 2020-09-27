using Planificador.Modelos;
using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class TareaVistaModelo : BaseVistaModelo
    {
        private int _id;
        private string _titulo;
        private string _descripcion;
        private DateTime _creacionFecha;
        private string _color;
        private readonly ObservableCollection<ObjetivoVistaModelo> _objetivos;
        private readonly RecurenciasPorDiaVistaModelo _recurrencias;

        public TareaVistaModelo(Tarea tarea)
        {
            _id = tarea.id;
            _titulo = tarea.titulo;
            _descripcion = tarea.descripcion;
            _creacionFecha = tarea.creacionFecha;
            _color = tarea.color;
            _objetivos = new ObservableCollection<ObjetivoVistaModelo>();
            _recurrencias = new RecurenciasPorDiaVistaModelo();
            CargarObjetivos();
            CargarRecurrencias();
            //
        }

        public int Id
        {
            get { return this._id; }

            set
            {
                SetPropertyValue(ref _id, value);
            }
        }

        public string Titulo
        {
            get { return this._titulo; }

            set
            {
                SetPropertyValue(ref _titulo, value);
            }
        }

        public string Descripcion
        {
            get { return this._descripcion; }

            set
            {
                SetPropertyValue(ref _descripcion, value);
            }
        }

        public DateTime CreacionFecha
        {
            get { return _creacionFecha; }
            set
            {
                if (_creacionFecha != value)
                {
                    _creacionFecha = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Color
        {
            get { return this._color; }
            set
            {
                if(SetPropertyValue(ref _color, value))
                {
                    RaisePropertyChanged("TextColor");
                    RaisePropertyChanged("BackgroundColor");
                    if(_id != 0)
                        new TareasN().modificarTarea(_id,_titulo,_descripcion,_color);
                }
            }
        }

        public string TextColor
        {
            get { return String.Format("#{0}", "FAFAFA"); }
        }

        public string BackgroundColor
        {
            get { return String.Format("#{0}", this._color); }
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
            foreach (var objetivo in new TareasN().consultarObjetivos(_id))
            {
                _objetivos.Add(new ObjetivoVistaModelo(objetivo));
            }
            RaisePropertyChanged(nameof(Objetivos));
        }

        public RecurenciasPorDiaVistaModelo Recurrencias
        {
            get
            {
                return _recurrencias;
            }
        }

        public void CargarRecurrencias()
        {
            _recurrencias.CargarRecurrencias(new TareasN().consultarRecurrencias(_id));
            RaisePropertyChanged(nameof(Recurrencias));
            RaisePropertyChanged(nameof(BackgroundColor));
        }
    }
}
