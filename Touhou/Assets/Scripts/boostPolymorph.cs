using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostPolymorph : MonoBehaviour
{
    private character c;
    private string boostType = "heal";
    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        c = GameObject.Find("P_character").GetComponent<character>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        setBoostType();
    }

    private void setBoostType()
    {
        int newTypeIndex = Random.Range(0, 7);
        switch (newTypeIndex)
        {
            
            default:
            case 0: boostType = "heal"; spriteRenderer.sprite = sprites[0]; spriteRenderer.color = Color.green;
            break;
            case 1: boostType = "infinyAmmo"; spriteRenderer.sprite = sprites[1]; spriteRenderer.color = Color.yellow;
            break;
            case 2: boostType = "refillAmmo"; spriteRenderer.sprite = sprites[2]; spriteRenderer.color = Color.magenta;
            break;
            case 3: boostType = "weaponUpgrade"; spriteRenderer.sprite = sprites[3]; spriteRenderer.color = Color.grey;
            break;
            case 4: boostType = "godmode"; spriteRenderer.sprite = sprites[4]; spriteRenderer.color = Color.white;
            break;     
            case 5: boostType = "increaseMaxAmmo"; spriteRenderer.sprite = sprites[5]; spriteRenderer.color = Color.cyan;
            break; 
            case 6: boostType = "increaseAmmoRefill"; spriteRenderer.sprite = sprites[6]; spriteRenderer.color = Color.black;
            break;                                                  
        }
    }
void OnTriggerEnter2D(Collider2D col)
    {
        applyBoost();
    }

    private void applyBoost()
    {
        switch (boostType)
        {
            
            default: c.heal(15f);
            break;
            case "heal": c.heal(15f);
            break;
            case "infinyAmmo": c.toggleInfinyAmmo();
            break;
            case "refillAmmo": c.refillAmmo();
            break;
            case "weaponUpgrade": c.weaponUpgrade();
            break;            
            case "godmode": c.togggleGodMode();
            break;   
            case "increaseMaxAmmo": c.increaseMaxAmmo();
            break;   
            case "increaseAmmoRefill": c.increaseAmmoRefill();
            break;                                                  
        }

        Destroy(this.gameObject);
    }
}
