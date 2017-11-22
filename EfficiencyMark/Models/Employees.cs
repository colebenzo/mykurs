using System;
using System.Collections.Generic;

namespace EfficiencyMark.Models
{
    public partial class Employees
    {
        public Employees()
        {
            AchievementsOfAnEmployee = new HashSet<AchievementsOfAnEmployee>();
            ListOfIndicators = new HashSet<ListOfIndicators>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime YearOfBirth { get; set; }
        public int Phone { get; set; }

        public ICollection<AchievementsOfAnEmployee> AchievementsOfAnEmployee { get; set; }
        public ICollection<ListOfIndicators> ListOfIndicators { get; set; }
    }
}
