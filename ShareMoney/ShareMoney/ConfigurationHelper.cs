using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMoney
{
    public class ConfigurationHelper
    {
        public static string ConnString()
        {
            return "Server=localhost; database=shareMoney; UID=root; password=\"\"";
        }

    }
}
