using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Store
    {
        // member variables (HAS A)
        private double pricePerLemon;
        private double pricePerSugarCube;
        private double pricePerIceCube;
        private double pricePerCup;


        // constructor (SPAWNER)
        public Store()
        {
            pricePerLemon = .9;
            pricePerSugarCube = .03;
            pricePerIceCube = .01;
            pricePerCup = .15;
        }

        // member methods (CAN DO)
        

        public void DisplayStorePrices(Player player)
        {
            Console.WriteLine($"Welcome in {player.name}");
            Console.WriteLine($"You have ${player.wallet.Money}\n");
            Console.WriteLine($"Lemons: ${pricePerLemon}           | You have {player.inventory.lemons.Count} lemons");
            Console.WriteLine($"Sugar Cubes: ${pricePerSugarCube}      | You have {player.inventory.sugarCubes.Count} sugar cubes");
            Console.WriteLine($"Cups: ${pricePerCup}            | You have {player.inventory.cups.Count} cups");
            Console.WriteLine($"Ice Cubes: ${pricePerIceCube}       | You have {player.inventory.iceCubes.Count} ice cubes");
        }

        public void SalesPrompt(Player player)
        {
            bool madeSale = false;
            bool bankrupt = false;
            while (bankrupt == false)
            {
                if(player.wallet.Money == 0 && player.inventory.sugarCubes.Count == 0 || player.wallet.Money == 0 && player.inventory.cups.Count == 0 || player.wallet.Money == 0 && player.inventory.iceCubes.Count == 0 || player.wallet.Money == 0 && player.inventory.lemons.Count == 0)
                {
                    bankrupt = true;
                    break;
                }
                Console.Clear();
                DisplayStorePrices(player);
                if (madeSale == false)
                {
                    Console.WriteLine("\nWhat would you like to purchase (Type LEAVE to leave the store)");
                }
                else
                {
                    Console.WriteLine("\nWould you like to purchase anything else (Type LEAVE to leave the store)");
                }
                madeSale = false;
                while(madeSale == false)
                {
                    string itemToBuy = Console.ReadLine();
                    switch (itemToBuy.ToLower())
                    {
                        case "ice":
                        case "ice cubes":
                            SellIceCubes(player);
                            madeSale = true;
                            break;
                        case "cups":
                            SellCups(player);
                            madeSale = true;
                            break;
                        case "sugar":
                        case "sugar cubes":
                            SellSugarCubes(player);
                            madeSale = true;
                            break;
                        case "lemons":
                        case "lemon":
                            SellLemons(player);
                            madeSale = true;
                            break;
                        case "leave":
                            player.totalCosts += player.todaysCosts;
                            return;
                        default:
                            Console.WriteLine("We dont carry that here, please select something that we have in stock");
                            break;
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("You have run out of money and no longer have enough supplies to keep going so you must shut down");
            Console.WriteLine("Game Over");
            Environment.Exit(0);

        }

        public void SellLemons(Player player)
        {
            int lemonsToPurchase = UserInterface.GetNumberOfItems("lemons");
            double transactionAmount = CalculateTransactionAmount(lemonsToPurchase, pricePerLemon);
            if (player.wallet.Money >= transactionAmount)
            {

                player.wallet.PayMoneyForItems(transactionAmount);
                player.inventory.AddLemonsToInventory(lemonsToPurchase);
                player.todaysCosts += transactionAmount;
                Console.WriteLine($"You have bought ${transactionAmount} worth of lemons");
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }

        public void SellSugarCubes(Player player)
        {
            int sugarToPurchase = UserInterface.GetNumberOfItems("sugar");
            double transactionAmount = CalculateTransactionAmount(sugarToPurchase, pricePerSugarCube);
            if(player.wallet.Money >= transactionAmount)
            {
                
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddSugarCubesToInventory(sugarToPurchase);
                player.todaysCosts += transactionAmount;
                Console.WriteLine($"You have bought ${transactionAmount} worth of sugar");
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }

        public void SellIceCubes(Player player)
        {
            int iceCubesToPurchase = UserInterface.GetNumberOfItems("ice cubes");
            double transactionAmount = CalculateTransactionAmount(iceCubesToPurchase, pricePerIceCube);
            if(player.wallet.Money >= transactionAmount)
            {
                
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddIceCubesToInventory(iceCubesToPurchase);
                player.todaysCosts += transactionAmount;
                Console.WriteLine($"You have bought ${transactionAmount} worth of ice cubes");
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }

        public void SellCups(Player player)
        {
            int cupsToPurchase = UserInterface.GetNumberOfItems("cups");
            double transactionAmount = CalculateTransactionAmount(cupsToPurchase, pricePerCup);
            if(player.wallet.Money >= transactionAmount)
            {
                
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddCupsToInventory(cupsToPurchase);
                player.todaysCosts += transactionAmount;
                Console.WriteLine($"You have bought ${transactionAmount} worth of cups");
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }

        private double CalculateTransactionAmount(int itemCount, double itemPricePerUnit)
        {
            double transactionAmount = itemCount * itemPricePerUnit;
            return transactionAmount;
        }

        private void PerformTransaction(Wallet wallet, double transactionAmount)
        {
            wallet.PayMoneyForItems(transactionAmount);
        }
    }
}
