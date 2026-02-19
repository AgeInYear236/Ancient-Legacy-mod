using AncientLegacyMod.Common.Systems;
using AncientLegacyMod.Content.Items.Armor;
using AncientLegacyMod.Content.Items.Misc;
using AncientLegacyMod.Content.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace AncientLegacyMod
{
	public class AncientLegacyMod : Mod
	{
        public static int goldCurrencyId;
        public static ModKeybind armletKeybind;
        public static ModKeybind ta1Keybind;
        public static ModKeybind ta2Keybind;
        public static ModKeybind bmKeybind;
        public static ModKeybind bkbKeybind;
        public static ModKeybind chronoKeybind;
        public static ModKeybind timelapseKeybind;


        public override void Load()
        {
            goldCurrencyId = CustomCurrencyManager.RegisterCurrency(new GoldCurrencySystem(ModContent.ItemType<GoldBag>(), 999L));
            armletKeybind = KeybindLoader.RegisterKeybind(this, "Toggle Armlet", "L");
            ta1Keybind = KeybindLoader.RegisterKeybind(this, "Activate Refraction", "K");
            ta2Keybind = KeybindLoader.RegisterKeybind(this, "Activate Meld", "J");
            bmKeybind = KeybindLoader.RegisterKeybind(this, "Activate Blade Mail", "P");
            bkbKeybind = KeybindLoader.RegisterKeybind(this, "Activate Black King Bar", "O");
            chronoKeybind = KeybindLoader.RegisterKeybind(this, "Activate Chronosphere", "I");
            timelapseKeybind = KeybindLoader.RegisterKeybind(this, "Use Time Lapse", "U");

        }

        public override void Unload()
        {
            armletKeybind = null;
            ta1Keybind = null;
            ta2Keybind = null;
            bmKeybind = null;
            bkbKeybind = null;
            chronoKeybind = null;
            timelapseKeybind = null;
        }

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("Census", out Mod censusMod))
            {
                censusMod.Call("TownNPCInfo",
                    ModContent.NPCType<StashTrader>(),
                    $"Have [i:{ModContent.ItemType<PatrolBannerItem>()}] Patrol Banner");
            }

        }
    }
}
