using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AncientLegacyMod.Content.Items.Misc
{
    public class Bottle : ModItem
    {
        public int charges = 3;

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.rare = ModContent.GetInstance<MiscItemRarity>().Type;
            Item.value = Item.sellPrice(silver: 30);

            Item.potion = true;
            Item.healLife = 100;
            Item.healMana = 100;

            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            return charges > 0 && !player.HasBuff(BuffID.PotionSickness);
        }

        public override bool? UseItem(Player player)
        {
            if (charges > 0)
            {/*
                player.statLife += 100;
                player.HealEffect(100);

                player.statMana += 100;
                player.ManaEffect(100);*/

                int potionTime = Item.potionDelay;
                if (potionTime <= 0) potionTime = 1800;

                player.AddBuff(BuffID.PotionSickness, potionTime);

                charges--;
                return true;
            }
            return false;
        }

        public override void PostUpdate()
        {
            
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            int frameHeight = texture.Height / 4;
            int currentFrame = 3 - charges;
            Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            spriteBatch.Draw(texture, position, sourceRect, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            int frameHeight = texture.Height / 4;
            int currentFrame = 3 - charges;
            Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height / 2);
            spriteBatch.Draw(texture, position, sourceRect, lightColor, rotation, new Vector2(texture.Width / 2, frameHeight / 2), scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["charges"] = charges;
        }

        public override void LoadData(TagCompound tag)
        {
            charges = tag.GetInt("charges");
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Charges", Language.GetTextValue("Mods.AncientLegacyMod.Misc.Bottle") + $"{charges}/3");
            line.OverrideColor = Color.LightBlue;
            tooltips.Add(line);
        }

        public void ResetCharges()
        {
            charges = 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 50);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddCondition(Condition.NearShimmer);
            recipe.Register();
        }
    }

    public class BottlePlayer : ModPlayer
    {
        public override void OnRespawn()
        {
            RestoreBottleCharges();
        }

        public override void PostUpdate()
        {
            Item heldItem = Player.HeldItem;

            if (Player.itemAnimation > 0 &&
                (heldItem.type == ItemID.MagicMirror ||
                 heldItem.type == ItemID.IceMirror ||
                 heldItem.type == ItemID.CellPhone ||
                 heldItem.type == ItemID.Shellphone ||
                 heldItem.type == ItemID.PotionOfReturn))
            {
                if (Player.itemAnimation == Player.itemAnimationMax - 1)
                {
                    RestoreBottleCharges();
                }
            }
        }

        private void RestoreBottleCharges()
        {
            for (int i = 0; i < 58; i++)
            {
                if (Player.inventory[i].ModItem is Bottle bottle)
                {
                    bottle.ResetCharges();
                }
            }

            if (Main.myPlayer == Player.whoAmI)
            {
                Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana);
            }
        }
    }
}