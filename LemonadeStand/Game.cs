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
        Recipe recipe = new Recipe();

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

        public void DayCycle()
        {
            while (day <= 7)
            {
                //Console.Clear();
                //weather.setWeather();
                //Console.WriteLine($"Start of day {day}");
                //weather.forecast();
                //Console.WriteLine("\nPress any key to continue");
                //Console.ReadKey();
                //store.SalesPrompt(player);
                recipe.ChangeRecipePrompt();

            }
        }
    }
}
