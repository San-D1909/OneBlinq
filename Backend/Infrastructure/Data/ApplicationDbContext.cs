using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
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
		public DbSet<CompanyModel> Company { get; set; }
		public DbSet<LicenseTypeModel> LicenseType { get; set; }
		public DbSet<PluginBundleModel> PluginBundle { get; set; }
		public DbSet<DeviceModel> Device { get; set; }
		public DbSet<ResetTokenModel> ResetToken { get; set; }
		public DbSet<PluginLicenseModel> PluginLicense { get; set; }
		public DbSet<PluginBundlesModel> PluginBundles { get; set; }
		public DbSet<PluginVariantModel> PluginVariant { get; set; }
		public DbSet<PluginImageModel> PluginImage { get; set; }
	}
}
