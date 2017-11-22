
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EfficiencyMark.Models;

namespace EfficiencyMark.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext()
        { }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<AchievementsOfAnEmployee> AchievementsOfAnEmployee { get; set; }
        public virtual DbSet<ListOfIndicators> ListOfIndicators { get; set; }
        public DbSet<EfficiencyMark.Models.User> User { get; set; }
    }
}
