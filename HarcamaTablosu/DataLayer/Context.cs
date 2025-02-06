using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Context : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-NNI8G0S; Database=AylikHarcama; Trusted_Connection=true");
        //}
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public Context()
        {
            
        }
        public DbSet<GiderTablosu> tbl_Giderler { get; set; }
        public DbSet<GiderTip> tbl_GiderTipleri { get; set; }
        public DbSet<Ayarlar> tbl_ayarlar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Sadece eğer DbContextOptions gelmemişse
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-NNI8G0S; Database=AylikHarcama; Trusted_Connection=True;");
            }
        }

    }
}
