using System;
using System.ComponentModel.DataAnnotations;

namespace ODataMicroservice
{
    public class Alert
    {
        public int AlertId { get; set; }

        [Required]
        [StringLength(250)]
        public string Message { get; set; }

        public TemperatureReading TemperatureReading { get; set; }

        public int TemperatureReadingId { get; set; }
    }
}
