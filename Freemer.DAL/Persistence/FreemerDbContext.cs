using Freemer.Domain.Entities.CatergoryAggregate;
using Freemer.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freemer.DAL.Persistence
{
    public class FreemerDbContext : DbContext, IFreemerDbContext
    {
        public FreemerDbContext(DbContextOptions<FreemerDbContext> options)
            : base(options)
        {

        }

       public DbSet<Order> Orders { get; set; }
       public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
