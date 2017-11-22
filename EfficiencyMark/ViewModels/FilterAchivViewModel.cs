using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfficiencyMark.ViewModels
{
    public class FilterAchivViewModel
    {
        public FilterAchivViewModel(string Name, string NameOfTheIndicator)
        {
            SelectedName = Name;
            SelectedNameOfTheIndicator = NameOfTheIndicator;

        }

        public string SelectedName { get; set; }
        public string SelectedNameOfTheIndicator { get; set; }

    }
}
