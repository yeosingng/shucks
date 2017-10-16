using System;
using System.Collections.Generic;
using System.Text;

namespace Shucks.Services.UserSystem
{
    class MoneyOwner
    {
        private int money;
        private string name;

        public MoneyOwner(String name)
        {
            this.name = name;
            money = UserConstants.STARTING_MONEY;
        }

        public MoneyOwner(String name, int money)
        {
            this.name = name;
            this.money = money;
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        override
        public string ToString()
        {
            return name;
        }
    }
}
