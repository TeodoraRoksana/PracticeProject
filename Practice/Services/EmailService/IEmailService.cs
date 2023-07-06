using Practice.Models.DTO;

namespace Practice.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
