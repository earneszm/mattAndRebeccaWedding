using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MattAndRebeccaWedding.Utilities
{
    public static class rsvpIdGenerator
    {
        static char[] IdArray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                           'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        static int intIdLength = 6;

        public static string GetNewRsvpId()
        {
            StringBuilder sb = new StringBuilder();

            Random ran = new Random();

            for (int i = 0; i < intIdLength; i++)
            {
                sb.Append(IdArray[ran.Next(0, IdArray.Length)]);
            }

            return sb.ToString();
        }
    }
}