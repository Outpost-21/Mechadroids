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
    public class CompProperties_Mechadroid : CompProperties
    {
        public CompProperties_Mechadroid() => this.compClass = typeof(Comp_Mechadroid);
    }
}
