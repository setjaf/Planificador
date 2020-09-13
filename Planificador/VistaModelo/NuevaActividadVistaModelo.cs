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
        private readonly List<string> _colores = Listas.Colores;
        private Dictionary<int, string> _tareas;
        private readonly INavigation _nav;
        private readonly Page _page;

        public ICommand GuardarActividadCommand { get; private set; }

        public NuevaActividadVistaModelo( INavigation nav, Page page, DateTime diaSeleccionado)
        {
            _tareasN = new TareasN();
            _actividadesN = new ActividadesN();
            _nuevaActividad = new ActividadVistaModelo(new Modelos.Actividad());
            _nuevaActividad.Dia = diaSeleccionado;
            _nuevaActividad.HoraInicio = DateTime.Now.TimeOfDay;
            _tareas = new Dictionary<int, string>();
            _nav = nav;
            _page = page;
            CargarTareas();
            GuardarActividadCommand = new Command(GuardarActividad);
        }

        private void GuardarActividad()
        {

            if ( 
                (
                (!String.IsNullOrWhiteSpace(NuevaActividad.Titulo) && !String.IsNullOrWhiteSpace(NuevaActividad.Color)) 
                || NuevaActividad.IdTarea != null) && 
                NuevaActividad.Duracion > 0 )
            {
                var res = _actividadesN.agregarActividad(
                NuevaActividad.HoraInicio,
                NuevaActividad.Dia,
                NuevaActividad.Duracion,
                NuevaActividad.IdTarea != null ? (int)NuevaActividad.IdTarea : -1,
                NuevaActividad.Descripcion,
                NuevaActividad.Titulo,
                NuevaActividad.Color);
                if (res)
                {
                    _nav.PopModalAsync();
                }
                else
                {
                    var traslape = _actividadesN.verificarNuevaActividad(new Modelos.Actividad() { dia=NuevaActividad.Dia, duracion=NuevaActividad.Duracion, horaInicio=NuevaActividad.HoraInicio });
                    if (traslape != null)
                    {
                        _page.DisplayAlert(
                            "Actividad no se ha guardado", 
                            "El día, hora y duración se sobreponen con otra actividad guardada que inicia a las "
                                +traslape.horaInicio.ToString(@"hh\:mm")
                                +" y concluye a las " 
                                +traslape.horaInicio.Add(new TimeSpan(0,traslape.duracion,0)).ToString(@"hh\:mm")
                                + ", selecciona otro horario para poder guardar la actividad", 
                            "Aceptar");
                    }
                    else
                    {
                        _page.DisplayAlert("Actividad no se ha guardado", "La actividad no se ha guardado correctamente, verifica nuevamente los datos.", "Aceptar");
                    }
                    
                }
            }
            else
            {
                _page.DisplayAlert("Datos incompletos", "Verifica que la tarea cuenta con título, una duración mayor o igual a 1 minuto y que has seleccionado un color.","Aceptar");
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
