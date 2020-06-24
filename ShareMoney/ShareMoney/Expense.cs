using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMoney
{
    public class Expense
    {
        public Guid expenseId { get; set; }
        public string expenseName { get; set; }
        public Dictionary<Guid, decimal> expenseDetail { get; set; }
        public Category category { get; set; }
        public DateTime createTimestamp { get; set; }

        public Expense(string name, Category category)
        {
            expenseId = Guid.NewGuid();
            expenseName = name;
            expenseDetail = new Dictionary<Guid, decimal>();
            createTimestamp = DateTime.Now;
            this.category = category;
        }
    }
}
