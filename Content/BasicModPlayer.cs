using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using testMod1.Content.Items.Accessories;
using testMod1.Content.Items.Armor;
using testMod1.Content.Items.Projectiles;
using static System.Net.Mime.MediaTypeNames;

namespace testMod1.Content
{
    public class BasicModPlayer : ModPlayer   
    {
        // Axe Armor
        public int previousHealth;
        public bool isAxed=false;
        public bool hasHitWithAxeAxeThisUse = false;
        int c = 0;

        // Magic Staff projectiles
        public List<Vector2> orbOffsets = new List<Vector2>();

        // Blink stuff
        public int blinkDashCooldown = 0;
        public const int MaxBlinkDashCooldown = 10;

        // Rogue stuff
        public int rogueDashCooldown = 0;
        public const int MaxRogueDashCooldown = 10;

        // Huskar blood
        public bool isBlooded = false;
        public float missingHealthPercent = 0f;

        public bool extraSlotUnlocked = false;

        public override void SaveData(TagCompound tag)
        {
            tag["extraSlotUnlocked"] = extraSlotUnlocked;
        }

        public override void LoadData(TagCompound tag)
        {
            extraSlotUnlocked = tag.GetBool("extraSlotUnlocked");
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (testMod1.armletKeybind.JustPressed)
            {
                if (Armlet.isActive == false)
                {
                    Armlet.isActive = true;
                }
                else
                {
                    Armlet.isActive = false;
                }
            }
        }
    }
}
