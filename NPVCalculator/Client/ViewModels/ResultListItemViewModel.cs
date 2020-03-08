using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Client.ViewModels
{
    public class ResultListItemViewModel
    {
        public string CashFlows { get; set; }
        public decimal InitialValue { get; set; }
        public float DiscountRate { get; set; }
        public decimal NPV { get; set; }
    }
}
