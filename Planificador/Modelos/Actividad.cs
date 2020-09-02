using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Planificador.Modelos
{
    public class Actividad
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int? idTarea { get; set; }

        [NotNull]
        public DateTime dia { get; set; }

        [NotNull]
        public TimeSpan horaInicio { get; set; }

        [NotNull]
        public int duracion { get; set; }

        public string descripcion { get; set; }

        [MaxLength(7)]
        public string color { get; set; }
    }
}
