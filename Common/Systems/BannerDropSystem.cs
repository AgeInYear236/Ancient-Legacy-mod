using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using testMod1.Content.Items.Misc;

namespace testMod1.Common.Systems
{   
    public class BannerDropSystem : ModSystem
    {
        // Tracks total kills per NPC type
        public static Dictionary<int, int> KillCounts = new();

        // Tracks which banners have already been awarded (to prevent re-dropping)
        public static HashSet<int> BannersAwarded = new();

        public override void ClearWorld()
        {
            KillCounts.Clear();
            BannersAwarded.Clear();
        }

        public override void SaveWorldData(TagCompound tag)
        {
            // Convert Dictionary to arrays for safe serialization
            var keys = KillCounts.Keys.ToArray();
            var values = KillCounts.Values.ToArray();

            tag["killCountKeys"] = keys;
            tag["killCountValues"] = values;

            tag["bannersAwarded"] = new List<int>(BannersAwarded);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("killCountKeys") && tag.ContainsKey("killCountValues"))
            {
                var keys = tag.Get<int[]>("killCountKeys");
                var values = tag.Get<int[]>("killCountValues");

                KillCounts = new Dictionary<int, int>();
                for (int i = 0; i < keys.Length; i++)
                {
                    KillCounts[keys[i]] = values[i];
                }
            }
            else
            {
                KillCounts = new Dictionary<int, int>();
            }

            if (tag.ContainsKey("bannersAwarded"))
            {
                BannersAwarded = new HashSet<int>(tag.Get<List<int>>("bannersAwarded"));
            }
            else
            {
                BannersAwarded = new HashSet<int>();
            }
        }
    }
}
