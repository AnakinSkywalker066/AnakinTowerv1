namespace starwarsmod.TopPath;

public class Top1 : ModUpgrade<Jedi>
{
    public override string DisplayName => "Youngling";
    public override string Description => "Camo and Lead Now Can Be Destoryed Jedi";
    public override int Path => TOP;
    public override int Tier => 1;
    public override int Cost => 1500;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = 0;
        towerModel.AddBehavior(new OverrideCamoDetectionModel("CamoDetect", true));
        towerModel.GetAttackModel().AddBehavior(new TargetFirstPrioCamoModel("FirstPrioCamo", true, false));
        towerModel.GetAttackModel().AddBehavior(new TargetLastPrioCamoModel("LastPrioCamo", true, false));
        towerModel.GetAttackModel().AddBehavior(new TargetClosePrioCamoModel("ClosePrioCamo", true, false));
        towerModel.GetAttackModel().AddBehavior(new TargetStrongPrioCamoModel("StrongPrioCamo", true, false));
        towerModel.towerSelectionMenuThemeId = "Camo";
        towerModel.range += 5;
        
    }
    public override string Icon => "TopUpgrade";
    public override string Portrait => "Anakin";
}