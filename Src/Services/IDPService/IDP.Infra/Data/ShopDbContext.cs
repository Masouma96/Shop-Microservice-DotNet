
using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDP.Infra.Data;

public class ShopCommandDbContext (DbContextOptions options) : DbContext(options)
{

    public virtual DbSet<User> Users { get; set; }
}
        