using LemonadeStand.WeatherTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    internal class WeatherSystem
    {
        public double daysTemp;
        public double tempModifier;
        Random rand = new Random();
        public WeatherType daysWeather;
        Sunny sunny = new Sunny();
        Rainy rainy = new Rainy();
        Cloudy cloudy = new Cloudy();
        PartlyCloudy partlyCloudy = new PartlyCloudy();

        public WeatherSystem()
        {
            
        }

        public void setWeather()
        {
            int odds = rand.Next(100);
            if(odds <= 10)
            {
                daysWeather = rainy;
            }
            else if(odds > 10 && odds <= 25)
            {
                daysWeather = cloudy;
            }
            else if(odds > 25 && odds <= 60)
            {
                daysWeather = partlyCloudy;
            }
            else
            {
                daysWeather = sunny;
            }

            daysTemp = Math.Round(rand.Next(70, 91) * daysWeather.tempModifier);
            tempModifier = daysTemp / 75;
        }

        public void forecast()
        {
            Console.WriteLine($"It looks like today is going to be {daysWeather.typeOfWeather}");
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
    }
}
