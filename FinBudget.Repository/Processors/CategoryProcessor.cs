using System.Text.RegularExpressions;
using FinBudget.Repository.Database;
using FinBudget.Repository.Exceptions;
using FinBudget.Repository.Models;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Edit;
using FinBudget.Repository.Models.Output;
using FinBudget.Repository.Processors.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FinBudget.Repository.Processors
{
    public class CategoryProcessor : ICategoryProcessor
    {
        private IMemoryCache _cache;
        private BudgetDbContext _dbContext;

        public CategoryProcessor(BudgetDbContext dbContext)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
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
            if (_cache != null && _cache.TryGetValue("categories_all", out List<Category>? cacheValue) && cacheValue != null)
            {
                return cacheValue;
            }

            var dbResults = await _dbContext.Categories.Select(x => new Category { Name = x.Name, ColorCode = x.ColorCode }).ToListAsync();

            if (_cache != null)
            {
                var entry = _cache.CreateEntry("categories_all");

                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                entry.SetValue(dbResults);
            }

            return await _dbContext.Categories.Select(x => new Category { Name = x.Name, ColorCode = x.ColorCode}).ToListAsync();
        }

        public async Task<ObjectResult<Category>> AddCategory(CreateCategoryModel model)
        {
            var result = new ObjectResult<Category>();

            if (string.IsNullOrWhiteSpace(model.Name)) 
            {
                result.ErrorMessages.Add("Categories must have a name");
            }

            if (!string.IsNullOrWhiteSpace(model.ColorCode) && !Regex.IsMatch(model.ColorCode, "^#[0-9a-fA-F]{6}$"))
            {
                result.ErrorMessages.Add("Category color is invalid");
            }

            if (result.ErrorMessages.Count > 0) return result;

            var newItem = _dbContext.Categories.Add(new DbCategory
            {
                Name = model.Name,
                ColorCode = model.ColorCode
            });

            var changes = await _dbContext.SaveChangesAsync();

            result.Success = changes > 0;

            if (result.Success)
            {
                result.Result = CategoryFromDbObject(newItem.Entity);
            }

            return result;
        }

        public async Task<ObjectResult<Category>> UpdateCategory(EditCategoryModel model)
        {
            var result = new ObjectResult<Category>();

            if (!string.IsNullOrWhiteSpace(model.ColorCode) && !Regex.IsMatch(model.ColorCode, "^#[0-9a-fA-F]{6}$"))
            {
                result.ErrorMessages.Add("Category colour is invalid");
            }

            if (result.ErrorMessages.Count > 0) return result;

            var existing = await _dbContext.Categories.FindAsync(model.Id);

            if (existing == null)
            {
                if (model.Name == null) throw new InvalidEditModelException($"Category with id {model.Id} was not found, and no new category could be created as the name was null.");

                return await AddCategory(new() { Name = model.Name, ColorCode = model.ColorCode });
            }

            if (model.Name != null) existing.Name = model.Name;
            existing.ColorCode = model.ColorCode;

            var changes = await _dbContext.SaveChangesAsync();

            result.Success = changes > 0;

            if (result.Success)
            {
                result.Result = CategoryFromDbObject(existing);
            }

            return result;
        }

        private Category CategoryFromDbObject(DbCategory dbResult) => new Category
        {
            Id = dbResult.Id,
            Name = dbResult.Name,
            ColorCode = dbResult.ColorCode
        };
    }
}
