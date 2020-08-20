using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace O21Mechadroids
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony O21Mechadroids = new Harmony("com.neronix17.o21mechadroids.rimworld.mod");

            O21Mechadroids.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(Thing), "ButcherProducts")]
    static class Thing_ButcherProducts
    {

        [HarmonyPostfix]
        static void Postfix(Thing __instance, ref IEnumerable<Thing> __result, float efficiency)
        {
            if (__instance is Pawn && ((Pawn)__instance).RaceProps.IsMechanoid)
            {
                Pawn pawn = __instance as Pawn;
                __result = GenerateExtraProducts(__result, pawn, efficiency);
            }
        }

        private static IEnumerable<Thing> GenerateExtraProducts(IEnumerable<Thing> things, Pawn pawn, float efficiency)
        {
            foreach (Thing thing in things)
            {
                yield return thing;
            }

            float baseSpawnRateParts = pawn.BodySize * 12;
            float baseSpawnRateCells = pawn.BodySize * 4;
            float baseSpawnRateComponents = pawn.BodySize * 1;

            int partsCount = GenMath.RoundRandom((float)baseSpawnRateParts * efficiency);
            if (partsCount > 0)
            {
                Thing parts = ThingMaker.MakeThing(MechadroidsDefOf.O21_MechanoidParts, null);
                parts.stackCount = partsCount;
                yield return parts;
            }
            int cellCount = GenMath.RoundRandom((float)baseSpawnRateCells * efficiency);
            if (cellCount > 0)
            {
                Thing chips = ThingMaker.MakeThing(MechadroidsDefOf.O21_MechanoidPowerCell, null);
                chips.stackCount = cellCount;
                yield return chips;
            }
            int componentCount = GenMath.RoundRandom((float)baseSpawnRateComponents * efficiency);
            if (componentCount > 0)
            {
                Thing components = ThingMaker.MakeThing(ThingDefOf.ComponentSpacer, null);
                components.stackCount = componentCount;
                yield return components;
            }
            if (UnityEngine.Random.Range(0f, 1f) >= 0.92f)
            {
                Thing personaCore = ThingMaker.MakeThing(ThingDefOf.AIPersonaCore, null);
                yield return personaCore;
            }
        }
    }
}
