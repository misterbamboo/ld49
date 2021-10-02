﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Chemicals
{
    public class ChemicalUsageExample
    {
        public void Example()
        {
            // Scriptable Objects
            new ChemicalDefinition { Element = ChemicalElements.Blue, /*                                    */ PlayerEffect = PlayerEffects.TwiceTheSize, FlowerEffect = FlowerEffects.TwiceTheSize };
            new ChemicalDefinition { Element = ChemicalElements.Blue, SecondElement = ChemicalElements.Purple, PlayerEffect = PlayerEffects.Hover, FlowerEffect = FlowerEffects.TearOffFlower };
            new ChemicalDefinition { Element = ChemicalElements.Blue, SecondElement = ChemicalElements.Green, PlayerEffect = PlayerEffects.InvertedControls, FlowerEffect = FlowerEffects.DoublePoison };
            new ChemicalDefinition { Element = ChemicalElements.Blue, SecondElement = ChemicalElements.Yellow, PlayerEffect = PlayerEffects.ExplosionInstant, FlowerEffect = FlowerEffects.ExplosionInstant };
            new ChemicalDefinition { Element = ChemicalElements.Blue, SecondElement = ChemicalElements.Red, PlayerEffect = PlayerEffects.MoreLife, FlowerEffect = FlowerEffects.MoreLife };

            // OnCollisionEnter OU OnTriggerEnter
            var keepElement1 = ChemicalElements.Blue;
            var keepElement2 = ChemicalElements.Green;

            // On consume on player
            var mixResult1 = ChemicalMixes.Mix(keepElement1, keepElement2);
            if (mixResult1.PlayerEffect == PlayerEffects.BestLife) ;

            // On consume on flower
            var mixResult2 = ChemicalMixes.Mix(keepElement1, keepElement2);
            if (mixResult2.FlowerEffect == FlowerEffects.ExplosionWhenDrink) ;
        }
    }
}