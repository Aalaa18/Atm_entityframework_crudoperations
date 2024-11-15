using Atm_sql.services;

namespace Atm_sql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbcontext();

            userservices userservices = new userservices(context);
            operations operations = new operations(context);

            while (true)
            {
                operations.start();
                Console.WriteLine("Enter your choice");

                var an = (Console.ReadLine());
                if (int.Parse(an) == 1)
                {
                    Console.Write("please enter your user name: ");
                    string name = Console.ReadLine();
                    Console.Write("please enter the password: ");
                    string pass = Console.ReadLine();
                    operations.save_nonmange(name, an);
                    //operations.save_nonmange(name, an);
                    if (userservices.checkusers(name, pass))
                    {
                        while (true)
                        {
                            operations.showoptions(name);
                            Console.WriteLine("Enter your choice");
                            string z = Console.ReadLine();
                            if (z.ToLower() == "x" || z.ToUpper() == "X")
                            {
                                // operations.save_nonmange(name, z);
                                operations.save_nonmange(name, z);
                                break;
                            }
                            int x = int.Parse(z);
                            if (x == 1)
                            {
                                userservices.showbalance(name);



                            }
                            else if (x == 2)
                            {

                                Console.WriteLine("enter the deposite amount ");
                                var dep = Console.ReadLine();
                                double value = operations.deposite(name, Math.Abs(double.Parse(dep)));
                                Console.WriteLine($"the balance after deposite :{value}");
                                //users.display();

                            }
                            else if (x == 3)
                            {
                                Console.WriteLine("enter the withdraw amount");
                                var with = Console.ReadLine();

                                double value2 = operations.withdraw(name, Math.Abs(double.Parse(with)));
                                Console.WriteLine($"the balance after withdraw :{value2}");
                            }
                            else if (x == 4)
                            {
                                Console.WriteLine("please enter the Reciever Name");
                                var rec_name = Console.ReadLine();
                                Console.WriteLine("please enter the amount");
                                var rec_amount = double.Parse(Console.ReadLine());
                                operations.transfermoney(name, rec_name, rec_amount);



                            }
                            else if (x == 5)
                            {

                                operations.recieveMoney(name);

                            }
                            else if (x == 6)
                            {

                                operations.DisplayTransactions(name);

                            }
                            else if (x == 7)
                            {

                                operations.display_nonmange(name);

                            }
                            else if (x == 8 && userservices.checkuser_vipcategory(name))
                            {
                                operations.addusers(z);
                                operations.save_nonmange(name, z);


                            }
                            else if (x == 9 && userservices.checkuser_vipcategory(name))
                            {
                                Console.WriteLine("please enter the user name");
                                var user_name = Console.ReadLine();
                                //users.display();
                                operations.removeusers(user_name, name);
                            }

                        }
                    }
                }
                else if (int.Parse(an) == 2)
                {

                    operations.addusers(an);
                   
                    Console.WriteLine("now you can login ! ");





                }
            }
        }
    }
}