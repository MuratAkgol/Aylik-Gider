using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            // Bağlantı dizesini ayarla
            optionsBuilder.UseSqlServer("Server=LAPTOP-26L8GN1D; Database=AylikHarcama; Trusted_Connection=True;");

            return new Context(optionsBuilder.Options);
        }
    }
}
