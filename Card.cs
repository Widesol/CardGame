using System;
using System.Collections.Generic;
using System.Text;

namespace PlayingCardGame
{
    public class Card
    {
            public Färg Färg { get; set; }
            public bool Dolt { get; set; }
            public Värde Värde { get; set; }

    }

    public enum Färg
    {
        Hjärter,
        Ruter,
        Spader,
        Klöver
    }

    public enum Värde
    {
        Ess,
        Två,
        Tre,
        Fyra,
        Fem,
        Sex,
        Sju,
        Åtta,
        Nio,
        Tio,
        Knekt,
        Dam,
        Kung
        
    }
}
