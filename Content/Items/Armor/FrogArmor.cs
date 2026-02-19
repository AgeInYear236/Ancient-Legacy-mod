using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Materials;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FrogArmor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<AncientMoss>(), 10);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}