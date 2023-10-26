using System;
using System.Collections.Generic;
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
        public void DisplayRecipe()
        {
            Console.WriteLine($"Your recipe currently consists of:\n{numberOfLemons} lemons per pitcher\n{numberOfSugarCubes} sugar cubes per pitcher\n{numberOfIceCubes} ice cubes per pitcher");
        }

        public void SetPrice()
        {
            bool isNum = false;
            double value;
            Console.WriteLine("How much would you like to sell each cup of lemonade for?");
            retryPrice:
            isNum = double.TryParse(Console.ReadLine(), out value);
            if (isNum == true)
            {
                this.price = value;
            }
            else
            {
                Console.WriteLine("Please enter a number");
                goto retryPrice;
            }
        }

        public void ChangeRecipePrompt()
        {
            bool firstChange = true;
            restart:
            Console.Clear();
            DisplayRecipe();
            if(firstChange == true)
            {
                Console.WriteLine("Would you like to change this recipe? (Y/N)");
            }
            else
            {
                Console.WriteLine("Would you like to change anything else (Y/N)");
            }
            retry:
            string change = Console.ReadLine();
            switch (change.ToLower())
            {
                case "yes":
                case "y":
                    Console.WriteLine("What part of the recipe would you like to change");
                    ChangeRecipe(Console.ReadLine());
                    firstChange = false;
                    goto restart;
                case "no":
                case "n":
                    break;
                default:
                    Console.WriteLine("That was not a valid option, please try again");
                    goto retry;
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
                    retryLem:
                    isNum = int.TryParse(Console.ReadLine(), out value);
                    if (isNum == true)
                    {
                        this.numberOfLemons = value;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number");
                        goto retryLem;
                    }
                    break;
                case "ice":
                case "ice cubes":
                    Console.WriteLine("How many ice cubes would you like to put in each cup");
                retryIce:
                    isNum = int.TryParse(Console.ReadLine(), out value);
                    if (isNum == true)
                    {
                        this.numberOfIceCubes = value;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number");
                        goto retryIce;
                    }
                    break;
                case "sugar":
                case "sugar cubes":
                    Console.WriteLine("How many sugar cubes would you like to use per pitcher");
                    retrySug:
                    isNum = int.TryParse(Console.ReadLine(), out value);
                    if(isNum == true)
                    {
                        this.numberOfSugarCubes = value;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number");
                        goto retrySug;
                    }
                    break;
                default:
                    break;
            }
        }


    }
}
