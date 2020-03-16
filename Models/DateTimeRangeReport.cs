using System;

namespace CarShop.Models
{
    public class DateTimeRangeReport
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Manager Manager { get; set; }
        public int? DealsCount { get; set; }

        public DateTimeRangeReport() { }
    }
}
