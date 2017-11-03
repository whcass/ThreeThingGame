using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Xml;
using System;

namespace SailAway
{
    
    public class SailAway : Game
    {
        protected GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string mLevelName = "C:\\3TG-2017\\FooReal3TG\\FooReal3TG\\XmlTool\\CrackerChase - lab\\CrackerChase\\bin\\Windows\\x86\\Debug\\Game_1.xml";


        List<Sprite> gameSprites = new List<Sprite>();

        public SailAway()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
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
            Texture2D mTexture = Content.Load<Texture2D>("Yellow3232");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D playerTexture = GenerateRedBox(32,32);
            Player player = new Player(playerTexture, 100, 100);
            LoadXmlStuff();//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            gameSprites.Add(player);
        }
        public void LoadXmlStuff()
        {
            XmlDocument mLevelXml = new XmlDocument();
            mLevelXml.Load(mLevelName);
            XmlNode LevelNode = mLevelXml.FirstChild.NextSibling;

            foreach (XmlNode lPlatform in LevelNode.SelectSingleNode("Platforms").ChildNodes)
            {
                Texture2D mTexture = Content.Load<Texture2D>("Yellow3232");
                XmlNode CoordinatesNode = lPlatform.SelectSingleNode("Coordinates");//I think it's more effecient to store this location and not write it out four times in the line below but idk.
                Platform platform = new Platform(mTexture, 32, 32, ChildNodeIntFromParent(lPlatform, "Length"));
                gameSprites.Add(platform);
            }

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
            spriteBatch.Begin();
            foreach(Sprite s in gameSprites)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();
            
            base.Draw(gameTime);

            
        }
        public int ChildNodeIntFromParent(XmlNode pParentNode, string pNodeName)
        {
            return NodeInt(pParentNode.SelectSingleNode(pNodeName));
        }
        public string ChildNodeStringFromParent(XmlNode pParentNode, string pNodeName)
        {
            return NodeString(pParentNode.SelectSingleNode(pNodeName));
        }
        public string NodeString(XmlNode pNode)
        {
            return pNode.FirstChild.Value;
        }
        public static int NodeInt(XmlNode pNode)
        {
            return Int32.Parse(pNode.FirstChild.Value);
        }
    }
}