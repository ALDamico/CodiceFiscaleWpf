using System.IO;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.Factories
{
    //Mainly used for Sql Server migrations
    public class SqlServerDataContextFactory: IDesignTimeDbContextFactory<SqlServerDataContext>
    {
        public SqlServerDataContext CreateDbContext(string[] args)
        {
            //TODO Improve
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../CodiceFiscaleApi/appsettings.Development.json").Build(); 
            var builder = new DbContextOptionsBuilder<SqlServerDataContext>(); 
            var connectionString = configuration.GetConnectionString("SqlServerConnection"); 
            builder.UseSqlServer(connectionString); 
            return new SqlServerDataContext(builder.Options);
        }
    }
}