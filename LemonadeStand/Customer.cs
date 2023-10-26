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
        public double weatherModifier;
        public double tempModifier;
        public double iceModifier;
        public bool tooMuchIce;
        public bool tooLittleIce;

        public Customer()
        {
            desire = rand.Next(1, 11);
            wallet = new Wallet(rand.Next(7));
        }

        public void reasonForNotPurchasing()
        {
            if(this.weatherModifier <= this.tempModifier && this.weatherModifier <= iceModifier) { Console.WriteLine("The weather today is just not good for lemonade"); }
            else if(this.tempModifier <= this.weatherModifier && this.tempModifier <= this.iceModifier) { Console.WriteLine("Its not hot enough today for a cup of lemonade"); }
            else
            {
                if(tooMuchIce == true) { Console.WriteLine("All the ice is going to water down the lemonade"); }
                else if(tooLittleIce == true) { Console.WriteLine("This lemonade would be great if there were more ice"); }
            }
        }

        public void setDesire(double tempurature, double weatherModifier, int ice)
        {
            this.weatherModifier = weatherModifier;
            this.desire *= weatherModifier;
            this.tempModifier = tempurature / 75;
            this.desire *= this.tempModifier;
            this.iceModifier = 1;
            if(ice >= 8) {iceModifier -= 0.3; tooMuchIce = true; }
            if(tempurature > 50 && tempurature <= 60)
            {
                if(ice == 0 || ice == 1) {iceModifier += 0.2;}
                else{iceModifier -= 0.2; tooMuchIce = true; }
            }
            else if(tempurature > 60 && tempurature <= 70)
            {
                if (ice == 1 || ice == 2 || ice == 3) {iceModifier += 0.2;}
                else if (ice > 1) {iceModifier -= 0.2; tooLittleIce = true; }
                else {iceModifier -= 0.2; tooMuchIce = true; }
            }
            else if (tempurature > 70 && tempurature <= 80)
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
            this.desire *= iceModifier;
        }
    }
}
