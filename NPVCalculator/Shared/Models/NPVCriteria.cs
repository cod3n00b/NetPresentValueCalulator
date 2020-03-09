using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Shared.Models
{
    public class NPVCriteria
    {
        [Key]
        public int ReferenceId { get; set; }
        public string CashFlows { get; set; }
        public decimal InitialValue { get; set; }
        public decimal LowerBoundDiscountRate { get; set; }
        public decimal UpperBoundDiscountRate { get; set; }
        public decimal DiscountRateIncrement { get; set; }
        public List<NPVResult> Results { get; set; } = new List<NPVResult>();
        public DateTime DateCreated { get; set; }

        public decimal ComputeNPV(decimal currentDiscount)
        {
            decimal[] cashFlows = GetCashFlowValues();
            decimal totalCashFlows = cashFlows.Sum();
            decimal npv = (totalCashFlows / (1 + currentDiscount)) - this.InitialValue;
            return npv;
        }

        public decimal[] GetCashFlowValues()
        {
            var cashflows = this.CashFlows.Split(',')
                .Select<string, decimal>(x => Convert.ToDecimal(x));
            return cashflows.ToArray();
        }

        public void ComputeNPVForAllDiscounts()
        {
            this.Results.Clear();

            decimal currentDiscount = 0;
            for (currentDiscount = this.LowerBoundDiscountRate;
                currentDiscount <= this.UpperBoundDiscountRate;
                currentDiscount += this.DiscountRateIncrement)
            {

                if (this.Results.Exists(x => x.DiscountRate == currentDiscount)) {
                    break;
                }

                var result = new NPVResult()
                {
                    ReferenceId = this.ReferenceId,
                    Criteria = this,
                    DiscountRate = currentDiscount,
                    NPV = ComputeNPV(currentDiscount)
                };

                this.Results.Add(result);
            }
        }

    }
}
