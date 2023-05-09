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
            
            
            towerModel.IncreaseRange(+5);
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

}
