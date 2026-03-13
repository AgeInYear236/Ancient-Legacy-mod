using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using AncientLegacyMod.Content.Items.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class FractureWhip : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<FractureWhipProjectile>(), 75, 2f, 12f);
            Item.rare = ModContent.GetInstance<SummonRarity>().Type;
            Item.value = Item.sellPrice(gold: 3);
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Boulder, 5);
            recipe.AddIngredient(ItemID.Seed, 50);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient(ModContent.ItemType<MagicEnergy>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}