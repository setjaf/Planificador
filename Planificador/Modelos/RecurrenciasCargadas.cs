using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planificador.Modelos
{
    public class RecurrenciasCargadas
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [NotNull]
        public DateTime dia { get; set; }
    }
}
