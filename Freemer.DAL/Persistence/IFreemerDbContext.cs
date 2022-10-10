using Freemer.Domain.Entities.CatergoryAggregate;
using Freemer.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Freemer.DAL.Persistence
{
    public interface IFreemerDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Category> Categories { get; set; }
        //DbSet<Genre> Genres { get; set; }
    }
}
