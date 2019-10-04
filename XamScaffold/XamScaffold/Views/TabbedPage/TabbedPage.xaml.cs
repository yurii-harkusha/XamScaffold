using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamScaffold.Views.TabbedPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage : Xamarin.Forms.TabbedPage
    {
        public TabbedPage()
        {
            InitializeComponent();
        }
    }
}