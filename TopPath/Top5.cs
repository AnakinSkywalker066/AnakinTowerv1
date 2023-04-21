using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace starwarsmod.TopPath;

    public class Top5 : ModUpgrade<Jedi>
    {
        public override string Name => "Top5";
        public override string DisplayName => "Grand Master Jedi";
        public override string Description => "The True Power of The Jedi Order";
        public override int Cost => 48000;
        public override int Path => TOP;
        public override int Tier => 5;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 10;
            foreach (var weaponModel in towerModel.GetWeapons())
        {
                weaponModel.projectile.GetDamageModel().damage += 5;
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 55, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 80, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 230, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 190, false, true));
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bad", "Bad", 1, 390, false, true));
                weaponModel.Rate = 0.05f;
            }
        }
        public override string Icon => "TopUpgrade";
        public override string Portrait => "2DAnakin";
    }

