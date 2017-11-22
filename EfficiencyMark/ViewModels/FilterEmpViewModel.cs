using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfficiencyMark.ViewModels
{
    public class FilterEmpViewModel
    {
        public FilterEmpViewModel(string Surname, string Name, string MiddleName)
        {
            SelectedName = Name;
            SelectedSurname = Surname;
            SelectedMiddleName = MiddleName;

        }

        public string SelectedName { get; set; }
        public string SelectedSurname { get; set; }
        public string SelectedMiddleName { get; set; }
    }
}
