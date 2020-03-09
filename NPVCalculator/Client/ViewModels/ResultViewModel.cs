using NPVCalculator.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Client.ViewModels
{
    public class ResultViewModel
    {
        public string CashFlows { get; set; }
        public decimal InitialValue { get; set; }
        public float LowerBoundDiscountRate { get; set; }
        public float UpperBoundDiscountRate { get; set; }
        public float DiscountRateIncrement { get; set; }
        public List<ResultListItemViewModel> Results { get; set; }

        public static ResultViewModel MapToViewModel(NPVCriteria criteria)
        {
            var resultVM = new ResultViewModel()
            {
                CashFlows = criteria.CashFlows,
                InitialValue = criteria.InitialValue,
                LowerBoundDiscountRate = float.Parse((criteria.LowerBoundDiscountRate * 100).ToString()),
                UpperBoundDiscountRate = float.Parse((criteria.UpperBoundDiscountRate * 100).ToString()),
                DiscountRateIncrement = float.Parse((criteria.DiscountRateIncrement * 100).ToString())
            };

            resultVM.Results = criteria.Results.Select(x =>
            new ResultListItemViewModel()
            {
                CashFlows = resultVM.CashFlows,
                InitialValue = resultVM.InitialValue,
                DiscountRate = float.Parse(x.DiscountRate.ToString()),
                NPV = x.NPV
            }
            ).ToList();

            return resultVM;
        }
    }
}
