using System;
using System.Collections.Generic;
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

        public void WillCustomersPurchace(double tempurature, double weatherModifier, int ice, double price)
        {
            for (int i = 0; i < daysCustomers.Count; i++)
            {
                Console.WriteLine("\nA customer approaches your stand");
                daysCustomers[i].setDesire(tempurature, weatherModifier, ice);
                if (daysCustomers[i].wallet.Money >= price)
                {
                    if (daysCustomers[i].desire < 2)
                    {
                        if(price <= 0.5) {daysCustomers[i].wallet.PayMoneyForItems(price); player.wallet.AcceptMoney(price); Console.WriteLine("I'd love a cup of lemonade!"); }
                        else { daysCustomers[i].reasonForNotPurchasing(); }
                    }
                    else if(daysCustomers[i].desire < 4 && daysCustomers[i].desire >= 2) 
                    {
                        if (price <= 1.0) { daysCustomers[i].wallet.PayMoneyForItems(price); player.wallet.AcceptMoney(price); Console.WriteLine("I'd love a cup of lemonade!"); }
                        else { daysCustomers[i].reasonForNotPurchasing(); }
                    }
                    else if (daysCustomers[i].desire < 6 && daysCustomers[i].desire >= 4) 
                    {
                        if (price <= 2.0) { daysCustomers[i].wallet.PayMoneyForItems(price); player.wallet.AcceptMoney(price); Console.WriteLine("I'd love a cup of lemonade!"); }
                        else { daysCustomers[i].reasonForNotPurchasing(); }
                    }
                    else if (daysCustomers[i].desire < 8 && daysCustomers[i].desire >= 6) 
                    {
                        if (price <= 2.5) { daysCustomers[i].wallet.PayMoneyForItems(price); player.wallet.AcceptMoney(price); Console.WriteLine("I'd love a cup of lemonade!"); }
                        else { daysCustomers[i].reasonForNotPurchasing(); }
                    }
                    else 
                    {
                        if (price <= 4.0) { daysCustomers[i].wallet.PayMoneyForItems(price); player.wallet.AcceptMoney(price); Console.WriteLine("I'd love a cup of lemonade!"); }
                        else { daysCustomers[i].reasonForNotPurchasing(); }
                    }
                }
                else { Console.WriteLine("Sorry, but your lemonade is just too expensive"); }
                Thread.Sleep(milliseconds);
            }
        }



        public void DayCycle()
        {
            while (day <= 7)
            {
                //Console.Clear();
                weather.setWeather();
                //Console.WriteLine($"Start of day {day}");
                weather.forecast();
                //Console.WriteLine("\nPress any key to continue");
                //Console.ReadKey();
                //store.SalesPrompt(player);
                //player.recipe.ChangeRecipePrompt();
                //player.recipe.SetPrice();
                GenerateCustomers(weather.daysWeather.weatherSpawnModifier, weather.daysTemp);
                WillCustomersPurchace(weather.daysTemp, weather.daysWeather.weatherLemonadeDesireModifier, player.recipe.numberOfIceCubes, player.recipe.price);
                Console.WriteLine($"The high for today was {weather.daysTemp}°");
                Console.ReadKey();


            }
        }
    }
}
