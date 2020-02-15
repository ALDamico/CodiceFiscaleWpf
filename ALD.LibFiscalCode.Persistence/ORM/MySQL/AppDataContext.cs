using System.Configuration;
using System.Net.Mime;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;
namespace ALD.LibFiscalCode.Persistence.ORM.MySQL
{
    public class AppDataContext:AppDataContextBase
    {
        public AppDataContext(DbContextOptions<AppDataContextBase> optionsBuilder):base(optionsBuilder)
        {
            
        }
        
       public AppDataContext()
        {

        }

        // No need to override OnModelCreating: we're using AppDataContextBase's

    }
}