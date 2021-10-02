using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Chemicals
{
    public enum PlayerEffects
    {
        /* Bleu          */ TwiceTheSize,       // TwiceTheSize
        /* Bleu + Mauve  */ Hover,              // TearOffFlower
        /* Bleu + Vert   */ InvertedControls,   // DoublePoison
        /* Bleu + Jaune  */ ExplosionInstant,   // ExplosionInstant
        /* Bleu + Rouge  */ MoreLife,           // MoreLife

        /* Mauve         */ FasterSpeed,        // FasterLoseLife
        /* Mauve + Vert  */ Unknown,            // CarnivorousFlower
        /* Mauve + Jaune */ ImplosionInstant,   // ImplosionInstant
        /* Mauve + Rouge */ Unknown2,           // Unknown

        /* Vert          */ Toxic,              // Toxic
        /* Vert + Jaune  */ ToxicCloudInstant,  // ToxicCloudInstant
        /* Vert + Rouge  */ Unknown3,

        /* Jaune         */ ExplosionWhenDrink, // ExplosionWhenDrink
        /* Jaune + Rouge */ BestLife,           // BestLife
        
        /* Rouge         */ LowLife,            // LowLife
    }

    public enum FlowerEffects
    {
        /* Bleu          */ TwiceTheSize,
        /* Bleu + Mauve  */ TearOffFlower,
        /* Bleu + Vert   */ DoublePoison,
        /* Bleu + Jaune  */ ExplosionInstant,
        /* Bleu + Rouge  */ MoreLife,

        /* Mauve         */ FasterLoseLife,
        /* Mauve + Vert  */ CarnivorousFlower,
        /* Mauve + Jaune */ ImplosionInstant,
        /* Mauve + Rouge */ Unknown,

        /* Vert          */ Toxic,
        /* Vert + Jaune  */ ToxicCloudInstant,
        /* Vert + Rouge  */ Unknown2,

        /* Jaune         */ ExplosionWhenDrink,
        /* Jaune + Rouge */ BestLife,
        
        /* Rouge         */ LowLife,
    }
}
