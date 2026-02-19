using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AncientLegacyMod.Content.Items.Accessories;
using AncientLegacyMod.Content.Items.Summons;

namespace AncientLegacyMod.Common.Systems
{
    public class ChestLootSystem : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];

                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers)
                {

                    int style = Main.tile[chest.x, chest.y].TileFrameX / 36;

                    if (style == 4 && WorldGen.genRand.NextBool(4)) // shadow chest
                    {
                        AddItemToChest(chest, ModContent.ItemType<RadianceAcc>(), 1);
                    }

                    if (style == 1 && WorldGen.genRand.NextBool(10)) // gold chest
                    {
                        AddItemToChest(chest, ModContent.ItemType<GhostScepter>(), 1);
                    }

                    if (style == 1 && WorldGen.genRand.NextBool(10)) // gold chest
                    {
                        AddItemToChest(chest, ModContent.ItemType<GyroStaff>(), 1);
                    }

                    if (style == 1 && WorldGen.genRand.NextBool(12)) // gold chest
                    {
                        AddItemToChest(chest, ModContent.ItemType<Desolator>(), 1);
                    }
                }
            }
        }

        private void AddItemToChest(Chest chest, int itemID, int stack)
        {
            for (int i = 0; i < 40; i++)
            {
                if (chest.item[i].type == ItemID.None)
                {
                    chest.item[i].SetDefaults(itemID);
                    chest.item[i].stack = stack;
                    break;
                }
            }
        }
    }
}
