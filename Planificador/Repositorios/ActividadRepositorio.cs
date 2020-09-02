using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Planificador.Modelos;

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

        public List<Actividad> consultarActividadesPorDia(DateTime dia)
        {
            return conn.Table<Actividad>().Where(x => x.dia.Date.Equals(dia.Date)).ToList();
        }

        public List<Actividad> consultarActividadesPorTarea(int idTarea)
        {
            return conn.Table<Actividad>().Where(x => x.idTarea == idTarea).ToList();
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
