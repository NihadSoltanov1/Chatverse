using Chatverse.UI.DTOs.SingleDto;

namespace Chatverse.UI.Services
{
    public interface IDateTimeConvertService
    {
        DateTimeDto Customize(DateTime sendDate);
    }
}
