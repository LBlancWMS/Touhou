//using System.Numerics;
using System.Collections;
using UnityEngine;

public class character : MonoBehaviour
{
    [SerializeField] private energyProjectileManager energyProjectilePrefab;
    private UI_inGame_Manager ui_InGame;
    public float speed = 75f;
    private float currentHP;
    private float maxHP = 100;

    private Vector2 directionn;
    private bool isMoving = false;
    private Vector2 shootAxis;
    private float currentShootEnergy = 0f;
    private float maxShootEnergy = 100f;
    private float energyShootCooldown = 0.15f;
    private bool godMode = false;
    private bool infinyAmmo = false;
    private bool isShooting = false;
    private int weaponLevel = 1;
    private float ammoRefillCooldown = 0.03f;
    [SerializeField] private GameObject projectileSpawnPoint1;
    [SerializeField] private GameObject projectileSpawnPoint2;
    private Rigidbody2D rb;
    private float deltaTime = 0.0f;
    void Awake()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
       //currentShootEnergy = maxShootEnergy;
        ui_InGame = GameObject.Find("UI_inGame").GetComponent<UI_inGame_Manager>();
        StartCoroutine("ammoRefill");
    }


    void OnGUI()
    {
        int fps = Mathf.FloorToInt(1.0f / deltaTime);
        string text = $"FPS: {fps}";
        
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 200, 20), text, style);
    }
void Update()
{
    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    if (isMoving)
    {
       // if(rb.position.x < 72f && rb.position.x > -72f && rb.position.y < 40f && rb.position.y > -40f)
       // {
         rb.MovePosition(rb.position + directionn * speed * Time.fixedDeltaTime);
       // }
       // transform.Translate(directionn * speed * Time.fixedDeltaTime);
    }
}

public void startMove(Vector2 axis)
{
    directionn = axis.normalized;
    isMoving = true;
}

public void stopMove()
{
    isMoving = false;
}

void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject != this.gameObject)
        {
            takeDMG(1);
        }
    }

    private void takeDMG(int damageAmout)
    {
        if(godMode == false)
        {
        currentHP -= damageAmout;
        if(currentHP <= 0)
        {
            Debug.Log("joueur mort");
        }
        else
        {
            ui_InGame.setHealthValue(currentHP);
        }
        }
    }

    public void startShoot(Vector2 axis)
    {
    shootAxis = axis.normalized;
    Vector2 shootDirection = axis.normalized;
    float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

    if (!isShooting)
    {
        StartCoroutine("shootCoroutine");
    }
    isShooting = true;
    }

    public void stopShoot()
    {
        isShooting = false;
        StopCoroutine("shootCoroutine");
        StartCoroutine("ammoRefill");
    }

    private void shoot()
    {
        StopCoroutine("ammoRefill");
        if(infinyAmmo == false)
        {
        currentShootEnergy --;
        }
        ui_InGame.setEnergyValue(currentShootEnergy);
        energyProjectileManager energyProjectile = Instantiate(energyProjectilePrefab, new Vector2(projectileSpawnPoint1.transform.position.x, projectileSpawnPoint1.transform.position.y), Quaternion.identity);
        energyProjectile.onSpawn(shootAxis);

        energyProjectileManager energyProjectile2 = Instantiate(energyProjectilePrefab, new Vector2(projectileSpawnPoint2.transform.position.x, projectileSpawnPoint2.transform.position.y), Quaternion.identity);
        energyProjectile2.onSpawn(shootAxis);
    }


    IEnumerator shootCoroutine()
    {
        while (currentShootEnergy > 0)
        {
            shoot();
            yield return new WaitForSeconds(energyShootCooldown);
        }
    }

    IEnumerator ammoRefill()
    {
        while (currentShootEnergy < maxShootEnergy)
        {
            currentShootEnergy ++;
            ui_InGame.setEnergyValue(currentShootEnergy);
            yield return new WaitForSeconds(ammoRefillCooldown);
        }
    }

    public void heal(float healAmount)
    {
        currentHP += healAmount;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        ui_InGame.setHealthValue(currentHP);
    }

    public void refillAmmo()
    {
        currentShootEnergy = maxShootEnergy;
        ui_InGame.setEnergyValue(currentShootEnergy);
    }

    public void increaseAmmoRefill()
    {
        ammoRefillCooldown *= 0.65f;
    }

    public void weaponUpgrade()
    {
        weaponLevel ++;
        energyShootCooldown *= 0.5f;
        ui_InGame.setWeaponLevel(weaponLevel);
    }

    public void increaseMaxAmmo()
    {
        if(maxShootEnergy < 300)
        {
        maxShootEnergy *= 1.25f; 
        ui_InGame.setEnergyMax(maxShootEnergy);  
        }
        else
        {
            Debug.Log("energymax max");
        }
    }

    

    public void toggleInfinyAmmo()
    {
        infinyAmmo = true;
        ui_InGame.setEnergyColor(true);
        StartCoroutine("infinyAmmoCoroutine");
    }

    IEnumerator infinyAmmoCoroutine()
    {
        if (0 == 0)
        yield return new WaitForSeconds(5.0f);
        infinyAmmo = false;
        ui_InGame.setEnergyColor(false);
        StopCoroutine("infinyAmmoCoroutine");
    }

    public void togggleGodMode()
    {
        godMode = true;
        ui_InGame.setHealthColor(true);
        StartCoroutine("godModeCoroutine");
    }

    IEnumerator godModeCoroutine()
    {
        if (0 == 0)
        yield return new WaitForSeconds(5.0f);
        godMode = false;
        ui_InGame.setHealthColor(false);
        StopCoroutine("godModeCoroutine");
    }
}
