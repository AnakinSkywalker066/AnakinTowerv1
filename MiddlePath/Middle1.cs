using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;

namespace starwarsmod.MiddlePath
{
    public class Middle1 : ModUpgrade<AnakinSkywalker>
    {
        public override string Name => "Middle1";
        public override string DisplayName => "The Beginning of The Sith";
        public override string Description => "Anakin Begins His Path With The Sith";
        public override int Cost => 500;
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                
                }
        }
    }
}
