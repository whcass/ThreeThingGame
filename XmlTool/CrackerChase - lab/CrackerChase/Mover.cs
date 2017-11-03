using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackerChase
{
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
            if (MovingLeft)
            {
                xPosition = xPosition - (xSpeed * deltaTime);
            }
            if (MovingRight)
            {
                xPosition = xPosition + (xSpeed * deltaTime);
            }
            if (MovingUp)
            {
                yPosition = yPosition - (ySpeed * deltaTime);
            }
            if (MovingDown)
            {
                yPosition = yPosition + (ySpeed * deltaTime);
            }

            if (xPosition < 0)
            {
                xPosition = 0;
            }
            if (xPosition + rectangle.Width > mScreenWidth)
            {
                xPosition = mScreenWidth - rectangle.Width;
            }

            if (yPosition < 0)
            {
                yPosition = 0;
            }
            if (yPosition + rectangle.Height > mScreenHeight)
            {
                yPosition = mScreenHeight - rectangle.Height;
            }

            base.Update(deltaTime);
        }

    }
}
