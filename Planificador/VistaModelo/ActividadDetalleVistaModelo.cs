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

        public ICommand EditarTituloCommand { get; private set; }
        public ICommand EliminarActividadCommand { get; private set; }
        public ICommand GuardarActividadCommand { get; private set; }

        public ActividadDetalleVistaModelo(ActividadVistaModelo actividad, INavigation nav)
        {
            _actividadAct = actividad;
            _actividadesN = new ActividadesN();
            _nav = nav;
            EditarTituloCommand = new Command(EditarTitulo);
            EliminarActividadCommand = new Command(EliminarActividad);
            GuardarActividadCommand = new Command(GuardarActividad);
        }

        private void EditarTitulo(object nTitulo) {
            _actividadAct.Titulo = (string)nTitulo;
            GuardarActividad();
        }

        private void GuardarActividad()
        {
            if (_actividadAct.IdTarea == null)
            {
                _actividadesN.modificarActividad(
                    _actividadAct.Id,
                    _actividadAct.HoraInicio,
                    _actividadAct.Dia,
                    _actividadAct.Duracion,
                    _actividadAct.Descripcion,
                    _actividadAct.Titulo,
                    _actividadAct.Color);
            }
            else
            {
                _actividadesN.modificarActividad(
                    _actividadAct.Id,
                    _actividadAct.HoraInicio,
                    _actividadAct.Dia,
                    _actividadAct.Duracion
                    );
            }
            
        }

        private async void EliminarActividad()
        {
            _actividadesN.eliminarActividad(_actividadAct.Id);
            await _nav.PopModalAsync();
        }

        public ActividadVistaModelo ActividadActual
        {
            get { return _actividadAct; }
        }

        public List<string> Colores
        {
            get { return _colores; }
        }
    }
}
