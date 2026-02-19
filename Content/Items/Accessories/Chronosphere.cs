using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Buffs;
using AncientLegacyMod.Content.Items.Materials;
using AncientLegacyMod.Content.Items.Projectiles;

    namespace AncientLegacyMod.Content.Items.Accessories
    {
        public class Chronosphere : ModItem
        {
            public override void SetDefaults()
            {
                Item.width = 32;
                Item.height = 32;
                Item.accessory = true;
                Item.rare = ModContent.GetInstance<EndgameRarity>().Type;
                Item.value = Item.sellPrice(gold: 5);
            }

            public override void UpdateAccessory(Player player, bool hideVisual)
            {
                player.GetModPlayer<BasicModPlayer>().hasChronosphere = true;
            }

            public override void AddRecipes()
            {
                CreateRecipe()
                    .AddIngredient(ModContent.ItemType<EnchantedPrism>(), 5)
                    .AddIngredient(ModContent.ItemType<Madstone>(), 99)
                    .AddTile(TileID.LunarCraftingStation)
                    .Register();
            }
        }
    }
