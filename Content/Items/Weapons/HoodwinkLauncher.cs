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
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class HoodwinkLauncher : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 44;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ModContent.GetInstance<RangedRarity>().Type;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;

            Item.shoot = ModContent.ProjectileType<AcornProjectile>();
            Item.shootSpeed = 9f;

            Item.useAmmo = ItemID.Acorn;

            Item.scale = 1.7f;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-5, 2);


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<AcornProjectile>();
        }
    }
}
