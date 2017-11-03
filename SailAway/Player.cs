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
        }
    }
}
