
using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurretBlueprint standardTurret;
    public TurretBlueprint laserBeamer;
    public TurretBlueprint missileLauncher;

    BuildManager buildManager;

    void Start(){
        buildManager = BuildManager.instance;
    }


    public void SelectStandardTurret(){
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectLaserBeamer(){
        buildManager.SelectTurretToBuild(laserBeamer);
    }

    public void SelectLauncher(){
        buildManager.SelectTurretToBuild(missileLauncher);
    }
}
