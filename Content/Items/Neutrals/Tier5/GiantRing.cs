using AncientLegacyMod.Common.Rarity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Neutrals.Tier5
{
    public class GiantRing : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ModContent.GetInstance<NeutralRarity>().Type;
            Item.value = Item.sellPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 100;
            player.GetDamage(DamageClass.Generic) += 0.25f;
            player.statDefense += 15;

            player.GetAttackSpeed(DamageClass.Generic) -= 0.20f;
            player.moveSpeed -= 0.15f;

            player.GetModPlayer<GiantPlayer>().hasGiantRing = true;
        }
    }

    public class GiantPlayer : ModPlayer
    {
        public bool hasGiantRing;

        public override void ResetEffects()
        {
            hasGiantRing = false;
        }

        private bool _wasInAir;

        public override void PostUpdate()
        {
            if (!hasGiantRing) return;

            bool isInAir = Player.velocity.Y != 0 && !Player.controlJump;

            if (_wasInAir && Player.velocity.Y == 0 && !Player.mount.Active)
            {
                SpawnLandingEffects();
            }

            _wasInAir = isInAir;
        }

        private void SpawnLandingEffects()
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.6f, Pitch = -0.7f }, Player.Center);

            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustDirect(Player.Bottom, 0, 0, DustID.Smoke, 0, 0, 100, default, 1.5f);
                d.velocity = new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-1f, -3f));
            }

/*            if (Player.whoAmI == Main.myPlayer)
            {
                var punch = new Terraria.Graphics.CameraModifiers.PunchCameraModifier(Player.Center, new Vector2(0, 1), 10f, 6f, 15);
                Main.instance.CameraModifiers.Add(punch);
            }*/
        }
    }
}
