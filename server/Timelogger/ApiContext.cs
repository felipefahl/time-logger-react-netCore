using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{
		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<Timelog> Timelogs { get; set; }

		 public override int SaveChanges()
		{
			AddTimestamps();
			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			AddTimestamps();
			return await base.SaveChangesAsync(cancellationToken);
		}

		private void AddTimestamps()
		{
			var entities = ChangeTracker.Entries()
				.Where(x => x.Entity is BaseEntity && x.State == EntityState.Added);

			foreach (var entity in entities)
			{
				var now = DateTime.UtcNow;
				((BaseEntity)entity.Entity).CreatedAt = now;
			}
		}
	}
}
