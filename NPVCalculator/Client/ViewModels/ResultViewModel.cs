using NPVCalculator.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Client.ViewModels
{
    public class ResultViewModel
    {
        public int ReferenceId { get; set; }
        public decimal InitialValue { get; set; }
        public decimal LowerBoundDiscountRate { get; set; }
        public decimal UpperBoundDiscountRate { get; set; }
        public decimal DiscountRateIncrement { get; set; }
        public string PositiveDiscountRateRange
        {
            get
            {
                return GetDiscountRateRange(NPVResultType.Positive);
            }
        }
        public string NegativeDiscountRateRange
        {
            get
            {
                return GetDiscountRateRange(NPVResultType.Negative);
            }
        }
        public List<decimal> CashFlows { get; set; } = new List<decimal>();

        public decimal TotalCashFlows
        {
            get
            {
                if (this.CashFlows.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.CashFlows.Sum();
                }
            }
        }
        public List<ResultListItemViewModel> Results { get; set; } = new List<ResultListItemViewModel>();

        private string GetDiscountRateRange(NPVResultType resultType)
        {
            decimal lowerBound;
            decimal upperBound;
            IOrderedEnumerable<ResultListItemViewModel> sortedResults;
            if (resultType == NPVResultType.Negative)
            {
                sortedResults = this.Results
                                        .Where(x => x.NPV < 0)
                                        .OrderBy(x => x.NPV);
            }
            else
            {
                sortedResults = this.Results
                        .Where(x => x.NPV >= 0)
                        .OrderBy(x => x.DiscountRate);
            }

            if (sortedResults.ToList().Count == 0)
            {
                return "N/A";
            }
            lowerBound = sortedResults.FirstOrDefault().DiscountRate;
            upperBound = sortedResults.LastOrDefault().DiscountRate;

            return (lowerBound == upperBound) ? lowerBound.ToString("#,#.00")
                    : lowerBound.ToString("#,#.00") + " - " + upperBound.ToString("#,#.00");
        }

        public static ResultViewModel MapToViewModel(NPVCriteria criteria)
        {
            var resultVM = new ResultViewModel()
            {
                ReferenceId = criteria.ReferenceId,
                CashFlows = criteria.CashFlows.Split(',')
                            .Select(x => Convert.ToDecimal(x))
                            .ToList(),
                InitialValue = criteria.InitialValue,
                LowerBoundDiscountRate = criteria.LowerBoundDiscountRate * 100,
                UpperBoundDiscountRate = criteria.UpperBoundDiscountRate * 100,
                DiscountRateIncrement = criteria.DiscountRateIncrement * 100,
            };

            resultVM.Results = criteria.Results.Select(x =>
            new ResultListItemViewModel()
            {
                DiscountRate = x.DiscountRate * 100,
                NPV = x.NPV
            }
            ).ToList();

            return resultVM;
        }

    }
}
