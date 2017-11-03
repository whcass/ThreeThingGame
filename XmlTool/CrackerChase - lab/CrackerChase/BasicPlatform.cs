using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CrackerChase
{
    class BasicPlatform : Sprite
    {
        protected Color mColor;

        protected string mName;

        public BasicPlatform(int pScreenWidth, int pScreenHeight, Texture2D pTexture, int pDrawWidth, int pDrawHeight, float pResetX, float pResetY, Color pColor, string pName) : base(pScreenWidth,pScreenHeight,pTexture,pDrawWidth,pResetX,pResetY)
        {
            mName = pName;
            //mRectangle = new Rectangle(0,0,pWidth,pHeight);
            mColor = pColor;
            GarbageThatRuinsMyPlatformGenerationTool(pTexture, pDrawWidth, pDrawHeight);
            Reset();
        }
        public override void GarbageThatRuinsMyPlatformGenerationTool(Texture2D pTexture, int pDrawWidth, int pDrawHeight)
        {
            rectangle = new Rectangle(0, 0, pDrawWidth, pDrawHeight);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            rectangle.X = (int)Math.Round(xPosition);
            rectangle.Y = (int)Math.Round(yPosition);
            spriteBatch.Draw(texture, rectangle, mColor);
        }
    }
}
