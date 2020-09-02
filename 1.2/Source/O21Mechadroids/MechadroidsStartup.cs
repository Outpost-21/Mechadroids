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
    [StaticConstructorOnStartup]
    public static class MechadroidsStartup
    {
        static MechadroidsStartup()
        {
            MechadroidsSettings settings = MechadroidsMod.mod.settings;

            if (!settings.faction_mechadroidsCivil)
            {
                MechadroidsDefOf.O21_MechadroidCivil.requiredCountAtGameStart = 0;
                MechadroidsDefOf.O21_MechadroidCivil.maxCountAtGameStart = 0;
            }
            if (!settings.faction_mechadroidsRough)
            {
                MechadroidsDefOf.O21_MechadroidRough.requiredCountAtGameStart = 0;
                MechadroidsDefOf.O21_MechadroidRough.maxCountAtGameStart = 0;
            }
        }
    }
}
