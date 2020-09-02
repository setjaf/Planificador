using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Planificador.Modelos
{
    public class Objetivo
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(150), NotNull]
        public string objetivo { get; set; }

        [NotNull]
        public int idTarea { get; set; }

        [NotNull]
        public DateTime creacionFecha { get; set; }

        public DateTime? finalizacionFecha { get; set; }
    

    }
}
