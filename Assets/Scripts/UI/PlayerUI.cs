using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshPro name;
    public GameObject highlight;
    public GameObject winIndicator;
    public GameObject lossIndicator;
    public GameObject tieIndicator;
    public void Reset() {
        winIndicator.SetActive(false);
        lossIndicator.SetActive(false);
        tieIndicator.SetActive(false);
    }

    public void SetPlayerType(PLAYER_TYPE playerType, int num) {
        if (playerType == PLAYER_TYPE.PLAYER) {
            name.text = "Player " + num;
        } else if (playerType == PLAYER_TYPE.AIEASY) {
            name.text = "AI EASY " + num;
        } else {
            name.text = "AI HARD " + num;
        }

    }

    public void SetWin(bool win) {
        winIndicator.SetActive(win);
        lossIndicator.SetActive(!win);
    }

    public void SetTie() {
        tieIndicator.SetActive(true);
    }

    public void Highlight(bool active) {
        highlight.SetActive(active);
    }
}
