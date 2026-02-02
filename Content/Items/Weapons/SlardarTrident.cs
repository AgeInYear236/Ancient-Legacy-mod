using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testMod1.Content.Buffs;

namespace testMod1.Content.Items.Weapons
{
    public class SlardarTrident : ModItem
    {
        public int attackNumber = 0;
        public override void SetDefaults()
        {
            Item.damage = 35;

            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            var modPlayer = player.GetModPlayer<modPlayer1>();
            modPlayer.bachAccEquipped = false;
            Main.NewText("Bash cant proc");
            if(attackNumber == 4 && !player.HasBuff(ModContent.BuffType<MagnusCooldown>()) && !modPlayer.bachAccEquipped)
            {
                attackNumber = 0;
                target.AddBuff(ModContent.BuffType<StunBuff>(), 60);
                player.AddBuff(ModContent.BuffType<MagnusCooldown>(), 300);
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
            attackNumber++;
        }
    }
}
