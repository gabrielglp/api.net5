using api.net5.Models.ClientModel;
using api.net5.Models.LoginModel;
using api.net5.Models.RegistrationModel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace api.net5.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RegistrationModel> registrationModels { get; set; }
        public DbSet<LoginModel> loginModels { get; set; }
        public DbSet<ClientModel> clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CLIENT"));
        }
    }
}
