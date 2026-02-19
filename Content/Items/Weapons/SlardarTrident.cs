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

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class SlardarTrident : ModItem
    {
        public int attackNumber = 0;
        public override void SetDefaults()
        {
            Item.damage = 80;

            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ModContent.GetInstance<MagicRarity3>().Type;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            var modPlayer = player.GetModPlayer<modPlayer1>();
            modPlayer.bachAccEquipped = false;
            attackNumber++;
            if (attackNumber == 4)// && !modPlayer.bachAccEquipped)
            {
                if (!player.HasBuff(ModContent.BuffType<MagnusCooldown>()))
                {
                    attackNumber = 0;
                    target.AddBuff(ModContent.BuffType<StunBuff>(), 60);
                    player.AddBuff(ModContent.BuffType<MagnusCooldown>(), 300);
                    CombatText.NewText(player.getRect(), Color.DarkSlateGray, "Bash!", true);
                }
                else
                {
                    attackNumber = 0;
                }
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

    }
}
