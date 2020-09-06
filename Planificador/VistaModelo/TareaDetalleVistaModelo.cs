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
        public ICommand EditarTituloCommand { get; private set; }
        public ICommand EditarDescripcionCommand { get; private set; }
        public ICommand EditarColorCommand { get; private set; }

        public TareaDetalleVistaModelo(TareaVistaModelo tarea)
        {
            _tareasN = new TareasN();
            TareaActual = tarea;
            AgregarObjetivoCommand = new Command(AgregarObjetivo);
            RefrescarObjetivosCommand = new Command(TareaActual.CargarObjetivos);
            EliminarObjetivoCommand = new Command(EliminarObjetivo);
            EliminarRecurrenciaCommand = new Command(EliminarRecurrencia);
            EditarTituloCommand = new Command(EditarTitulo);
            EditarDescripcionCommand = new Command(EditarDescripcion);
            EditarColorCommand = new Command(EditarColor);
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
            _tareasN.eliminarObjetivo((int)idObjetivo);
            TareaActual.CargarObjetivos();
        }

        private void EliminarRecurrencia(object idRecurrencia)
        {
            _tareasN.eliminarRecurrecia((int)idRecurrencia);
            TareaActual.CargarRecurrencias();
        }

        private void EditarTitulo(object nuevoTitulo)
        {
            var nTitulo = (string)nuevoTitulo;
            TareaActual.Titulo = nTitulo;
            GuardarTarea();
        }

        private void EditarDescripcion(object nuevaDesc)
        {
            var nDesc = (string)nuevaDesc;
            TareaActual.Descripcion = nDesc;
            GuardarTarea();
        }

        private void EditarColor(object nuevoColor)
        {
            var nColor = (string)nuevoColor;
            TareaActual.Color = nColor;
            GuardarTarea();
        }

        private void GuardarTarea()
        {
            _tareasN.modificarTarea(TareaActual.Id, TareaActual.Titulo, TareaActual.Descripcion, TareaActual.Color);
        }
    }
}
