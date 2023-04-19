namespace starwarsmod.TopPath;

public class Top3 : ModUpgrade<Jedi>
{
    public override string Name => "Top3";
    public override string DisplayName => "Jedi Knight";
    public override string Description => "The Jedi now Destroy Moab Class Bloons";
    public override int Cost => 5500;
    public override int Path => TOP;
    public override int Tier => 3;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.range += 5;
        foreach (var weaponModel in towerModel.GetWeapons())
        {
            weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Moab", "Moab", 1, 4, false, true));
            weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Bfb", "Bfb", 1, 4, false, true));
            weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Zomg", "Zomg", 1, 4, false, true));
            weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("Ddt", "Ddt", 1, 4, false, true));
            weaponModel.Rate = 0.15f;
        }
    }
    public override string Icon => "TopUpgrade";
    public override string Portrait => "Anakin";
} 
