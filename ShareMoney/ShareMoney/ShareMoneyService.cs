using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMoney
{
    public class ShareMoneyService
    {
        public List<Group> groups;
        public List<User> userList;

        public void setUp()
        {
            groups = new List<Group>();
            userList = new List<User>();
        }
        public Dictionary<User, int> simplyfyDebt(Group group)
        {
            var result = new Dictionary<User, int>();



            return result;
        }

        public List<User> GetAllUsers()
        {
            return userList;
        }

        public List<Group> GetAllGroups()
        {
            return groups;
        }
    }
}
