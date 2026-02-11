using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Items.Materials;

namespace testMod1.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FrogLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 20, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
            player.jumpSpeedBoost += 2.5f; 
            player.autoJump = true; 
            player.GetDamage(DamageClass.Melee) += 0.05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<AncientMoss>(), 6);
            recipe.AddIngredient(ItemID.JungleSpores, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}