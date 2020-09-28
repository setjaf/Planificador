using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class ActividadesVistaModelo : BaseVistaModelo
    {

        private ObservableCollection<ActividadesG> _actividadesG;
        private bool _isRefreshing;
        private ActividadesN _actividadesN;
        private ActividadesN _actN;
        private List<string> meses = Listas.Meses;
        private List<string> diasSemana = Listas.Dias;
        private DateTime _diaSeleccionado;

        private ICommand AgregarActividadCommand;
        private ICommand VerActividadCommand;
        public ICommand CargarActividadesCommand { get; private set; }
        public ICommand CambioEstadoActividadesCommand { get; private set; }
        public ICommand DiaSiguienteCommand { get; private set; }
        public ICommand DiaAnteriorCommand { get; private set; }

        public ICommand EliminarActividadCommand { get; private set; }

        public ActividadesVistaModelo()
        {
            _actividadesN = new ActividadesN();
            _isRefreshing = false;
            _actividadesG = new ObservableCollection<ActividadesG>();
            _diaSeleccionado = DateTime.Now;
            _actN = new ActividadesN();
            DiaSiguienteCommand = new Command(DiaSiguiente);
            DiaAnteriorCommand = new Command(DiaAnterior);
            CargarActividadesCommand = new Command(CargarActividades);
            EliminarActividadCommand = new Command(EliminarActividad);
            CambioEstadoActividadesCommand = new Command(CambioEstadoActividades);
            CargarActividades();
        }

        public void CambioEstadoActividades()
        {
            if (_actividadesG.Count > 0 && _actividadesG[0].Estado == "En Curso")
            {
                if (_actividadesG[0][0].HoraFin <= DateTime.Now.TimeOfDay)
                    CargarActividades();
            }
            else if (_actividadesG.Count > 0 && _actividadesG[0].Estado == "Por Realizar")
            {
                if (_actividadesG[0][0].HoraInicio <= DateTime.Now.TimeOfDay)
                    CargarActividades();
            }else if(_actividadesG.Count >= 2  && _actividadesG[1].Estado == "Por Realizar")
            {
                if (_actividadesG[1][0].HoraInicio <= DateTime.Now.TimeOfDay)
                    CargarActividades();
            }
        }
        public void CargarActividades()
        {
            IsRefreshing = true;
            _actividadesG.Clear();
            var hora = DateTime.Now;
            var enCurso = new ActividadesG() { Estado = "En Curso" };
            var porRealizar = new ActividadesG() { Estado = "Por Realizar" };
            var realizadas = new ActividadesG() { Estado = "Realizadas" };

            var actividades = _actN.listarActividades(_diaSeleccionado);
            foreach (var actividad in actividades)
            {
                var actVM = new ActividadVistaModelo(actividad);
                var resultado = actVM.Dia.Date == _diaSeleccionado.Date;
                if (actVM.HoraInicio <= hora.TimeOfDay && hora.TimeOfDay <= actVM.HoraFin && actVM.Dia.Date == hora.Date)
                {
                    enCurso.Add(actVM);
                }
                else if ((hora.TimeOfDay < actVM.HoraInicio && actVM.Dia.Date == hora.Date) || hora.Date < _diaSeleccionado.Date)
                {
                    porRealizar.Add(actVM);
                }
                else
                {
                    realizadas.Add(actVM);
                }
            }

            if (enCurso.Count > 0)
                _actividadesG.Add(enCurso);
            if (porRealizar.Count > 0)
                _actividadesG.Add(porRealizar);
            if (realizadas.Count > 0)
                _actividadesG.Add(realizadas);

            RaisePropertyChanged("Actividades");
            RaisePropertyChanged("EstaVacia");
            IsRefreshing = false;
        }

        public ObservableCollection<ActividadesG> Actividades
        {
            get { return _actividadesG; }
        }

        ///<summary>
        /// Devuelve el día seleccionado en 'string' formato largo, por ejemplo, Domingo 10 de agosto del 2020
        ///</summary>
        public string DiaSeleccionado
        {
            get { return String.Format("{3} {0} de {1} del {2}", _diaSeleccionado.Day, meses[_diaSeleccionado.Month - 1], _diaSeleccionado.Year, diasSemana[(int)_diaSeleccionado.DayOfWeek]); }
        }

        ///<summary>
        /// Devuelve el día seleccionado en 'Datetime'
        ///</summary>
        public DateTime DiaSeleccionadoDT
        {
            get { return _diaSeleccionado; }
        }

        public void DiaSiguiente()
        {
            _diaSeleccionado = _diaSeleccionado.AddDays(1);
            RaisePropertyChanged("DiaSeleccionado");
            CargarActividades();
        }

        public void DiaAnterior()
        {
            _diaSeleccionado = _diaSeleccionado.AddDays(-1);
            RaisePropertyChanged("DiaSeleccionado");
            CargarActividades();
        }

        public bool EstaVacia
        {
            get { return Actividades.Count <= 0; }
        }

        private void EliminarActividad(object id)
        {
            _actividadesN.eliminarActividad((int)id);
            CargarActividades();
        }

        public bool IsRefreshing { get { return _isRefreshing; } set { _isRefreshing = value; RaisePropertyChanged(); } }

    }

    public class ActividadesG : ObservableCollection<ActividadVistaModelo>
    {
        public string Estado { get; set; }
    }
}
