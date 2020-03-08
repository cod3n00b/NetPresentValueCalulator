using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPVCalculator.Shared.Models
{
    public class NPVResult
    {
        [Key]
        public int ResultId { get; set; }
        public int ReferenceId { get; set; }
        [ForeignKey("ReferenceId")]
        public NPVCriteria Criteria { get; set; }
        public string CashFlows { get; set; }
        public decimal InitialValue { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal NPV { get; set; }

    }
}
