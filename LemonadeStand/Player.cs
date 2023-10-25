using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Player
    {
        // member variables (HAS A)
        public string name;
        public Inventory inventory;
        public Wallet wallet;
        public Recipe recipe;

        // constructor (SPAWNER)
        public Player()
        {
            name = null;
            inventory = new Inventory();
            wallet = new Wallet(20);
            recipe = new Recipe();
        }

        // member methods (CAN DO)

    }
}
