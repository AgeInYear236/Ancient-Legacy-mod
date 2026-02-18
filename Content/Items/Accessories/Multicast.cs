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

namespace testMod1.Content.Items.Accessories
{
    public class Multicast : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityPas>().Type;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MulticastPlayer>().hasMulticast = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LuckPotionGreater, 10);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    public class MulticastPlayer : ModPlayer
    {
        public bool hasMulticast;

        public override void ResetEffects()
        {
            hasMulticast = false;
        }
    }
}
