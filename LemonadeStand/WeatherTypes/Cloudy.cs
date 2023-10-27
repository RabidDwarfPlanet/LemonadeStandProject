using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.WeatherTypes
{
    internal class Cloudy : WeatherType
    {
        public Cloudy()
        {
            this.typeOfWeather = "cloudy";
            this.weatherLemonadeDesireModifier = 1;
            this.weatherSpawnModifier = 0.8;
            this.tempModifier = 0.9;
        }

        public override void WeatherEffect()
        {
            Console.WriteLine("Its rather overcast today, hopefully it doesn't cool down too much");
        }
    }
    
    
}
