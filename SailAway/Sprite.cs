﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SailAway
{
    class Sprite
    {
        protected Texture2D Texture;
        protected Rectangle Rectangle;

        protected float XPos;
        protected float YPos;

        public Sprite(Texture2D texture, float xPos, float yPos)
        {
            Texture = texture;
            XPos = xPos;
            YPos = yPos;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle.X = (int)Math.Round(XPos);
            Rectangle.Y = (int)Math.Round(YPos);
            spriteBatch.Draw(Texture, Rectangle,Color.White);
        }
    }
}
