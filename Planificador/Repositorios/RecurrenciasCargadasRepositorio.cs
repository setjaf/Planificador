using Planificador.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planificador.Repositorios
{
    public class RecurrenciasCargadasRepositorio
    {
        private SQLiteConnection conn;

        public RecurrenciasCargadasRepositorio()
        {
            conn = ConnectionManager.getConnection();
        }

        public int agregarRecurrenciasCargadas(RecurrenciasCargadas RecurrenciasCargadas)
        {
            return conn.Insert(RecurrenciasCargadas);
        }

        public RecurrenciasCargadas consultarRecurrenciasCargadas(int idRecurrenciasCargadas)
        {
            return conn.Table<RecurrenciasCargadas>().Where(x => x.id == idRecurrenciasCargadas).FirstOrDefault();
        }

        public int editarRecurrenciasCargadas(RecurrenciasCargadas RecurrenciasCargadas)
        {
            return conn.Update(RecurrenciasCargadas);
        }

        public int eliminarRecurrenciasCargadas(int idRecurrenciasCargadas)
        {
            return conn.Delete<RecurrenciasCargadas>(idRecurrenciasCargadas);
        }

    }
}
