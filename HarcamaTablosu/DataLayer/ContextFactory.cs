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
            optionsBuilder.UseSqlServer("Server=DESKTOP-NNI8G0S; Database=AylikHarcama; Trusted_Connection=True;");

            return new Context(optionsBuilder.Options);
        }
    }
}
