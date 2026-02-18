using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;

namespace testMod1.Content.Items.Weapons
{
    public class LightningStrike : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Magic; 
            Item.width = 50;
            Item.height = 50;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ModContent.RarityType<CoolStuffRarity>();
            Item.UseSound = SoundID.Item1; 

            Item.autoReuse = true; 
            Item.mana = 8; 
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Item93, target.Center);

            for (int i = 0; i < 3; i++)
            {
                Vector2 spawnPos = target.Center + new Vector2(Main.rand.Next(-80, 81), -600);

                Vector2 velocity = new Vector2(0, 20f);

                Projectile.NewProjectile(
                    player.GetSource_ItemUse(Item),
                    spawnPos,
                    velocity,
                    ModContent.ProjectileType<Projectiles.ThunderStrikeProjectile>(),
                    (int)(Item.damage * 0.8f),
                    Item.knockBack,
                    player.whoAmI
                );
            }

            for (int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Electric, 0, 0, 100, default, 1.5f);
                d.noGravity = true;
                d.velocity *= 3f;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
            }
        }
    }
}
