using System;
using System.Collections.Generic;

namespace EfficiencyMark.Models
{
    public partial class AchievementsOfAnEmployee
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string NameOfAchievement { get; set; }

        public Employees Employee { get; set; }
    }
}
