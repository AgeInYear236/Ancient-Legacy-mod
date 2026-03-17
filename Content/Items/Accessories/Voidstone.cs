using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Buffs;
using AncientLegacyMod.Content.Items.Materials;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class Voidstone : ModItem
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
            player.GetDamage(DamageClass.Magic) += 0.1f;
            player.manaRegen += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 40);
            recipe.AddIngredient(ModContent.ItemType<VoidFragment>(), 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
    }
}
