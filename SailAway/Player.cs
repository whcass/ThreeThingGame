using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailAway
{
    class Player : Sprite
    {
        private JumpState currentJumpState;
        private MoveState currentMoveState;

        private float playerJumpHeight = 60.0F;
        private float playerTargetJumpHeight;
        private float floor;
        private bool gravityOn = true;

        private bool JumpAvailable;

        private float moveSpeed = 3.0F;
        private float jumpSpeed = 3.0F;
        private float fallSpeed = 3.5F;

        private bool playerCollidedWithFloor;


        public enum JumpState
        {
            Jumping,
            Falling,
            Landed
        }

        public enum MoveState
        {
            MovingRight,
            MovingLeft,
            NotMoving
        }

        public Player(Texture2D texture, float xPos, float yPos) : base(texture, xPos, yPos)
        {
            currentJumpState = JumpState.Landed;
            currentMoveState = MoveState.NotMoving;
            JumpAvailable = true;
        }

        public void SetPlayerCollidedWithFloor(bool state)
        {
            playerCollidedWithFloor = state;
        }

        private void MovePlayerIfPossible()
        {
            float oldXPos = XPos;
            float newXPos;
            switch (currentMoveState)
            {
                case MoveState.MovingLeft:
                    newXPos = XPos - 1 * moveSpeed;
                    if (!Level.CurrentLevel.CheckPlatformCollisions(this))
                    {
                        XPos = newXPos;
                    }
                    else {
                        XPos = oldXPos - 1 * moveSpeed;
                    }

                    
                    break;
                case MoveState.MovingRight:
                    newXPos = XPos + 1 * moveSpeed;
                    
                    if (!Level.CurrentLevel.CheckPlatformCollisions(this))
                    {
                        XPos = newXPos;
                    }
                    else
                    {
                        XPos = oldXPos + 1*moveSpeed;
                    }

                    break;
            }
        }

        public Vector2 GetPlayerVector()
        {
            return new Vector2(XPos, YPos);
        }

        private void JumpPlayerIfPossible()
        {
            float oldYpos = YPos;
            float newYpos;
            switch (currentJumpState)
            {
                case JumpState.Jumping:
                    if (YPos >= playerTargetJumpHeight)
                    {
                        YPos -= 1 * jumpSpeed;
                    }
                    else
                    {
                        currentJumpState = JumpState.Falling;
                    }
                    break;
                case JumpState.Falling:
                    if (YPos <= floor)
                    {
                        newYpos = YPos + 1 * fallSpeed;
                        if (!Level.CurrentLevel.CheckPlatformCollisions(this)) {
                            YPos = newYpos;
                        }
                        else
                        {
                            YPos = oldYpos - 8;
                            currentJumpState = JumpState.Landed;
                            JumpAvailable = true;
                        }
                    }
                    else
                    {
                        currentJumpState = JumpState.Landed;
                        
                    }
                    break;

            }
        }

        public JumpState GetJumpState()
        {
            return currentJumpState;
        }

        public override void Update(float deltaTime)
        {
            Console.WriteLine(JumpAvailable);
            //Console.WriteLine(currentJumpState);
            MovePlayerIfPossible();

            JumpPlayerIfPossible();
            
            if (currentJumpState == JumpState.Landed)
            {
                JumpAvailable = true;
                AffectPlayerWithGravity();
            }
        }

        public MoveState GetMoveState()
        {
            return currentMoveState;
        }

        public bool SetJumpStateIfWeCan()
        {
            if (JumpAvailable&&(currentJumpState==JumpState.Landed))
            {
                JumpAvailable = false;
                floor = YPos;
                playerTargetJumpHeight = YPos - playerJumpHeight;
                currentJumpState = JumpState.Jumping;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetMoveState(string direction)
        {

            switch (direction)
            {
                case "left":
                    currentMoveState = MoveState.MovingLeft;
                    break;
                case "right":
                    currentMoveState = MoveState.MovingRight;
                    break;
                case "stopped":
                    currentMoveState = MoveState.NotMoving;
                    break;
            }


        }

        //private bool CheckIfPlatformIsOnEitherSide()
        //{
        //    if(Level)
        //    return false;
        //}



        public void AffectPlayerWithGravity()
        {
            float newYPos = YPos + 1 * fallSpeed;
            float oldYPos = YPos;
            if (gravityOn && !Level.CurrentLevel.CheckPlatformCollisions(this))
            {
                YPos = newYPos;
            }
            else
            {
                // gravityOn = false;
                YPos = oldYPos;
            }
        }

        public Rectangle GetRectangle()
        {
            return Rectangle;
        }
    }
}
