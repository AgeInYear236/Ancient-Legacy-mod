using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Items.Projectiles
{
    public class PugnaLifeDrainProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!owner.controlUseTile || owner.dead || !owner.active)
            {
                Projectile.Kill();
                return;
            }

            owner.itemTime = 2;
            owner.itemAnimation = 2;
            Projectile.timeLeft = 2;

            Vector2 mouseWorld = Main.MouseWorld;
            Entity target = null;

            foreach (Player p in Main.player)
            {
                if (p.active && !p.dead && p != owner && p.Distance(mouseWorld) < 70f)
                {
                    target = p;
                    break;
                }
            }
            if (target == null)
            {
                foreach (NPC n in Main.npc)
                {
                    if (n.active && (n.townNPC || n.friendly) && n.Distance(mouseWorld) < 70f)
                    {
                        target = n;
                        break;
                    }
                }
            }

            if (target != null && owner.statLife > 20)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 pos = Vector2.Lerp(owner.Center, target.Center, Main.rand.NextFloat());
                    Dust d = Dust.NewDustPerfect(pos, DustID.TerraBlade, Vector2.Zero);
                    d.noGravity = true;
                    d.scale = 0.6f + Main.rand.NextFloat(0.4f);
                }

                Projectile.ai[0]++;
                if (Projectile.ai[0] >= 6)
                {
                    Projectile.ai[0] = 0;

                    int healAmount = 4;

                    owner.statLife -= healAmount;
                    CombatText.NewText(owner.getRect(), Color.Red, healAmount.ToString(), false, true);

                    if (target is Player tp)
                    {
                        tp.statLife = (int)MathHelper.Min(tp.statLife + healAmount, tp.statLifeMax2);
                        tp.HealEffect(healAmount);
                    }
                    else if (target is NPC tn)
                    {
                        tn.life = (int)MathHelper.Min(tn.life + healAmount, tn.lifeMax);
                        tn.HealEffect(healAmount);
                    }

                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.PlayerHeal, -1, -1, null, owner.whoAmI);
                    }
                }
            }
        }
    }
}
