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
using testMod1.Content.Items.Materials;
using testMod1.Content.Items.Misc;
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class SnapfireShotgun : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.GetInstance<RangedRarity2>().Type;
            Item.UseSound = SoundID.Item36; 

            Item.shoot = ModContent.ProjectileType<FireballProjectile>(); 
            Item.shootSpeed = 10f;
            Item.useAmmo = ModContent.ItemType<Fireball>(); 
            Item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(0, 0);


        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProjectiles = 3;

            float spread = MathHelper.ToRadians(15f); 

            for (int i = 0; i < numProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(spread);

                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Shotgun)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ModContent.ItemType<RawFury>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

