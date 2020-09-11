using Planificador.Modelos;
using Planificador.Negocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planificador.VistaModelo
{
    public class CalendarioVistaModelo : BaseVistaModelo
    {
        private ActividadesN _actN; 
        private List<string> meses = new List<string> { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
        private List<string> diasSemana = new List<string> {"Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private ObservableCollection<ActividadVistaModelo> _actividades;
        private DateTime _diaSeleccionado;

        private ICommand AgregarActividadCommand;
        private ICommand VerActividadCommand;
        public ICommand CargarActividadesCommand { get; private set; }
        public ICommand DiaSiguienteCommand { get; private set; }
        public ICommand DiaAnteriorCommand { get; private set; }

        public CalendarioVistaModelo()
        {
            _actividades = new ObservableCollection<ActividadVistaModelo>();
            _diaSeleccionado = DateTime.Now;
            _actN = new ActividadesN();
            DiaSiguienteCommand = new Command(DiaSiguiente);
            DiaAnteriorCommand = new Command(DiaAnterior);
            CargarActividadesCommand = new Command(CargarActividades);
            CargarActividades();
        }

        public void CargarActividades()
        {
            _actividades.Clear();
            var actividades =_actN.listarActividades(_diaSeleccionado);
            foreach (var actividad in actividades)
            {
                _actividades.Add(new ActividadVistaModelo(actividad));
            }
            RaisePropertyChanged("Actividades");
        }

        public ObservableCollection<ActividadVistaModelo> Actividades
        {
            get { return _actividades; }
        }

        public string DiaSeleccionado
        {
            get { return String.Format("{3} {0} de {1} del {2}",_diaSeleccionado.Day, meses[_diaSeleccionado.Month-1], _diaSeleccionado.Year, diasSemana[(int)_diaSeleccionado.DayOfWeek]); }
        }

        public void DiaSiguiente()
        {            
            _diaSeleccionado = _diaSeleccionado.AddDays(1);
            Console.WriteLine(DiaSeleccionado);
            RaisePropertyChanged("DiaSeleccionado");
            CargarActividades();
        }

        public void DiaAnterior()
        {
            _diaSeleccionado = _diaSeleccionado.AddDays(-1);
            Console.WriteLine(DiaSeleccionado);
            RaisePropertyChanged("DiaSeleccionado");
            CargarActividades();
        }

    }
}
