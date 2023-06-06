﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CosmicHunter
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private GamePlay gamePlay;
        private MainMenu mainMenu;
        private Basic2d cursor;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //screen size
            Globals.screenWidth = 900; //900-1600-1920
            Globals.screenHeight = 500; //500-900-1080

            graphics.PreferredBackBufferWidth = Globals.screenWidth;
            graphics.PreferredBackBufferHeight = Globals.screenHeight;

            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            //load cursor image, set cursor position and size
            cursor = new Basic2d("2d\\Misc\\cursor", new Vector2(0, 0), new Vector2(35, 35)); //28, 28

            Globals.keyboard = new MdKeyboard();
            Globals.mouse = new MdMouseControl();

            mainMenu = new MainMenu(ChangeGameState, ExitGame);
            gamePlay = new GamePlay(ChangeGameState);
        }

        protected override void UnloadContent()
        {
            //get rid of anything you don't want
        }

        protected override void Update(GameTime gameTime)
        {
            //updating the game logic, the keyboard and the mouse
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.gameTime = gameTime;    //gives number of milliseconds between frames
            Globals.keyboard.Update();
            Globals.mouse.Update();

            if (Globals.gameState == 0)
            {
                mainMenu.Update();
            }
            else if (Globals.gameState == 1)
            {
                gamePlay.Update();
            }

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        public virtual void ChangeGameState(object info)
        {
            Globals.gameState = Convert.ToInt32(info, Globals.culture);
        }

        public virtual void ExitGame(object info)
        {
            //sluit de game af, 0 betekent zonder errors afsluiten
            System.Environment.Exit(0);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);

            //spriteBatch has to open and close, everything that happens in the game should come between it
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            if (Globals.gameState == 0)
            {
                mainMenu.Draw();    //draw the menu
            }
            else if (Globals.gameState == 1)
            {
                gamePlay.Draw();    //draw the world
            }

            //cursor.Draw(new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y), new Vector2(0, 0));  //position = top left of image
            cursor.Draw(new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y), Color.White);   //position = center of image
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }

        internal class Program
        {
            private static void Main(string[] args)
            {
                using var game = new CosmicHunter.Main();
                game.Run();
            }
        }
    }
}