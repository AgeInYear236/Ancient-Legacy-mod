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
using Terraria.Map;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class vsh : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 12;
            Item.rare = ItemRarityID.Purple;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.20f;
            player.GetCritChance(DamageClass.Ranged) += 15;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<vsb>() &&
                   legs.type == ModContent.ItemType<vsl>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AncientLegacyMod.Items.ItemSetBonus.vsh");

            player.GetModPlayer<VoidPlayer>().hasVoidSet = true;
            player.armorEffectDrawShadowSubtle = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 6);
            recipe.AddIngredient(ModContent.ItemType<VoidFragment>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    public class VoidPlayer : ModPlayer
    {
        public bool hasVoidSet;
        public int remnantCooldown = 0;
        public const int MaxCooldown = 600;

        public override void ResetEffects()
        {
            hasVoidSet = false;
        }

        public override void PostUpdate()
        {
            if (remnantCooldown > 0)
            {
                remnantCooldown--;
            }
        }

        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            if (hasVoidSet && AncientLegacyMod.remnantKeybind.JustPressed)
            {
                if (remnantCooldown > 0)
                {
                    return;
                }

                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<VoidRemnant>(), 0, 0, Player.whoAmI);
                remnantCooldown = MaxCooldown;
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.Demonite);
                }

                Player.AddBuff(BuffID.Invisibility, 60);

                Player.aggro -= 2000;

                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item103, Player.Center);
            }
        }
    }
}
