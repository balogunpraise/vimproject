using Core.Entities.LinkingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
	{
		public void Configure(EntityTypeBuilder<UserCourse> builder)
		{
			builder.HasKey(x => new {x.UserId, x.CourseId});
		}
	}
}
