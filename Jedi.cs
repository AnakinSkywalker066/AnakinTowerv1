namespace starwarsmod;

public class Jedi : ModTower
{
    public override bool IsValidCrosspath(int[] tiers) =>
    ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
    public override string Name => "AnakinSkywalker";
    public override TowerSet TowerSet => TowerSet.Military;
    public override string BaseTower => "BoomerangMonkey";
    public override int Cost => 1000;
    public override int TopPathUpgrades => 5;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;
    public override int GetTowerIndex(List<TowerDetailsModel> towerSet)
    {
        return towerSet.First(model => model.towerId == TowerType.GlueGunner).towerIndex + 1;
    }

    public override string Description => "Uses Jedi Training to pop Bloons";
    public override string Icon => "Anakin";
    public override string Portrait => "Anakin";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.GetBehavior<DisplayModel>().scale = towerModel.GetBehavior<DisplayModel>().scale * 0.6f;
        towerModel.GetAttackModel().weapons[0].projectile = Game.instance.model.GetTower(TowerType.BoomerangMonkey).GetAttackModel().weapons[0].projectile.Duplicate();
        towerModel.GetAttackModel().weapons[0].projectile.ApplyDisplay<Lightsaber>();
        towerModel.GetAttackModel().weapons[0].projectile.pierce = 100;
    }
    
}