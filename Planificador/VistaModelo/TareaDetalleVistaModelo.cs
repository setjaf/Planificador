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
        private List<string> _colores = new List<string>()
        {
            "B71C1C",
            "F44336",
            "880E4F",
            "E91E63",
            "4A148C",
            "9C27B0",
            "311B92",
            "673AB7",
            "1A237E",
            "3F51B5",
            "0D47A1",
            "2196F3",
            "01579B",
            "03A9F4",
            "006064",
            "00BCD4",
            "004D40",
            "009688",
            "1B5E20",
            "4CAF50",
            "33691E",
            "8BC34A",
            "827717",
            "CDDC39",
            "FFD600",
            "FFFF00",
            "E65100",
            "FF9800",
            "3E2723",
            "795548",
            "263238",
            "607D8B",
        };

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

        public List<string> Colores
        {
            get { return _colores; }
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
