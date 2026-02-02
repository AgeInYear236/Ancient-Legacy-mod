using Microsoft.Xna.Framework;
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
using testMod1.Content.Items.Projectiles;

namespace testMod1.Content.Items.Weapons
{
    public class PADagger : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ModContent.GetInstance<MeleeRarity2>().Type;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.LocalPlayer.HasItem(Item.type))
            {
                Main.LocalPlayer.GetModPlayer<modPlayer1>().critChanceCounter = 0;
            }
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            modPlayer1 rp = player.GetModPlayer<modPlayer1>();

            int bonusChance = rp.critChanceCounter * 2;
            int totalCritChance = (int)player.GetTotalCritChance(DamageClass.Melee) + bonusChance;

            if (Main.rand.Next(1, 101) <= totalCritChance)
            {
                modifiers.SetCrit();

                float damageMultiplier = 1.5f + (rp.critChanceCounter * 0.5f);
                modifiers.CritDamage *= damageMultiplier;

                int displayPercent = (int)(damageMultiplier * 100);
                CombatText.NewText(target.Hitbox, Color.Gold, $"CRIT {displayPercent}%!", true);

                rp.critChanceCounter = 0;
            }
            else
            {
                rp.critChanceCounter++;

                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Flare, 0, 0, 150);
                d.velocity *= 0.2f;
                d.noGravity = true;
            }
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.HasBuff(ModContent.BuffType<Buffs.NoBloody>()))
                {
                    return false;
                }

                Item.noMelee = true;
                Item.shoot = ModContent.ProjectileType<PABlinkProjectile>();
                Item.shootSpeed = 0f; 
                Item.useTime = 30;
                Item.useAnimation = 30;
            }
            else
            {
                Item.noMelee = false;
                Item.shoot = ProjectileID.None;
                Item.mana = 0;
                Item.useTime = 10;
                Item.useAnimation = 10;
            }
            return base.CanUseItem(player);
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
