using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Planificador.Modelos;
using System.Linq;

namespace Planificador.Repositorios
{
    public class ActividadRepositorio
    {
        private SQLiteConnection conn;

        public ActividadRepositorio()
        {
            conn = ConnectionManager.getConnection();
        }

        public int agregarActividad(Actividad actividad)
        {
            return conn.Insert(actividad);
        }

        public Actividad consultarActividad(int idActividad)
        {
            return conn.Table<Actividad>().Where(x => x.id == idActividad).FirstOrDefault();
        }

        public Actividad consultarUltimaActividad()
        {
            return conn.Table<Actividad>().OrderByDescending(x=>x.id).FirstOrDefault();
        }

        public List<Actividad> consultarActividadesPorDia(DateTime dia)
        {
            return conn.Table<Actividad>().ToList().Where(x => x.dia.Date == dia.Date).ToList();
        }

        public List<Actividad> consultarActividadesPorTarea(int idTarea)
        {
            return conn.Table<Actividad>().Where(x => x.idTarea == idTarea).ToList();
        }

        public List<Actividad> eliminarActividadesPorRecurrencia(Recurrencia recur)
        {

            List<Actividad> actividades = conn.Table<Actividad>()
                .ToList()
                .Where(x => x.esRecurrencia 
                    && ((int)x.dia.DayOfWeek == recur.dia) 
                    && (recur.duracion == x.duracion) 
                    && (recur.horaInicio == x.horaInicio)
                    && (x.dia.Add(recur.horaInicio) >= DateTime.Now))
                .ToList();
            
            return actividades;
        }

        public int editarActividad(Actividad actividad)
        {
            return conn.Update(actividad);
        }

        public int eliminarActividad(int idActividad)
        {
            return conn.Delete<Actividad>(idActividad);
        }
    }
}
