using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace Planificador.VistaModelo
{
    public class NotasPorTareaVistaModelo : BaseVistaModelo
    {
        private ObservableCollection<ActividadVistaModelo> _actividades;
        private ActividadesN _actividadesN;

        public NotasPorTareaVistaModelo(int idTarea)
        {
            _actividadesN = new ActividadesN();
            _actividades = new ObservableCollection<ActividadVistaModelo>();
            CargarTareas(idTarea);
        }

        private void CargarTareas(int idTarea)
        {
            _actividades.Clear();
            foreach (var act in _actividadesN.listarActividades(idTarea).Where(x => x.comentarios != null).ToList())
            {
                _actividades.Add(new ActividadVistaModelo(act));
            }
            RaisePropertyChanged("Actividades");
        }

        public ObservableCollection<ActividadVistaModelo> Actividades
        {
            get { return _actividades; }
        }
    }
}
