using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    [Header("Unique Config")]
    public int row;
    public int col;
    public PLAYER_TYPE playerSelect;
    public int playerNum;
    [Header("Prefabs")]
    public GameObject xMark;
    public GameObject oMark;
    [Header("Scene References")]
    public Transform markContainer;
    
    bool clicked = false;

    void Start() {
        GameBoard.spaces[row, col] = this;
    }

    void OnMouseOver()
    {
        if (!GameState.gameOver) {
            if (Input.GetMouseButtonDown(0)) {
                if (GameState.gameStarted) {
                    clicked = true;
                    if (!GameState.IsCurrentPlayerAI()) {
                        GameBoard.PlaceMark(GameState.GetCurrentPlayer(), row, col);
                    }
                } else if (playerNum > 0) {
                    GameState.SelectPlayer(playerNum, playerSelect, true);
                }
            }
        }
        
    }



    public void Clear() {
        foreach (Transform child in markContainer) {
            Animator anim = child.GetComponent<Animator>();
            anim.SetTrigger("outro");
        }
    }

    public void InstantiateMarker(int mark) {
        if (mark == 1) {
            Instantiate(xMark, markContainer);
        } else {
            Instantiate(oMark, markContainer);
        }
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
    }
}
