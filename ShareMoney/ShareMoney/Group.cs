using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMoney
{
    public class Group
    {
        public Guid groupId { get; set; }
        public string groupName { get; set; }
        public List<User> members { get; set; }
        public List<Expense> expenseList { get; set; }
        public List<Expense> settledExpenseList { get; set; }


        public Group(string name)
        {
            members = new List<User>();
            expenseList = new List<Expense>();
            settledExpenseList = new List<Expense>();
            groupName = name;
            groupId = Guid.NewGuid();
        }

        public string AddMember(User user)
        {
            if(members.Contains(user))
            {
                return "Already a member";
            }

            members.Add(user);
            return "success";
        }

        public string AddExpense(Expense expense)
        {
            expenseList.Add(expense);
            return "success";
        }

        public Dictionary<User, decimal> SimplyfyDebt()
        {
            var totalexpenses = 0m;
            var result = new Dictionary<User, decimal>();

            foreach (var member in members)
            {
                var paid = 0m;
                foreach (var exp in expenseList)
                {
                    paid += exp.expenseDetail.Where(a => a.Key == member.userId).Sum(a => a.Value);
                }

                totalexpenses += paid;
                result.Add(member, paid);
            }

            var perPersonShare = totalexpenses / members.Count();
            foreach(var member in members)
            {
                var owe = perPersonShare - result[member];
                member.totalAmoutYouOwe += owe;
                result[member] = owe;
            }

            return result;
        }

        public void SettleGroupExpenses()
        {
            settledExpenseList.AddRange(expenseList);
            expenseList.Clear();
        }
    }
}
