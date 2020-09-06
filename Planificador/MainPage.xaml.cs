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
        public MainPage()
        {
            InitializeComponent();            
            Children.Add(new Paginas.Tareas());
            Children.Add(new Paginas.Calendario());
        }
    }
}
