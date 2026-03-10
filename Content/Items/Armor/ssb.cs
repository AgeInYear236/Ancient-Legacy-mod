using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ssb : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 50, 0);
            Item.rare = ModContent.GetInstance<MagicRarity2>().Type;
            Item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.15f;
            player.manaRegenBonus += 25;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 5);
            recipe.AddIngredient(ItemID.Star, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}