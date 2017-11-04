using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailAway
{
    class Level
    {
        private static List<Platform> Platforms;
        public static Level CurrentLevel { get; private set; }
        public Level(List<Platform> platformsList)
        {
            Platforms = platformsList;
            Level.CurrentLevel = this;
        }

        public bool CheckPlatformCollisions(Player player)
        {
            bool playerCollided = false;
            Platform collidingPlatform;
            foreach (Platform p in Platforms)
            {
                if (p.CheckPlayerCollision(player))
                {
                    //Console.WriteLine("We have collided with platform:" + p);
                    playerCollided = true;
                    collidingPlatform = p;
                    //player.SetPlayerCollidedWithFloor(true);
                }

            }
            
            return playerCollided;
        }
    }
}
