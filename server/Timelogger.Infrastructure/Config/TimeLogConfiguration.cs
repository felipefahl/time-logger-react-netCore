using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Entities;

namespace Timelogger.Infrastructure.Entities
{
	public class TimeLogConfiguration : IEntityTypeConfiguration<Timelog>
    {
        public void Configure(EntityTypeBuilder<Timelog> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder
                .HasOne(e => e.Project)
                .WithMany(e => e.TimeLogs)
                .HasForeignKey(e => e.ProjectId)
                .IsRequired();
        }
    }
}