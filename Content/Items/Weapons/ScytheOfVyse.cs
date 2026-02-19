using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Misc;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class ScytheOfVyse : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ModContent.GetInstance<EndgameRarity>().Type;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.mana = 100;
        }
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ShardOfEternity>()] = ModContent.ItemType<ScytheOfVyse>();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.boss || target.friendly || target.lifeMax <= 5 || target.type == NPCID.TargetDummy)
            {
                return;
            }

            int roll = Main.rand.Next(1, 101);

            if (roll == 1)
            {
                player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + "Mods.AncientLegacyMod.Death.Hex"), 666, 0);

                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Blood, 0, -2f, 0, Color.Red, 2f);
                }
            }
            else
            {
                TransformIntoAnimal(target);
            }
        }

        private void TransformIntoAnimal(NPC target)
        {
            int[] animals = { NPCID.Bunny, NPCID.Frog, NPCID.Bird, NPCID.Butterfly, NPCID.Squirrel, NPCID.Goldfish };
            int randomAnimal = Main.rand.Next(animals);

            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(4f, 9f);
                Dust d = Dust.NewDustPerfect(target.Center, DustID.ManaRegeneration, speed, 100, Color.DarkBlue, 2f);
                d.noGravity = true;
                d.fadeIn = 1.2f;
            }

            for (int i = 0; i < 15; i++)
            {
                Dust dSmoke = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Smoke, 0, 0, 150, Color.White, 1.5f);
                dSmoke.velocity *= 0.5f;
            }

            for (int i = 0; i < 10; i++)
            {
                Dust dStars = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Enchanted_Pink, 0, 0, 100, Color.Pink, 1f);
                dStars.velocity *= 3f;
                dStars.noGravity = true;
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item65, target.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(target.GetSource_FromAI(), (int)target.Center.X, (int)target.Center.Y, randomAnimal);
                target.active = false;
                target.netUpdate = true;
            }
        }
    }
}