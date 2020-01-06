using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    //public Vector3 positionOffset;
    public Transform spawnSpot;


    public GameObject turret;
    BuildManager buildManager;

    public TurretBlueprint currentTurret;
    public int upCount;

    public bool isUpgraded = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    void OnMouseEnter()
    { 
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }
        rend.material.color = hoverColor;
    }
    
  
   
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectNode(this);
            // Debug.Log("Can't build"); // will need to display on screen
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }


        BuildTurretOn(buildManager.GetTurretToBuild());
    }

    void BuildTurretOn(TurretBlueprint blueprint)
    {
        if (PlayerStats.Energy < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Energy -= blueprint.cost;
        GameObject _turret = null;
        if(blueprint.prefab == buildManager.turret1Prefab)
        {
            _turret = (GameObject)Instantiate(blueprint.prefab, spawnSpot.position, Quaternion.identity);
        } else
        {
            _turret = (GameObject)Instantiate(blueprint.prefab, spawnSpot.position, Quaternion.identity);
        }
        
        turret = _turret;

        currentTurret = blueprint;

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (isUpgraded || upCount >= 3)
        {
            Debug.Log("Fully Upgraded!!!");
            return;
        }

        if (PlayerStats.Energy < currentTurret.upCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Energy -= currentTurret.upCost;

        //Get rid of the old turret
        Destroy(turret);

        GameObject _turret = null;
        if (currentTurret.prefab == buildManager.turret1Prefab)
        {
            _turret = (GameObject)Instantiate(currentTurret.upgradedPrefabs[upCount], spawnSpot.position, Quaternion.identity);
        }
        else
        {
            _turret = (GameObject)Instantiate(currentTurret.upgradedPrefabs[upCount], spawnSpot.position, Quaternion.identity);
        }

        turret = _turret;

        if(upCount >= 3)
        {
            isUpgraded = true;
            return;
        }

        upCount++;
        Debug.Log("Turret upgraded!");
    }



}
