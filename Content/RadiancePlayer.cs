using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Players
{
    public class RadiancePlayer : ModPlayer
    {
        public bool radianceActive = false;
        public float radianceRadius = 0f;
        public int damage = 0;
        private int radianceTimer = 0;

        public override void ResetEffects()
        {
            radianceActive = false;
        }

        public override void PostUpdate()
        {
            if (!radianceActive)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            radianceTimer++;

            if (radianceTimer >= 30)
            {
                radianceTimer = 0;

                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (!npc.CanBeChasedBy()) 
                        continue;

                    if (Vector2.Distance(Player.Center, npc.Center) <= radianceRadius)
                    {
                        npc.SimpleStrikeNPC(damage, hitDirection: 0);

                        npc.AddBuff(BuffID.OnFire, 180);

                        if (Main.rand.NextBool(3))
                        {
                            Dust.NewDust(npc.position, npc.width, npc.height, DustID.FlameBurst, 0f, 0f, 100, default, 1.2f);
                        }
                    }
                }
            }




            if (Player.whoAmI == Main.myPlayer && Main.rand.NextBool(2))
            {

                for (int i = 0; i < 3; i++)
                {
                    float angle = Main.rand.NextFloat(0, MathHelper.TwoPi);
                    Vector2 spawnPosition = Player.Center + new Vector2(radianceRadius, 0).RotatedBy(angle);
                    Dust dust = Dust.NewDustPerfect(spawnPosition, DustID.FlameBurst, Vector2.Zero, 100, default, 1.5f);
                    dust.noGravity = true; 
                }

                Vector2 spawnPos = Player.Center + Main.rand.NextVector2Circular(radianceRadius, radianceRadius);
                Dust dust2 = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                dust2.velocity *= 0.5f;
                dust2.noGravity = true;
            }
        }
    }
}