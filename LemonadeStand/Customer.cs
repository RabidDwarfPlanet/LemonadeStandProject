using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    internal class Customer
    {
        public string name;
        public Wallet wallet;
        Random rand = new Random();
        public double desire;
        public double playerModifier;
        bool tooMuchIce;
        bool tooLittleIce;

        public Customer()
        {
            desire = rand.Next(1, 11);
            wallet = new Wallet(rand.Next(7));
        }

        public void reasonForNotPurchasing(WeatherSystem weather)
        {
            if(weather.daysWeather.weatherLemonadeDesireModifier <= weather.tempModifier && weather.daysWeather.weatherLemonadeDesireModifier <= this.playerModifier) { Console.WriteLine("The weather today is just not good for lemonade"); }
            else if(weather.tempModifier <= weather.daysWeather.weatherLemonadeDesireModifier && weather.tempModifier <= this.playerModifier) { Console.WriteLine("Its not hot enough today for a cup of lemonade"); }
            else
            {
                if(tooMuchIce == true) { Console.WriteLine("All the ice is going to water down the lemonade"); }
                else if(tooLittleIce == true) { Console.WriteLine("This lemonade would be great if there were more ice"); }
            }
        }

        public void setDesire(WeatherSystem weather, int ice)
        {
            this.desire *= weather.daysWeather.weatherLemonadeDesireModifier;
            this.desire *= weather.tempModifier;
            double iceModifier = 1;
            if(ice >= 8) {iceModifier -= 0.3; tooMuchIce = true; }
            if(weather.daysTemp > 50 && weather.daysTemp <= 60)
            {
                if(ice == 0 || ice == 1) {iceModifier += 0.2;}
                else{iceModifier -= 0.2; tooMuchIce = true; }
            }
            else if(weather.daysTemp > 60 && weather.daysTemp <= 70)
            {
                if (ice == 1 || ice == 2 || ice == 3) {iceModifier += 0.2;}
                else if (ice > 1) {iceModifier -= 0.2; tooLittleIce = true; }
                else {iceModifier -= 0.2; tooMuchIce = true; }
            }
            else if (weather.daysTemp > 70 && weather.daysTemp <= 80)
            {
                if (ice == 2 || ice == 3 || ice == 4) {iceModifier += 0.2;}
                else if(ice > 2) {iceModifier -= 0.2; tooLittleIce = true; }
                else{iceModifier -= 0.2; tooMuchIce = true; }
            }
            else
            {
                if (ice == 3 || ice == 4 || ice == 5) {iceModifier += 0.2;}
                else if (ice > 3) {iceModifier -= 0.2; tooLittleIce = true; }
                else {iceModifier -= 0.2; tooMuchIce = true; }
            }
            this.playerModifier = iceModifier;
            this.desire *= iceModifier;
        }
    }
}
