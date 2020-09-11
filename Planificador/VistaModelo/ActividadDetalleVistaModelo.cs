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
