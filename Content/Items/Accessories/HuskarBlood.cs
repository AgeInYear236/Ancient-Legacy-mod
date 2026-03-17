using AncientLegacyMod.Common.Rarity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stubble.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class HuskarBlood : ModItem
    {
        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 24;
            Item.height = 26;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityStats>().Type;
        }

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(15, 3));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            int maxLife = player.statLifeMax2;
            int currentLife = player.statLife;
            float missingHealthPercent = 0f;

            if (maxLife > 0)
            {
                missingHealthPercent = 1f - (float)currentLife / maxLife;
            }
            else
            {
                missingHealthPercent = 0f;
            }

            missingHealthPercent = MathHelper.Clamp(missingHealthPercent, 0f, 1f);

            player.GetAttackSpeed(DamageClass.Generic) += missingHealthPercent * 0.3f;
            player.GetDamage(DamageClass.Generic) += missingHealthPercent * 0.3f;
            
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            Rectangle customFrame = frame;
            customFrame.Height += 1;
            spriteBatch.Draw(texture, position, customFrame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            Rectangle frame = Main.itemAnimations[Item.type].GetFrame(texture);
            frame.Height += 1;
            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height / 2);
            spriteBatch.Draw(texture, position, frame, lightColor, rotation, frame.Size() / 2, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
