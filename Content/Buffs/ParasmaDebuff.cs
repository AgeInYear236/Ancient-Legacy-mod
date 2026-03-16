using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AncientLegacyMod.Content.Buffs
{
    public class ParasmaDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.GreenBlood);
            }
        }
    }

    public class ParasmaGlobalNPC : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<ParasmaDebuff>() && projectile.DamageType == DamageClass.Magic)
            {
                modifiers.FinalDamage *= 1.2f;
            }
        }
    }
}