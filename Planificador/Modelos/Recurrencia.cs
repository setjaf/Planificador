using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Planificador.Modelos
{
    public class Recurrencia
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public int idTarea { get; set; }

        [NotNull]
        public int dia { get; set; }

        [NotNull]
        public TimeSpan horaInicio { get; set; }

        [NotNull]
        public int duracion { get; set; }
    }
}
