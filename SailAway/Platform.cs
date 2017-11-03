using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailAway
{
    class Platform : Sprite
    {
        public Platform(Texture2D texture, float xPos, float yPos,int platformLength) : base(texture, xPos, yPos)
        {
            
        }

        public bool CheckPlayerCollision(Player player)
        {
            if (Rectangle.Intersects(player.GetRectangle()))
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
