using AncientLegacyMod.Common.Rarity;
using AncientLegacyMod.Content.Items.Materials;
using AncientLegacyMod.Content.Items.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Accessories
{
    public class Vessel : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<MiscItemRarity>().Type;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<VesselPlayer>().hasVesselAcc = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer.GetModPlayer<VesselPlayer>();
            var line = new TooltipLine(Mod, "SoulCount", Language.GetTextValue("Mods.AncientLegacyMod.Misc.Vessel") + $"{player.soulCharges}/{VesselPlayer.MaxSouls}");
            line.OverrideColor = new Color(81, 174, 99);
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Madstone>(), 50);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 1);
            recipe.AddIngredient(ItemID.SoulofFright, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 1);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddTile(TileID.ClayPot);
            recipe.AddCondition(Condition.NearShimmer);
            recipe.Register();
        }
    }

    public class VesselPlayer : ModPlayer
    {
        public bool hasVesselAcc = false;
        public int soulCharges = 0;
        public const int MaxSouls = 10;

        public override void ResetEffects()
        {
            hasVesselAcc = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasVesselAcc && target.life <= 0 && !target.friendly && target.lifeMax > 5)
            {
                if (soulCharges < MaxSouls)
                {
                    soulCharges++;
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasVesselAcc && target.life <= 0 && !target.friendly && target.lifeMax > 5)
            {
                if (soulCharges < MaxSouls)
                {
                    soulCharges++;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasVesselAcc && soulCharges > 0 && Main.rand.NextFloat() < 0.10f)
            {
                soulCharges--;
                if (Main.myPlayer == Player.whoAmI)
                {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), Player.Center, new Vector2(Main.rand.Next(-3, 4), -5),
                        ModContent.ProjectileType<VesselProjectile>(), 20, 2f, Player.whoAmI);
                }
            }
        }
    }
}
