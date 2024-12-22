using FinBudget.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static readonly string _dbPath = "FinBudget\\Database.db";
    private static readonly string _dbPathBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Environment.GetEnvironmentVariable("OneDrive"); 

    private static void Main(string[] args)
    {
        var oneDrivePath = Environment.GetEnvironmentVariable("OneDrive");
        var dbLocation = Path.Combine(_dbPathBase, _dbPath);

        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        if (!File.Exists(dbLocation)) 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dbLocation));

            var file = File.Create(dbLocation);

            file.Close();
        }

        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDbContext<BudgetDbContext>(options => options.UseSqlite($"Data Source={dbLocation};"));

        using IHost host = builder.Build();

        BudgetDbContext db = null;

        using (var scope = host.Services.CreateScope())
        {
            db = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
            db.Database.Migrate();
        }
    }
}