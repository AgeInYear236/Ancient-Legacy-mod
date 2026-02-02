using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Buffs
{
    public class ShallowGraveBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.statLife < 1)
            {
                player.statLife = 1;
            }

            player.immune = true;
            player.immuneTime = 2;

            if (Main.rand.NextBool(1))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.PurpleCrystalShard, 0f, 0f, 150, default, 2f);
                d.noGravity = true;
                d.velocity += player.velocity;
            }

            Lighting.AddLight(player.Center, 1f, 0.0f, 0.8f);
        }
    }
}
