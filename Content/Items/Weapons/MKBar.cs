using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Common.Rarity;
using testMod1.Content.Buffs;

namespace testMod1.Content.Items.Weapons
{
    public class MKBar : ModItem
    {
        public int attackNumber = 0;

        public override void SetDefaults() {
            Item.damage = 32;
            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ModContent.GetInstance<MeleeRarity>().Type;
            Item.UseSound = SoundID.Item1;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.HasBuff(ModContent.BuffType<JinguMasteryBuff>()))
            {
                damage *= 1.5f; 
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.HasBuff(ModContent.BuffType<JinguMasteryBuff>()))
            {
                player.HeldItem.scale = 2.5f;
            }
            else
            {
                player.HeldItem.scale = 1.0f;
            }
        }


        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
            }

            Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);

            player.itemRotation = dir.ToRotation() + MathHelper.PiOver4;

            if (player.direction == -1)
            {
                player.itemRotation += MathHelper.PiOver2;
            }
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!player.HasBuff(ModContent.BuffType<JinguMasteryBuff>()))
            {
                attackNumber++;
            }

            if (attackNumber >= 4)
            {
                player.AddBuff(ModContent.BuffType<JinguMasteryBuff>(), 300);
                attackNumber = 0; 

                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item60, player.Center);
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.GoldCoin);
                }
            }
            Main.NewText($"Attack: {attackNumber}");
            Main.NewText($"Hit: {target.FullName} (ID: {target.type})");
            Main.NewText($"Life: {target.life}/{target.lifeMax}");
            Main.NewText($"Defense: {target.defense}");
            Main.NewText($"Damage Done: {damageDone}");
            Main.NewText($"Knockback: {hit.Knockback}");
        }
    }
}
