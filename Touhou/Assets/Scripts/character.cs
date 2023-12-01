//using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
{
    [SerializeField] private energyProjectileManager energyProjectilePrefab;
    private UI_inGame_Manager ui_InGame;
    public float speed = 35f;
    private float currentHP;
    private float maxHP = 100;
    private bool canTakeDMG = true;
    private bool doOnce = true;
    private Vector2 moveDirection;
    private bool isMoving = false;
    private Vector2 shootAxis;
    private float shootEnergyCost = 0.75f;
    private float currentShootEnergy = 0f;
    private float maxShootEnergy = 100f;
    private float energyShootCooldown = 0.075f;
    private bool godMode = false;
    private bool infinyAmmo = false;
    private bool isShooting = false;
    private int weaponLevel = 1;
    private Camera cam;
    private float screenWidth;
    private float screenHeight;
    private float ammoRefillCooldown = 0.075f;
    [SerializeField] private GameObject projectileSpawnPoint1;
    private Rigidbody2D rb;
    private Vector3 playerScreenPoint;
    [SerializeField] private GameObject UI_endGame;
    private SpriteRenderer playerSprite;
    private AudioClip randomClip;
    private AudioSource audioSource;
    public AudioClip[] soundClips;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private Text debugText;
    private bool debugGodMode = false;

    private float deltaTime = 0.0f;
    void Awake()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        ui_InGame = GameObject.Find("UI_inGame").GetComponent<UI_inGame_Manager>();
        StartCoroutine("ammoRefill");
        cam = Camera.main;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        audioSource = gameObject.AddComponent<AudioSource>();
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
         rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
        playerScreenPoint = cam.WorldToScreenPoint(transform.position);

        if (playerScreenPoint.y < 0) 
            screenLimitTeleportation(new Vector3(playerScreenPoint.x, screenHeight, playerScreenPoint.z));
        else if (playerScreenPoint.y > screenHeight) 
            screenLimitTeleportation(new Vector3(playerScreenPoint.x, 0, playerScreenPoint.z));
    }

    private void screenLimitTeleportation(Vector3 newPosition)
    {
        transform.position = cam.ScreenToWorldPoint(newPosition);
    }

public void startMove(Vector2 axis)
{   
    moveDirection = axis.normalized;
    isMoving = true;
}

public void stopMove()
{
    isMoving = false;
}

    void OnParticleCollision(GameObject other)
    {
        if(debugGodMode == false)
        {
            takeDMG(10);
        }

    }

    private void toogleCanTakeDMG()
    {
        canTakeDMG = true;
        playerSprite.color = Color.white;
    }

    public void godModDEBUG()
    {
        debugGodMode = !debugGodMode;
        debugText.enabled = debugGodMode;
    }

    private void takeDMG(int damageAmout)
    {
        if(godMode == false)
        {  
        currentHP -= damageAmout;
        audioSource.clip = hitClip;
        audioSource.Play();
        ui_InGame.setHealthValue(currentHP);
        if(canTakeDMG == true)
        {
            canTakeDMG = false;
            playerSprite.color = Color.red;
            Invoke("toogleCanTakeDMG", 0.15f);
        }

        if(currentHP <= 0)
        {
            if(doOnce)
            {
                doOnce = false;
                GameObject ui_dead = Instantiate(UI_endGame);
                ui_dead.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(1f,0f,0f, 0.1f);
                Time.timeScale = 0f;
            }
        }
        }
    }

    public void startShoot(Vector2 axis)
    {
    shootAxis = axis.normalized;
    Vector2 shootDirection = axis.normalized;
    float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));



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
        playRandomShootSound();
        if(infinyAmmo == false)
        {
        StopCoroutine("ammoRefill");
        currentShootEnergy -= shootEnergyCost;
        }

        ui_InGame.setEnergyValue(currentShootEnergy);
        energyProjectileManager energyProjectile = Instantiate(energyProjectilePrefab, new Vector2(projectileSpawnPoint1.transform.position.x, projectileSpawnPoint1.transform.position.y), transform.rotation);
        energyProjectile.onSpawn(shootAxis);
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
        transform.localScale /= 0.85f;
        energyShootCooldown *= 0.5f;
        ui_InGame.setWeaponLevel(weaponLevel);
    }

    public void increaseMaxAmmo()
    {
        if(maxShootEnergy < 300)
        {
        increaseAmmoRefill();
        maxShootEnergy *= 1.25f; 
        ui_InGame.setEnergyMax(maxShootEnergy);  
        }
    }

    private void playRandomShootSound()
    {
            randomClip = soundClips[Random.Range(0, soundClips.Length)];
            audioSource.clip = randomClip;
            audioSource.Play();
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
        yield return new WaitForSeconds(10.0f);
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
        yield return new WaitForSeconds(8.0f);
        godMode = false;
        ui_InGame.setHealthColor(false);
        StopCoroutine("godModeCoroutine");
    }




    
}
