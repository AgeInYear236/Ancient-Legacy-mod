using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using testMod1.Content.Items.Materials;



namespace testMod1.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AxeHelm : ModItem
    {
        public static bool isAxed = false;
        public static int setDamage = 10;

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 8;
            Item.rare = ItemRarityID.Red;
        }

        public override void SetStaticDefaults()
        {
            // If your head equipment should draw hair while drawn, use one of the following:
            //ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
            // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
            // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
            // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;

        }


        public override void UpdateEquip(Player player)
        {
            player.aggro += 100;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AxeArmor>() && legs.type == ModContent.ItemType<AxeBoots>();
        }


        public override void UpdateArmorSet(Player player)
        {
            modPlayer1 axePlayer = player.GetModPlayer<modPlayer1>();
            player.setBonus = Language.GetTextValue("Mods.testMod1.Items.ItemSetBonus.AxeHelm");
            axePlayer.isAxed = true;

        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RawFury>(), 12);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

    }
}
