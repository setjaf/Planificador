using Planificador.Modelos;
using Planificador.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planificador.Negocio
{
    public class ActividadesN
    {
        private TareaRepositorio _tareaRepo;
        private ActividadRepositorio _actividadRepo;
        private RecurrenciaRepositorio _recurRepo;

        public ActividadesN()
        {
            _tareaRepo = new TareaRepositorio();
            _actividadRepo = new ActividadRepositorio();
            _recurRepo = new RecurrenciaRepositorio();


        }

        public List<Actividad> listarActividades(DateTime dia)
        {
            return _actividadRepo.consultarActividadesPorDia(dia);
        }

        public List<Tarea> listarTareas()
        {
            return _tareaRepo.consultarTareas();
        }

        private bool verificarNuevaActividad(Actividad actividad)
        {

            var actividades = _actividadRepo.consultarActividadesPorDia(actividad.dia);
            foreach (var activ in actividades)
            {
                var horaFin = activ.horaInicio.Add(new TimeSpan(0, activ.duracion, 0));
                if (activ.horaInicio <= actividad.horaInicio && actividad.horaInicio < horaFin)
                {
                    return false;
                }
                else if (activ.horaInicio < actividad.horaInicio.Add(new TimeSpan(0, actividad.duracion, 0)) && actividad.horaInicio.Add(new TimeSpan(0, actividad.duracion, 0)) <= horaFin)
                {
                    return false;
                }
            }

            return true;
        }

        public bool agregarActividad(TimeSpan horaInicio, DateTime dia, int duracion, int idTarea = -1, string descripcion = null, string titulo = null, string color = null)
        {
            var nuevaActividad = new Actividad()
            {
                dia = dia,
                duracion = duracion,
                horaInicio = horaInicio,
                idTarea = null,
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

            if (verificarNuevaActividad(nuevaActividad))
            {
                if (_actividadRepo.agregarActividad(nuevaActividad) > 0)
                    return true;
            }
            return false;
        }

        public bool modificarActividad(int idActividad,TimeSpan horaInicio, DateTime dia, int duracion, string descripcion = null, string titulo = null, string color = null)
        {
            var actividad = _actividadRepo.consultarActividad(idActividad);

            actividad.horaInicio = horaInicio;
            actividad.duracion = duracion;
            actividad.dia = dia;
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
            if (_actividadRepo.eliminarActividad(idActividad)>0)
                return true;
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
