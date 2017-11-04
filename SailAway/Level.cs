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
        private static Sprite EndFlag;
        public static Level CurrentLevel { get; private set; }
        public Level(List<Platform> platformsList,Sprite endFlag)
        {
            Platforms = platformsList;
            Level.CurrentLevel = this;
            EndFlag = endFlag;
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

        public bool CheckEndFlag(Player player)
        {
            //Console.WriteLine("Player" + player.GetRectangle().X + "," + player.GetRectangle().Y);
            //Console.WriteLine("End Flag"+EndFlag.GetRectangle().X + "," + EndFlag.GetRectangle().Y);
            if (EndFlag.GetRectangle().Intersects(player.GetRectangle()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
