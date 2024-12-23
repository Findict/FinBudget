using FinBudget.Repository.Models;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Edit;
using FinBudget.Repository.Models.Output;

namespace FinBudget.Repository.Processors.Interfaces
{
    public interface ICategoryProcessor
    {
        Task<Category?> GetCategory(int id);

        Task<List<Category>> GetCategories();

        Task<ObjectResult<Category>> AddCategory(CreateCategoryModel model);

        Task<ObjectResult<Category>> UpdateCategory(EditCategoryModel model);
    }
}