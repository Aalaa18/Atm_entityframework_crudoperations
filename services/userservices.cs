using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_sql.services
{
    public class userservices
    {
        private readonly ApplicationDbcontext _context;

        public userservices(ApplicationDbcontext context)
        {
            _context = context;
        }

        public bool checkuser_vipcategory(string name)
        {
            bool check = false;
            var user=_context.users.SingleOrDefault(x => x.name == name);
            if(user.category=="vip")
            {
                check = true;
                Console.WriteLine("vip");
            }
               
            else
            {
                check = false;
                Console.WriteLine("ordinary");
            }
                
            return check;
                
        }

        public void showbalance(string name)
        {
            var user=_context.users.SingleOrDefault(x=>x.name == name);
            Console.WriteLine(user != null ? $"your current balance equals {user.balance}":"incorrect name");
        }

        public bool checkusers(string name, string pass)
        {
            bool check=false;
            var user=_context.users.SingleOrDefault(x=>x.name == name&&x.pass==pass);
            if(user != null)
            {
                check = true;
                Console.WriteLine("user found");
            }
            else
            {
                check = false;
                Console.WriteLine("incorrect name or password");
            }
            return check;
        }

    }
}
