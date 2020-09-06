using System;
using System.Collections.Generic;
using System.Text;
using Planificador.Modelos;
using Planificador.Repositorios;

namespace Planificador.Negocio
{
    public class TareasN
    {
        private TareaRepositorio _tareaRepo;
        private ObjetivoRepositorio _objetivoRepo;
        private RecurrenciaRepositorio _recurrenciaRepo;

        public TareasN()
        {
            _tareaRepo = new TareaRepositorio();
            _objetivoRepo = new ObjetivoRepositorio();
            _recurrenciaRepo = new RecurrenciaRepositorio();
        }

        public List<Tarea> listarTareas()
        {
            return _tareaRepo.consultarTareas();
        }

        public bool agregarTarea(string titulo,string descripcion, string color)
        {
            Tarea nuevaTarea = new Tarea()
            {
                titulo = titulo,
                descripcion = descripcion,
                color = color,
                creacionFecha = DateTime.Now,
            };

            if (_tareaRepo.agregarTarea(nuevaTarea)>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tarea consultarTarea(int IdTarea)
        {
            Tarea tarea = _tareaRepo.consultarTarea(IdTarea);

            tarea.objetivos = this.consultarObjetivos(tarea.id);
            tarea.recurrencias = this.consultarRecurrencias(tarea.id);

            return tarea;
        }

        public bool eliminarTarea(int IdTarea)
        {
            if (_tareaRepo.eliminarTarea(IdTarea)>0)
            {
                foreach (var objetivo in this.consultarObjetivos(IdTarea))
                {
                    this.eliminarObjetivo(objetivo.id);
                }
                foreach (var recurrencia in this.consultarRecurrencias(IdTarea))
                {
                    this.eliminarObjetivo(recurrencia.id);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool modificarTarea(int IdTarea, string titulo,string descripcion, string color)
        {
            Tarea nuevaTarea = _tareaRepo.consultarTarea(IdTarea);
            nuevaTarea.titulo = titulo;
            nuevaTarea.descripcion = descripcion;
            nuevaTarea.color = color;
            if (_tareaRepo.editarTarea(nuevaTarea) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool agregarObjetivoATarea(int IdTarea, string objetivo)
        {
            Objetivo nuevoObjetivo = new Objetivo()
            {
                idTarea = IdTarea,
                objetivo = objetivo,
                creacionFecha = DateTime.Now,
            };

            if (_objetivoRepo.agregarObjetivo(nuevoObjetivo) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Recurrencia? agregarRecurrenciaATarea(int IdTarea, int dia, TimeSpan horaInicio, int duracion)
        {
            Recurrencia nuevaRecurrencia = new Recurrencia()
            {
                idTarea = IdTarea,
                dia = dia,
                horaInicio = horaInicio,
                duracion = duracion,
            };
            var resultado = verificarNuevaRecurrencia(nuevaRecurrencia);
            if ( resultado == null)
                if (_recurrenciaRepo.agregarRecurrencia(nuevaRecurrencia) > 0)
                    return null;
            
            return resultado;
        }

        public Recurrencia? verificarNuevaRecurrencia(Recurrencia recurrencia)
        {
            Recurrencia recurError = null;
            foreach(var recur in _recurrenciaRepo.consultarRecurrencias())
            {
                if(recur.dia == recurrencia.dia)
                {
                    var horaFin = recur.horaInicio.Add(new TimeSpan(0, recur.duracion, 0));
                    if (recur.horaInicio <= recurrencia.horaInicio && recurrencia.horaInicio < horaFin)
                    {
                        recurError = recur;
                        break;
                    }
                    else if (recur.horaInicio < recurrencia.horaInicio.Add(new TimeSpan(0,recurrencia.duracion,0)) && recurrencia.horaInicio.Add(new TimeSpan(0, recurrencia.duracion, 0)) <= horaFin)
                    {
                        recurError = recur;
                        break;
                    }                      

                }
            }
            return recurError;
        }

        public bool eliminarObjetivo(int IdObjetivo)
        {
            if (_objetivoRepo.eliminarObjetivo(IdObjetivo) > 0)
                return true;
            else
                return false;            
        }

        public bool eliminarRecurrecia(int IdRecurrencia)
        {
            if (_recurrenciaRepo.eliminarRecurrencia(IdRecurrencia) > 0)
                return true;
            else
                return false;
        }

        public bool modificarObjetivo(int IdObjetivo, string objetivo = null, DateTime? finalizado = null)
        {
            Objetivo nuevoObejtivo = _objetivoRepo.consultarObjetivo(IdObjetivo);
            if (objetivo != null)
                nuevoObejtivo.objetivo = objetivo;
            if (finalizado != null)
                nuevoObejtivo.finalizacionFecha = finalizado;
            else
                nuevoObejtivo.finalizacionFecha = null;

            if (_objetivoRepo.editarObjetivo(nuevoObejtivo) > 0)
                return true;
            else
                return false;
        }

        public bool modificarRecurrencia(int IdRecurrencia, int dia, TimeSpan horaInicio, int duracion)
        {
            Recurrencia nuevaRecurrencia = _recurrenciaRepo.consultarRecurrencia(IdRecurrencia);
            nuevaRecurrencia.dia = dia;
            nuevaRecurrencia.horaInicio = horaInicio;
            nuevaRecurrencia.duracion = duracion;

            if (_recurrenciaRepo.editarRecurrencia(nuevaRecurrencia) > 0)
                return true;
            else
                return false;

        }

        public List<Objetivo> consultarObjetivos(int IdTarea)
        {
            return _objetivoRepo.consultarObjetivos(IdTarea);
        } 

        public List<Recurrencia> consultarRecurrencias(int IdTarea)
        {
            return _recurrenciaRepo.consultarRecurrencias(IdTarea);
        }
    }
}
