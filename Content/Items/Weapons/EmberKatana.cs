using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using AncientLegacyMod.Content.Items.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class EmberKatana : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ModContent.GetInstance<MeleeRarity3>().Type;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = false;
            Item.shoot = ProjectileID.None;
            Item.UseSound = SoundID.Item1;

            if (player.altFunctionUse == 2)
            {
                Item.useTime = 35;
                Item.useAnimation = 35;
                Item.useStyle = ItemUseStyleID.Swing; 
                Item.noMelee = true; 
                Item.shoot = ModContent.ProjectileType<EmberSlash>();
                Item.shootSpeed = 0.1f;
                Item.UseSound = SoundID.Item45;
            }

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, damage * 2, knockback, player.whoAmI);
                return false;
            }
            return true;
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
            }

            Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);
            player.itemRotation = dir.ToRotation();
            if (player.direction == -1) player.itemRotation += MathHelper.Pi;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Katana, 1);
            recipe.AddIngredient(ItemID.Obsidian, 30);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient(ModContent.ItemType<MagicEnergy>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}