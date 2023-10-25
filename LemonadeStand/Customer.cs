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

        public Customer()
        {
            desire = rand.Next(1, 11);
            wallet = new Wallet(rand.Next(7));
        }

        public void setDesire(double tempurature, double weatherModifier, int ice)
        {
            this.desire *= weatherModifier;
            this.desire *= tempurature / 75;
            double iceModifier = 1;
            if(ice <= 8) {iceModifier -= 0.3;}
            if(tempurature > 50 && tempurature <= 60)
            {
                if(ice == 0 && ice == 1) {iceModifier += 0.2;}
                else{iceModifier -= 0.2;}
            }
            else if(tempurature > 60 && tempurature <= 70)
            {
                if (ice == 2 && ice == 3) {iceModifier += 0.2;}
                else{iceModifier -= 0.2;}
            }
            else if (tempurature > 70 && tempurature <= 80)
            {
                if (ice == 2 && ice == 3 || ice == 4) {iceModifier += 0.2;}
                else{iceModifier -= 0.2;}
            }
            else
            {
                if (ice == 3 && ice == 4 || ice == 5) {iceModifier += 0.2;}
                else {iceModifier -= 0.2;}
            }
            this.desire *= iceModifier;
        }
    }
}
