using Planificador.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planificador.VistaModelo
{
    public class ObjetivoVistaModelo : BaseVistaModelo
    {
        private readonly int id;
        private string objetivo;
        private readonly int idTarea;
        private readonly DateTime creacionFecha;
        private DateTime? finalizacionFecha;

        public ObjetivoVistaModelo(Objetivo objetivoP)
        {
            id = objetivoP.id;
            objetivo = objetivoP.objetivo;
            idTarea = objetivoP.idTarea;
            creacionFecha = objetivoP.creacionFecha;
            finalizacionFecha = objetivoP.finalizacionFecha;
        }

        public int Id
        {
            get { return this.id; }
        }

        public int IdTarea
        {
            get { return this.idTarea; }
        }

        public DateTime CreacionFecha
        {
            get { return this.creacionFecha; }
        }

        public string Objetivo
        {
            get { return this.objetivo; }
            set { SetPropertyValue(ref objetivo, value); }
        }

        public DateTime? FinalizacionFecha
        {
            get { return this.finalizacionFecha; }
            set {
                if (finalizacionFecha != value)
                {
                    finalizacionFecha = value;
                    new Negocio.TareasN().modificarObjetivo(this.id, finalizado: value);
                    RaisePropertyChanged();
                }
            }
        }

        public bool EstaHecha
        {
            get { return finalizacionFecha != null; }
        }

    }
}
