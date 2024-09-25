using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace itext7_sample
{
    internal class Flexible
    {
        public void Flexible_Regex_Patterns(string section)
        {
            var invoiceNoMatch = Regex.Match(section, @"(?:Invoice|Inovide)\s*No[\s,:]*([\w\d]+)", RegexOptions.IgnoreCase);
        }

        public void Optional_Fields(string section)
        {
            // Extract Status (which might sometimes be empty or have different labels)
            var statusMatch = Regex.Match(section, @"Status\s+(\w+)", RegexOptions.IgnoreCase);
            if (statusMatch.Success)
            {
                invoice.Status = statusMatch.Groups[1].Value.Trim();
            }
            else
            {
                invoice.Status = "Unknown";
            }
        }
    }
}
