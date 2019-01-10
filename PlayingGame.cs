using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlayingCardGame
{
    public class PlayingGame
    {
        public int AntalSpelare { get; set; }
        public CardDeck CardDeck;
        string[] spelare;
        StreamWriter SB;


        public void BlandaKort()
        {
            
            Random blanda = new Random();
            for (int i = 0; i < CardDeck.Cards.Count; i++)
            {
                Swap(i, i + blanda.Next(CardDeck.Cards.Count - i));
            }

        }
        public void Swap(int a, int b)
        {
            Card bla = CardDeck.Cards[a];
            CardDeck.Cards[a] = CardDeck.Cards[b];
            CardDeck.Cards[b] = bla;
        }

        public void FrågaEfterAntalSpelare()
        {
            string x;
            int antalSpelare=0;
            bool y=true;
            while (y==true)
            {
            Console.WriteLine("Hur många spelare ska vara med?");
                    x = Console.ReadLine();
                if (int.TryParse(x, out antalSpelare))
                {
                    AntalSpelare = antalSpelare;
                    Console.Clear();
                    y=false;
                }  
            }
            if (antalSpelare<10&&antalSpelare>1)
            {
                Console.Clear();
                NamngeSpelare(antalSpelare);

            }
            else
            {
                FrågaEfterAntalSpelare();
            }
            //return antalSpelare;
            
        }

        public void NamngeSpelare (int antalSpelare)
        {
            spelare = new string[antalSpelare];
            SB = new StreamWriter(@"Scoreboard.txt");
            for (int i = 0; i < antalSpelare; i++)
            {
                Console.WriteLine($"Ange namn på spelare {i + 1}");
                spelare[i] = Console.ReadLine().ToUpper();
                SB.WriteLine(spelare[i]);
            }
            SB.Close();
                Console.Clear();
        }

        public void VisaMeny()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("MENY");
            Console.ResetColor();
            Console.WriteLine("1) Spela");
            Console.WriteLine("2) Regler");
            Console.WriteLine("3) Scoreboard");
            Console.WriteLine("4) Avsluta");
            string menyval = Console.ReadLine();
            switch (menyval)
            {
                case "1":
                    Console.Clear();
                    SpelaSpel();
                    break;
                case "2":
                    Console.Clear();
                    try
                    {
                        RegelMeny();
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine("Regelfilen kan inte hittas.");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("Regelfilen är tom.");
                        Console.WriteLine(ex.Message);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "3":
                    Console.Clear();
                    SpelaSpel();
                    break;
                case "4":
                    Console.Clear();
                    AvslutaSpel();
                    break;
                default:
                    Console.Clear();
                    VisaMeny();
                    break;
            }
        }



        public void SpelaSpel()
        {
            
            Card tagetKort = new Card();
            CardDeck = new CardDeck();
           FrågaEfterAntalSpelare();
            int spelarAntal = spelare.Length;
           
            BlandaKort();


            Console.WriteLine($"{spelare[0]} Ta ett kort"); //Ta kort
            tagetKort = CardDeck.TaKort();
            Console.Clear();


            EnOmgång(tagetKort);
            
        }

        

     

        private void EnOmgång(Card aktuelltKort)
        {
            int spelareMedKortlek = 0;
            int spelareSomGissar = 1;
            int klarade = 0;
            int kort1;
            int kort2;
            string answer;
            Card jämförelseKort;// = new Card();


            while (true)
            {
                
                kort1 = SättVärdePåKort(aktuelltKort);
                Console.WriteLine($"Kortet är {aktuelltKort.Färg} {aktuelltKort.Värde}");
                while (true)
                {
                Console.WriteLine($"{spelare[spelareSomGissar]} Gissa (H)ögre eller (L)ägre");
                answer = Console.ReadLine().Trim().ToLower();
                    if (answer=="l"||answer=="h")
                    {
                        break;
                    }
                } 

                

                 jämförelseKort=CardDeck.TaKort();
                Console.WriteLine($"{spelare[spelareMedKortlek]} Lyft kort");
                Console.ReadLine();

                kort2 = SättVärdePåKort(jämförelseKort);
                
                
                

                if (answer == "l" && kort2 < kort1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("RÄTT!");
                    Console.WriteLine($"Kortet var {jämförelseKort.Färg} {jämförelseKort.Värde} ");
                    Console.WriteLine($"{spelare[spelareMedKortlek]} dricker 3 stycken klunkar");
                    Console.ResetColor();
                    Console.ReadLine();
                    klarade = 0;
                    CardDeck.LäggTillbakaKort(aktuelltKort);
                    aktuelltKort = jämförelseKort;
                    spelareSomGissar = BytGissare(spelareSomGissar, spelareMedKortlek);
                    Console.Clear();

                }
                else if (kort1==kort2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Wow lika höga kort, den såg ni inte komma va? ALLA dricker en klunk, se det som en bonus!");
                    Console.ResetColor();
                    Console.ReadLine();
                    CardDeck.LäggTillbakaKort(aktuelltKort);
                    aktuelltKort = jämförelseKort;
                    spelareSomGissar = BytGissare(spelareSomGissar, spelareMedKortlek);
                    Console.Clear();


                }
                else if (answer == "h" && kort2 > kort1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("RÄTT!");
                    Console.WriteLine($"Kortet var {jämförelseKort.Färg} {jämförelseKort.Värde} ");
                    Console.WriteLine($"{spelare[spelareMedKortlek]} dricker 3 stycken klunkar");
                    Console.ResetColor();
                    Console.ReadLine();
                    klarade = 0;
                    CardDeck.LäggTillbakaKort(aktuelltKort);
                    aktuelltKort = jämförelseKort;
                    spelareSomGissar = BytGissare(spelareSomGissar, spelareMedKortlek);
                    Console.Clear();
                }
                else
                {
                    klarade++;

                    if (answer=="l")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Fel! Kortet var {jämförelseKort.Färg} {jämförelseKort.Värde} ");
                        Console.WriteLine($"{spelare[spelareSomGissar]} dricker {kort2-kort1} klunkar! ");
                        Console.ResetColor();
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Fel! Kortet var {jämförelseKort.Färg} {jämförelseKort.Värde} ");
                        Console.WriteLine($"{spelare[spelareSomGissar]} dricker {kort1 - kort2} klunkar! ");
                        Console.ResetColor();
                        Console.ReadLine();
                        Console.Clear();
                    }

                    CardDeck.LäggTillbakaKort(aktuelltKort);
                    aktuelltKort = jämförelseKort;

                    if (klarade == 2)
                    {
                        if (spelareMedKortlek==AntalSpelare-1)
                        {
                            Console.WriteLine($"BAAM! {spelare[spelareMedKortlek]} har klarat två i rad och får skicka kortleken vidare till {spelare[0]}");
                            spelareMedKortlek = BytHenMedKortlek(spelareSomGissar, spelareMedKortlek);
                            klarade = 0;
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                        Console.WriteLine($"BAAM! {spelare[spelareMedKortlek]} har klarat två i rad och får skicka kortleken vidare till {spelare[spelareMedKortlek+1]}");
                        spelareMedKortlek = BytHenMedKortlek(spelareSomGissar, spelareMedKortlek);
                            klarade = 0;
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }

                    spelareSomGissar = BytGissare(spelareSomGissar, spelareMedKortlek);

                }
            }
        }

        private void AvslutaSpel()
        {
            Console.WriteLine("Då var det slut, hoppas ni blivit lite i runda under fötterna");
        }

        private int SättVärdePåKort(Card kort)
        {
            int nummer=0;

            switch (kort.Värde)
            {
                case Värde.Ess:
                    nummer = 1;
                    break;
                case Värde.Två:
                    nummer = 2;
                    break;
                case Värde.Tre:
                    nummer = 3;
                    break;
                case Värde.Fyra:
                    nummer = 4;
                    break;
                case Värde.Fem:
                    nummer = 5;
                    break;
                case Värde.Sex:
                    nummer = 6;
                    break;
                case Värde.Sju:
                    nummer = 7;
                    break;
                case Värde.Åtta:
                    nummer = 8;
                    break;
                case Värde.Nio:
                    nummer = 9;
                    break;
                case Värde.Tio:
                    nummer = 10;
                    break;
                case Värde.Knekt:
                    nummer = 11;
                    break;
                case Värde.Dam:
                    nummer = 12;
                    break;
                case Värde.Kung:
                    nummer = 13;
                    break;
                
            }
            return nummer;
        }
        private int BytGissare(int spelareSomGissar, int spelareMedKortlek)
        {
            if (spelareSomGissar + 1 == spelareMedKortlek && spelareMedKortlek != AntalSpelare - 1)
            {
                spelareSomGissar = spelareSomGissar + 2;
            }
            else if (spelareSomGissar==spelareMedKortlek&&spelareSomGissar!=AntalSpelare-1)
            {
                spelareSomGissar++;
            }
            else if (spelareSomGissar==spelareMedKortlek&&spelareSomGissar==AntalSpelare-1)
            {
                spelareSomGissar = 0;
            }
            else if (spelareSomGissar == AntalSpelare - 1 && spelareMedKortlek == 0)
            {
                spelareSomGissar = 1;
            }
            else if (spelareSomGissar == AntalSpelare - 1 && spelareMedKortlek != 0)
            {
                spelareSomGissar = 0;
            }
            else
            {
                spelareSomGissar++;
            }
            return spelareSomGissar;
        }
        private int BytHenMedKortlek(int spelareSomGissar, int spelareMedKortlek)
        {
            if (spelareMedKortlek==AntalSpelare-1)
            {
                spelareMedKortlek = 0;
               
            }
            else
            {
                spelareMedKortlek++;
            }

            return spelareMedKortlek;
        }

        private void RegelMeny()
        {

            string[] regler = File.ReadAllLines(@"C:\Users\Administrator\source\repos\PlayingCardGame\Dryckesregler.txt");
            foreach (var item in regler)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Tryck enter för att gå tillbaka");
            Console.ReadLine();
            VisaMeny();
        }

    }
}
