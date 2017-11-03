using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;


namespace DemoGame
{


    /// <summary>
    /// Provides a sprite on the screen
    /// 
    /// </summary>
    class Sprite
    {

        protected int screenWidth;
        protected int screenHeight;

        protected Texture2D texture;
        protected Rectangle rectangle;

        protected float xPosition;
        protected float yPosition;

        protected float xResetPosition;
        protected float yResetPosition;

        public Sprite(int inScreenWidth, int inScreenHeight, Texture2D inSpriteTexture, int inDrawWidth, float inResetX, float inResetY)
        {

            screenWidth = inScreenWidth;
            screenHeight = inScreenHeight;
            texture = inSpriteTexture;
            xResetPosition = inResetX;
            yResetPosition = inResetY;

            float aspect = inSpriteTexture.Width / inSpriteTexture.Height;
            int height = (int)Math.Round(inDrawWidth * aspect);
            rectangle = new Rectangle(0, 0, inDrawWidth, height);

            Reset();
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

    class Target : Sprite
    {

        static Random rand = new Random();
        override public void Reset()
        {
            int x = rand.Next(0, screenWidth - rectangle.Width);
            int y = rand.Next(0, screenHeight - rectangle.Height);
            SetPosition(x, y);
        }

        public Target(int inScreenWidth, int inScreenHeight, Texture2D inSpriteTexture, int inDrawWidth, float inResetX, float inResetY) :
            base(inScreenWidth, inScreenHeight, inSpriteTexture, inDrawWidth, inResetX, inResetY)
        {
        }
    }

    class Mover : Sprite
    {
        public void StartMovingUp()
        {
            MovingUp = true;
        }
        public void StopMovingUp()
        {
            MovingUp = false;
        }

        public void StartMovingDown()
        {
            MovingDown = true;
        }
        public void StopMovingDown()
        {
            MovingDown = false;
        }

        public void StartMovingLeft()
        {
            MovingLeft = true;
        }
        public void StopMovingLeft()
        {
            MovingLeft = false;
        }

        public void StartMovingRight()
        {
            MovingRight = true;
        }
        public void StopMovingRight()
        {
            MovingRight = false;
        }


        protected bool MovingUp;
        protected bool MovingDown;
        protected bool MovingLeft;
        protected bool MovingRight;

        protected float resetXSpeed;
        protected float resetYSpeed;

        protected float xSpeed;
        protected float ySpeed;

        public Mover(int inScreenWidth, int inScreenHeight, Texture2D inSpriteTexture, int inDrawWidth, float inResetX, float inResetY, float inResetXSpeed, float inResetYSpeed) :
            base(inScreenWidth, inScreenHeight, inSpriteTexture, inDrawWidth, inResetX, inResetY)
        {
            resetXSpeed = inResetXSpeed;
            resetYSpeed = inResetYSpeed;
            Reset();
        }

        public override void Reset()
        {
            MovingDown = false;
            MovingUp = false;
            MovingLeft = false;
            MovingRight = false;
            SetSpeed(resetXSpeed, resetYSpeed);
            base.Reset();
        }

        public void SetSpeed(float inXSpeed, float inYSpeed)
        {
            xSpeed = inXSpeed;
            ySpeed = inYSpeed;
        }

        public override void Update(float deltaTime)
        {
            if (MovingLeft) xPosition = xPosition - (xSpeed * deltaTime);
            if (MovingRight) xPosition = xPosition + (xSpeed * deltaTime);
            if (MovingUp) yPosition = yPosition - (ySpeed * deltaTime);
            if (MovingDown) yPosition = yPosition + (ySpeed * deltaTime);

            if (xPosition < 0) xPosition = 0;
            if (xPosition + rectangle.Width > screenWidth) xPosition = screenWidth - rectangle.Width;

            if (yPosition < 0) yPosition = 0;
            if (yPosition + rectangle.Height > screenHeight) yPosition = screenHeight - rectangle.Height;

            base.Update(deltaTime);
        }

    }

    class PhysicsMover : Mover
    {
        protected float xAcceleration;
        protected float yAcceleration;

        protected float resetXAcceleration;
        protected float resetYAcceleration;

        protected float friction;
        protected float resetFriction;

        public override void Reset()
        {
            xAcceleration = resetXAcceleration;
            yAcceleration = resetYAcceleration;
            friction = resetFriction;
            base.Reset();
        }


        public PhysicsMover(int inScreenWidth, int inScreenHeight, Texture2D inSpriteTexture, int inDrawWidth, float inResetX, float inResetY, float inResetXSpeed, float inResetYSpeed, float inResetXAccel, float inResetYAccel, float inResetFriction) :
            base(inScreenWidth, inScreenHeight, inSpriteTexture, inDrawWidth, inResetX, inResetY, inResetXSpeed, inResetYSpeed)
        {
            resetXAcceleration = inResetXAccel;
            resetYAcceleration = inResetYAccel;
            resetFriction = inResetFriction;
            Reset();
        }

        public void SetAcceleration( int inX, int inY)
        {
            xAcceleration = inX;
            yAcceleration = inY;
        }


        public override void Update(float deltaTime)
        {
            if (MovingLeft) xSpeed = xSpeed - (xAcceleration * deltaTime);
            if (MovingRight) xSpeed = xSpeed + (xAcceleration * deltaTime);
            if (MovingUp) ySpeed = ySpeed - (yAcceleration * deltaTime);
            if (MovingDown) ySpeed = ySpeed + (yAcceleration * deltaTime);

            xPosition = xPosition + (xSpeed * deltaTime);
            yPosition = yPosition + (ySpeed * deltaTime);

            if (xPosition < 0) xSpeed = Math.Abs(xSpeed);
            if (xPosition + rectangle.Width > screenWidth) xSpeed = Math.Abs(xSpeed) * -1;

            if (yPosition < 0) ySpeed = Math.Abs(ySpeed);
            if (yPosition + rectangle.Height > screenHeight) ySpeed = Math.Abs(ySpeed) * -1 ;

            xSpeed = xSpeed * (1 - deltaTime * friction);
            ySpeed = ySpeed * (1 - deltaTime * friction);

            if (Math.Abs(xSpeed) < 0.01) xSpeed = 0;
            if (Math.Abs(ySpeed) < 0.01) ySpeed = 0;
        }
    }
}
