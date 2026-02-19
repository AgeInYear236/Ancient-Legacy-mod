using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AncientLegacyMod.Common.Systems
{
    public class BossKillMessageSystem : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.boss || (npc.type == NPCID.EaterofWorldsHead && !NPC.AnyNPCs(NPCID.EaterofWorldsBody)))
            {

                if (!IsBossDowned(npc.type))
                {
                    string bossName = npc.TypeName;

                    string message = $"Mods.AncientLegacyMod.ChatMessages.ShopEntry";
                    Color messageColor = new Color(255, 128, 0);

                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText(message, messageColor);
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), messageColor);
                    }
                }
            }
        }

        private bool IsBossDowned(int type)
        {
            return type switch
            {
                NPCID.KingSlime => NPC.downedSlimeKing,
                NPCID.EyeofCthulhu => NPC.downedBoss1,
                NPCID.EaterofWorldsHead => NPC.downedBoss2,
                NPCID.BrainofCthulhu => NPC.downedBoss2,
                NPCID.QueenBee => NPC.downedQueenBee,
                NPCID.SkeletronHead => NPC.downedBoss3,
                NPCID.Deerclops => NPC.downedDeerclops,
                NPCID.WallofFlesh => Main.hardMode,
                NPCID.TheDestroyer => NPC.downedMechBoss1,
                NPCID.Retinazer => NPC.downedMechBoss2,
                NPCID.Spazmatism => NPC.downedMechBoss2,
                NPCID.SkeletronPrime => NPC.downedMechBoss3,
                NPCID.Plantera => NPC.downedPlantBoss,
                NPCID.Golem => NPC.downedGolemBoss,
                NPCID.DukeFishron => NPC.downedFishron,
                NPCID.CultistBoss => NPC.downedAncientCultist,
                NPCID.MoonLordCore => NPC.downedMoonlord,
                _ => false
            };
        }
    }
}