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
//using project_main_1;
namespace project_main_1
{  
    class Card: Sprite
    {
        public string[] paramTexture = { "1", "2", "3.1", "4" };
        public string[] Digits = { "u1", "u2", "u3", "u4", "u5", "u6", "u7", "u8", "u9" };
        public int HP, Dps, MANA, ID;
        public Ray Mouse;
        public Card NextCard;
        public bool MouseEnterCard = false;
        //event
        public delegate void SampleEventArgs(Card sender);
        public event SampleEventArgs MouseEnter;
        public event SampleEventArgs Click;
        public event SampleEventArgs MouseLeave;
        public Card(Game1 H, int back, int M, int D, int H1, int pos)
        {
            background = H.Content.Load<Texture2D>(paramTexture[back]) as Texture2D;// H.Content.Load<Texture2D>(paramTexture[t,0]) as Texture2D;
            Mana = H.Content.Load<Texture2D>(Digits[M]) as Texture2D;
            DPS = H.Content.Load<Texture2D>(Digits[D]) as Texture2D;
            Hp = H.Content.Load<Texture2D>(Digits[H1]) as Texture2D;
            HP = H1;
            Dps = D;
            MANA = M;
            this.pos = pos;           
        }
        public bool UpdateMouse(MouseState mouse)
        {
            Mouse = new Ray(new Vector3(mouse.X, mouse.Y, 0), new Vector3(mouse.X, mouse.Y, 100));            
            if(ClikBoxCard.Intersects(Mouse) != null)
            {
                MouseEnterCard = true;
                if(mouse.LeftButton == ButtonState.Pressed|| mouse.RightButton == ButtonState.Pressed)
                {
                    Click(this);
                    return true;
                }
                MouseEnter(this);
                return false;
            }
            else 
            {
                MouseEnterCard = false;
                MouseLeave(this);
                return false;
            }
        }
        public void move_r(Card PreviousCard)
        {
            if (CardPosition_x <= PreviousCard.CardPosition_x + PreviousCard.background_rectangle.Width)
            {
                CardPosition_x += 4;
                if (NextCard != null)
                {
                   NextCard.move_r(this);                    
                }                
            }
        }
        internal void move_l(Card PreviousCard)
        {
            if (CardPosition_x >= PreviousCard.CardPosition_x + PreviousCard.background_rectangle.Width/2)
            {                
                CardPosition_x -= 4;
                if (NextCard != null)
                {
                    NextCard.move_l(this);
                }
            }
        }
    }
    class Sprite: Width_Height_Screen, IDrow, IRectangle
    {
        //Texture2D
        public Texture2D background;
        public Texture2D Mana;
        public Texture2D DPS;
        public Texture2D Hp;        
        public int pos_in_mas;
        private int _pos;
        public int pos
        {
            get
            {
                return _pos;
            }         
            set
            {
                if (1 == value) _pos = 0;
                if (2 == value) _pos = 25;
                if (3 == value) _pos = 50;
                if (4 == value) _pos = 75;
            }
        }
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
        public int CardPosition_x;//is specified from the animation class
        public int CardPosition_y
        {
            get
            {
                return (int)(ScreenPercentage_Y * pos);
            }
        }
        //Rectangle
        public Rectangle background_rectangle
        {
            get { return new Rectangle(CardPosition_x, CardPosition_y, (int)(ScreenPercentage_X*10d), (int)(ScreenPercentage_Y*20d)); }
        }       
        public double СardPercentage_X
        {
            get
            {
                return background_rectangle.Width / 100d;
            }
        }
        public double СardPercentage_Y
        {
            get
            {
                return background_rectangle.Height / 100d;
            }
        }
        public Rectangle Mana_rectangle
        {
            get
            {  
                return new Rectangle(CardPosition_x+ (int)(СardPercentage_X*13d), CardPosition_y + (int)(СardPercentage_Y * 6d), (int)(СardPercentage_X*5d), (int)(СardPercentage_Y * 8d));
            }
        }
        public Rectangle DPS_rectangle
        {
            get
            {
                return new Rectangle(CardPosition_x + (int)(СardPercentage_X * 14d), CardPosition_y + (int)(СardPercentage_Y * 87d), (int)(СardPercentage_X * 5d), (int)(СardPercentage_Y * 8d));
            }
        }
        public Rectangle HP_rectangle
        {
            get
            {
              
                return new Rectangle(CardPosition_x + (int)(СardPercentage_X * 87d), CardPosition_y + (int)(СardPercentage_Y * 87d), (int)(СardPercentage_X * 5d), (int)(СardPercentage_Y * 8d));
            }
        }      
        public BoundingBox ClikBoxCard
        {
            get { return new BoundingBox(new Vector3((float)CardPosition_x, (float)CardPosition_y, pos_in_mas), new Vector3((float)(CardPosition_x+ScreenPercentage_X * 10d), (float)(CardPosition_y+ScreenPercentage_Y * 20d), pos_in_mas)); }
        }       
        public void Drow(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(background, background_rectangle, Color.White);
            spriteBatch.Draw(Mana, Mana_rectangle, Color.White);
            spriteBatch.Draw(DPS, DPS_rectangle, Color.White);
            spriteBatch.Draw(Hp, HP_rectangle, Color.White);
        }      
    }
}
