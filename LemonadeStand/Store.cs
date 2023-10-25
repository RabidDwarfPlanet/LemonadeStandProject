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
            pricePerLemon = .5;
            pricePerSugarCube = .1;
            pricePerIceCube = .01;
            pricePerCup = .25;
        }

        // member methods (CAN DO)
        public void SellLemons(Player player)
        {
            int lemonsToPurchase = UserInterface.GetNumberOfItems("lemons");
            double transactionAmount = CalculateTransactionAmount(lemonsToPurchase, pricePerLemon);
            if(player.wallet.Money >= transactionAmount)
            {
                Console.WriteLine($"You have bought ${transactionAmount} worth of lemons");
                player.wallet.PayMoneyForItems(transactionAmount);
                player.inventory.AddLemonsToInventory(lemonsToPurchase);
            }
        }

        public void DisplayStorePrices(Player player)
        {
            Console.WriteLine($"\nWelcome in {player.name}");
            Console.WriteLine($"You have ${player.wallet.Money}\n");
            Console.WriteLine($"Lemons: ${pricePerLemon}");
            Console.WriteLine($"Sugar Cubes: ${pricePerSugarCube}");
            Console.WriteLine($"Cups: ${pricePerCup}");
            Console.WriteLine($"Ice Cubes: ${pricePerIceCube}");
        }

        public void SalesPrompt(Player player)
        {
            bool madeSale = false;
            Console.WriteLine("\nWhat would you like to purchase (Type LEAVE to leave the store)");
            moreSales:
            if(madeSale == true)
            {
                Console.WriteLine("\nWould you like to purchase anything else");
            }
            string itemToBuy = Console.ReadLine();
            switch (itemToBuy.ToLower())
            {
                case "ice":
                case "ice cubes":
                    SellIceCubes(player);
                    madeSale = true;
                    goto moreSales;
                case "cups":
                    SellCups(player);
                    madeSale = true;
                    goto moreSales;
                case "sugar":
                case "sugar cubes":
                    SellSugarCubes(player);
                    madeSale = true;
                    goto moreSales;
                case "lemons":
                case "lemon":
                    SellLemons(player);
                    madeSale = true;
                    goto moreSales;
                case "leave":
                    break;
            }
        }

        public void SellSugarCubes(Player player)
        {
            int sugarToPurchase = UserInterface.GetNumberOfItems("sugar");
            double transactionAmount = CalculateTransactionAmount(sugarToPurchase, pricePerSugarCube);
            if(player.wallet.Money >= transactionAmount)
            {
                Console.WriteLine($"You have bought ${transactionAmount} worth of sugar and now have ${player.wallet.Money} left");
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddSugarCubesToInventory(sugarToPurchase);
            }
        }

        public void SellIceCubes(Player player)
        {
            int iceCubesToPurchase = UserInterface.GetNumberOfItems("ice cubes");
            double transactionAmount = CalculateTransactionAmount(iceCubesToPurchase, pricePerIceCube);
            if(player.wallet.Money >= transactionAmount)
            {
                Console.WriteLine($"You have bought ${transactionAmount} worth of ice cubes");
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddIceCubesToInventory(iceCubesToPurchase);
            }
        }

        public void SellCups(Player player)
        {
            int cupsToPurchase = UserInterface.GetNumberOfItems("cups");
            double transactionAmount = CalculateTransactionAmount(cupsToPurchase, pricePerCup);
            if(player.wallet.Money >= transactionAmount)
            {
                Console.WriteLine($"You have bought ${transactionAmount} worth of sups");
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddCupsToInventory(cupsToPurchase);
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
