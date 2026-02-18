using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Items.Materials;

namespace testMod1.Content.Items.Accessories
{
    public class BladeMail : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityPas>().Type;
            Item.value = Item.sellPrice(gold: 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<BasicModPlayer>();
            modPlayer.hasBladeMail = true;
            player.statDefense += 5;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Chainmail>(), 5)
                .AddIngredient(ModContent.ItemType<Madstone>(), 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}