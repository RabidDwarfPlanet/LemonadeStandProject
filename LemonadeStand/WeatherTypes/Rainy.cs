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
            this.weatherSpawnModifier = 0.5;
            this.tempModifier = 0.75;
        }


    }
}
