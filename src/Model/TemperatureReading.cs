using System;
using System.ComponentModel.DataAnnotations;

namespace ODataMicroservice
{
    public class TemperatureReading
    {
        public int TemperatureReadingId { get; set; }

        [Required]
        public DateTime MeasureDateTime { get; set; }

        [Required]
        [Range(-273.15, double.MaxValue)]
        public double Temperature { get; set; }
    }
}
