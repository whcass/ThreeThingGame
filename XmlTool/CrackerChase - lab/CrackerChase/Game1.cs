using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrackerChase
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Game World
        // These variables define the world 

        enum GameStates { StartScreen, GamePlaying };
        int CurrentGameState;

        Mover cheese;
        Target cracker;
        Sprite background;
        SoundEffect BurpSound;

        int screenWidth;
        int screenHeight;

        List<Sprite> gameSprites = new List<Sprite>();
        List<Target> crackers = new List<Target>();
        List<BasicPlatform> Platforms = new List<BasicPlatform>();

        SpriteFont messageFont;

        string messageString = "Hello world";

        int score;
        int timer;

        public XmlDocument mFuck_The_Content_Pipline = new XmlDocument();


        void startPlayingGame()
        {
            foreach (Sprite s in gameSprites)
            {
                s.Reset();
            }
            foreach (Target t in crackers)
            {
                t.Reset();
            }
            messageString = "Cracker Chase";

            timer = 600;
            score = 0;

        }


        public Game1(XmlDocument pFuck_The_Content_Pipline)
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            mFuck_The_Content_Pipline = pFuck_The_Content_Pipline;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LoadCrackerChaseShit();
            //XmlDocument inputFile = new XmlDocument();
            //inputFile.Load("Game_1");
            //XmlDocument inputFile = Content.Load<XmlDocument>("Game_1");
            //LoadXmlStuff(mFuck_The_Content_Pipline);
        }
        private void LoadXmlStuff(XmlDocument XmlFile)
        {
            XmlNode LevelNode = XmlFile.FirstChild.NextSibling;
            Texture2D Pixel = new Texture2D(GraphicsDevice, 1, 1);

            foreach (XmlNode lPlatform in LevelNode.SelectSingleNode("Platforms").ChildNodes)
            {
                XmlNode ColorNode = lPlatform.SelectSingleNode("ColorRGBA");//I think it's more effecient to store this location and not write it out four times in the line below but idk.
                Color mPlatformColor = new Color(new Vector4(ChildNodeIntFromParent(ColorNode, "Red"), ChildNodeIntFromParent(ColorNode, "Green"), ChildNodeIntFromParent(ColorNode, "Blue"), ChildNodeIntFromParent(ColorNode, "Alpha")));

                XmlNode CoordinatesNode = lPlatform.SelectSingleNode("Coordinates");//I think it's more effecient to store this location and not write it out four times in the line below but idk.
                                                                                    //SpriteBatch.Draw(Pixel, new Rectangle(ChildNodeIntFromParent(CoordinatesNode, "x1"), ChildNodeIntFromParent(CoordinatesNode, "x2"), Math.Abs(ChildNodeIntFromParent(CoordinatesNode, "x1") - ChildNodeIntFromParent(CoordinatesNode, "x2")), Math.Abs(ChildNodeIntFromParent(CoordinatesNode, "y1") - ChildNodeIntFromParent(CoordinatesNode, "y2"))), mPlatformColor);
                BasicPlatform basicPlatform = new BasicPlatform(screenWidth, screenHeight, Pixel, Math.Abs(ChildNodeIntFromParent(CoordinatesNode, "x1") - ChildNodeIntFromParent(CoordinatesNode, "x2")), Math.Abs(ChildNodeIntFromParent(CoordinatesNode, "y1") - ChildNodeIntFromParent(CoordinatesNode, "y2")), ChildNodeIntFromParent(CoordinatesNode, "x1"), ChildNodeIntFromParent(CoordinatesNode, "x2"), mPlatformColor, ChildNodeStringFromParent(lPlatform, "Name"));
                BasicPlatform basicPlatform2 = new BasicPlatform(screenWidth, screenHeight, Pixel, 300,300,1,1, Color.HotPink, "cock");

                Platforms.Add(basicPlatform);
                gameSprites.Add(basicPlatform);
                Platforms.Add(basicPlatform2);
                gameSprites.Add(basicPlatform2);
            }
        }
        private static int CheckIfSomeonePutCoordinatesBackwards(int pValue1, int pValue2)//Not in use
        {
            if (pValue1 < pValue2)
            {
                return pValue1;
            }
            else
            {
                return pValue1;
            }
        }
        private void LoadCrackerChaseShit()
        {
            CurrentGameState = (int)GameStates.StartScreen;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            messageFont = Content.Load<SpriteFont>("MessageFont");

            //LoadXmlStuff(mFuck_The_Content_Pipline);

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            Texture2D cheeseTexture = Content.Load<Texture2D>("cheese");
            Texture2D cloth = Content.Load<Texture2D>("Tablecloth");
            Texture2D crackerTexture = Content.Load<Texture2D>("cracker");

            BurpSound = Content.Load<SoundEffect>("Burp");

            background = new Sprite(screenWidth, screenHeight, cloth, screenWidth, 0, 0);
            gameSprites.Add(background);

            int crackerWidth = screenWidth / 20;

            for (int i = 0; i < 100; i++)
            {
                cracker = new Target(screenWidth, screenHeight, crackerTexture, crackerWidth, 0, 0);
                gameSprites.Add(cracker);
                crackers.Add(cracker);
            }

            int cheeseWidth = screenWidth / 15;
            cheese = new Mover(screenWidth, screenHeight, cheeseTexture, cheeseWidth, screenWidth / 2, screenHeight / 2, 500, 500);
            gameSprites.Add(cheese);
            LoadXmlStuff(mFuck_The_Content_Pipline);
            messageString = "Press space to start the game";
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }





        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case 0:
                    UpdateStartScreen();
                    break;
                case 1:
                    UpdateGamePlay();
                    break;
            }
            base.Update(gameTime);
        }

        private void UpdateGamePlay()
        {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Up))
            {
                cheese.StartMovingUp();
            }
            else
            {
                cheese.StopMovingUp();
            }

            if (keys.IsKeyDown(Keys.Down))
            {
                cheese.StartMovingDown();
            }
            else
            {
                cheese.StopMovingDown();
            }

            if (keys.IsKeyDown(Keys.Left))
            {
                cheese.StartMovingLeft();
            }
            else
            {
                cheese.StopMovingLeft();
            }

            if (keys.IsKeyDown(Keys.Right))
            {
                cheese.StartMovingRight();
            }
            else
            {
                cheese.StopMovingRight();
            }

            foreach (Sprite s in gameSprites)
            {
                s.Update(1.0f / 60.0f);
            }
            foreach (Target t in crackers)
            {
                if (cheese.IntersectsWith(t))
                {
                    ////BurpSound.Play();                                                              Burp sound is here
                    t.Reset();
                    score = score + 10;
                }
            }

            timer = timer - 1;

            int secsLeft = timer / 60;
            messageString = "Time: " + secsLeft.ToString() + " Score: " + score;

            if (timer == 0)
            {
                messageString = " Game Over : Press Space to exit   Score: " + score.ToString();
                gameOver();
            }
        }

        private void UpdateStartScreen()
        {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Space))
            {
                CurrentGameState = (int)GameStates.GamePlaying;
                startPlayingGame();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case 0:
                    DrawStartScreen();
                    break;
                case 1:
                    DrawGamePlay();
                    break;
            }

            base.Draw(gameTime);
        }

        private void DrawStartScreen()
        {
            spriteBatch.Begin();

            foreach (Sprite s in gameSprites)
            {
                s.Draw(spriteBatch);
            }
            float xPos = (screenWidth - messageFont.MeasureString(messageString).X) / 2;

            Vector2 statusPos = new Vector2(xPos, screenHeight / 2);

            spriteBatch.DrawString(messageFont, messageString, statusPos, Color.Red);

            spriteBatch.End();
        }

        private void DrawGamePlay()
        {
            spriteBatch.Begin();

            foreach (Sprite s in gameSprites)
            {
                s.Draw(spriteBatch);
            }
            float xPos = (screenWidth - messageFont.MeasureString(messageString).X) / 2;

            Vector2 statusPos = new Vector2(xPos, 10);

            spriteBatch.DrawString(messageFont, messageString, statusPos, Color.Red);

            spriteBatch.End();
        }

        private void gameOver()
        {
            messageString = "Game over friend. Your score was: " + score + " . Press space bar to play again!";
            CurrentGameState = (int)GameStates.StartScreen;
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.Space))
            {
                startPlayingGame();
            }
        }

        public static int ChildNodeIntFromParent(XmlNode pParentNode, string pNodeName)
        {
            return NodeInt(pParentNode.SelectSingleNode(pNodeName));
        }
        public static string ChildNodeStringFromParent(XmlNode pParentNode, string pNodeName)
        {
            return NodeString(pParentNode.SelectSingleNode(pNodeName));
        }
        public static string NodeString(XmlNode pNode)
        {
            return pNode.FirstChild.Value;
        }
        public static int NodeInt(XmlNode pNode)
        {
            return Int32.Parse(pNode.FirstChild.Value);
        }
    }
}
