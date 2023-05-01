using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using static StarWarsMod.StarWars;
using BTD_Mod_Helper.Extensions;

namespace starwarsmod.TopPath
{
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
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 5, 11, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 5, 11, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 5, 11, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 5, 11, false, true));
                weaponModel.Rate = 0.1f;
            }
        }
        public override string Icon => "top4";
        public override string Portrait => "top4";
    }
}
