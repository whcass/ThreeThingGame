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
            switch (currentMoveState)
            {
                case MoveState.MovingLeft:
                    if (!Level.CurrentLevel.CheckPlatformCollisions(this))
                    {
                        XPos -= 1 * moveSpeed;
                    }
                    else {
                        XPos = oldXPos;
                    }

                    Console.WriteLine(XPos);
                    break;
                case MoveState.MovingRight:
                    Console.WriteLine(XPos);
                    if (!Level.CurrentLevel.CheckPlatformCollisions(this))
                    {
                        XPos += 1 * moveSpeed;
                    }
                    else
                    {
                        XPos = oldXPos;
                    }

                    break;
            }
        }

        private void PlayerJumping()
        {
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
                        if (!Level.CurrentLevel.CheckPlatformCollisions(this)) {
                            YPos += 1 * fallSpeed;
                        }
                        else
                        {
                            currentJumpState = JumpState.Landed;
                        }
                    }
                    else
                    {
                        currentJumpState = JumpState.Landed;
                        JumpAvailable = true;
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
            Console.WriteLine(currentJumpState);
            MovePlayerIfPossible();
            PlayerJumping();
            if (currentJumpState == JumpState.Landed)
            {
                AffectPlayerWithGravity();
            }
        }

        public MoveState GetMoveState()
        {
            return currentMoveState;
        }

        public bool SetJumpStateIfWeCan()
        {
            if (JumpAvailable)
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
            if (gravityOn&& !Level.CurrentLevel.CheckPlatformCollisions(this))
            {
                YPos += 1*fallSpeed;
            }
        }

        public Rectangle GetRectangle()
        {
            return Rectangle;
        }
    }
}
