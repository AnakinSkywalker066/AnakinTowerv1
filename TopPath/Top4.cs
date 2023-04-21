using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace starwarsmod.TopPath;

public class Top4 : ModUpgrade<Jedi>
    {
        public override string Name => "Top4";
        public override string DisplayName => "Jedi Master";
        public override string Description => "The Jedi has Become One With the Force.";
        public override int Cost => 10000;
        public override int Path => TOP;
        public override int Tier => 4;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 5;
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.projectile.GetDamageModel().damage += 3;
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 6, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 6, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 6, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 6, false, true));
                weaponModel.Rate = 0.1f;
            }
        }
        public override string Icon => "TopUpgrade";
        public override string Portrait => "2DAnakin";
    }