using AncientLegacyMod.Common.Rarity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AncientLegacyMod.Content.Items.Accessories.Boots
{
    public class PowerTreads : ModItem
    {
        // 0 - STR, 1 - AGI, 2 - INT
        public int mode = 0;

        private static Asset<Texture2D> textureSTR;
        private static Asset<Texture2D> textureAGI;
        private static Asset<Texture2D> textureINT;

        public override void SetStaticDefaults()
        {
            textureSTR = ModContent.Request<Texture2D>(Texture + "STR");
            textureAGI = ModContent.Request<Texture2D>(Texture + "AGI");
            textureINT = ModContent.Request<Texture2D>(Texture + "INT");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ModContent.GetInstance<MaterialRarity1>().Type;
            Item.accessory = true;
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            mode = (mode + 1) % 3;
            Item.stack++;
            Terraria.Audio.SoundEngine.PlaySound(SoundID.MenuTick);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.2f;
            player.jumpSpeedBoost += 0.2f;

            switch (mode)
            {
                case 0: // STR
                    player.statLifeMax2 += 20;
                    player.statDefense += 5;
                    break;
                case 1: // AGI
                    player.GetDamage(DamageClass.Generic) += 0.05f;
                    player.moveSpeed += 0.10f;
                    break;
                case 2: // INT
                    player.statManaMax2 += 40;
                    player.manaCost -= 0.10f;
                    break;
            }
        }

        private Texture2D GetCurrentTexture()
        {
            return mode switch
            {
                0 => textureSTR.Value,
                1 => textureAGI.Value,
                2 => textureINT.Value,
                _ => textureSTR.Value
            };
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = GetCurrentTexture();
            spriteBatch.Draw(texture, position, null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = GetCurrentTexture();
            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height / 2);
            spriteBatch.Draw(texture, position, null, lightColor, rotation, texture.Size() / 2, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void SaveData(TagCompound tag) => tag["mode"] = mode;
        public override void LoadData(TagCompound tag) => mode = tag.GetInt("mode");

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            string modeName = mode == 0 ? Language.GetTextValue("Mods.AncientLegacyMod.Boots.PT.Mode0")
                : mode == 1 ? Language.GetTextValue("Mods.AncientLegacyMod.Boots.PT.Mode1")
                : Language.GetTextValue("Mods.AncientLegacyMod.Boots.PT.Mode2");
            Color modeColor = mode == 0 ? Color.Red : mode == 1 ? Color.Green : Color.Blue;

            var line = new TooltipLine(Mod, "Mode", Language.GetTextValue("Mods.AncientLegacyMod.Boots.PT.ModeText") + $"{modeName}");
            line.OverrideColor = modeColor;
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LeatherBoots>(), 1);
            recipe.AddIngredient(ItemID.BladedGlove, 1);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddIngredient(ItemID.LifeCrystal, 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
    }
}
