using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using static StarWarsMod.StarWars;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using StarWarsMod;

namespace starwarsmod.TopPath
{
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
            
            towerModel.IncreaseRange(+10);
            foreach (var weaponModel in towerModel.GetWeapons())
            {

                weaponModel.projectile.pierce += 5;
                weaponModel.projectile.GetDamageModel().damage += 20;
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

