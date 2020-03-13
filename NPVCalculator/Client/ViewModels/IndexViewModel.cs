using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NPVCalculator.Client.ViewModels
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Please enter Reference ID.")]
        [RegularExpression("\\d+", ErrorMessage = "Reference ID should be a number.")]
        public string ReferenceId { get; set; }
    }
}
