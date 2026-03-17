using AncientLegacyMod.Common.Rarity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class Flux : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 12));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<AccRarityPas>().Type;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FluxPlayer>().hasFlux = true;
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

    public class FluxPlayer : ModPlayer
    {
        public bool hasFlux;

        public override void ResetEffects()
        {
            hasFlux = false;
        }
    }

    public class FluxGlobalNPC : GlobalNPC
    {
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            ApplyFluxEffect(npc, player);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];
            ApplyFluxEffect(npc, player);
        }

        private void ApplyFluxEffect(NPC npc, Player player)
        {
            if (player.GetModPlayer<FluxPlayer>().hasFlux)
            {
                if (Main.rand.NextBool(10))
                {
                    int duration = 120;

                    npc.AddBuff(BuffID.Confused, duration);
                    npc.AddBuff(BuffID.Slow, duration);

                    for (int i = 0; i < 5; i++)
                    {
                        Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.EnchantedNightcrawler);
                        d.velocity *= 1.5f;
                        d.noGravity = true;
                    }
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(BuffID.Confused))
            {
                if (Main.rand.NextBool(4))
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.EnchantedNightcrawler, 0f, 0f, 150, default, 1.5f);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.5f; 

                    Main.dust[dust].velocity.Y -= 1f;
                }
                drawColor = Color.Lerp(drawColor, Color.MediumPurple, 0.5f);
            }
        }
    }
}