using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Mechadroids
{
    [DefOf]
    public static class MechadroidsDefOf
    {
        static MechadroidsDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MechadroidsDefOf));
        }

        public static ThingDef Asimov_Chargepack;
    }
}
