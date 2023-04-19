namespace starwarsmod.TopPath;

public class Top2 : ModUpgrade<Jedi>
    {
        public override string Name => "Top2";
        public override string DisplayName => "Jedi Padawan";
        public override string Description => "Lightsaber attack increase";
        public override int Cost => 2250;
        public override int Path => TOP;
        public override int Tier => 2;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 5;
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.Rate *= 0.5f;
                weaponModel.projectile.GetDamageModel().damage += 2;
            }
        }
        public override string Icon => "TopUpgrade";
        public override string Portrait => "Anakin";
    }
