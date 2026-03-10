using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ssl : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 20, 0);
            Item.rare = ModContent.GetInstance<MagicRarity2>().Type;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
            player.maxRunSpeed += 2f; 
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 6);
            recipe.AddIngredient(ItemID.Star, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}