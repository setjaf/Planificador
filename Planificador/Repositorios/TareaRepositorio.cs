using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Planificador.Modelos;

namespace Planificador.Repositorios
{
    public class TareaRepositorio
    {
        private SQLiteConnection conn;

        public TareaRepositorio()
        {
            conn = ConnectionManager.getConnection();
        }

        public int agregarTarea(Tarea tarea)
        {
            return conn.Insert(tarea);
        }

        public List<Tarea> consultarTareas()
        {
            return conn.Table<Tarea>().ToList();
        }

        public Tarea consultarTarea(int idTarea)
        {
            return conn.Table<Tarea>().Where(x => x.id == idTarea).FirstOrDefault();
        }

        public int editarTarea(Tarea tarea)
        {
            return conn.Update(tarea);
        }

        public int eliminarTarea(int idTarea)
        {
            return conn.Delete<Tarea>(idTarea);
        }
        
    }
}
