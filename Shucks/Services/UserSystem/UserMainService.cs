using System;
using System.Collections.Generic;

using System.IO;

using Discord;
using Discord.Commands;

namespace Shucks.Services.UserSystem
{
    public class UserMainService
    {
        private Dictionary<string, MoneyOwner> moneyOwners = new Dictionary<string, MoneyOwner>();
        private string filePath = "C:\\Users\\yeosi\\source\\repos\\Shucks\\Shucks\\Services\\UserSystem\\Currency.txt";

        FileStream fs;

        public UserMainService()
        {
            if (File.Exists(filePath))
            {
                using (fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs, false))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] nameAndMoney = line.Split(':');
                            moneyOwners.Add(nameAndMoney[0], new MoneyOwner(nameAndMoney[0], int.Parse(nameAndMoney[1])));
                        }    
                    }
                    fs.Dispose();
                }
                
            }
        }

        public string addUserUsingDefaults(string name)
        {
            if (moneyOwners.ContainsKey(name))
            {
                return String.Format("User {0} already exists.", name);
            }
            else
            {
                if (File.Exists(filePath))
                {
                    using (fs = new FileStream(filePath, FileMode.Open))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(String.Format("{0}:{1}", name, UserConstants.STARTING_MONEY));
                        }
                        moneyOwners.Add(name, new MoneyOwner(name, UserConstants.STARTING_MONEY));

                        return String.Format("User {0} was added.", name);
                    }
                }
                return "Did not work";
            }
        }

        public string checkUserBalance(string userName)
        {
            MoneyOwner user;
            if (!moneyOwners.TryGetValue(userName, out user))
            {
                return "You do not exist in the database.";
            }
            else
            {
                return String.Format("User {0} has ${1}.", userName, user.Money);
            }
        }

        private String[] splitTextLine(string line)
        {
            return line.Split(':');
        }
    }
}
