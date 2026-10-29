using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using HarmonyLib;

namespace Mechadroids
{

    [HarmonyPatch(typeof(Thing), "ButcherProducts")]
    public static class Patch_Thing_ButcherProducts
    {

        [HarmonyPostfix]
        public static void Postfix(Thing __instance, ref IEnumerable<Thing> __result, float efficiency)
        {
            if (__instance is Pawn pawn && pawn.def.defName.Contains("Mechadroid"))
            {
                __result = GenerateExtraProducts(__result, pawn, efficiency);
            }
        }

        public static IEnumerable<Thing> GenerateExtraProducts(IEnumerable<Thing> things, Pawn pawn, float efficiency)
        {
            foreach (Thing thing in things)
            {
                yield return thing;
            }

            float baseSpawnRateComponents = pawn.BodySize * MechadroidsMod.settings.baseSpawnRateComponents;
            float personaCoreChance = ((float)MechadroidsMod.settings.baseSpawnChancePersonaCore / 100f);

            int componentCount = GenMath.RoundRandom((float)baseSpawnRateComponents * efficiency);
            if (componentCount > 0)
            {
                Thing components = ThingMaker.MakeThing(ThingDefOf.ComponentSpacer, null);
                components.stackCount = componentCount;
                yield return components;
            }
            if (UnityEngine.Random.Range(0f, 1f) <= personaCoreChance)
            {
                Thing personaCore = ThingMaker.MakeThing(ThingDefOf.AIPersonaCore, null);
                yield return personaCore;
            }
        }
    }
}
