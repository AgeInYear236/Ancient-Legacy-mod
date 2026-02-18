using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Accessories
{
    public class Armlet : ModItem
    {
        public override string Texture => "testMod1/Content/Items/Accessories/ArmletOff";

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<BasicModPlayer>();
            modPlayer.hasArmlet = true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<BasicModPlayer>();

            string texturePath = modPlayer.armletActive ? "testMod1/Content/Items/Accessories/ArmletOn" : "testMod1/Content/Items/Accessories/ArmletOff";
            Texture2D texture = ModContent.Request<Texture2D>(texturePath).Value;

            spriteBatch.Draw(texture, position, null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}