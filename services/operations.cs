using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Atm_sql.services
{
    public class operations
    {
        private readonly ApplicationDbcontext _context;

        public operations(ApplicationDbcontext context)
        {
            _context = context;
        }

        private users GetUsers(string name)
        {
            return _context.users.SingleOrDefault(x => x.name == name);
        }

        public void start()
        {
            Console.WriteLine(@"
1- login
2- create account
");
        }
        public void showoptions(string name)
        {
            var user = GetUsers(name);
            if (user.category == "vip")
            {


                Console.WriteLine(@"
1- Perform a current balance inquiry
2- Perform a deposit
3- Perform a withdrawal
4- make a transaction
5- show recieved transcation
6- show sent transactions
7- show non_managerical operations
8-Add new user
9-remove a user
press x to exit

"
);
            }
            else
            {
                Console.WriteLine(@"
1- Perform a current balance inquiry
2- Perform a deposit
3- Perform a withdrawal
4- make a transaction
5- show recieved transcation
6- show sent transactions
7- show non_managerical operations
press x to exit

");
            }


        }
        public double deposite(string name, double amount)
        {
            var user = GetUsers(name);

            user.balance += amount;
            _context.SaveChanges();
            return user.balance;


        }
        public double withdraw(string name, double amount)
        {
            var user = GetUsers(name);
            if (amount > user.balance)
            {
                Console.WriteLine("Insufficient balance");
            }
            else
            {
                user.balance -= amount;
                _context.SaveChanges();
            }
            return user.balance;
        }
        internal int count = 1;
        public void transfermoney(string sender, string reciever, double amount)
        {
            var user = GetUsers(sender);
            if (user.max_count > 10)
            {
                Console.WriteLine("sorry you have reached your max operations today");
            }
            else
            {
                if (user.balance > amount)
                {
                    var user2 = GetUsers(reciever);
                    var tran = new TransactionInfo
                    {
                        UserId = user2.id,
                        TransactionId = 1,
                        sender_Username = sender,
                        OperationAmount = amount,
                        balancebefore = user2.balance,
                        operationdatetime = DateTime.Now,
                        iscomplete = false,
                        operation_name = "transfer money",
                        reciever_Username = reciever,
                        balanceafter = 0,

                    };
                    user.max_count = count++;
                    _context.transactionInfos.Add(tran);
                    Console.WriteLine("Added successfully");
                    user.balance -= amount;
                    _context.SaveChanges();
                }

                else
                {

                    Console.WriteLine("there's no sufficient amount of money");
                }
            }
        }
        public void DisplayTransactions(string name)
        {

            var trans2 = _context.transactionInfos.Where(x => x.sender_Username == name).ToList();
            if (trans2.Count > 0)
            {
                foreach (var trans in trans2)
                {
                    if(trans.reciever_Username!="")
                    {
                        Console.WriteLine("Transaction ID: " + trans.TransactionId);
                        Console.WriteLine("Username: " + trans.reciever_Username);
                        Console.WriteLine("Operation ID: " + trans.OperationId);
                        Console.WriteLine("Operation Amount: " + trans.OperationAmount);
                        Console.WriteLine("Operation Date and Time: " + trans.operationdatetime);
                        Console.WriteLine("Is Complete: " + trans.iscomplete);
                        Console.WriteLine("------------------------------------");
                    }

                }
            }
            else
            {
                Console.WriteLine("there's no operations done yet");
            }
        }
        public void recieveMoney(string name)
        {
            var trans = _context.transactionInfos.SingleOrDefault(x => x.reciever_Username == name);
            var user = GetUsers(name);
            if (trans != null && !trans.iscomplete)
            {
                Console.WriteLine(@"Sender_Name:" + " " + trans.sender_Username + " " + "Operation ID: " + trans.OperationId + " " + "Operation Amount: " + trans.OperationAmount + " " + "Operation Date and Time: " + trans.operationdatetime
                 + " " + "if you want to accept it please enter the operation_id other wise press x"
                   );
                string x = Console.ReadLine();
                while (true)
                {
                    if (x.ToLower() == "x")
                    {
                        break;
                    }
                    else
                    {
                        trans.balanceafter = user.balance + trans.OperationAmount;
                        trans.iscomplete = true;
                        user.balance = trans.balanceafter;
                        Console.WriteLine("Transaction accepted and balance updated.");
                        _context.SaveChanges();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("there's no transaction");
            }

        }
        public void removeusers(string name, string sender)
        {
            var trans = _context.transactionInfos.Where(x => x.reciever_Username == name).ToList();
            var user = GetUsers(sender);
            if (trans.Count()==0)
            {
                _context.users.Remove(GetUsers(name));
            }
            else
            {
                foreach (var transactionInfo in trans)
                {
                    Console.WriteLine("Alerttttt there's a transactions");
                    Console.WriteLine(@"sender_name:" + " " + transactionInfo.sender_Username + " " + "operation_id:" + " " + transactionInfo.OperationId + " " + "amount:" + " " + transactionInfo.OperationAmount);
                    Console.WriteLine("Do you want to cancel these transactions  y/n ");
                }
                char c = char.Parse(Console.ReadLine());
                foreach (var transactionInfo in trans)
                {
                    if (c == 'n' || c == 'N')
                    {
                        break;

                    }
                    else if (c == 'y' || c == 'Y')
                    {
                        _context.users.Remove(GetUsers(name));
                        user.balance += transactionInfo.OperationAmount;
                        Console.WriteLine("removed succesfly");
                        break;

                    }
                }
            }
            _context.SaveChanges();
        }
        public void addusers(string x)
        {

            while (true)
            {
                Console.WriteLine("please enter the user name");
                var user_name = Console.ReadLine();

                Console.WriteLine("please enter the  password the password should be at lease 8 characters");
                var user_pass = Console.ReadLine();

                Console.WriteLine("please enter the  email it should be like exaple***@gmail.com");
                var user_mail = Console.ReadLine();

                Console.WriteLine("please enter the  birthdate in the format MM/dd/yyyy:\" ");
                var user_birthdate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("please enter the  balance you want too add");
                var user_balance = double.Parse(Console.ReadLine());
                if (user_pass.Length > 8 && user_mail.Contains("@gmail.com") && GetUsers(user_name) == null)
                {
                    var user = new users
                    {
                        balance = user_balance,
                        birthdate = user_birthdate,
                        email = user_mail,
                        name = user_name,
                        pass = user_pass,
                        category = user_balance > 10000 ? "vip" : "ordinary",
                        max_count = 0,

                    };

                    _context.users.Add(user);
                   
                    _context.SaveChanges();
                    save_nonmange(user_name, x);
                    break;

                }
                else
                {
                    Console.WriteLine("there's something wrong please reenter the credientails again");
                }
            }

        }
        public void save_nonmange(string sender, string x)
        {
            var user=GetUsers(sender);
            if (int.TryParse(x, out int z))
            {
                var info = new TransactionInfo
                {
                    sender_Username = sender,
                    UserId = user.id,  // Use the user id fetched earlier
                    operationdatetime = DateTime.Now,
                    balanceafter = 0,
                    balancebefore = 0,
                    iscomplete = false,
                    OperationAmount = 0

                };
                if (z == 1)
                {
                    info.operation_name = "login";
                        info.TransactionId = 2;
                    info.reciever_Username = "";

                }
                else if (z == 2 || z == 6)
                {
                    info.operation_name = "create account";
                    info.TransactionId = 3;
                    info.reciever_Username = "";


                }
                _context.transactionInfos.Add(info);
            }
            else if (x.ToLower() == "x" || x.ToUpper() == "X")
            {
                var info = new TransactionInfo
                {
                    sender_Username = sender,
                    TransactionId = 3,
                    operation_name = "Log out",
                    operationdatetime = DateTime.Now,
                    reciever_Username = "",
                    balanceafter = 0,
                    balancebefore = 0,
                    iscomplete = false,
                    OperationAmount = 0,
                    UserId = user.id


                };
                _context.transactionInfos.Add(info);
                
            }
            _context.SaveChanges();

        }
        public void display_nonmange(string name)
        {
            var transs = _context.transactionInfos.Where(x => x.sender_Username == name).ToList();
         
                foreach (var trans in transs)
                {
                    Console.WriteLine(trans.sender_Username + " " + trans.TransactionId + " " + trans.operationdatetime + " " + trans.operation_name);
                }
            
      
               
        }
    }
}
