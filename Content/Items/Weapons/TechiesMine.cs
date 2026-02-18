using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;

namespace testMod1.Content.Items.Weapons
{
    public class TechiesMine : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.crit = 0;
            Item.DamageType = DamageClass.Generic;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ModContent.RarityType<CoolStuffRarity>();
            Item.shoot = ModContent.ProjectileType<Projectiles.TechiesMineProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.shoot = ProjectileID.None;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = ModContent.ProjectileType<Projectiles.TechiesMineProjectile>();

                int mineCount = 0;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == Item.shoot && p.owner == player.whoAmI)
                    {
                        mineCount++;
                    }
                }
                if (mineCount >= 5) return false; 
            }
            return true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == ModContent.ProjectileType<Projectiles.TechiesMineProjectile>() && p.owner == player.whoAmI)
                    {
                        p.Kill();
                    }
                }
                return true;
            }
            return base.UseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = player.Center;
            velocity = new Vector2(0, 2f);
        }
    }
}
