﻿using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using StarWarsMod;
using static StarWarsMod.StarWars;

namespace starwarsmod.TopPath
{
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
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound2.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound3.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound4.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound5.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound6.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound7.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound8.assetId = ModContent.CreateAudioSourceReference<StarWars>("PowerDouble");
            towerModel.IncreaseRange(+ 5);
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
}