using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using sprite_for_drow;

namespace project_main_1
{       
    public class Game1 : Game
    {

        static public int quantityCard1=0, quantityCard2,ind=0;
        static public GraphicsDeviceManager graphics;
        static public SpriteBatch spriteBatch;
        static public Texture2D[] card1=new Texture2D[12];
        static Sprite[] karta=new Sprite[12];
        static public Vector2[] V=new Vector2[12];
        public MouseState M;
        int DisplayWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; //ширина экрана1680
        int DisplayHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; //ширина экрана1050
       
        public Texture2D fata;      
        Animation anim;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";            
            graphics.PreferredBackBufferWidth = DisplayWidth;            
            graphics.PreferredBackBufferHeight = DisplayHeight;     
            //graphics.IsFullScreen=true;
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            F = new main_menu(this);

            card1[0] = Content.Load<Texture2D>("u1");


        }

        
        protected override void UnloadContent()
        {
            
        }
        
        int f=0;
       
        Vector2 S1 = new Vector2(0, 0);

        public bool main_event;
        main_menu F;
        //user us = new user();
        protected override void Update(GameTime gameTime)
        {
            Mouse.WindowHandle = Window.Handle;
            M = Mouse.GetState();
            if (F.event_Anim == false)
            {
                F.event_main(M);
            }
            else if (anim == null)
            {
                anim = new Animation(this);
                anim.add_kart();
                anim.add_kart();
                anim.add_kart();
                anim.add_kart();
                anim.add_kart();
            }
            else
            {
                anim.shift_maps1(M);
            }
            if (M.X > 10 && M.X < 20 && M.Y > 10 && M.Y < 20 && M.LeftButton == ButtonState.Pressed)
            {
                Exit();
            }
            f++;
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
        
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (F.event_Anim == false)
            {
                F.Drow(spriteBatch);
            }
            else if(anim != null)
            {
                anim.Drow(spriteBatch);
            }


            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
