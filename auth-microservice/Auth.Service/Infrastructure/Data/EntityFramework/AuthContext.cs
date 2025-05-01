namespace Auth.Service.Infrastructure.Data.EntityFramework;

using System.Threading.Tasks;
using Auth.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class AuthContext : DbContext, IAuthStore
{
  public AuthContext(DbContextOptions<AuthContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserConfiguration());
  }
  public async Task<User?> VerifyUserLogin(string username, string password) =>
    await Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
  }
}
