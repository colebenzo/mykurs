using EfficiencyMark.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EfficiencyMark.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Employees> Employees { get; set; }
        public IEnumerable<AchievementsOfAnEmployee> AchievementsOfAnEmployee { get; set; }
        public IEnumerable<ListOfIndicators> ListOfIndicators { get; set; }
        public PageViewModel PageViewModel { get; set; }

        
        public FilterAchivViewModel FilterAchivViewModel { get; set; }
        public FilterEmpViewModel FilterEmpViewModel { get; set; }
        public FilterListViewModel FilterListViewModel { get; set; }
        
    }
}
