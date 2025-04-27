using Auth.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Infrastructure.Data.EntityFramework;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);
    builder.Property(u => u.Username)
      .IsRequired();
    builder.Property(u => u.Password)
      .IsRequired();
    builder.HasData(new User
    {
      Id = Guid.NewGuid(),
      Username = "microservices@erivalo.com",
      Password = "oKNrqkO7iC#G"
    });
  }
}