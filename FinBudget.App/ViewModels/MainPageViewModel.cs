using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Processors.Interfaces;

namespace FinBudget.App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryProcessor _categoryProcessor;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand OnSubmitClicked { get; }

        public string CategoryInput { get; set; }

        public Color CategoryColor { get; set; }

        public ObservableCollection<CategoryViewModel> Categories { get; set; } = new();

        public MainPageViewModel(ICategoryProcessor categoryProcessor)
        {
            OnSubmitClicked = new Command(async () => await SubmitNewCategory());

            _categoryProcessor = categoryProcessor ?? throw new ArgumentNullException(nameof(categoryProcessor));
        }

        internal async Task OnInitializedAsync()
        {
            var categories = await _categoryProcessor.GetCategories();

            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel(category));
            }

            OnPropertyChanged(nameof(Categories));
        }

        private async Task SubmitNewCategory()
        {
            var result = await _categoryProcessor.AddCategory(new CreateCategoryModel 
            { 
                Name = CategoryInput,
                ColorCode = CategoryColor.ToHex()
            });

            if (result.Success && result.Result != null)
            {
                Categories.Add(new CategoryViewModel(result.Result));
                CategoryInput = string.Empty;

                OnPropertyChanged(nameof(CategoryInput));
                OnPropertyChanged(nameof(Categories));
            }
        }

        internal void UpdateCategoryColor(Color pickedColor)
        {
            CategoryColor = pickedColor;

            OnPropertyChanged(nameof(CategoryColor));
        }

        private void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}