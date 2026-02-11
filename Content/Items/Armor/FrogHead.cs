using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using testMod1.Content.Items.Materials;

namespace testMod1.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FrogHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.10f;
            player.GetCritChance(DamageClass.Melee) += 5;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FrogArmor>() && legs.type == ModContent.ItemType<FrogLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.testMod1.Items.ItemSetBonus.FrogHead");

            /*player.setBonus = "Melee attacks can summon note, that empowers you with\n " +
                "3 random buffs: speed, damage, attack speed\n " +
                "Collecting 3 buffs at one time summons little friend!";*/
            player.GetModPlayer<FrogPlayer>().frogSet = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoweredSteelBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<AncientMoss>(), 8);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    public class FrogPlayer : ModPlayer
    {
        public bool frogSet;

        public override void ResetEffects()
        {
            frogSet = false;
        }

        public override void PostUpdate()
        {
            if (!frogSet) return;

            int b1 = ModContent.BuffType<Buffs.NoteDamageBuff>();
            int b2 = ModContent.BuffType<Buffs.NoteSpeedBuff>();
            int b3 = ModContent.BuffType<Buffs.NoteMoveBuff>();

            if (Player.HasBuff(b1) && Player.HasBuff(b2) && Player.HasBuff(b3))
            {
                if (Main.myPlayer == Player.whoAmI)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, new Vector2(Main.rand.NextFloat(-2, 2), -4), ModContent.ProjectileType<Misc.FrogMinion>(), 30, 2f, Player.whoAmI);

                    for (int i = 0; i < 15; i++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.GreenFairy, 0, -3f);
                    }
                }

                Player.ClearBuff(b1);
                Player.ClearBuff(b2);
                Player.ClearBuff(b3);

                Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath1, Player.Center);
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (frogSet && item.DamageType == DamageClass.Melee) SpawnNote(target);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (frogSet && proj.DamageType == DamageClass.Melee) SpawnNote(target);
        }

        private void SpawnNote(NPC target)
        {
            if (Main.rand.NextBool(5))
            { 
                int[] types = {
                    ModContent.ProjectileType<Misc.Note1>(),
                    ModContent.ProjectileType<Misc.Note2>(),
                    ModContent.ProjectileType<Misc.Note3>()
                };
                int type = Main.rand.Next(types);

                Vector2 vel = new Vector2(Main.rand.NextFloat(-3f, 3f), -5f);
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, vel, type, 0, 0, Player.whoAmI);
            }
        }
    }
}