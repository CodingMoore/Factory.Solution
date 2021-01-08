using Microsoft.EntityFrameworkCore;
//Identifying the Database Schema

namespace Factory.Models
{
  public class FactoryContext : DbContext
  {
    public virtual DbSet<Engineer> Engineers { get; set; } //DBSets are new tables being created. 
    public DbSet<Machine> Machines { get; set; }

    public DbSet<EngineerMachine> EngineerMachine { get; set; }

    public FactoryContext(DbContextOptions options) : base(options) { } 
  }
}