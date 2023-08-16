using Chatverse.UI.DTOs.SingleDto;

namespace Chatverse.UI.Services
{
    public class DateTimeConvertService : IDateTimeConvertService
    {
        public DateTimeDto Customize(DateTime sendDate)
        {
            DateTimeDto date = new DateTimeDto();
          
                
                string format1 = sendDate.ToString("d MMM yyyy");
                string format2 = sendDate.ToString("h:mm tt").ToLower();
            date.Hour = format2;
            date.DayYear = format1;
            
            return date;
        }
    }
}
