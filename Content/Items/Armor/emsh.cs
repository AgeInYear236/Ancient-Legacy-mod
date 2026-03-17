using AncientLegacyMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class emsh : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 15;
            player.GetDamage(DamageClass.Melee) += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<emsb>() &&
                   legs.type == ModContent.ItemType<emsl>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AncientLegacyMod.Items.ItemSetBonus.emsh");

            player.armorEffectDrawShadow = true;

            player.GetModPlayer<EmberPlayer>().hasSet = true;

            if (Main.rand.NextBool(5))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.CoralTorch, 0, 0, 100, default, 0.6f);
                d.noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<UnstableComponent>(), 8);
            recipe.AddIngredient(ItemID.LivingFireBlock, 75);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class EmberPlayer : ModPlayer
    {
        public bool hasSet;
        public int remnantProjectileID = -1;
        public int remnantCooldown = 0;
        public const int MaxCooldown = 600;

        public override void ResetEffects()
        {
            hasSet = false;
        }

        public override void PostUpdate()
        {
            if (remnantCooldown > 0)
            {
                remnantCooldown--;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            if (hasSet && AncientLegacyMod.remnantKeybind.JustPressed)
            {
                if (remnantCooldown > 0)
                {
                    return;
                }
                if (remnantProjectileID == -1 || !Main.projectile[remnantProjectileID].active || Main.projectile[remnantProjectileID].type != ModContent.ProjectileType<Content.Items.Projectiles.EmberRemnant>())
                {

                    remnantProjectileID = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<Content.Items.Projectiles.EmberRemnant>(), 0, 0, Player.whoAmI);

                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item8, Player.Center);
                }
                else
                {
                    Projectile remnant = Main.projectile[remnantProjectileID];

                    TeleportEffects(Player.Center);

                    Player.Center = remnant.Center;
                    Player.velocity = Vector2.Zero;

                    TeleportEffects(Player.Center);

                    remnant.Kill();
                    remnantProjectileID = -1;
                    remnantCooldown = MaxCooldown;

                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item6, Player.Center);
                }
            }
        }

        private void TeleportEffects(Vector2 position)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustDirect(position - new Vector2(16, 24), 32, 48, DustID.RedTorch, 0, 0, 100, default, 1.2f);
                d.noGravity = true;
                d.velocity *= 2f;
            }
        }
    }
}
