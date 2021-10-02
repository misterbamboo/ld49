using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Chemicals
{
    public class ChemicalDefinition
    {
        public ChemicalElements Element;

        public ChemicalElements? SecondElement;

        public bool isInstant;

        public PlayerEffects PlayerEffect;

        public FlowerEffects FlowerEffect;
    }
}
