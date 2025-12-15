using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IEmailServices
    {
        void SendEmail(EmailDto dto);
        void SendEmailToken(EmailTokenDto dto, string token);
    }
}