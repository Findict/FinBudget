using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Output;
using FinBudget.Repository.Processors.Interfaces;

namespace FinBudget.App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryProcessor _categoryProcessor;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand OnSubmitClicked { get; }

        public string CategoryInput { get; set; }

        public ObservableCollection<Category> Categories { get; set; } = new();

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
                Categories.Add(category);
            }

            OnPropertyChanged(nameof(Categories));
        }

        private async Task SubmitNewCategory()
        {
            var result = await _categoryProcessor.AddCategory(new CreateCategoryModel { Name = CategoryInput });

            if (result)
            {
                Categories.Add(new Category { Name = CategoryInput });
                CategoryInput = string.Empty;

                OnPropertyChanged(nameof(CategoryInput));
                OnPropertyChanged(nameof(Categories));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}