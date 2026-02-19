using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content
{
    public class DesolatorPlayer : ModPlayer
    {
        public bool hasDesolator = false;
        public int armorReduction = 0;

        public override void ResetEffects()
        {
            hasDesolator = false;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasDesolator && !target.boss)
            {
                ApplyDesolatorDebuff(target);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasDesolator)
            {
                ApplyDesolatorDebuff(target);
            }
        }

        private void ApplyDesolatorDebuff(NPC target)
        {
            if(target.defense - armorReduction > 1)
            {
                target.defense -= armorReduction;
            }


            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.LifeDrain, 0, 0, 100, default, 1.5f);
            }
        }
    }
}
