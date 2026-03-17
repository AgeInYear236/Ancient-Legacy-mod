using AncientLegacyMod.Common.Rarity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Neutrals.Tier4
{
    public class OutworldStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<NeutralRarity>().Type;
            Item.value = Item.sellPrice(gold: 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<OutworldPlayer>().hasStaff = true;
        }
    }

    public class OutworldPlayer : ModPlayer
    {
        public bool hasStaff;

        public override void ResetEffects()
        {
            hasStaff = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (hasStaff)
            {
                if (Main.rand.NextBool(10))
                {
                    TriggerOutworldShockwave();
                }
            }
        }

        private void TriggerOutworldShockwave()
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item62, Player.Center);

            float shockwaveRadius = 250f;
            float knockbackStrength = 18f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && !npc.friendly && npc.damage > 0 &&
                    Vector2.Distance(Player.Center, npc.Center) < shockwaveRadius)
                {
                    Vector2 awayFromPlayer = npc.Center - Player.Center;
                    awayFromPlayer.Normalize();
                    awayFromPlayer.Y *= 0.3f;
                    if (awayFromPlayer.Y < -0.5f) awayFromPlayer.Y = -0.5f;
                    npc.velocity = awayFromPlayer * knockbackStrength;
                    npc.netUpdate = true;
                }
            }

            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(15, 15);
                int dustType = Main.rand.NextBool() ? DustID.TerraBlade : DustID.MagicMirror;
                Dust d = Dust.NewDustDirect(Player.Center, 0, 0, dustType, speed.X, speed.Y, 100, default, 1.8f);
                d.noGravity = true;
            }
        }
    }
}
