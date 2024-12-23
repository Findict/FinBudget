using FinBudget.Repository.Database;
using FinBudget.Repository.Exceptions;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Edit;
using FinBudget.Repository.Models.Output;
using FinBudget.Repository.Processors.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Processors
{
    public class CategoryProcessor : ICategoryProcessor
    {
        private BudgetDbContext _dbContext;

        public CategoryProcessor(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> GetCategory(int id)
        {
            var dbModel = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (dbModel == null) return null;

            return new Category()
            {
                Name = dbModel.Name
            };
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _dbContext.Categories.Select(x => new Category { Name = x.Name }).ToListAsync();
        }

        public async Task<bool> AddCategory(CreateCategoryModel model)
        {
            _dbContext.Categories.Add(new DbCategory
            {
                Name = model.Name,
                ColorCode = model.ColorCode
            });

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> UpdateCategory(EditCategoryModel model)
        {
            var existing = await _dbContext.Categories.FindAsync(model.Id);

            if (existing == null)
            {
                if (model.Name == null) throw new InvalidEditModelException($"Category with id {model.Id} was not found, and no new category could be created as the name was null.");

                return await AddCategory(new() { Name = model.Name, ColorCode = model.ColorCode });
            }

            if (model.Name != null) existing.Name = model.Name;
            existing.ColorCode = model.ColorCode;

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }
    }
}
