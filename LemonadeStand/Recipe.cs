using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    internal class Recipe
    {
        // member variables (HAS A)
        public int numberOfLemons;
        public int numberOfSugarCubes;
        public int numberOfIceCubes;
        public double price;


        // constructor (SPAWNER)
        public Recipe()
        {
            numberOfLemons = 2;
            numberOfSugarCubes = 4;
            numberOfIceCubes = 4;
            price = 1;
        }

        //Member Methods (CAN DO)
        public void DisplayRecipe(Player player)
        {
            Console.WriteLine($"{player.name} your recipe currently consists of:\n{numberOfLemons} lemons per pitcher\n{numberOfSugarCubes} sugar cubes per pitcher\n{numberOfIceCubes} ice cubes per pitcher");
        }

        public void SetPrice()
        {
            double value = 0;
            bool isNum = false;
            Console.WriteLine("How much would you like to sell each cup of lemonade for?");
            while (isNum == false)
            {
                Console.WriteLine("Please enter a number");
                isNum = double.TryParse(Console.ReadLine(), out value);
            }
            this.price = value;
        }

        public void ChangeRecipePrompt(Player player)
        {
            bool firstChange = true;
            bool makingChanges = true;
            while (makingChanges)
            {
                Console.Clear();
                DisplayRecipe(player);
                if (firstChange == true) {Console.WriteLine("Would you like to change this recipe? (Y/N)");}
                else {Console.WriteLine("Would you like to change anything else (Y/N)");}
                while (makingChanges)
                {
                    string change = Console.ReadLine();
                    switch (change.ToLower())
                    {
                        case "yes":
                        case "y":
                            Console.WriteLine("What part of the recipe would you like to change");
                            ChangeRecipe(Console.ReadLine());
                            firstChange = false;
                            break;
                        case "no":
                        case "n":
                            return;
                        default:
                            Console.WriteLine("That was not a valid option, please try again");
                            break;
                    }
                }
            }
        }

        public void ChangeRecipe(string item)
        {
            bool isNum = false;
            int value = 0;
            switch (item.ToLower())
            {
                case "lemons":
                case "lemon":
                    Console.WriteLine("How many lemons would you like to use per pitcher?");
                    while (isNum == false)
                    {
                        Console.WriteLine("Please enter a number");
                        isNum = int.TryParse(Console.ReadLine(), out value);
                    }
                    this.numberOfLemons = value;
                    break;
                case "ice":
                case "ice cubes":
                    Console.WriteLine("How many ice cubes would you like to put in each cup");
                    while (isNum == false)
                    {
                        Console.WriteLine("Please enter a number");
                        isNum = int.TryParse(Console.ReadLine(), out value);
                    }
                    this.numberOfIceCubes = value;
                    break;
                case "sugar":
                case "sugar cubes":
                    Console.WriteLine("How many sugar cubes would you like to use per pitcher");
                    while (isNum == false)
                    {
                        Console.WriteLine("Please enter a number");
                        isNum = int.TryParse(Console.ReadLine(), out value);
                    }
                    this.numberOfSugarCubes = value;
                    break; 
                default:
                    break;
            }
        }
    }
}
