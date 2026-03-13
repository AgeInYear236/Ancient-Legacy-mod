using AncientLegacyMod.Content.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class esb : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 26;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 50;
            player.GetDamage(DamageClass.Summon) += 0.12f;
            player.whipRangeMultiplier += 0.2f;
            player.endurance += 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 10);
            recipe.AddIngredient(ItemID.StoneBlock, 300);
            recipe.AddIngredient(ItemID.DirtBlock, 500);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
