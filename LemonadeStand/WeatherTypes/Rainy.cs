using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.WeatherTypes
{
    internal class Rainy : WeatherType
    {
        public Rainy()
        {
            this.typeOfWeather = "rainy";
            this.weatherLemonadeDesireModifier = 0.8;
            this.weatherSpawnModifier = 0.75;
            this.tempModifier = 0.75;
        }

        public override void WeatherEffect()
        {
            Console.WriteLine("Why did it have to rain today, it doesnt seem like many people are out");
        }

    }
}
