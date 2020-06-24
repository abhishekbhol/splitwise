using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMoney
{
    public class User
    {
        public Guid userId { get; set; }
        public string name { get; set; }
        public decimal totalAmoutYouOwe { get; set; }

        public User(string userName)
        {
            userId = Guid.NewGuid();
            name = userName;
            totalAmoutYouOwe = 0;
        }
    }
}
