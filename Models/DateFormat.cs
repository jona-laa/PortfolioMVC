using System;

namespace PortfolioMVC.Models
{
    public class DateFormat
    {
        public string ToReadableDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}