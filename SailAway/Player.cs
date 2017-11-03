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

        private bool JumpAvailable;
        
        private float moveSpeed = 3.0F;
        private float jumpSpeed = 3.0F;
        private float fallSpeed = 3.5F;

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



        private void MovePlayer()
        {
            switch (currentMoveState)
            {
                case MoveState.MovingLeft:
                    Console.WriteLine(XPos);
                    XPos -= 1 * moveSpeed;
                    break;
                case MoveState.MovingRight:
                    Console.WriteLine(XPos);
                    XPos += 1 * moveSpeed;
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
                    if(YPos <= floor)
                    {
                        YPos += 1 * fallSpeed;
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

            MovePlayer();
            PlayerJumping();


        }

        public MoveState GetMoveState()
        {
            return currentMoveState;
        }

        public void SetJumpStateIfWeCan()
        {
            if (JumpAvailable)
            {
                JumpAvailable = false;
                floor = YPos;
                playerTargetJumpHeight = YPos - playerJumpHeight;
                currentJumpState = JumpState.Jumping;
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
    }
}
