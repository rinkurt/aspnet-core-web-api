using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;

namespace Routine.Api.Data
{
	public class RoutineDbContext : DbContext
	{
		public RoutineDbContext(DbContextOptions<RoutineDbContext> options) 
			: base(options)
		{
			
		}

		public DbSet<Company> Companies { get; set; }
		public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>().Property(x => x.Name)
				.IsRequired().HasMaxLength(100);
			modelBuilder.Entity<Company>().Property(x => x.Introduction)
				.HasMaxLength(500);

			modelBuilder.Entity<Employee>().Property(x => x.EmployeeNo)
				.IsRequired().HasMaxLength(10);
			modelBuilder.Entity<Employee>().Property(x => x.FirstName)
				.IsRequired().HasMaxLength(50);
			modelBuilder.Entity<Employee>().Property(x => x.LastName)
				.IsRequired().HasMaxLength(50);

			modelBuilder.Entity<Employee>()
				.HasOne(x => x.Company)
				.WithMany(x => x.Employees)
				.HasForeignKey(x => x.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Company>().HasData(
				new Company
				{
					Id = Guid.Parse("0c131a59-dbec-4527-a92a-daeb910efecb"),
					Name = "Microsoft",
					Introduction = "Great Company"
				},
				new Company
				{
					Id = Guid.Parse("080d684e-21aa-46d8-8eb8-8c6709879f59"),
					Name = "Google",
					Introduction = "Don't be evil"
				},
				new Company
				{
					Id = Guid.Parse("d76af422-8f46-429c-b576-ad4d62216861"),
					Name = "Alibaba",
					Introduction = "Fubao Company"
				});
		}
	}
}
