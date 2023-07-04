using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Mechadroids
{
    public class MechadroidsMod : Mod
    {
        public static MechadroidsMod mod;
        public static MechadroidsSettings settings;

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public MechadroidsMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<MechadroidsSettings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            LogUtil.LogMessage($"{CurrentVersion} ::");

            File.WriteAllText(VersionDir, CurrentVersion);

            Harmony harmony = new Harmony("Neronix17.Mechadroids.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "Mechadroids";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            bool flag = optionsViewRectHeight > inRect.height;
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(inRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            DoOptionsCategoryContents(listing);
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        public void DoOptionsCategoryContents(Listing_Standard listing)
        {
            listing.Label("Mechadroid / Mechanoid Drop Rates");
            listing.Label("These values are the base values used to determine how much any given mechanoid or mechadroid drops. Changing these will change the intended balance, I'm not going to stop you (I'm obviously helping at this point), but if you have ANY issues from this it's your own fault.");
            string componentsBufferString = settings.baseSpawnRateComponents.ToString();
            listing.TextFieldNumericLabeled("Advanced Components", ref settings.baseSpawnRateComponents, ref componentsBufferString, 0, 500);
            string personaBufferString = settings.baseSpawnChancePersonaCore.ToString();
            listing.TextFieldNumericLabeled("Percentage Chance of an AI Persona Core", ref settings.baseSpawnChancePersonaCore, ref personaBufferString, 0, 100);
        }
    }
}
