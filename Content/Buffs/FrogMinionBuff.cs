using Terraria;
using Terraria.ModLoader;

namespace AncientLegacyMod.Content.Buffs
{
    public class FrogMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Misc.FrogMinion>()] <= 0)
            {
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<Items.Misc.FrogMinion>(), 20, 1f, player.whoAmI);
            }
        }
    }
}