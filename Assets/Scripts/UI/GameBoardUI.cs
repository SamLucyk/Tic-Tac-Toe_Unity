using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBoardUI : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject playerSelectors;
    public GameObject gameTitle;
    public Animator startButton;

    [Header("Game Over")]
    public GameObject resetIndicator;
    public TextMeshPro winnerText;

    [Header("In-Game")]
    public PlayerUI player1;
    public PlayerUI player2;   
    public GameObject playerIndicators;
        
    void Start() {
        winnerText.text = "";
        resetIndicator.SetActive(false);
        playerIndicators.SetActive(false);
        gameTitle.SetActive(true);
    }

    public void Reset() {
        gameTitle.SetActive(true);
        resetIndicator.SetActive(false);
        startButton.SetTrigger("intro");
        playerIndicators.SetActive(false);
        playerSelectors.SetActive(true);
        winnerText.text = "";
    }

    public void StartGame() {
        player1.SetPlayerType(GameState.player1Type, 1);
        player2.SetPlayerType(GameState.player2Type, 2);
        playerSelectors.SetActive(false);
        resetIndicator.SetActive(false);
        playerIndicators.SetActive(true);
        player1.Reset();
        player2.Reset();
        gameTitle.SetActive(false);
    }

    public void SetWinText() {
        PlayerUI winner = player1;
        if (!GameState.xTurn) winner = player2;
        winnerText.text = winner.name.text + " Wins!";
    }

    public void GameTie() {
        player1.SetTie();
        player2.SetTie();
        winnerText.text = "TIE GAME!";
        resetIndicator.SetActive(true);
    }

    public void GameWin() {
        player1.SetWin(GameState.xTurn);
        player2.SetWin(!GameState.xTurn);
        SetWinText();
        resetIndicator.SetActive(true);
    }

    public void StartTurn() {
        player1.Highlight(GameState.xTurn);
        player2.Highlight(!GameState.xTurn);
    }
  
}
