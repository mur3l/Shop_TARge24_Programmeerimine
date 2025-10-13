using System;

namespace ShopTARge24.Core.Dto
{
    public class AccuLocationRootDto
    {
        public string LocalObservationDateTime { get; set; } = string.Empty;
        public int EpochTime { get; set; }
        public string WeatherText { get; set; } = string.Empty;
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; } = string.Empty;
        public bool IsDayTime { get; set; }
        public AccuTemperatureDto? Temperature { get; set; }
        public string MobileLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }

    public class AccuTemperatureDto
    {
        public AccuWeatherUnitDto? Metric { get; set; }
        public AccuWeatherUnitDto? Imperial { get; set; }

        public class AccuWeatherUnitDto
        {
            public double Value { get; set; }
            public string Unit { get; set; } = string.Empty;
            public int UnitType { get; set; }
        }
    }
}
