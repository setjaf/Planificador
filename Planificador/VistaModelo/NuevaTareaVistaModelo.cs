using Planificador.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Planificador.Negocio;
using System.Windows.Input;

namespace Planificador.VistaModelo
{
    public class NuevaTareaVistaModelo : BaseVistaModelo
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
        public TareaVistaModelo NuevaTarea { get; set; }

        private INavigation _navigation;
        public ICommand AgregarTareaCommand { get; private set; }
        public ICommand CerrarNuevaTareaCommand { get; private set; }

        public NuevaTareaVistaModelo(INavigation navigation)
        {
            _navigation = navigation;
            AgregarTareaCommand = new Command(agregarTarea);
            CerrarNuevaTareaCommand = new Command(cerrarModal);
            NuevaTarea = new TareaVistaModelo(new Tarea());
        }

        public async void agregarTarea()
        {
            Console.WriteLine(NuevaTarea.Titulo);
            if (!String.IsNullOrWhiteSpace(NuevaTarea.Titulo) && !String.IsNullOrWhiteSpace(NuevaTarea.Descripcion) && !String.IsNullOrWhiteSpace(NuevaTarea.Color))
            {
                if (new TareasN().agregarTarea(NuevaTarea.Titulo, NuevaTarea.Descripcion, NuevaTarea.Color))
                {
                    //Tareas.CargarTareas();
                    await _navigation.PopModalAsync();
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error al guardar la tarea","Verifica que todos los campos tengan los datos correctos", "Aceptar");
            }
        }

        public async void cerrarModal()
        {
            await _navigation.PopModalAsync();
        }

        public List<string> Colores
        {
            get { return _colores; }
        }
    }
}
