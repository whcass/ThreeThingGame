using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml;

namespace SailAway
{
    
    public class SailAway : Game
    {
        public XmlDocument mLevelXml;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        List<Sprite> gameSprites = new List<Sprite>();

        public SailAway(XmlDocument pInputFile)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            mLevelXml = pInputFile;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public Texture2D GenerateRedBox(int width, int height)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, width, height);
            Color[] colorData = new Color[width * height];
            for (int i = 0; i < (width*height); i++)
                colorData[i] = Color.Red;

            texture.SetData<Color>(colorData);

            return texture;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D playerTexture = GenerateRedBox(32,32);
            player = new Player(playerTexture, 100, 100);

            gameSprites.Add(player);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (keys.IsKeyDown(Keys.D))
            {
                player.SetMoveState("right");
            }
            else if (keys.IsKeyDown(Keys.A))
            {
                player.SetMoveState("left");
            }
            else
            {
                player.SetMoveState("stopped");
            }

            Console.WriteLine(player.GetMoveState());
            player.Update(1.0f / 60.0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach(Sprite s in gameSprites)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}