using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostPolymorph : MonoBehaviour
{
    private character c;
    private string boostType = "heal";
    [SerializeField] private Sprite orbSprite;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        c = GameObject.Find("P_character").GetComponent<character>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        setBoostType();
    }

    private void setBoostType()
    {
        int newTypeIndex = Random.Range(0, 5);
        switch (newTypeIndex)
        {

            default:
            case 0: boostType = "heal"; spriteRenderer.sprite = orbSprite; spriteRenderer.color = Color.green;
            break;
            case 1: boostType = "infinyAmmo"; spriteRenderer.sprite = orbSprite; spriteRenderer.color = new Color(0.4f, 0.0f, 0.6f, 1.0f);
            break;
            case 2: boostType = "weaponUpgrade"; spriteRenderer.sprite = orbSprite; spriteRenderer.color = new Color(0.8f, 0.3333f, 0.0f, 1.0f);
            break;
            case 3: boostType = "godmode"; spriteRenderer.sprite = orbSprite;
            break;     
            case 4: boostType = "increaseMaxAmmo"; spriteRenderer.sprite = orbSprite; spriteRenderer.color = new Color(1.0f, 0.7137f, 0.7569f, 1.0f);
            break;                                              
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "player")
        {
        applyBoost();
        }
    }

    private void applyBoost()
    {
        switch (boostType)
        {
            default: c.heal(30f);
            break;
            case "heal": c.heal(10f);
            break;
            case "infinyAmmo": c.toggleInfinyAmmo();
            break;
            case "weaponUpgrade": c.weaponUpgrade();
            break;            
            case "godmode": c.togggleGodMode();
            break;   
            case "increaseMaxAmmo": c.increaseMaxAmmo();
            break;                                                 
        }

        Destroy(this.gameObject);
    }
}
