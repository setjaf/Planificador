using Planificador.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public RecurrenciasCargadas consultarRecurrenciasCargadas(DateTime dia)
        {
            return conn.Table<RecurrenciasCargadas>().ToList().Where(x => (x.dia.Date==dia.Date)).FirstOrDefault();
        }

        public List<RecurrenciasCargadas> consultarRecurrenciasCargadasPorDiaSemana(DayOfWeek dia)
        {
            return conn.Table<RecurrenciasCargadas>().ToList().Where(x => (x.dia.DayOfWeek == dia) && (x.dia >= DateTime.Now.Date)).ToList();
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
