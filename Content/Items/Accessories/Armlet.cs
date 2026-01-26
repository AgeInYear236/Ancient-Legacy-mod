using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Accessories
{
    public class Armlet : ModItem
    {
        public override string Texture => "testMod1/Content/Items/Accessories/ArmletOff";
        public int lifeDrainTimer = 0;
        public static bool isActive = false;

        public override void SetDefaults() {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture;
            if (!isActive)
            {
                texture = ModContent.Request<Texture2D>("testMod1/Content/Items/Accessories/ArmletOff").Value;
            }
            else
            {
                texture = ModContent.Request<Texture2D>("testMod1/Content/Items/Accessories/ArmletOn").Value;
            }

            spriteBatch.Draw(texture, position, null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!isActive) return;

            player.AddBuff(ModContent.BuffType<ArmletBuff>(), 2);

            float damageBonus = 0.20f;
            float defenseBonus = 10f;

            int originalMaxLife = player.statLifeMax;

            lifeDrainTimer++;
            if (lifeDrainTimer >= 50)
            {
                if (player.statLife > 15)
                {
                    player.statLife -= 1;
                }
                lifeDrainTimer = 0;
            }

            if (player.statLife < originalMaxLife / 2)
            {
                damageBonus *= 2f; // 0.40
                defenseBonus *= 2f; // 20

                if (Main.netMode != NetmodeID.Server && Main.rand.NextBool(10))
                {
                    Vector2 position = player.position + new Vector2(player.width / 2f, player.height - 18f);
                    Dust blood = Dust.NewDustDirect(position, 0, 0, DustID.Blood, 0f, -1f, 100, default, 1.5f);
                    blood.noGravity = false;
                    blood.velocity *= 0.3f;
                }
            }

            player.GetDamage(DamageClass.Generic) += damageBonus;
            player.statDefense += (int)defenseBonus;
        }
    }
}
