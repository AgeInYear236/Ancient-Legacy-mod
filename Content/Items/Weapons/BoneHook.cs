using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace testMod1.Content.Items.Weapons
{
    public class BoneHook : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 120; 
            Item.useTime = 120; 
            Item.knockBack = 2f;
            Item.width = 30;
            Item.height = 10;
            Item.damage = 45;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.BoneHookProjectile>();
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 1, 50, 0);
            Item.DamageType = DamageClass.Melee;
        }

    }
}