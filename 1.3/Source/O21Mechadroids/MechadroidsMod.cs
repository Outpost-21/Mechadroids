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
    public class MechadroidsMod : Mod
    {
        public MechadroidsSettings settings;
        public static MechadroidsMod mod;

        public MechadroidsMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<MechadroidsSettings>();
            mod = this;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.GapLine();

            listingStandard.Label("Mechanoid/Mechanoid Drop Rates");
            listingStandard.Label("These values are the base values used to determine how much any given mechanoid or mechadroid drops. Changing these will change the intended balance, I'm not going to stop you (I'm obviously helping at this point), but if you have ANY issues from this it's your own fault.");
            string partsBufferString = settings.baseSpawnRateParts.ToString();
            listingStandard.TextFieldNumericLabeled("Mechanoid Parts", ref settings.baseSpawnRateParts, ref partsBufferString, 0, 500);
            string cellsBufferString = settings.baseSpawnRateCells.ToString();
            listingStandard.TextFieldNumericLabeled("Power Cells", ref settings.baseSpawnRateCells, ref cellsBufferString, 0, 500);
            string componentsBufferString = settings.baseSpawnRateComponents.ToString();
            listingStandard.TextFieldNumericLabeled("Advanced Components", ref settings.baseSpawnRateComponents, ref componentsBufferString, 0, 500);
            string personaBufferString = settings.baseSpawnChancePersonaCore.ToString();
            listingStandard.TextFieldNumericLabeled("Percentage Chance of an AI Persona Core", ref settings.baseSpawnChancePersonaCore, ref personaBufferString, 0, 100);

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Mechadroids";
        }
    }

    public class MechadroidsSettings : ModSettings
    {
        public bool faction_mechadroidsCivil = true;
        public bool faction_mechadroidsRough = true;

        public int baseSpawnRateParts = 12;
        public int baseSpawnRateCells = 4;
        public int baseSpawnRateComponents = 1;
        public int baseSpawnChancePersonaCore = 3;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref baseSpawnRateParts, "baseSpawnRateParts", 12);
            Scribe_Values.Look(ref baseSpawnRateCells, "baseSpawnRateCells", 4);
            Scribe_Values.Look(ref baseSpawnRateComponents, "baseSpawnRateComponents", 1);
            Scribe_Values.Look(ref baseSpawnChancePersonaCore, "baseSpawnChancePersonaCore", 3);
            Scribe_Values.Look(ref faction_mechadroidsCivil, "faction_mechadroidsCivil", true);
            Scribe_Values.Look(ref faction_mechadroidsRough, "faction_mechadroidsRough", true);
        }
    }
}
