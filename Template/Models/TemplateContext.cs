using Microsoft.EntityFrameworkCore;
//Identifying the Database Schema

namespace Template.Models
{
  public class TemplateContext : DbContext
  {
    public virtual DbSet<Parent> Parents { get; set; } //DBSets are new tables being created. 
    public DbSet<Child> Childs { get; set; }

    public DbSet<ParentChild> ParentChild { get; set; }

    public TemplateContext(DbContextOptions options) : base(options) { } 
  }
}