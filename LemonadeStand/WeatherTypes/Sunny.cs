using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.WeatherTypes
{
    internal class Sunny : WeatherType
    {
        public Sunny()
        {
            this.typeOfWeather = "sunny";
            this.weatherLemonadeDesireModifier = 1.3;
            this.weatherSpawnModifier = 1.5;
            this.tempModifier = 1.20;
        }
    }
}
