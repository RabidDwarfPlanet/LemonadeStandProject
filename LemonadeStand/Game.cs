using LemonadeStand.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    internal class Game
    {
        public int day;
        Store store = new Store();
        WeatherSystem weather = new WeatherSystem();
        Player player = new Player();
        List<Customer> daysCustomers = new List<Customer>();
        Random r = new Random();
        int milliseconds = 1000;
        public double totalProfits;

        public Game()
        {
            day = 1;
        }

        
        public void welcomeMessage()
        {
            Console.WriteLine("Welcome to this Lemonade Stand Game!\nIn this game you are going to have 7 days to try to earn as much money from your stand as you can");
            Console.WriteLine("Each day you will be given a forecast that will have an impact on your ability to make sales");
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        public void NamePlayer()
        {
            Console.Clear();
            Console.WriteLine("What would you like to name your character");
            player.name = Console.ReadLine();
        }

        public void GenerateCustomers(double weatherModifier, double temp)
        {
            double customers = r.Next(10, 20);
            customers *= weatherModifier;
            customers *= temp / 75;
            int finalCustomers = (int)customers;
            daysCustomers.Clear();

            for (int i = 0; i < finalCustomers; i++)
            {
                Customer customer = new Customer();
                daysCustomers.Add(customer);
            }
        }

        public double WillCustomersPurchace(double tempurature, double weatherModifier, int ice, double price)
        {
            double todaysProfits = 0;
            Console.Clear();
            player.pitcher.Clear();
            bool stocked = CanMakeNewPitcher();
            if(stocked == true && player.inventory.cups.Count != 0 && player.inventory.iceCubes.Count >= ice)
            {
                Console.WriteLine("Your lemonade stand is open for buisness!");
                MakeNewPitcher();
                foreach (Customer customer in daysCustomers)
                {

                    if (player.pitcher.Count == 0)
                    {
                        stocked = CanMakeNewPitcher();
                        if (stocked == true) { MakeNewPitcher(); }
                        else
                        {
                            Console.WriteLine("You have run out of supplies and must close for the day");
                            break;
                        }
                    }
                    if (player.inventory.iceCubes.Count < ice)
                    {
                        Console.WriteLine("You have run out of supplies and must close for the day");
                        break;
                    }
                    else if (player.inventory.cups.Count == 0)
                    {
                        Console.WriteLine("You have run out of supplies and must close for the day");
                        break;
                    }
                    
                    Console.WriteLine("\nA customer approaches your stand");
                    customer.setDesire(tempurature, weatherModifier, ice);
                    if (customer.wallet.Money >= price)
                    {
                        if (customer.desire < 2)
                        {
                            if (price <= 0.5)
                            {
                                todaysProfits += MakeASale(price, ice, customer);
                                Console.WriteLine("I guess I'll get a cup");
                            }
                            else { customer.reasonForNotPurchasing(); }
                        }
                        else if (customer.desire < 4 && customer.desire >= 2)
                        {
                            if (price <= 1.0)
                            {
                                todaysProfits += MakeASale(price, ice, customer);
                                Console.WriteLine("Alright, I'll take a cup of lemonade");
                            }
                            else { customer.reasonForNotPurchasing(); }
                        }
                        else if (customer.desire < 6 && customer.desire >= 4)
                        {
                            if (price <= 2.0)
                            {
                                todaysProfits += MakeASale(price, ice, customer);
                                Console.WriteLine("I'd like a cup of lemondade please");
                            }
                            else { customer.reasonForNotPurchasing(); }
                        }
                        else if (customer.desire < 8 && customer.desire >= 6)
                        {
                            if (price <= 2.5)
                            {
                                todaysProfits += MakeASale(price, ice, customer);
                                Console.WriteLine("A cup of lemonade sounds pretty good!");
                            }
                            else { customer.reasonForNotPurchasing(); }
                        }
                        else
                        {
                            if (price <= 4.0)
                            {
                                todaysProfits += MakeASale(price, ice, customer);
                                Console.WriteLine("I'd love a cup of lemonade!");
                            }
                            else { customer.reasonForNotPurchasing(); }
                        }
                    }
                    else { Console.WriteLine("Sorry, but your lemonade is just too expensive"); }
                    Thread.Sleep(milliseconds);
                }

            }
            else
            {
                Console.WriteLine("You do not have enough supplies to open today");
                
            }
            return todaysProfits;
        }

        public double MakeASale(double price, int ice, Customer customer)
        {
            customer.wallet.PayMoneyForItems(price);
            player.wallet.AcceptMoney(price);
            double profits = price;
            totalProfits += price;
            player.pitcher.RemoveAt(0); player.inventory.RemoveCupsFromInventory(1); player.inventory.RemoveIceCubesFromInventory(ice);
            return profits;
        }

        public bool CanMakeNewPitcher()
        {
            if (player.inventory.lemons.Count >= player.recipe.numberOfLemons && player.inventory.sugarCubes.Count >= player.recipe.numberOfSugarCubes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void MakeNewPitcher()
        {
            player.inventory.RemoveLemonsFromInventory(player.recipe.numberOfLemons);
            player.inventory.RemoveSugarCubesFromInventory(player.recipe.numberOfSugarCubes);
            for (int i = 0; i < 8; i++)
            {
                Lemonade lemonade = new Lemonade();
                player.pitcher.Add(lemonade);
            }
        }   
            
            
        

        public void DayCycle()
        {
            while (day <= 7)
            {
                Console.Clear();
                weather.setWeather();
                Console.WriteLine($"Start of day {day}");
                weather.forecast();
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
                store.SalesPrompt(player);
                player.recipe.ChangeRecipePrompt();
                player.recipe.SetPrice();
                GenerateCustomers(weather.daysWeather.weatherSpawnModifier, weather.daysTemp);
                double todaysProfits = WillCustomersPurchace(weather.daysTemp, weather.daysWeather.weatherLemonadeDesireModifier, player.recipe.numberOfIceCubes, player.recipe.price);
                Console.WriteLine($"\nThe high for today was {weather.daysTemp}°");
                Console.WriteLine($"Today you made ${todaysProfits}");
                Console.ReadKey();
                day++;


            }
        }
    }
}
