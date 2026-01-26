using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace testMod1.Content.Items.Projectiles
{
    public class MagicProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmberBolt);
            // projectile.aiStyle = 3; This line is not needed since CloneDefaults sets it already.
            AIType = ProjectileID.AmberBolt;
            Projectile.velocity = new Vector2(100, 0);
            Projectile.DamageType = DamageClass.Magic;
        }

        // Additional hooks/methods here.
    }
}
