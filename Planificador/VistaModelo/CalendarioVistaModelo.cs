using Planificador.Modelos;
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
        private List<string> meses = new List<string> {"enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" }; 
        private ObservableCollection<Actividad> _actividades;
        private DateTime _diaSeleccionado;

        private ICommand AgregarActividadCommand;
        private ICommand VerActividadCommand;
        public ICommand DiaSiguienteCommand { get; private set; }
        public ICommand DiaAnteriorCommand { get; private set; }

        public CalendarioVistaModelo()
        {
            _actividades = new ObservableCollection<Actividad>();
            _diaSeleccionado = DateTime.Now;
            DiaSiguienteCommand = new Command(DiaSiguiente);
            DiaAnteriorCommand = new Command(DiaAnterior);
        }

        public string DiaSeleccionado
        {
            get { return String.Format("{0} de {1} del {2}",_diaSeleccionado.Day, meses[_diaSeleccionado.Month-1], _diaSeleccionado.Year); }
        }

        public void DiaSiguiente()
        {            
            _diaSeleccionado = _diaSeleccionado.AddDays(1);
            Console.WriteLine(DiaSeleccionado);
            RaisePropertyChanged("DiaSeleccionado");
        }

        public void DiaAnterior()
        {
            _diaSeleccionado = _diaSeleccionado.AddDays(-1);
            Console.WriteLine(DiaSeleccionado);
            RaisePropertyChanged("DiaSeleccionado");
        }

    }
}
