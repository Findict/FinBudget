using FinBudget.App.Pages;
using FinBudget.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace FinBudget.App
{
    public partial class App : Application
    {
        public App(BudgetDbContext dbContext)
        {
            InitializeComponent();

            MainPage = new AppShell();

            dbContext.Database.Migrate();
            dbContext.Dispose();
        }
    }
}
