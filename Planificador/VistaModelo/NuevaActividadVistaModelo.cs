using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class NuevaActividadVistaModelo : BaseVistaModelo
    {
        private TareasN _tareasN;
        private ActividadesN _actividadesN;
        private ActividadVistaModelo _nuevaActividad;
        private readonly List<string> _colores = new List<string>()
        {
            "B71C1C",
            "F44336",
            "880E4F",
            "E91E63",
            "4A148C",
            "9C27B0",
            "311B92",
            "673AB7",
            "1A237E",
            "3F51B5",
            "0D47A1",
            "2196F3",
            "01579B",
            "03A9F4",
            "006064",
            "00BCD4",
            "004D40",
            "009688",
            "1B5E20",
            "4CAF50",
            "33691E",
            "8BC34A",
            "827717",
            "CDDC39",
            "FFD600",
            "FFFF00",
            "E65100",
            "FF9800",
            "3E2723",
            "795548",
            "263238",
            "607D8B",
        };
        private Dictionary<int, string> _tareas;
        private readonly INavigation _nav;
        
        public ICommand GuardarActividadCommand { get; private set; }

        public NuevaActividadVistaModelo( INavigation nav)
        {
            _tareasN = new TareasN();
            _actividadesN = new ActividadesN();
            _nuevaActividad = new ActividadVistaModelo(new Modelos.Actividad());
            _nuevaActividad.Dia = DateTime.Now;
            _tareas = new Dictionary<int, string>();
            _nav = nav;
            CargarTareas();
            GuardarActividadCommand = new Command(GuardarActividad);
        }

        private void GuardarActividad()
        {
            var res = _actividadesN.agregarActividad(
                NuevaActividad.HoraInicio,
                NuevaActividad.Dia,
                NuevaActividad.Duracion,
                NuevaActividad.IdTarea!=null?(int)NuevaActividad.IdTarea:-1,
                NuevaActividad.Descripcion,
                NuevaActividad.Titulo,
                NuevaActividad.Color);
            if (res)
            {
                _nav.PopModalAsync();
            }
        }

        public void CargarTareas()
        {
            _tareas.Clear();
            _tareas.Add(-1, "Sin tarea");
            foreach (var tarea in _tareasN.listarTareas())
            {
                _tareas.Add(tarea.id, tarea.titulo);
            }
            RaisePropertyChanged("ListaTareas");
        }

        public List<string> Colores
        {
            get { return _colores; }
        }

        public List<string> ListaTareas
        {
            get
            {
                return _tareas.Values.ToList();
            }
        }

        public int? TareaSeleccionada
        {
            get {
                if (NuevaActividad.IdTarea == null)
                {
                    return 0;
                }
                return _tareas.Keys.ToList().FindIndex(x => x == NuevaActividad.IdTarea); 
            }
            set
            {
                if ((int)value == 0)
                {
                    NuevaActividad.IdTarea = null;
                }
                else
                {
                    NuevaActividad.IdTarea = _tareas.ElementAt((int)value).Key;
                }
                RaisePropertyChanged();
            }
        }

        public ActividadVistaModelo NuevaActividad
        {
            get{ return _nuevaActividad; }
        }

        public DateTime Hoy
        {
            get { return DateTime.Now; }
        }

    }
}
