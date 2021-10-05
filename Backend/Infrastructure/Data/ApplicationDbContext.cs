﻿using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{ public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}

		public DbSet<License> License { get; set; }
		public DbSet<Plugin> Plugin { get; set; }
		public DbSet<PluginLicenses> PluginLicenses { get; set; }
		public DbSet<User> User { get; set; }

	}
}
