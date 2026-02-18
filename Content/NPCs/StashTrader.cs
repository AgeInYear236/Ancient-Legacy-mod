using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using testMod1.Common.Systems;
using testMod1.Content.Items.Misc;

namespace testMod1.Content.NPCs
{
    [AutoloadHead]
    public class StashTrader : ModNPC
    {
        public override string Texture => "testmod1/Content/NPCs/StashTrader";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700; 
            NPCID.Sets.AttackType[NPC.type] = 0;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f, 
                Direction = -1 
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            ContentSamples.NpcBestiaryRarityStars[Type] = 3; 
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            bool talkingToPlayer = Main.player[Main.myPlayer].talkNPC == NPC.whoAmI;

            bool talkingToNPC = NPC.ai[0] == 5f;


            if (NPC.velocity.Y != 0f)
            {
                NPC.frame.Y = 16 * frameHeight;
            }
/*
            else if (NPC.ai[1] > 0f)
            {
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter >= 5.0)
                {
                    NPC.frameCounter = 0.0;
                    NPC.frame.Y += frameHeight;

                    if (NPC.frame.Y < 21 * frameHeight || NPC.frame.Y > 25 * frameHeight)
                    {
                        NPC.frame.Y = 21 * frameHeight;
                    }
                }
            }*/

            else if (NPC.velocity.X != 0f)
            {
                NPC.frameCounter += Math.Abs(NPC.velocity.X) * 1.2f;
                if (NPC.frameCounter >= 6.0)
                {
                    NPC.frameCounter = 0.0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y < 1 * frameHeight || NPC.frame.Y > 15 * frameHeight)
                    {
                        NPC.frame.Y = 1 * frameHeight;
                    }
                }
            }

            else if (NPC.ai[0] == 1f)
            {
                NPC.frame.Y = 17 * frameHeight;
            }

            else if (talkingToPlayer || talkingToNPC)
            {
                NPC.frame.Y = 20 * frameHeight;
            }

            else
            {
                NPC.frame.Y = 0;
                NPC.frameCounter = 0.0;
            }
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.homeless = true;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];

                if (player.active)
                {
                    if (player.HasItem(ModContent.ItemType<PatrolBannerItem>()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
        BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

        //new FlavorTextBestiaryInfoElement("Этот таинственный поставщик всегда знает, где достать лучшее снаряжение. Говорят, его рюкзак бездонен, а камуфляж позволяет скрываться даже от самых зорких глаз.")
        new FlavorTextBestiaryInfoElement("Mods.testMod1.NPCs.StashTrader.Best")
    });
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                    Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.Name")
                };
        }

        public override void SetChatButtons(ref string button, ref string button2) { 
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.Button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = "Shop";
            }
            else
            {
                int rewardCounter = 0;
                foreach (Item item in Main.LocalPlayer.inventory)
                {
                    if (item != null && !item.IsAir && item.type == ModContent.ItemType<PatrolBannerItem>())
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.RewardOption1");
                        rewardCounter += 1;
                        Main.LocalPlayer.ConsumeItem(ModContent.ItemType<PatrolBannerItem>());
                    }
                }
                if (rewardCounter > 0)
                {
                    Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ModContent.ItemType<GoldBag>(), rewardCounter * 10 * (int)(1 + Main.rand.NextFloat()));
                }
                else
                {
                    Main.npcChatText = Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.MiscOption1");
                }

            }
        }

        public override void AddShops()
        {
            new NPCShop(Type).Register();
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();


            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];

                if (player.active)
                {
                    if (player.HasItem(ModContent.ItemType<ShardOfEternity>()))
                    {
                        chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.HelpOption1"));
                        chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.HelpOption2"));
                        chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.HelpOption3"));
                        return chat.Get();
                    }
                    if (player.HasItem(ModContent.ItemType<ShardOfDesolation>()))
                    {
                        chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.HelpOption11"));
                        chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.HelpOption21"));
                        chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.HelpOption31"));
                        return chat.Get();
                    }
                }
            }



            chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.ChatOption1"));
            chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.ChatOption2"));
            chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.ChatOption3"));

            return chat.Get();
        }
    }
}