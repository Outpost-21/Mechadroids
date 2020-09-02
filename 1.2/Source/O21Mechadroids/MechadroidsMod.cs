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
            listingStandard.Label("Faction Settings - Changing any of these requires a restart to take effect.");
            listingStandard.CheckboxLabeled("Civil Mechadroids", ref settings.faction_mechadroidsCivil, "Controls spawning of the Neutral Mechadroid factions.");
            listingStandard.CheckboxLabeled("Rough Mechadroids", ref settings.faction_mechadroidsRough, "Controls spawning of the Hostile Mechadroid factions.");
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

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref faction_mechadroidsCivil, "faction_mechadroidsCivil", true);
            Scribe_Values.Look(ref faction_mechadroidsRough, "faction_mechadroidsRough", true);
        }
    }
}
