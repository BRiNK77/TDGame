using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TurretBlueprint gunTurret;
    public TurretBlueprint megaTurret;
    public TurretBlueprint resTurret;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectTurret1()
    {
        Debug.Log("Gun Turret selected.");
        buildManager.SelectTurretToBuild(gunTurret);
        
    }

    public void SelectTurret2()
    {
        Debug.Log("Mega Turret selected.");
        buildManager.SelectTurretToBuild(megaTurret);
    }
    public void SelectTurret3()
    {
        Debug.Log("Resource Turret selected.");
        buildManager.SelectTurretToBuild(resTurret);
    }
}
