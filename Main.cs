using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using StarWarsMod;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2Cpp;
using BTD_Mod_Helper.Api.Display;

[assembly: MelonInfo(typeof(StarWarsMod.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace StarWarsMod;
public class Main : BloonsTD6Mod
{
    public class AnakinSaber : ModCustomDisplay
    {
        public override string AssetBundleName => "sky";
        public override string PrefabName => "AnakinSaber";
    }

    public class Lightsaber : ModCustomDisplay
    {
        public override string AssetBundleName => "sky";
        public override string PrefabName => "lightsaber";
    }
    public class AnakinSkywalker : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.BoomerangMonkey;
        public override int Cost => 1000;
        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;
        public override string Portrait => "2DAnakin";
        public override string Icon => "2DAnakin";
        public override bool DontAddToShop => false;
        public override string Description => "Younglins Beware of This Man!";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<AnakinSaber>();
            towerModel.GetDescendant<DamageModel>().immuneBloonProperties = (BloonProperties)0;
            towerModel.GetBehavior<DisplayModel>().scale = towerModel.GetBehavior<DisplayModel>().scale * 1f;
            //required for custom displays to be recognized
            towerModel.displayScale = 30f;
            var proj = towerModel.GetAttackModel().weapons[0].projectile;
            proj.scale = 50f;
            proj.ApplyDisplay<Lightsaber>();

            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.animateOnMainAttack = true;
                weaponModel.projectile.pierce = 100000;
            }


        }
        public override int GetTowerIndex(List<TowerDetailsModel> towerSet)
        {
            return towerSet.First(model => model.towerId == TowerType.SniperMonkey).towerIndex;
        }
        public override bool IsValidCrosspath(int[] tiers) =>
           ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);

        public class Top1 : ModUpgrade<AnakinSkywalker>
        {
            public override string Name => "Top1";
            public override string DisplayName => "Youngling";
            public override string Description => "Anakin Can Now Use His LightSaber Better";
            public override int Cost => 750;
            public override int Path => TOP;
            public override int Tier => 1;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                foreach (var weaponModel in towerModel.GetWeapons())
                {
                    weaponModel.Rate *= 0.75f;
                    weaponModel.animateOnMainAttack = true;
                    weaponModel.projectile.GetDamageModel().damage += 5;

                }
            }
            public override string Icon => "Top1";
            public override string Portrait => "Top1";
        }

        public class Top2 : ModUpgrade<AnakinSkywalker>
        {
            public override string Name => "Top2";
            public override string DisplayName => "Padawan";
            public override string Description => "Anakin is Now Being Taught The Ways Of Killing Younglings I Mean Force...";
            public override int Cost => 1500;
            public override int Path => TOP;
            public override int Tier => 2;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                foreach (var weaponModel in towerModel.GetWeapons())
                {
                    weaponModel.Rate *= 0.5f;
                    weaponModel.projectile.GetDamageModel().damage += 5;
                    weaponModel.animateOnMainAttack = true;
                }
            }
            public override string Icon => "Top2";
            public override string Portrait => "Top2";
        }

        public class Top3 : ModUpgrade<AnakinSkywalker>
        {
            public override string Name => "Top3";
            public override string DisplayName => "Jedi Knight";
            public override string Description => "Now Anakin Will Show His True Jedi Power!";
            public override int Cost => 7500;
            public override int Path => TOP;
            public override int Tier => 3;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                foreach (var weaponModel in towerModel.GetWeapons())
                {
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 4, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 4, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 4, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 4, false, true));
                    weaponModel.Rate = 0.15f;
                    weaponModel.animateOnMainAttack = true;
                    weaponModel.projectile.GetDamageModel().damage += 5;
                }
            }
            public override string Icon => "Clonewars";
            public override string Portrait => "Clonewars";
        }

        public class Top4 : ModUpgrade<AnakinSkywalker>
        {
            public override string Name => "Top4";
            public override string DisplayName => "Jedi Master";
            public override string Description => "Something Anakin Never Got xD";
            public override int Cost => 15000;
            public override int Path => TOP;
            public override int Tier => 4;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                foreach (var weaponModel in towerModel.GetWeapons())
                {
                    weaponModel.animateOnMainAttack = true;
                    weaponModel.projectile.pierce += 5;
                    weaponModel.projectile.GetDamageModel().damage += 5;
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 6, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 6, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 6, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 6, false, true));
                    weaponModel.Rate = 0.1f;
                }
            }
            public override string Icon => "top4";
            public override string Portrait => "top4";
        }

        public class Top5 : ModUpgrade<AnakinSkywalker>
        {
            public override string Name => "Top5";
            public override string DisplayName => "Grand Master Jedi";
            public override string Description => "Only The True Power Of The Jedi Can Be Achieved By The Will Of The Force";
            public override int Cost => 75000;
            public override int Path => TOP;
            public override int Tier => 5;
            public override void ApplyUpgrade(TowerModel towerModel)
            {


                foreach (var weaponModel in towerModel.GetWeapons())
                {

                    weaponModel.projectile.pierce += 5;
                    weaponModel.projectile.GetDamageModel().damage += 5;
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 55, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 80, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 230, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 190, false, true));
                    weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bad", "Bad", 1, 390, false, true));
                    weaponModel.Rate = 0.05f;
                    weaponModel.animateOnMainAttack = true;
                }
            }
            public override string Icon => "Top5";
            public override string Portrait => "Top5";
        }
    }
}
    

