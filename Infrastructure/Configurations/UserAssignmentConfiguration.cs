using Core.Entities.LinkingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class UserAssignmentConfiguration : IEntityTypeConfiguration<UserAssignment>
	{
		public void Configure(EntityTypeBuilder<UserAssignment> builder)
		{
			builder.HasKey(x => new {x.UserId, x.AssignmentId });
		}
	}
}
