using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using starwarsmod.CustomDisplays;
using System.Collections.Generic;
using System.Linq;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.Api;
using StarWarsMod;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity.TowerSets;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Audio;
using UnityEngine;
using Il2CppAssets.Scripts.Utils;

namespace starwarsmod
{
    public class AnakinSkywalker : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.BoomerangMonkey;
        public override int Cost => 1000;
        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 1;
        public override int BottomPathUpgrades => 0;
        public override string Portrait => "2DAnakin";
        public override string Icon => "2DAnakin";
        public override bool DontAddToShop => false;
        public override string Description => "Younglins Beware of This Man!";

        
        

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound2.assetId = CreateAudioSourceReference<StarWars>("fun");
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId = CreateAudioSourceReference<StarWars>("fun");
            ///^^^ Tower Place Sound
            towerModel.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = (BloonProperties)1;
            towerModel.ApplyDisplay<AnakinSaber>();
            towerModel.GetDescendant<DamageModel>().immuneBloonProperties = 0;
            towerModel.GetBehavior<DisplayModel>().scale = towerModel.GetBehavior<DisplayModel>().scale * 1f;
            //Scale required for custom models to be recognized
            towerModel.displayScale = 30f;
            
            
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.Rate = 0.5f;
                weaponModel.projectile.pierce = 10;
                weaponModel.projectile.GetDamageModel().damage = 1;
                weaponModel.projectile.scale = 10f;
                weaponModel.projectile.display = new PrefabReference() { guidRef = "6b874567d3b27004ab1b06ad85b0bf36" };

            }


        }
        public override int GetTowerIndex(List<TowerDetailsModel> towerSet)
        {
            return towerSet.First(model => model.towerId == TowerType.SniperMonkey).towerIndex;
        }
        public override bool IsValidCrosspath(int[] tiers) =>
           ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
    }
}
