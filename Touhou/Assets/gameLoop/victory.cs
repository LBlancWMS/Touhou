using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class victory : MonoBehaviour
{
    [SerializeField] private GameObject UI_endGame;
    public void onVictory()
    {
        GameObject ui_victory = Instantiate(UI_endGame);
        ui_victory.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(0f,1f,0f, 0.1f);
        ui_victory.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = "Félicitations, vous avez gagné le droit de recommencer !";
        Time.timeScale = 0f;
    }
}
