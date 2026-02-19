using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;

namespace AncientLegacyMod.Content.Items.Misc
{
    public class Eul : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.mana = 20;
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.rare = ModContent.GetInstance<MiscItemRarity>().Type;
            Item.UseSound = SoundID.Item45;
            Item.shoot = ModContent.ProjectileType<Projectiles.AirWall>();
            Item.shootSpeed = 0f; 
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int maxAirWalls = 2;
            int count = 0;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.type == type && p.owner == player.whoAmI)
                {
                    count++;
                }
            }

            if (count >= maxAirWalls)
            {
                return false;
            }

            Vector2 mousePos = Main.MouseWorld;
            Projectile.NewProjectile(source, mousePos, Vector2.Zero, type, 0, 0, player.whoAmI);

            return false;
        }

        public override bool CanUseItem(Player player)
        {
            int type = ModContent.ProjectileType<Projectiles.AirWall>();
            int count = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type && Main.projectile[i].owner == player.whoAmI)
                {
                    count++;
                }
            }
            return count < 2;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GiantHarpyFeather, 1)
                .AddIngredient(ItemID.Feather, 8)
                .AddIngredient(ItemID.SoulofFlight, 25)
                .AddIngredient(ModContent.ItemType<MagicEnergy>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}