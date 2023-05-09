using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using StarWarsMod;
using static StarWarsMod.StarWars;

namespace starwarsmod.TopPath
{
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
}
