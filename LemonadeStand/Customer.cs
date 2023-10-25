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
        public double sweetTooth;

        public Customer()
        {
            sweetTooth = rand.Next(10);

        }

        public void setDesire()
        {

        }


    }
}
