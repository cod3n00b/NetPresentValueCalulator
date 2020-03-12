using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Client.ViewModels
{
    public class InputCashFlowViewModel
    {
        [Required]
        [RegularExpression("^\\d{0,}[.]{0,}\\d{1,}$", ErrorMessage = "Cash flow must be in a numeric format.")]
        public string InputCashFlow { get; set; }
    }
}
