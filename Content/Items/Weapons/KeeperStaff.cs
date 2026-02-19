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
using AncientLegacyMod.Content.Items.Projectiles;

namespace AncientLegacyMod.Content.Items.Weapons
{
    public class KeeperStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ModContent.GetInstance<MagicRarity>().Type;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.channel = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.statMana < 40)
                {
                    return false; 
                }

                Vector2 dirToMouse = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item12);

                player.GetModPlayer<modPlayer1>().LaunchOrbs(dirToMouse);
                player.statMana -= 40;
                return true;
            }
            else
            {
                modPlayer1 keeper = player.GetModPlayer<modPlayer1>();
                if (keeper.orbOffsets.Count < 20 && player.statMana >= 10)
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item8);
                    float radius = Main.rand.NextFloat(40f, 70f);
                    float angle = Main.rand.NextFloat(MathHelper.TwoPi);
                    Vector2 offset = new Vector2(
                        (float)System.Math.Cos(angle),
                        (float)System.Math.Sin(angle)
                    ) * radius;
                    keeper.orbOffsets.Add(offset);
                    player.statMana -= 20;
                }
                return true;
            }
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.ChangeDir(Main.MouseWorld.X < player.Center.X ? -1 : 1);
            }

            Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX);
            player.itemRotation = dir.ToRotation();
            if (player.direction == -1) player.itemRotation += MathHelper.Pi;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.PixieDust, 8);
            recipe.AddIngredient(ItemID.LightShard, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
