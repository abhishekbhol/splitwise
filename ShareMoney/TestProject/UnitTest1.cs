using NUnit.Framework;
using ShareMoney;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        ShareMoneyService sm;

        [SetUp]
        public void Setup()
        {
            sm = new ShareMoneyService();
            sm.setUp();
        }

        [Test]
        public void TestGroupAndMembers()
        {
            Add4GlobalUsers();
            AddMembersAnd1Group(sm.userList.GetRange(0,3), "flat");

            var users = sm.GetAllUsers();
            var groups = sm.GetAllGroups();

            Assert.AreEqual(4, users.Count);
            Assert.AreEqual(1, groups.Count);
            Assert.AreEqual(3, groups[0].members.Count);
        }

        [Test]
        public void TestSimplifyDebt()
        {
            Add4GlobalUsers();
            AddMembersAnd1Group(sm.userList.GetRange(0, 3), "flat");

            var groups = sm.GetAllGroups();

            Add2Expenses(groups[0]);
            var res = groups[0].SimplyfyDebt();

            Assert.AreEqual(res[groups[0].members[0]], 800);
            Assert.AreEqual(res[groups[0].members[1]], 100);
            Assert.AreEqual(res[groups[0].members[2]], -900);
        }

        [Test]
        public void TestSettleGroupExpenses()
        {
            Add4GlobalUsers();
            AddMembersAnd1Group(sm.userList.GetRange(0, 3), "flat");

            var groups = sm.GetAllGroups();

            Add2Expenses(groups[0]);
            var res = groups[0].SimplyfyDebt();

            Assert.AreEqual(res[groups[0].members[0]], 800);
            Assert.AreEqual(res[groups[0].members[1]], 100);
            Assert.AreEqual(res[groups[0].members[2]], -900);

            groups[0].SettleGroupExpenses();

            res = groups[0].SimplyfyDebt();

            Assert.AreEqual(res[groups[0].members[0]], 0);
            Assert.AreEqual(res[groups[0].members[1]], 0);
            Assert.AreEqual(res[groups[0].members[2]], 0);

        }

        [Test]
        public void TestMultipleGroups()
        {
            Add4GlobalUsers();
            AddMembersAnd1Group(sm.userList.GetRange(0, 3), "flat");
            AddMembersAnd1Group(sm.userList.GetRange(1, 3), "office");

            var groups = sm.GetAllGroups();

            Assert.AreEqual(2, groups.Count);

            Add2Expenses(groups[0]);
            Add2OfficeExpenses(groups[1]);

            var res = groups[0].SimplyfyDebt();

            Assert.AreEqual(res[groups[0].members[0]], 800);
            Assert.AreEqual(res[groups[0].members[1]], 100);
            Assert.AreEqual(res[groups[0].members[2]], -900);

            res = groups[1].SimplyfyDebt();

            Assert.AreEqual(res[groups[1].members[0]], 500);
            Assert.AreEqual(res[groups[1].members[1]], 1500);
            Assert.AreEqual(res[groups[1].members[2]], -2000);

            var users = sm.GetAllUsers();

            Assert.AreEqual(users[0].totalAmoutYouOwe, 800);
            Assert.AreEqual(users[1].totalAmoutYouOwe, 600);
            Assert.AreEqual(users[2].totalAmoutYouOwe, 600);
            Assert.AreEqual(users[3].totalAmoutYouOwe, -2000);

            Assert.AreEqual(users.Sum(a => a.totalAmoutYouOwe), 0);

        }

        private void Add4GlobalUsers()
        {
            sm.userList.Add(new User("Abhi"));
            sm.userList.Add(new User("Ram"));
            sm.userList.Add(new User("Shyam"));
            sm.userList.Add(new User("Ravi"));
        }

        private void AddMembersAnd1Group(List<User> userList, string groupName)
        {
            var flat = new Group(groupName);

            foreach(var user in userList)
            {
                flat.AddMember(user);
            }

            sm.groups.Add(flat);
        }

        private void Add2Expenses(Group group)
        {

            var rent = new Expense("rent", Category.Rent);
            rent.expenseDetail.Add(group.members[0].userId, 1000);
            rent.expenseDetail.Add(group.members[1].userId, 2000);
            rent.expenseDetail.Add(group.members[2].userId, 3000);

            group.AddExpense(rent);

            var movie = new Expense("movie", Category.Party);
            movie.expenseDetail.Add(group.members[0].userId, 600);
            movie.expenseDetail.Add(group.members[1].userId, 300);
            movie.expenseDetail.Add(group.members[2].userId, 300);

            group.AddExpense(movie);

        }

        private void Add2OfficeExpenses(Group group)
        {

            var outing = new Expense("outing", Category.Party);
            outing.expenseDetail.Add(group.members[0].userId, 1200);
            outing.expenseDetail.Add(group.members[1].userId, 800);
            outing.expenseDetail.Add(group.members[2].userId, 4000);

            group.AddExpense(outing);

            var booz = new Expense("booz", Category.Medical);
            booz.expenseDetail.Add(group.members[0].userId, 900);
            booz.expenseDetail.Add(group.members[1].userId, 300);
            booz.expenseDetail.Add(group.members[2].userId, 600);

            group.AddExpense(booz);

        }

        private void Add3Expenses(Group group)
        {

            var rent = new Expense("rent", Category.Rent);
            rent.expenseDetail.Add(group.members[0].userId, 1000);
            rent.expenseDetail.Add(group.members[1].userId, 2000);
            rent.expenseDetail.Add(group.members[2].userId, 3000);

            group.AddExpense(rent);

            var movie = new Expense("movie", Category.Party);
            movie.expenseDetail.Add(group.members[0].userId, 600);
            movie.expenseDetail.Add(group.members[1].userId, 300);
            movie.expenseDetail.Add(group.members[2].userId, 300);

            group.AddExpense(movie);

            var party = new Expense("party", Category.Party);
            party.expenseDetail.Add(group.members[0].userId, 3000);

            group.AddExpense(party);

        }
    }
}