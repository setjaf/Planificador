using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planificador
{
    public partial class MainPage : TabbedPage
    {
        public bool prueba; 
        public MainPage()
        {
            InitializeComponent();
            Children.Add(new Paginas.Actividades());
            Children.Add(new Paginas.Tareas());
            Children.Add(new Paginas.Recurrencias());
        }

        //protected override void OnCurrentPageChanged()
        //{
        //    base.OnCurrentPageChanged();
        //}


    }
}
