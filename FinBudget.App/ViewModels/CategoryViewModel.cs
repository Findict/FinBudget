using FinBudget.Repository.Models.Output;

namespace FinBudget.App.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public Color Color { get; set; }

        public CategoryViewModel(Category result)
        {
            Name = result.Name;
            Color = Color.FromArgb(result.ColorCode);
        }
    }
}