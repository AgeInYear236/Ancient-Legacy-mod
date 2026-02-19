// Common/Systems/BannerDropSystem.cs
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Misc;
using AncientLegacyMod.Common.Systems;

namespace AncientLegacyMod.Common.Systems
{
    public class BannerDropLoot : GlobalNPC
    {
        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            return npc.boss == false &&
                   npc.type > NPCID.None &&
                   !NPCID.Sets.ShouldBeCountedAsBoss[npc.type];
        }

        public override void OnKill(NPC npc)
        {
            int npcType = npc.type;

            if (!BannerDropSystem.KillCounts.ContainsKey(npcType))
                BannerDropSystem.KillCounts[npcType] = 0;

            BannerDropSystem.KillCounts[npcType]++;

            if (BannerDropSystem.KillCounts[npcType] >= 30 /*&& !BannerDropSystem.BannersAwarded.Contains(npcType)*/)
            {
                BannerDropSystem.BannersAwarded.Add(npcType);

                // Uncomment lower & comment second condition in if to allow > 1 banner per NPCType.
                BannerDropSystem.KillCounts[npcType] = 0;

                Item.NewItem(
                    npc.GetSource_Loot(),
                    (int)npc.position.X,
                    (int)npc.position.Y,
                    npc.width,
                    npc.height,
                    ModContent.ItemType<PatrolBannerItem>()
                );

                if (Main.netMode != NetmodeID.Server)
                {
                    Main.NewText($"You've defeated 30 {npc.FullName}! A patrol banner has been dropped.", Microsoft.Xna.Framework.Color.Yellow);
                }
            }
        }
    }
}