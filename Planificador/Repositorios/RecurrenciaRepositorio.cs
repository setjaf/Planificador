using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Planificador.Modelos;

namespace Planificador.Repositorios
{
    public class RecurrenciaRepositorio
    {
        private SQLiteConnection conn;

        public RecurrenciaRepositorio()
        {
            conn = ConnectionManager.getConnection();
        }

        public int agregarRecurrencia(Recurrencia recurrencia)
        {
            return conn.Insert(recurrencia);
        }

        public List<Recurrencia> consultarRecurrencias(int idTarea)
        {
            return conn.Table<Recurrencia>().Where(x => x.idTarea == idTarea).OrderBy(x => x.horaInicio).ToList();
        }

        public List<Recurrencia> consultarRecurrenciasPorDia(int dia)
        {
            return conn.Table<Recurrencia>().Where(x => x.dia == dia).OrderBy(x => x.horaInicio).ToList();
        }

        public List<Recurrencia> consultarRecurrencias()
        {
            return conn.Table<Recurrencia>().OrderBy(x => x.horaInicio).ToList();
        }

        public Recurrencia consultarRecurrencia(int idRecurrencia)
        {
            return conn.Table<Recurrencia>().Where(x => x.id == idRecurrencia).FirstOrDefault();
        }

        public int editarRecurrencia(Recurrencia recurrencia)
        {
            return conn.Update(recurrencia);
        }

        public int eliminarRecurrencia(int idRecurrencia)
        {
            return conn.Delete<Recurrencia>(idRecurrencia);
        }
    }
}
