using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Chemicals
{
    public enum PlayerEffects
    {
        None,
        /* Bleu          */ TwiceTheSize,
        /* Bleu + Mauve  */ Hover,
        /* Bleu + Vert   */ InvertedControls,
        /* Bleu + Jaune  */
        /* Bleu + Rouge  */ MoreLife,

        /* Mauve         */ FasterSpeed,
        /* Mauve + Vert  */ LooseControl,
        /* Mauve + Jaune */
        /* Mauve + Rouge */ SlowDownSpeed,

        /* Vert          */ Toxic,
        /* Vert + Jaune  */
        /* Vert + Rouge  */ BestLifePotion,

        /* Jaune         */ ExplosionWhenDrink,
        /* Jaune + Rouge */
        
        /* Rouge         */ LowLife,
    }

    public enum FlowerEffects
    {
        None,
        /* Bleu          */ TwiceTheSize,
        /* Bleu + Mauve  */ TearOffFlower,
        /* Bleu + Vert   */ DoublePoison,
        /* Bleu + Jaune  */
        /* Bleu + Rouge  */ MoreLife,

        /* Mauve         */ FasterLoseLife,
        /* Mauve + Vert  */ CarnivorousFlower,
        /* Mauve + Jaune */
        /* Mauve + Rouge */ SlowDownLoseLife,

        /* Vert          */ Toxic,
        /* Vert + Jaune  */
        /* Vert + Rouge  */ BestLifePotion,

        /* Jaune         */ ExplosionWhenDrink,
        /* Jaune + Rouge */
        
        /* Rouge         */ LowLife,
    }

    public enum InstantEffects
    {
        None,
        /* Bleu          */
        /* Bleu + Mauve  */
        /* Bleu + Vert   */
        /* Bleu + Jaune  */ Explosion,
        /* Bleu + Rouge  */

        /* Mauve         */
        /* Mauve + Vert  */
        /* Mauve + Jaune */ Implosion,
        /* Mauve + Rouge */

        /* Vert          */
        /* Vert + Jaune  */ ToxicCloud,
        /* Vert + Rouge  */

        /* Jaune         */
        /* Jaune + Rouge */ LifeExplosion

        /* Rouge         */
    }
}
