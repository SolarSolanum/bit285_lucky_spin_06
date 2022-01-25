using System;

namespace LuckySpin.Models
{
    public class Spin
    {
        public int spinId;
        public bool isWinning;
        public Player player;
        private Random rnd;
        public Spin()
        {
            this.rnd = new Random();
            this.spinId = rnd.Next(100);
        }
    }

}
