using System;
using System.Collections.Generic;
using System.Text;

namespace universityBD
{
    public class WeekDays
    {
        public static String Parse(int number)
        {
            switch(number)
            {
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                case 7:
                    return "Sunday";
                default:
                    return "ERROR";
            }
        }
    }
}
