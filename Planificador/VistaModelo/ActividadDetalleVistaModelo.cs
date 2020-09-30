using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class ActividadDetalleVistaModelo : BaseVistaModelo
    {
        private ActividadesN _actividadesN;
        private INavigation _nav;
        private List<string> _colores = Listas.Colores;

        private ActividadVistaModelo _actividadAct;
        private bool _detallesVisibles;
        private bool _showEditor;

        public ICommand EditarTituloCommand { get; private set; }
        public ICommand EliminarActividadCommand { get; private set; }
        public ICommand GuardarActividadCommand { get; private set; }
        public ICommand MostrarDetallesCommand { get; private set; }
        public ICommand MostrarEditorCommand { get; private set; }

        public ICommand AgregarObjetivoCommand { get; private set; }

        public ActividadDetalleVistaModelo(ActividadVistaModelo actividad, INavigation nav)
        {
            _actividadAct = actividad;
            _actividadesN = new ActividadesN();
            _nav = nav;
            _detallesVisibles = false;
            _showEditor = _actividadAct.Comentarios != null ? false : true;
            EditarTituloCommand = new Command(EditarTitulo);
            EliminarActividadCommand = new Command(EliminarActividad);
            GuardarActividadCommand = new Command(GuardarActividad);
            MostrarDetallesCommand = new Command(MostrarDetalles);
            MostrarEditorCommand = new Command(MostrarEditor);
            AgregarObjetivoCommand = new Command(AgregarObjetivo);

        }

        private void EditarTitulo(object nTitulo) {
            _actividadAct.Titulo = (string)nTitulo;
            GuardarActividad();
        }

        private void GuardarActividad()
        {
            ShowEditor = false;
            if (_actividadAct.IdTarea == null)
            {
                _actividadesN.modificarActividad(
                    _actividadAct.Id,
                    _actividadAct.HoraInicio,
                    _actividadAct.Dia,
                    _actividadAct.Duracion,
                    _actividadAct.Descripcion,
                    _actividadAct.Titulo,
                    _actividadAct.Color,
                    comentarios:_actividadAct.Comentarios);
            }
            else
            {
                _actividadesN.modificarActividad(
                    _actividadAct.Id,
                    _actividadAct.HoraInicio,
                    _actividadAct.Dia,
                    _actividadAct.Duracion,
                    comentarios:_actividadAct.Comentarios);
            }
            _actividadAct.Comentarios = _actividadAct.Comentarios;
        }

        private async void EliminarActividad()
        {
            _actividadesN.eliminarActividad(_actividadAct.Id);
            await _nav.PopModalAsync();
        }

        private void MostrarDetalles()
        {
            DetallesVisibles = !DetallesVisibles;
        }

        public void AgregarObjetivo(object nuevoObjetivo)
        {
            var nuevoObj = (string)nuevoObjetivo;
            if (new TareasN().agregarObjetivoATarea((int)_actividadAct.IdTarea, nuevoObj))
            {
                _actividadAct.CargarObjetivos();
            }
        }

        public ActividadVistaModelo ActividadActual
        {
            get { return _actividadAct; }
        }

        public List<string> Colores
        {
            get { return _colores; }
        }

        public bool DetallesVisibles
        {
            get { return _detallesVisibles; }
            set
            {
                _detallesVisibles = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowEditor
        {
            get { return _showEditor; }
            set
            {
                _showEditor = value;
                RaisePropertyChanged();
            }
        }

        private void MostrarEditor()
        {
            ShowEditor = !ShowEditor;
        }
    }
}
