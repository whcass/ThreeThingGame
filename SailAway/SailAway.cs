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
        bool playerCollided;

        Camera2d cam = new Camera2d();
        



        Level level;

        string mLevelName = "C:\\Users\\Leo\\Documents\\University\\First Year\\3TG\\repo\\Levels\\Level_2.xml";

        Player player;

        List<Sprite> gameSprites = new List<Sprite>();
        List<Platform> levelPlatformList;

        public SailAway()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //mLevelXml = pInputFile;
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
            LoadXmlStuff();
            gameSprites.Add(player);

        }

        public void LoadXmlStuff()
        {
            levelPlatformList = new List<Platform>();
            XmlDocument mLevelXml = new XmlDocument();
            mLevelXml.Load(mLevelName);
            XmlNode LevelNode = mLevelXml.FirstChild.NextSibling;

            Texture2D playerTexture = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Player").SelectSingleNode("Textures").SelectSingleNode("Idle_1")));
            player = new Player(playerTexture, ChildNodeIntFromParent(LevelNode.SelectSingleNode("Player").SelectSingleNode("StartPos"), "x"), ChildNodeIntFromParent(LevelNode.SelectSingleNode("Player").SelectSingleNode("StartPos"), "y"));


            foreach (XmlNode lPlatform in LevelNode.SelectSingleNode("Platforms").ChildNodes)
            {
                Texture2D mTexture = Content.Load<Texture2D>("Yellow3232");
                XmlNode CoordinatesNode = lPlatform.SelectSingleNode("Coordinates");//I think it's more effecient to store this location and not write it out four times in the line below but idk.

                Texture2D mTextureLeft = Content.Load<Texture2D>(NodeString(lPlatform.SelectSingleNode("Textures").SelectSingleNode("Left")));
                Texture2D mTextureRight = Content.Load<Texture2D>(NodeString(lPlatform.SelectSingleNode("Textures").SelectSingleNode("Right")));
                Texture2D mTextureCentre = Content.Load<Texture2D>(NodeString(lPlatform.SelectSingleNode("Textures").SelectSingleNode("Centre")));

                int PlatformLength = ChildNodeIntFromParent(lPlatform, "Length");
                for (int i = 0; i < PlatformLength; i++)
                {
                    if (i == 0)
                    {
                        Platform mPlatformLeft = new Platform(mTextureLeft, ChildNodeIntFromParent(CoordinatesNode, "x"), ChildNodeIntFromParent(CoordinatesNode, "y"), ChildNodeIntFromParent(lPlatform, "Length"));
                        gameSprites.Add(mPlatformLeft);
                        levelPlatformList.Add(mPlatformLeft);
                    }
                    else if (i == PlatformLength - 1)
                    {
                        Platform mPlatformRight = new Platform(mTextureRight, ChildNodeIntFromParent(CoordinatesNode, "x") + (32 * i), ChildNodeIntFromParent(CoordinatesNode, "y"), ChildNodeIntFromParent(lPlatform, "Length"));
                        gameSprites.Add(mPlatformRight);
                        levelPlatformList.Add(mPlatformRight);
                    }
                    else
                    {
                        Platform mPlatformCentre = new Platform(mTextureCentre, ChildNodeIntFromParent(CoordinatesNode, "x") + (32 * i), ChildNodeIntFromParent(CoordinatesNode, "y"), ChildNodeIntFromParent(lPlatform, "Length"));
                        gameSprites.Add(mPlatformCentre);
                        levelPlatformList.Add(mPlatformCentre);
                    }
                }
                //Platform platform = new Platform(mTexture, ChildNodeIntFromParent(CoordinatesNode, "x"), ChildNodeIntFromParent(CoordinatesNode, "y"), ChildNodeIntFromParent(lPlatform, "Length"));
                //gameSprites.Add(platform);
                level = new Level(levelPlatformList);
            }
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

            if (keys.IsKeyDown(Keys.Space))
            {
                if (player.SetJumpStateIfWeCan())
                {
                    playerCollided = false;
                }
            }

            //Console.WriteLine(player.GetMoveState());
            //Console.WriteLine(player.GetJumpState());
            player.Update(1.0f / 60.0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            cam.Pos = player.GetPlayerVector();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.get_transformation(GraphicsDevice));
            foreach (Sprite s in gameSprites)
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