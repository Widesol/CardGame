using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayingCardGame
{
    public class CardDeck 
    {
        int maxAntalKort = 52;

        public List<Card> Cards;
        

        public CardDeck()
        {
            //Constructor
            SkapaKortlek();
        }

        private void SkapaKortlek()
        {
            Cards = new List<Card>();
            int counter = 0;

            foreach (Färg item in Enum.GetValues(typeof(Färg)))
            {
                foreach (Värde värde in Enum.GetValues(typeof(Värde)))
                {
                    var kort = new Card();
                    kort.Färg = item;
                    kort.Värde = värde;
                    Cards.Add(kort);
                    counter++;
                }
            }
       
        }

        public void SkrivUtKortlek()
        {
            foreach (var item in Cards)
            {
                Console.WriteLine($"{item.Värde} {item.Färg}");
            }
        }
        
        public Card TaKort()
        {
           
            Card tagetKort = new Card();
            tagetKort = Cards[0];
            tagetKort.Dolt = false;

            Cards = Cards.Skip(1).ToList();

            //var tempKortlek = new List<Card>();
            //for (int i = 1; i <= Cards.Count; i++)
            //{
            //    tempKortlek.Add(Cards[i]);
            //}

            //Cards = tempKortlek;

            return tagetKort;

        }
        public void LäggTillbakaKort(Card tagetKort)
        {
            //var temp = Cards[0];

            //for (int i = 1; i <= Cards.Length-1; i++)
            //{
            //    Cards[i-1] = Cards[i];

            //}
            //Cards[Cards.Length-1] = temp;
            Cards.Add(tagetKort);
        
        }


    }
}
