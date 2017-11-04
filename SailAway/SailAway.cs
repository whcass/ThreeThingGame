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

        const int DEATH_PLANE_HEIGHT = 800;


        Level level;

        string mLevelName = "C:\\Users\\528945\\Documents\\GIT\\repo\\Levels\\Level_1.xml";

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
            for (int i = 0; i < (width * height); i++)
                colorData[i] = Color.Red;

            texture.SetData<Color>(colorData);

            return texture;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadXmlStuff(mLevelName);
            gameSprites.Add(player);

        }

        protected void ReloadLevel()
        {
            //LoadXmlStuff(mLevelName);
            player.ResetPlayerToStart();
        }

        public void LoadXmlStuff(string levelName)
        {
            //<SetUp>
            levelPlatformList = new List<Platform>();
            XmlDocument mLevelXml = new XmlDocument();
            mLevelXml.Load(levelName);
            XmlNode LevelNode = mLevelXml.FirstChild.NextSibling;
            //</SetUp>

            //<Player>
            Texture2D playerTexture_Idle_1 = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Player").SelectSingleNode("Textures").SelectSingleNode("Idle_1")));
            player = new Player(playerTexture_Idle_1, ChildNodeIntFromParent(LevelNode.SelectSingleNode("Player").SelectSingleNode("StartPos"), "x"), ChildNodeIntFromParent(LevelNode.SelectSingleNode("Player").SelectSingleNode("StartPos"), "y"));

            Texture2D playerTexture_Idle_2 = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Player").SelectSingleNode("Textures").SelectSingleNode("Idle_2")));
            Texture2D playerTexture_Idle_3 = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Player").SelectSingleNode("Textures").SelectSingleNode("Idle_3")));
            Texture2D playerTexture_Moving_1 = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Player").SelectSingleNode("Textures").SelectSingleNode("Moving_1")));
            Texture2D playerTexture_Moving_2 = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Player").SelectSingleNode("Textures").SelectSingleNode("Moving_2")));
            Texture2D playerTexture_Jumping_1 = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Player").SelectSingleNode("Textures").SelectSingleNode("Jumping_1")));

            //player.AddTexture(playerTexture_Idle_1);
            //player.AddTexture(playerTexture_Idle_2);
            //player.AddTexture(playerTexture_Idle_3);
            //player.AddTexture(playerTexture_Moving_1);
            //player.AddTexture(playerTexture_Moving_2);
            //player.AddTexture(playerTexture_Jumping_1);
            //</Player>

            //<Flags>
            Texture2D FlagTextureStart = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Flags").SelectSingleNode("Start").SelectSingleNode("Texture")));
            Texture2D FlagTextureEnd = Content.Load<Texture2D>(NodeString(LevelNode.SelectSingleNode("Flags").SelectSingleNode("End").SelectSingleNode("Texture")));

            Sprite FlagStartSprite = new Sprite(FlagTextureStart, ChildNodeIntFromParent(LevelNode.SelectSingleNode("Flags").SelectSingleNode("Start").SelectSingleNode("Coordinates"), "x"), ChildNodeIntFromParent(LevelNode.SelectSingleNode("Flags").SelectSingleNode("Start").SelectSingleNode("Coordinates"), "y"));
            Sprite FlagEndSprite = new Sprite(FlagTextureStart, ChildNodeIntFromParent(LevelNode.SelectSingleNode("Flags").SelectSingleNode("End").SelectSingleNode("Coordinates"), "x"), ChildNodeIntFromParent(LevelNode.SelectSingleNode("Flags").SelectSingleNode("End").SelectSingleNode("Coordinates"), "y"));

            gameSprites.Add(FlagStartSprite);
            gameSprites.Add(FlagEndSprite);
            //</Flags>

            //<Platforms>
            foreach (XmlNode lPlatform in LevelNode.SelectSingleNode("Platforms").ChildNodes)
            {
                XmlNode CoordinatesNode = lPlatform.SelectSingleNode("Coordinates");

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
                level = new Level(levelPlatformList);
            }
            //</Platforms>
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (player.CheckForDeath())
            {
                ReloadLevel();
            }
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

        protected void NextLevel()
        {

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