using MCLH.CLN.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MCLH.CLN.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}