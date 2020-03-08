﻿using NPVCalculator.Shared.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Client.ViewModels
{
    public class CriteriaViewModel
    {
        [Required]
        [Display(Name = "Cash Flows")]
        public string CashFlows { get; set; }
        [Required]
        [RegularExpression("^\\d{0,}[.]{0,}\\d{1,}$", ErrorMessage = "Initial Value must be a number")]
        [Display(Name = "Initial Value")]
        public string InitialValue { get; set; }
        [Required]
        [RegularExpression("^\\d{0,}[.]{0,}\\d{1,}$", ErrorMessage = "Lower Bound Discount Rate must be a number")]
        [Display(Name = "Lower Bound Discount Rate")]
        public string LowerBoundDiscountRate { get; set; }
        [Required]
        [Display(Name = "Upper Bound Discount Rate")]
        [RegularExpression("^\\d{0,}[.]{0,}\\d{1,}$", ErrorMessage = "Upper Bound Discount Rate must be a number")]
        public string UpperBoundDiscountRate { get; set; }
        [Required]
        [Display(Name = "Discount Rate Increment")]
        [RegularExpression("^\\d{0,}[.]{0,}\\d{1,}$", ErrorMessage = "Discount Rate Increment must be a number")]
        public string DiscountRateIncrement { get; set; }

        public NPVCriteria MapToModel()
        {
            var criteria = new NPVCriteria()
            {
                CashFlows = this.CashFlows,
                InitialValue = Convert.ToDecimal(this.InitialValue),
                LowerBoundDiscountRate = Convert.ToDecimal(this.LowerBoundDiscountRate) / 100,
                UpperBoundDiscountRate = Convert.ToDecimal(this.UpperBoundDiscountRate) / 100,
                DiscountRateIncrement = Convert.ToDecimal(this.DiscountRateIncrement) / 100
            };

            return criteria;
        }
    }
}
