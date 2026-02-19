using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Buffs;
using AncientLegacyMod.Content.Items.Materials;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class TAShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24; Item.height = 24;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityAct>().Type;
            Item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<BasicModPlayer>();
            modPlayer.hasTAShield = true;
            player.statLifeMax2 -= 100;
        }
public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltShield, 1);
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<MagicEnergy>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 40);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ItemID.LightShard, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
