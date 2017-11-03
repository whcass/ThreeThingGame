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

        private float playerJumpHeight = 128.0F;
        private float 

        private bool JumpAvailable;

        private float moveSpeed = 3.0F;
        private float jumpSpeed = 1.0F;
        private float fallSpeed = 1.0F;

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

        public Player(Texture2D texture, float xPos, float yPos) : base(texture,xPos,yPos)
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

                    XPos -= 1 * moveSpeed;
                    Console.WriteLine(XPos);
                    break;
                case MoveState.MovingRight:
                    XPos += 1 * moveSpeed;
                    Console.WriteLine(XPos);
                    break;
            }
        }

        private void PlayerJumping()
        {
            switch (currentJumpState)
            {
                case JumpState.Falling:

                    YPos -= 1 * moveSpeed;
                    Console.WriteLine(XPos);
                    break;
            }
        }

        public override void Update(float deltaTime)
        {
            
            


        }

        public MoveState GetMoveState()
        {
            return currentMoveState;
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
