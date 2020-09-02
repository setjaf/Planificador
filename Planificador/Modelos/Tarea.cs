using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace Planificador.Modelos
{
    public class Tarea : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100), NotNull]
        public string titulo { get; set; }

        [MaxLength(255), NotNull]
        public string descripcion { get; set; }

        [NotNull]
        public DateTime creacionFecha { get; set; }

        [MaxLength(6), NotNull]
        public string color { get; set; }

        [Ignore]
        public List<Objetivo> objetivos { get; set; }

        [Ignore]
        public List<Recurrencia> recurrencias { get; set; }
        
    }
}
