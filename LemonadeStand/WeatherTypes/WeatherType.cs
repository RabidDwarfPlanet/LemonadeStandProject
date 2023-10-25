using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.WeatherTypes
{
    abstract class WeatherType
    {
        public double weatherSpawnModifier;
        public double weatherLemonadeDesireModifier;
        public string typeOfWeather;
        public double tempModifier;

        static WeatherType() 
        { 
        }
    }
}
