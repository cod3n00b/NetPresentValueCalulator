using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Client.ViewModels
{
    public class CashFlowViewModel
    {
        public CashFlowViewModel(decimal cashFlow)
        {
            this.CashFlow = cashFlow;
        }
        public decimal CashFlow { get; set; }

        public string ToString(string format)
        {
            return this.CashFlow.ToString(format);
        }

        public override string ToString()
        {
            return this.CashFlow.ToString();
        }
    }
}
