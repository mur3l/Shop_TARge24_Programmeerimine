using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.Dto.Serviceinterface
{
    public class IWeatherForecastServices
    {
        Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto);
    }
}
