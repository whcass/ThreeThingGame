using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace SailAway
{
    
    public class SailAway : Game
    {
        public XmlDocument mLevelXml;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
            Player player = new Player(playerTexture, 100, 100);



        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}