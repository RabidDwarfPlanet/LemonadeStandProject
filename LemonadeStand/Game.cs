using LemonadeStand.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    internal class Game
    {
        public int day;
        public Store store = new Store();
        WeatherSystem weather = new WeatherSystem();
        public int playerCount;
        public Player playerOne;
        public Player playerTwo;
        List<Customer> daysCustomers = new List<Customer>();
        Random r = new Random();
        int milliseconds = 1000;
        

        public Game()
        {
            day = 7;
        }

        
        public void WelcomeMessage()
        {
            Console.WriteLine("Welcome to this Lemonade Stand Game!\nIn this game you are going to have 7 days to try to earn as much money from your stand as you can");
            Console.WriteLine("Each day you will be given a forecast that will have an impact on your ability to make sales");
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        public void PlayerCount()
        {
            bool validNumber = false;
            Console.Clear();
            Console.WriteLine("Are you going to be playing with 1 or 2 players?");
            while (validNumber == false)
            {
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                    case "one":
                        playerCount = 1;
                        return;
                    case "two":
                    case "2":
                        playerCount = 2;
                        return;
                    default:
                        Console.WriteLine("That is not a valid number");
                        break;

                }
            }
        }
        public void NamePlayers()
        {
            if (playerCount == 1)
            {
                Console.Clear();
                Console.WriteLine("What would you like to name your character");
                playerOne = new Player();
                playerOne.name = Console.ReadLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Player 1 what would you like to name your character");
                playerOne = new Player();
                playerOne.name = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Player 2 what would you like to name your character");
                playerTwo = new Player();
                playerTwo.name = Console.ReadLine();
            }
        }

        public void GenerateCustomers(WeatherSystem weather)
        {
            double customers = r.Next(10, 20);
            if (playerCount == 2)
            {
                customers *= 1.6;
            }
            customers *= weather.daysWeather.weatherSpawnModifier;
            customers *= weather.daysTemp / 75;
            int finalCustomers = (int)customers;
            daysCustomers.Clear();

            for (int i = 0; i < finalCustomers; i++)
            {
                Customer customer = new Customer();
                daysCustomers.Add(customer);
            }
        }

        public void WillCustomerPurchace(Customer customer, WeatherSystem weather, Player player)
        {
            double price = player.recipe.price;
                if (playerCount == 1) { Console.WriteLine("\nA customer approaches your stand"); }
                else { Console.WriteLine($"\nA customer approaches {player.name}'s stand"); }
                customer.setDesire(weather, player.recipe.numberOfIceCubes);
                if (customer.wallet.Money >= price)
                {
                    if (customer.desire < 2)
                    {
                        if (price <= 0.5)
                        {
                            player.todaysProfits += MakeASale(player, customer);
                            Console.WriteLine("I guess I'll get a cup");
                        }
                        else { customer.reasonForNotPurchasing(weather); }
                    }
                    else if (customer.desire < 4 && customer.desire >= 2)
                    {
                        if (price <= 1.0)
                        {
                            player.todaysProfits += MakeASale(player, customer);
                            Console.WriteLine("Alright, I'll take a cup of lemonade");
                        }
                        else { customer.reasonForNotPurchasing(weather); }
                    }
                    else if (customer.desire < 6 && customer.desire >= 4)
                    {
                        if (price <= 2.0)
                        {
                            player.todaysProfits += MakeASale(player, customer);
                            Console.WriteLine("I'd like a cup of lemondade please");
                        }
                        else { customer.reasonForNotPurchasing(weather); }
                    }
                    else if (customer.desire < 8 && customer.desire >= 6)
                    {
                        if (price <= 2.5)
                        {
                            player.todaysProfits += MakeASale(player, customer);
                            Console.WriteLine("A cup of lemonade sounds pretty good!");
                        }
                        else { customer.reasonForNotPurchasing(weather); }
                    }
                    else
                    {
                        if (price <= 4.0)
                        {
                            player.todaysProfits += MakeASale(player, customer);
                            Console.WriteLine("I'd love a cup of lemonade!");
                        }
                        else { customer.reasonForNotPurchasing(weather); }
                    }
                }
                else { Console.WriteLine("Sorry, but your lemonade is just too expensive"); }
                Thread.Sleep(milliseconds);
            
        }

        public void MultiOpening()
        {
            bool playerOneOpen = true;
            bool playerTwoOpen = true;
            Console.Clear();
            playerOne.pitcher.Clear();
            playerTwo.pitcher.Clear();
            Player currentPlayer;
            if (CanMakeNewPitcher(playerOne) && playerOne.inventory.cups.Count != 0 && playerOne.inventory.iceCubes.Count >= playerOne.recipe.numberOfIceCubes)
            {
                Console.WriteLine($"{playerOne.name} opens their lemonade stand");
                MakeNewPitcher(playerOne);
            }
            else
            {
                Console.WriteLine($"{playerOne.name} cannot open today");
                playerOneOpen = false;
            }
            if (CanMakeNewPitcher(playerTwo) && playerTwo.inventory.cups.Count != 0 && playerTwo.inventory.iceCubes.Count >= playerTwo.recipe.numberOfIceCubes)
            {
                Console.WriteLine($"{playerTwo.name} opens their lemonade stand");
                MakeNewPitcher(playerTwo);
            }
            else
            {
                Console.WriteLine($"{playerTwo.name} cannot open today");
                playerTwoOpen = false;
            }
            weather.daysWeather.WeatherEffect();
            foreach (Customer customer in daysCustomers)
            {
                if(playerOneOpen == false && playerTwoOpen == false) { return; }
                int player = r.Next(2);
                if(player == 1) 
                {
                    if (playerOneOpen) { currentPlayer = playerOne; }
                    else { currentPlayer = playerTwo; }
                }
                else 
                {

                    if(playerTwoOpen) { currentPlayer = playerTwo; }
                    else { currentPlayer = playerOne; }
                }
                if (currentPlayer.pitcher.Count == 0)
                {
                    if (CanMakeNewPitcher(currentPlayer)) { MakeNewPitcher(currentPlayer); }
                    else
                    {
                        Console.WriteLine($"{currentPlayer.name} has run out of supplies and must close for the day");

                        break;
                    }
                }
                if (currentPlayer.inventory.iceCubes.Count < currentPlayer.recipe.numberOfIceCubes)
                {
                    Console.WriteLine($"{currentPlayer.name} has run out of supplies and must close for the day");
                    if(currentPlayer == playerOne) { playerOneOpen = false; }
                    else { playerTwoOpen = false; }
                    break;
                }
                else if (currentPlayer.inventory.cups.Count == 0)
                {
                    Console.WriteLine($"{currentPlayer.name} has run out of supplies and must close for the day");
                    if (currentPlayer == playerOne) { playerOneOpen = false; }
                    else { playerTwoOpen = false; }
                    break;
                }
                WillCustomerPurchace(customer, weather, currentPlayer);
            }
            Console.WriteLine("\nThe sun is going down, so you both pack up your things");
            Console.ReadKey();
        }

        public void SoloOpening()
        {
            Console.Clear();
            playerOne.pitcher.Clear();
            bool stocked = CanMakeNewPitcher(playerOne);
            if(stocked == true && playerOne.inventory.cups.Count != 0 && playerOne.inventory.iceCubes.Count >= playerOne.recipe.numberOfIceCubes)
            {
                Console.WriteLine("Your lemonade stand is open for buisness!");
                weather.daysWeather.WeatherEffect();
                MakeNewPitcher(playerOne);
                foreach(Customer customer in daysCustomers)
                {
                    if (playerOne.pitcher.Count == 0)
                    {
                        stocked = CanMakeNewPitcher(playerOne);
                        if (stocked == true) { MakeNewPitcher(playerOne); }
                        else
                        {
                            Console.WriteLine("You have run out of supplies and must close for the day");
                            break;
                        }
                    }
                    if (playerOne.inventory.iceCubes.Count < playerOne.recipe.numberOfIceCubes)
                    {
                        Console.WriteLine("You have run out of supplies and must close for the day");
                        break;
                    }
                    else if (playerOne.inventory.cups.Count == 0)
                    {
                        Console.WriteLine("You have run out of supplies and must close for the day");
                        break;
                    }
                    WillCustomerPurchace(customer, weather, playerOne);
                }
                Console.WriteLine("\nThe sun is going down, so you pack up your things");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("You do not have enough supplies to open today");
                
            }
        }

        public double MakeASale(Player player, Customer customer)
        {
            customer.wallet.PayMoneyForItems(player.recipe.price);
            player.wallet.AcceptMoney(player.recipe.price);
            double profits = player.recipe.price;
            player.totalProfits += player.recipe.price;
            player.pitcher.RemoveAt(0); player.inventory.RemoveCupsFromInventory(1); player.inventory.RemoveIceCubesFromInventory(player.recipe.numberOfIceCubes);
            return profits;
        }

        public bool CanMakeNewPitcher(Player player)
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
        public void MakeNewPitcher(Player player)
        {
            player.inventory.RemoveLemonsFromInventory(player.recipe.numberOfLemons);
            player.inventory.RemoveSugarCubesFromInventory(player.recipe.numberOfSugarCubes);
            for (int i = 0; i < 8; i++)
            {
                Lemonade lemonade = new Lemonade();
                player.pitcher.Add(lemonade);
            }
        }

        public void MultiEndDay(Player player)
        {
            Console.Clear();
            Console.WriteLine($"Today {player.name} made ${player.todaysProfits - player.todaysCosts}");
            Console.WriteLine($"So far they have made ${player.totalProfits - player.totalCosts}");
            Console.ReadKey();
        }

        public void SoloEndDay(Player player)
        {
            Console.Clear();
            Console.WriteLine($"Today you made ${player.todaysProfits - player.todaysCosts}");
            Console.WriteLine($"So far you have made ${player.totalProfits - player.totalCosts}");
            Console.ReadKey();
        }


        public void EndGame()
        {
            if (playerCount == 1)
            {
                Console.Clear();
                Console.WriteLine("You have made it through 7 days!!");
                Console.WriteLine($"You ended with ${playerOne.wallet.Money} but in total you made ${playerOne.totalProfits - playerOne.totalCosts}");
                Console.WriteLine("Well done on your entrepreneurial endeavors!");
                Console.WriteLine("Press anykey to end the game");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You have made it through 7 days!!");
                Console.WriteLine($"{playerOne.name} ended with ${playerOne.wallet.Money} but in total made ${playerOne.totalProfits - playerOne.totalCosts}");
                Console.WriteLine($"{playerTwo.name} ended with ${playerTwo.wallet.Money} but in total made ${playerTwo.totalProfits - playerTwo.totalCosts}");
                if(playerOne.totalProfits - playerOne.totalCosts > playerTwo.totalProfits - playerTwo.totalCosts)
                {
                    Console.WriteLine($"{playerOne.name} made the most so they have won the game!");
                }
                else
                {
                    Console.WriteLine($"{playerTwo.name} made the most so they have won the game!");
                }
            }
            
        }

        public void MultiDayCycle()
        {
            while (day <= 7)
            {
                Console.Clear();
                weather.setWeather();
                Console.WriteLine($"Start of day {day}");
                weather.forecast();
                store.SalesPrompt(playerOne);
                store.SalesPrompt(playerTwo);
                playerOne.recipe.ChangeRecipePrompt(playerOne);
                playerOne.recipe.SetPrice();
                playerTwo.recipe.ChangeRecipePrompt(playerTwo);
                playerTwo.recipe.SetPrice();
                GenerateCustomers(weather);
                MultiOpening();
                Console.WriteLine($"\nThe high for today was {weather.daysTemp}°");
                MultiEndDay(playerOne);
                MultiEndDay(playerTwo);
                day++;
            }
        }

        public void SoloDayCycle()
        {
            while (day <= 7)
            {
                Console.Clear();
                weather.setWeather();
                Console.WriteLine($"Start of day {day}");
                weather.forecast();
                store.SalesPrompt(playerOne);
                playerOne.recipe.ChangeRecipePrompt(playerOne);
                playerOne.recipe.SetPrice();
                GenerateCustomers(weather);
                SoloOpening();
                Console.WriteLine($"\nThe high for today was {weather.daysTemp}°");
                SoloEndDay(playerOne);
                day++;
            }
        }
    }
}
