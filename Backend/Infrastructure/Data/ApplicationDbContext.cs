using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}

		public DbSet<LicenseModel> License { get; set; }

		public DbSet<UserModel> User { get; set; }

		public DbSet<PluginModel> Plugin { get; set; }
		public DbSet<RegisterCompanyModel> Company { get; set; }

	}
}
