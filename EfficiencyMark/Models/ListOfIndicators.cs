using System;
using System.Collections.Generic;

namespace EfficiencyMark.Models
{
    public partial class ListOfIndicators
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string NameOfTheIndicator { get; set; }
        public Employees Employee { get; set; }
    }
}
