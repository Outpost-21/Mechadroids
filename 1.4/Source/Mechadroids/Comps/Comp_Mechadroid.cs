using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using Asimov;

namespace Mechadroids
{
    public class Comp_Mechadroid : ThingComp
    {
        public CompProperties_Mechadroid Props => (CompProperties_Mechadroid)props;

        public bool painted = false;

        public int ageStage = 0;

        public int ticks = 0;

        public override void CompTick()
        {
            base.CompTick();
            try
            {
                ticks++;
                if (ticks >= (60000 * 7))
                {
                    if (!painted)
                    {
                        switch (ageStage)
                        {
                            case 0:
                                SetFirstAge();
                                break;
                            case 1:
                                SetSecondAge();
                                break;
                            case 2:
                                SetThirdAge();
                                break;
                            case 3:
                                SetFourthAge();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("Something went wrong with the colour ageing of Mechadroids. Exception:" + e);
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach(Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }
            yield return new Command_Action
            {
                defaultLabel = "ShieldGenColorLabel".Translate(),
                defaultDesc = "ShieldGenColorDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Buttons/ShieldColor"),
                action = () => Find.WindowStack.Add(new Popup_ColourPicker(this))
            };
        }

        public void SetSkinColor(Color color, bool painted = false)
        {
            Pawn pawn = parent as Pawn;
            if (pawn != null)
            {
                ageStage++;
                ticks = 0;
                Comp_PawnData comp = pawn.TryGetComp<Comp_PawnData>();
                if (comp != null)
                {
                    comp.skinFirst = color;
                    comp.skinSecond = color;
                    comp.ResolveGraphics();
                    if (painted)
                    {
                        this.painted = true;
                    }
                }
            }
        }

        public Color GetSkinColor()
        {
            Color result = Color.white;
            Pawn pawn = parent as Pawn;
            if (pawn != null)
            {
                Comp_PawnData comp = pawn.TryGetComp<Comp_PawnData>();
                if (comp != null)
                {
                    result = comp.skinFirst ?? Color.white;
                }
            }
            return result;
        }

        public void SetFirstAge()
        {
            SetSkinColor(new Color(235, 236, 233));
        }

        public void SetSecondAge()
        {
            SetSkinColor(new Color(216, 217, 211));
        }

        public void SetThirdAge()
        {
            SetSkinColor(new Color(196, 198, 189));
        }

        public void SetFourthAge()
        {
            SetSkinColor(new Color(176, 179, 167));
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref painted, "painted");
            Scribe_Values.Look(ref ageStage, "ageStage");
        }
    }
}
