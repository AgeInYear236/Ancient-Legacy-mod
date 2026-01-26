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
            Main.npcFrameCount[Type] = 25; // The amount of frames the NPC has

            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the npc that it tries to attack enemies.
            NPCID.Sets.PrettySafe[Type] = 300;
            NPCID.Sets.AttackType[Type] = 1; // Shoots a weapon.
            NPCID.Sets.AttackTime[Type] = 60; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
            NPCID.Sets.ShimmerTownTransform[Type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

            NPCID.Sets.ActsLikeTownNPC[Type] = true;

            NPCID.Sets.NoTownNPCHappiness[Type] = true;

            NPCID.Sets.SpawnsWithCustomName[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f,
                Direction = 1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            
        }

        public override void FindFrame(int frameHeight)
        {

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

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                    "Shopkeeper"
                };
        }

        public override void SetChatButtons(ref string button, ref string button2) { 
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Patrol rewards";
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
                        Main.npcChatText = "Nicely done! Here are your patrol rewards!";
                        rewardCounter += 1;
                        Main.LocalPlayer.ConsumeItem(ModContent.ItemType<PatrolBannerItem>());
                    }
                }
                if (rewardCounter > 0)
                {
                    Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ModContent.ItemType<GoldBag>(), rewardCounter);
                }
                else
                {
                    Main.npcChatText = "You better find more banners!";
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

            chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.ChatOption1"));
            chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.ChatOption2"));
            chat.Add(Language.GetTextValue("Mods.testMod1.NPCs.StashTrader.ChatOption3"));


            return chat.Get();
        }
    }
}