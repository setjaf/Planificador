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
        private List<string> _colores = Listas.Colores;
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
