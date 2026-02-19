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

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class CKSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ModContent.GetInstance<MeleeRarity3>().Type;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.damage = 80;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage.Flat += Main.rand.Next(-21, 31);

            if (Main.rand.NextFloat() < 0.15f)
            {
                float extraCrit = Main.rand.NextFloat(0.2f, 2.0f);
                modifiers.CritDamage += extraCrit;

                modifiers.SetCrit();

                string critText = $"{(int)(extraCrit * 100)}% CRIT!";
                CombatText.NewText(player.getRect(), Color.OrangeRed, critText, true);

            }
            else
            {
                modifiers.DisableCrit();
            }

            if (Main.rand.NextFloat() < 0.15f)
            {
                int healAmount = Main.rand.Next(2, 11);
                player.statLife += healAmount;
                player.HealEffect(healAmount);

                string healText = $"{(int)(healAmount)} HP!";
                CombatText.NewText(player.getRect(), Color.Red, healText, true);
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
