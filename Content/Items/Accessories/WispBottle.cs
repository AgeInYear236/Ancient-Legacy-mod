using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Items.Materials;

namespace testMod1.Content.Items.Accessories
{
    public class WispBottle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<MiscRarity>().Type;
            Item.value = Item.sellPrice(0, 15, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            bool isEquipped = false;

            for (int i = 3; i < 10; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<WispBottle>())
                {
                    isEquipped = true;
                    break;
                }
            }

            if (isEquipped)
            {
                if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.WispProjectile>()] <= 0)
                {
                    Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.WispProjectile>(), 0, 0f, player.whoAmI);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ModContent.ItemType<MagicEnergy>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 10);
            recipe.Register();
        }
    }
}