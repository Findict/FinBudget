using FinBudget.App.ViewModels;
using Microsoft.Maui.Controls;

namespace FinBudget.App.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();

            Loaded += async (o, e) => await viewModel.OnInitializedAsync();

            BindingContext = viewModel;
        }
    }
}
