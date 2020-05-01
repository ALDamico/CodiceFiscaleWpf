using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ALD.LibFiscalCode.Tests
{
    public class DbFixture
    {
        private const string connectionString = "Server=localhost;Database=fiscal_code;Trusted_Connection=True;";

        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<AppDataContext>(
                    options =>
                        options.UseSqlServer(connectionString),
                    ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}