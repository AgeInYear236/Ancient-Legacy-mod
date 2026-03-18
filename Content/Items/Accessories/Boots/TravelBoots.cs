using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Accessories.Boots
{
    public class TravelBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.7f;
            player.jumpSpeedBoost += 1f;
            player.GetModPlayer<TravelBootsPlayer>().hasBoots = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LeatherBoots>(), 1);
            recipe.AddIngredient(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.PotionOfReturn, 4);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
    }

    public class TravelBootsPlayer : ModPlayer
    {
        public bool hasBoots;

        public override void ResetEffects()
        {
            hasBoots = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (AncientLegacyMod.travelKeybind.JustPressed && hasBoots)
            {
                for (int i = 0; i < 40; i++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
                }
                Player.Spawn(PlayerSpawnContext.RecallFromItem);
                for (int i = 0; i < 40; i++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
                }
            }
        }
    }
}
