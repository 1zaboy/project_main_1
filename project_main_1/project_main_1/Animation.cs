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
  
    class Animation : Width_Height_Screen, IDrow
    {
        public List<Card> karts = new List<Card>(12);
        public List<Card> karts_field = new List<Card>(7);
        public List<Card> karts1_field = new List<Card>(7);
        public int[,] paramTexture = { { 1, 3, 6 }, { 2, 7, 3 }, { 3, 7, 4 }, { 4, 6, 6 } };
        
        private ButtonNext BN;
        public Animation(Game1 H)
        {
            this.H = H;
            BN = new ButtonNext(H);
        } 
        /// <summary>
        /// shift_card
        /// </summary>
        public void shift_maps1(MouseState s1)
        {
            for (int f = karts.Count-1; f >= 0; f--)
            {
                if(karts[f].UpdateMouse(s1))
                    break;
            }           
        }

        public void add_kart()
        {
            int r = Rand.Next(0, 3);

            var TMP_Card = new Card(H, r, paramTexture[r, 0], paramTexture[r, 1], paramTexture[r, 2], 1);
            TMP_Card.MouseEnter += Card_MouseEnter;
            TMP_Card.MouseLeave += Card_MouseLeave;
            TMP_Card.Click += Card_ClickChoice;
            if (karts.Count>0) karts.Last().NextCard = TMP_Card;
            TMP_Card.ID = karts.Count;                      
            karts.Add(TMP_Card);
            karts = start_local_arm(karts);
        }
        /// <summary>
        /// aad_card_on_table
        /// </summary>
        public void ExtraditionID()
        {
            for (int f = 0; f < karts.Count; f++)
            {
                if (f < karts.Count - 1)
                {
                    karts[f].NextCard = karts[f + 1];
                }
                else
                {
                    karts[f].NextCard = null;
                }
                karts[f].ID = f;
            }            
        }
        private void Card_ClickChoice(Card sender)
        {            
            var TMP_Card = karts[sender.ID];
            karts.Remove(karts[sender.ID]);
            ExtraditionID();
            karts = start_local_arm(karts);

            if (karts_field.Count > 0)
            {
                karts_field.Last().NextCard = TMP_Card;
            }
            TMP_Card.NextCard = null;
            TMP_Card.ID = karts_field.Count;
            TMP_Card.pos = 2;

            karts_field.Add(TMP_Card);
            karts_field = start_local_arm_for_table(karts_field);
        }
        /// <summary>
        /// leave - Entre
        /// </summary>
        private void Card_MouseLeave(Card sender)
        {
            if (sender.NextCard != null)
                sender.NextCard.move_l(sender);
        }
        private void Card_MouseEnter(Card sender)
        {
            if(sender.NextCard!=null)
                if(!sender.NextCard.MouseEnterCard)
                    sender.NextCard.move_r(sender);
        }
        /// <summary>
        /// Screen_Percentage
        /// </summary>
        double starting_position_card;
        public double ScreenPercentage_X
        {
            get
            {
                return ScreenWidth / 100d;
            }
        }
        public double ScreenPercentage_Y
        {
            get
            {
                return ScreenHeight / 100d;
            }
        }
        /// <summary>
        /// start_local
        /// </summary>
        public List<Card> start_local_arm(List<Card> A)
        {
            var cards = A;
            int eee = 0;
            while (eee < cards.Count)
            {
                starting_position_card = ScreenWidth / 2 + (ScreenPercentage_X * 10d) / 2;
                cards[eee].CardPosition_x = (int)(starting_position_card - (cards[eee].СardPercentage_X * 50d) * (cards.Count - eee));
                cards[eee].pos_in_mas = cards.Count - eee;
                eee++;
            }
            return cards;
        }
        public List<Card> start_local_arm_for_table(List<Card> A)
        {
            var cards = A;
            int eee = 0;
            while (eee < cards.Count)
            {
                starting_position_card = ScreenWidth / 2 + ((ScreenPercentage_X * 10d) * cards.Count) / 2;
                cards[eee].CardPosition_x =
                    (int) (starting_position_card - (cards[eee].СardPercentage_X * 100d) * (cards.Count - eee));
                cards[eee].pos_in_mas = cards.Count - eee;
                eee++;
            }

            return cards;
        }
        /// <summary>
        /// drow
        /// </summary>       
        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(H.Content.Load<Texture2D>("stol") as Texture2D,
                new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            BN.Drow(spriteBatch);
            for (int x = 0; x < karts.Count; x++)
            {
                karts[x].Drow(spriteBatch);
            }
            for (int x = 0; x < karts_field.Count; x++)
            {             
                    karts_field[x].Drow(spriteBatch);
            }
        }
    }
}
