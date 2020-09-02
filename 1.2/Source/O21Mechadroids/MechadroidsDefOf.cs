using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Mechadroids
{
    [DefOf]
    public static class MechadroidsDefOf
    {
        static MechadroidsDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MechadroidsDefOf));
        }

        public static ThingDef O21_MechanoidParts;
        public static ThingDef O21_MechanoidPowerCell;
        public static ThingDef O21_MechanoidRepairKit;

        public static FactionDef O21_MechadroidCivil;
        public static FactionDef O21_MechadroidRough;
    }
}
