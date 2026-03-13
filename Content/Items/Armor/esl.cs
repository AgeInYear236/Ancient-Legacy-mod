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
    [AutoloadEquip(EquipType.Legs)]
    public class esl : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.15f;
            player.noKnockback = true;
            player.moveSpeed -= 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 6);
            recipe.AddIngredient(ItemID.StoneBlock, 90);
            recipe.AddIngredient(ItemID.DirtBlock, 150);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
