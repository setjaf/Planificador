using Planificador.Modelos;
using Planificador.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.LocalNotifications;

namespace Planificador.Negocio
{
    public class ActividadesN
    {
        private TareaRepositorio _tareaRepo;
        private ActividadRepositorio _actividadRepo;
        private RecurrenciaRepositorio _recurRepo;
        private RecurrenciasCargadasRepositorio _recurCarRepo;

        public ActividadesN()
        {
            _tareaRepo = new TareaRepositorio();
            _actividadRepo = new ActividadRepositorio();
            _recurRepo = new RecurrenciaRepositorio();
            _recurCarRepo = new RecurrenciasCargadasRepositorio();


        }

        public List<Actividad> listarActividades(DateTime dia)
        {
            var resultado = _recurCarRepo.consultarRecurrenciasCargadas(dia);
            if ( (resultado == null) && (dia >= DateTime.Now.Date))
            {
                cargarRecurrencias(dia);                
                int valor=_recurCarRepo.agregarRecurrenciasCargadas(new RecurrenciasCargadas() { dia = dia.Date });
            }
            return _actividadRepo.consultarActividadesPorDia(dia);
        }

        public List<Actividad> listarActividades(int idTarea)
        {
            return _actividadRepo.consultarActividadesPorTarea(idTarea);
        }

        public List<Tarea> listarTareas()
        {
            return _tareaRepo.consultarTareas();
        }

        public Actividad verificarNuevaActividad(Actividad actividad)
        {

            var actividades = _actividadRepo.consultarActividadesPorDia(actividad.dia);
            foreach (var activ in actividades)
            {
                var horaFin = activ.horaInicio.Add(new TimeSpan(0, activ.duracion, 0));
                if (activ.horaInicio <= actividad.horaInicio && actividad.horaInicio < horaFin)
                {
                    return activ;
                }
                else if (activ.horaInicio < actividad.horaInicio.Add(new TimeSpan(0, actividad.duracion, 0)) && actividad.horaInicio.Add(new TimeSpan(0, actividad.duracion, 0)) <= horaFin)
                {
                    return activ;
                }
            }

            return null;
        }

        public bool agregarActividad(TimeSpan horaInicio, DateTime dia, int duracion, int idTarea = -1, string descripcion = null, string titulo = null, string color = null, bool esRecur = false, string? comentarios = null)
        {
            var nuevaActividad = new Actividad()
            {
                dia = dia,
                duracion = duracion,
                horaInicio = horaInicio,
                idTarea = null,
                esRecurrencia = esRecur,
                comentarios = comentarios,
            };
            if (idTarea == -1)
            {
                nuevaActividad.descripcion = descripcion;
                nuevaActividad.titulo = titulo;
                nuevaActividad.color = color;
            }
            else
            {
                nuevaActividad.idTarea = idTarea;
            }

            if (verificarNuevaActividad(nuevaActividad) == null)
            {
                if (_actividadRepo.agregarActividad(nuevaActividad) > 0)
                {
                    var nActividad = _actividadRepo.consultarUltimaActividad();

                    if (idTarea!=-1)
                    {
                        var tarea = _tareaRepo.consultarTarea(idTarea);
                        titulo = tarea.titulo;
                        descripcion = tarea.descripcion;
                    }

                    string tituloInicio = "Por iniciar " + titulo;
                    string mensajeInicio = descripcion;
                    DateTime fechaNotificacion = dia.Date.AddHours(horaInicio.Hours).AddMinutes(horaInicio.Minutes).AddSeconds(-10);

                    CrossLocalNotifications.Current.Show(tituloInicio,mensajeInicio,nActividad.id,fechaNotificacion);
                    return true;
                }
            }
            return false;
        }

        public bool modificarActividad(int idActividad,TimeSpan horaInicio, DateTime dia, int duracion, string descripcion = null, string titulo = null, string color = null, string? comentarios = null)
        {
            var actividad = _actividadRepo.consultarActividad(idActividad);

            actividad.horaInicio = horaInicio;
            actividad.duracion = duracion;
            actividad.dia = dia;
            actividad.comentarios = comentarios;
            actividad.descripcion = descripcion ?? actividad.descripcion;
            actividad.titulo = titulo ?? actividad.titulo;
            actividad.color = color ?? actividad.color;

            if (_actividadRepo.editarActividad(actividad)>0)
            {
                return true;
            }

            return false;
        }

        public bool eliminarActividad(int idActividad)
        {
            if (_actividadRepo.eliminarActividad(idActividad) > 0)
            {
                CrossLocalNotifications.Current.Cancel(idActividad);
                return true;
            }
            return false;
        }

        public int cargarRecurrencias(DateTime dia)
        {
            int recurContador = 0;
            var recurrencias = _recurRepo.consultarRecurrenciasPorDia((int)dia.DayOfWeek);
            foreach (var recurrencia in recurrencias)
            {
                if (cargarRecurrencia(recurrencia, dia))
                    recurContador++;
            }
            return recurContador;
        }

        public bool cargarRecurrencia(Recurrencia recurrencia, DateTime dia)
        {
            if (agregarActividad(recurrencia.horaInicio, dia, recurrencia.duracion, recurrencia.idTarea))
                return true;
            return false;
        }


    }
}
