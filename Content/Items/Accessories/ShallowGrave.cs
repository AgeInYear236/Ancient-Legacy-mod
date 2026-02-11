using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Buffs;
using testMod1.Content.Items.Materials;

namespace testMod1.Content.Items.Accessories
{
    public class ShallowGrave : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityPas>().Type;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ShallowGravePlayer>().hasShallowGrave = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrossNecklace, 1);
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<MagicEnergy>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 40);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
            recipe.AddIngredient(ItemID.DarkShard, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    public class ShallowGravePlayer : ModPlayer
    {
        public bool hasShallowGrave;

        public override void ResetEffects()
        {
            hasShallowGrave = false;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (hasShallowGrave && !Player.HasBuff(ModContent.BuffType<ShallowGraveCooldownBuff>()))
            {

                Player.AddBuff(ModContent.BuffType<ShallowGraveBuff>(), 300);

                Player.AddBuff(ModContent.BuffType<ShallowGraveCooldownBuff>(), 3600);

                Player.statLife = 1;

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.HeartCrystal);
                }

                Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath2, Player.Center);

                return false;
            }
            return true;
        }
    }
}
