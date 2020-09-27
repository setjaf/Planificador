using Planificador.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Planificador.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecurrenciasSemana : ContentPage
    {
        public RecurrenciasSemana()
        {            
            InitializeComponent();

            ViewModel = new RecurrenciasSemanaVistaModelo();

            //for (int i = ViewModel.HoraInicio; i <= 24; i++)
            //{
            //    absLay.Children.Add(
            //        new BoxView()
            //        {
            //            BackgroundColor = Color.LightGray,
            //        },
            //        new Rectangle(0, (i - ViewModel.HoraInicio) * 60, 1, 1),
            //        AbsoluteLayoutFlags.WidthProportional
            //    );

            //    //for (int j = 0; j < 7; j++)
            //    //{
            //    //    absLay.Children.Add(
            //    //        new Label()
            //    //        {
            //    //            Text = new TimeSpan(i, 0, 0).ToString(@"hh\:mm"),
            //    //            TextColor = Color.LightGray,
            //    //            FontAttributes = FontAttributes.Bold,
            //    //        },
            //    //        new Rectangle((j * 200) + j, (i - ViewModel.HoraInicio) * 60, 40, 40)
            //    //    );
            //    //}
            //}

        }

        protected override void OnAppearing()
        {
            ViewModel.CargarRecurrenciasCommand.Execute(null);
            base.OnAppearing();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
        }

        public RecurrenciasSemanaVistaModelo ViewModel
        {
            get { return BindingContext as RecurrenciasSemanaVistaModelo; }
            set { BindingContext = value; }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new NuevaRecurrencia(null));
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var rec = (RecurrenciaVistaModelo)(((TappedEventArgs)e).Parameter);
            var texto = await DisplayActionSheet(rec.Titulo, "Cancelar", "Eliminar", "Ver tarea");
            if ("Eliminar".Equals(texto))
            {
                if (await DisplayAlert("Eliminar recurrencia", "¿Estás seguro de eliminar la recurrencia?", "Aceptar", "Cancelar"))
                    ViewModel.EliminarRecurrenciaCommand.Execute(rec.Id);
            }else if ("Ver tarea".Equals(texto))
            {
                TareaVistaModelo tarea = new TareaVistaModelo(new Negocio.TareasN().consultarTarea(rec.IdTarea));
                var navPage = new NavigationPage(new TareaDetalle(tarea))
                {
                    BarBackgroundColor = Color.FromHex(tarea.BackgroundColor),
                    BarTextColor = Color.FromHex(tarea.TextColor)
                };

                navPage.BindingContext = tarea;
                navPage.SetBinding(NavigationPage.BarBackgroundColorProperty, path: "BackgroundColor");
                navPage.SetBinding(NavigationPage.BarTextColorProperty, path: "TextColor");
                await Navigation.PushModalAsync(navPage);
            }
        }
    }
}