using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace project_main_1
{
    class main_menu: IDrow, IRectangle
    {
       
        int DisplayWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int DisplayHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public Texture2D background_main_menu_texture, button_main_menu_texture;
        public bool event_Anim;
        /// <summary>
        /// 
        /// </summary>
        public Vector3 f1
        {
            get { return new Vector3((DisplayWidth / 2) - ((DisplayWidth / 10) / 2), (DisplayHeight / 2) - ((DisplayHeight / 10) / 2), 0); }
        }
        public Vector3 f2
        {
            get { return new Vector3(((DisplayWidth / 2) - ((DisplayWidth / 10) / 2))+ DisplayWidth / 10, ((DisplayHeight / 2) - ((DisplayHeight / 10) / 2))+ DisplayWidth / 10, 0); }
        }
        public BoundingBox s
        {
            get { return new BoundingBox(f1, f2); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Rectangle background_rectangle
        {
            get { return new Rectangle(0, 0, DisplayWidth, DisplayHeight); }
        }
        public Rectangle button_main_menu_rectangle
        {
            get { return new Rectangle((DisplayWidth / 2)- ((DisplayWidth / 10)/2), (DisplayHeight / 2) - ((DisplayHeight / 10) / 2), DisplayWidth/10, DisplayWidth/10); }
        }
        /// <summary>
        /// 
        /// </summary>
        public main_menu(Game1 H)
        {
            button_main_menu_texture = H.Content.Load<Texture2D>("mainbutton") as Texture2D;
           background_main_menu_texture = H.Content.Load<Texture2D>("mainmenu") as Texture2D;
        }
        public bool event_main(MouseState mouse)
        {
            Ray Mouse = new Ray(new Vector3(mouse.X, mouse.Y, 0), new Vector3(mouse.X, mouse.Y, 100));
            if (s.Intersects(Mouse) != null && mouse.LeftButton == ButtonState.Pressed)
            {
                event_Anim = true;
            }
            else
            {
                event_Anim = false;
            }

            return event_Anim;
        }
        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background_main_menu_texture, background_rectangle, Color.White);
           spriteBatch.Draw(button_main_menu_texture,button_main_menu_rectangle, Color.White);
        }
    }
}
