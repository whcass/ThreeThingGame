using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackerChase
{
    class Sprite
    {

        protected int mScreenWidth;
        protected int mScreenHeight;

        protected Texture2D texture;
        protected Rectangle rectangle;

        protected float xPosition;
        protected float yPosition;

        protected float xResetPosition;
        protected float yResetPosition;

        public Sprite(int pScreenWidth, int pScreenHeight, Texture2D pTexture, int pDrawWidth, float pResetX, float pResetY)
        {

            mScreenWidth = pScreenWidth;
            mScreenHeight = pScreenHeight;
            texture = pTexture;
            xResetPosition = pResetX;
            yResetPosition = pResetY;
            GarbageThatRuinsMyPlatformGenerationTool(pTexture, pDrawWidth, 0);

            Reset();
        }

        public virtual void GarbageThatRuinsMyPlatformGenerationTool(Texture2D pTexture, int pDrawWidth, int nothing)
        {
            float aspect = pTexture.Width / pTexture.Height;
            int height = (int)Math.Round(pDrawWidth * aspect);
            rectangle = new Rectangle(0, 0, pDrawWidth, height);
        }

        public void SetPosition(float x, float y)
        {
            xPosition = (int)Math.Round(x);
            yPosition = (int)Math.Round(y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            rectangle.X = (int)Math.Round(xPosition);
            rectangle.Y = (int)Math.Round(yPosition);
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public virtual void Update(float deltaTime)
        {

        }

        public void SetResetPosition(float x, float y)
        {
            xResetPosition = x;
            yResetPosition = y;
        }

        public virtual void Reset()
        {
            SetPosition(xResetPosition, yResetPosition);
        }

        public Vector2 GetCentre()
        {
            float x = xPosition + rectangle.Width / 2;
            float y = yPosition + rectangle.Height / 2;
            return new Vector2(x, y);
        }

        public float GetDistanceFrom(Sprite s)
        {
            Vector2 v1 = s.GetCentre();
            Vector2 v2 = GetCentre();
            float dx = v1.X - v2.X;
            float dy = v1.Y - v2.Y;
            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }

        public bool IntersectsWith(Sprite s)
        {
            return rectangle.Intersects(s.rectangle);
        }
    }

    
}
