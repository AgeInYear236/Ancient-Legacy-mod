using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace testMod1.Common.Systems
{
    public class BurnGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool hasBurnDebuff = false;
        public int burnDamage = 0;

        public override void ResetEffects(NPC npc)
        {
            hasBurnDebuff = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (hasBurnDebuff)
            {
                if (npc.lifeRegen > 0) npc.lifeRegen = 0;

                npc.lifeRegen -= burnDamage * 2;

                damage = burnDamage / 5;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (hasBurnDebuff && Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 6, 0f, 0f, 100, default, 1.5f);
                dust.noGravity = true;
            }
        }
    }
}
