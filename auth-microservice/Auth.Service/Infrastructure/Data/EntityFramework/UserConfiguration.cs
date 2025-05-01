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
    builder.Property(u => u.Role)
      .IsRequired();
    builder.HasData(new User
    {
      Id = Guid.Parse("e02fd0e4-00fd-090A-ca30-0d00a0038ba0"),
      Username = "microservices@erivalo.com",
      Password = "oKNrqkO7iC#G",
      Role = "Administrator"
    });
  }
}