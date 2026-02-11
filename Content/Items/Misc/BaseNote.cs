using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace testMod1.Content.Items.Misc
{
    public abstract class BaseNote : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float dist = Vector2.Distance(player.Center, Projectile.Center);
            if (dist < 150f)
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 8f;
            }
            else
            {
                Projectile.velocity *= 0.65f;
            }

            // Визуальный эффект (искры)
            if (Main.rand.NextBool(5))
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Flare);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) { }

        public override bool? CanCutTiles() => false;

        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                OnPickup(player);
                Projectile.Kill();
            }
        }

        public abstract void OnPickup(Player player);
    }

    public class Note1 : BaseNote
    {
        public override void OnPickup(Player player) => player.AddBuff(ModContent.BuffType<Buffs.NoteMoveBuff>(), 480);
    }
    public class Note2 : BaseNote
    {
        public override void OnPickup(Player player) => player.AddBuff(ModContent.BuffType<Buffs.NoteDamageBuff>(), 480);
    }
    public class Note3 : BaseNote
    {
        public override void OnPickup(Player player) => player.AddBuff(ModContent.BuffType<Buffs.NoteSpeedBuff>(), 480);
    }
}