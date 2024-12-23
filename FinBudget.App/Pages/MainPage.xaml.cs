using FinBudget.App.ViewModels;
using Maui.ColorPicker;
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

        private void ColorPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is ColorPicker picker && e.PropertyName == nameof(picker.PickedColor) && BindingContext is MainPageViewModel vm)
            {
                vm.UpdateCategoryColor(picker.PickedColor);
            }
        }

        private void CollectionView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is CollectionView collectionView && e.PropertyName == nameof(collectionView.Width))
            {
                var newSpan = Math.Max(1, (int)(collectionView.Width / 198d));

                if (collectionView.ItemsLayout is GridItemsLayout itemsLayout && newSpan != itemsLayout.Span)
                {
                    itemsLayout.Span = newSpan;
                }
            }
        }
    }
}
