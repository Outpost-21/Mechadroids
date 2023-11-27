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
    public class MechadroidsSettings : ModSettings
    {
        public bool verboseLogging = false;

        public int baseSpawnRateComponents = 3;
        public int baseSpawnChancePersonaCore = 1;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref baseSpawnRateComponents, "baseSpawnRateComponents", 3);
            Scribe_Values.Look(ref baseSpawnChancePersonaCore, "baseSpawnChancePersonaCore", 1);
        }

        public bool IsValidSetting(string input)
        {
            if (GetType().GetFields().Where(p => p.FieldType == typeof(bool)).Any(i => i.Name == input))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }
    }
}
