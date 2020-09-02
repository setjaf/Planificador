using Planificador.Modelos;
using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class TareaDetalleVistaModelo : BaseVistaModelo
    {
        private TareasN _tareasN;
        public TareaVistaModelo TareaActual { get; set; }
        

        public ICommand AgregarObjetivoCommand { get; private set; }
        public ICommand RefrescarObjetivosCommand { get; private set; }
        public ICommand EliminarObjetivoCommand { get; private set; }
        public ICommand EliminarRecurrenciaCommand { get; private set; }

        public TareaDetalleVistaModelo(TareaVistaModelo tarea)
        {
            _tareasN = new TareasN();
            TareaActual = tarea;
            AgregarObjetivoCommand = new Command(AgregarObjetivo);
            //RefrescarObjetivosCommand = new Command(TareaActual.CargarObjetivos);
            EliminarObjetivoCommand = new Command(EliminarObjetivo);
            EliminarRecurrenciaCommand = new Command(EliminarRecurrencia);
        }

        public void AgregarObjetivo(object nuevoObjetivo)
        {
            var nuevoObj = (string)nuevoObjetivo;
            if(_tareasN.agregarObjetivoATarea(TareaActual.Id, nuevoObj))
            {
                TareaActual.CargarObjetivos();
            }
        }

        public void EliminarObjetivo(object idObjetivo)
        {
            var id = (int)idObjetivo;
            _tareasN.eliminarObjetivo(id);
        }

        private void EliminarRecurrencia(object idObjetivo)
        {
            _tareasN.eliminarRecurrecia((int)idObjetivo);
            TareaActual.CargarRecurrencias();
        }
    }
}
