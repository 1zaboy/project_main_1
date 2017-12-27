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
using System.Threading;
namespace project_main_1
{
    class ButtonNext: Width_Height_Screen, IScreen, IDrow, IRectangle
    {
        private Texture2D Button;
        public double ScreenPercentage_X
        {
            get
            {
                return ScreenWidth / 100d;
            }
        }//процент от экрана X
        public double ScreenPercentage_Y
        {
            get
            {
                return ScreenHeight / 100d;
            }
        }//процент от экрана Y
        /// <summary>
        /// Location
        /// </summary>
        public Rectangle background_rectangle
        {
            get
            {
                return new Rectangle((int) (ScreenPercentage_X * 90d), (int) (ScreenPercentage_Y * 50d),
                    (int) (ScreenPercentage_X * 5d),
                    (int) (ScreenPercentage_X * 5d));
            }
        }
        public BoundingBox ClikBoxCard
        {
            get
            {
                return new BoundingBox(new Vector3((float) background_rectangle.X, (float) background_rectangle.Y, 0),
                    new Vector3((float) (background_rectangle.Width),
                        (float) (background_rectangle.Height), 0));
            }
        }
        /// <summary>
        /// Delegate
        /// </summary>
        public delegate void SampleEventArgs(ButtonNext sender);
        public event SampleEventArgs Click;
        /// <summary>
        /// Constructor
        /// </summary>
        public ButtonNext(Game1 H)
        {
            this.H = H;
            Button = H.Content.Load<Texture2D>("movie") as Texture2D;
        }
        /// <summary>
        /// Work with mouse
        /// </summary>

        public bool UpdateMouse(MouseState mouse)
        {
            Ray Mouse = new Ray(new Vector3(mouse.X, mouse.Y, 0), new Vector3(mouse.X, mouse.Y, 100));
            if (ClikBoxCard.Intersects(Mouse) != null && mouse.LeftButton == ButtonState.Pressed ||
                mouse.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Drow
        /// </summary>
        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Button, background_rectangle, Color.White);
          
        }
    }
}


