using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    [Header("Scene References")]
    public GameBoardUI boardDisplay;
    /// GameState
    public static GameState instance;
    public static bool gameOver;
    public static bool gameStarted = false;
    public static bool xTurn = true; // Player1
    public static PLAYER_TYPE player1Type = PLAYER_TYPE.PLAYER; //X
    public static PLAYER_TYPE player2Type = PLAYER_TYPE.AIHARD; //O
    static float gameEndTime;

    void Awake() {
        instance = this;
    }
    
    void Start() {
        SetDefaultGameState();
    }

    void Update() {
        ResetGameIfShould();
    }

    void ResetGameIfShould() {
        if (gameOver && Input.GetMouseButtonDown(0) && Time.time > gameEndTime + .5f) {
            ResetGame();
        }
    }

    void SetDefaultGameState() {
        gameStarted = false;
        gameOver = false;
        xTurn = true;
    }

    static int GetPlayerSelectRow(int playerNum) {
        if (playerNum == 1) {
            return 0;
        }
        return 2;
    }

    public static void CheckEndOfTurn() {
        int winningPlayer = GameBoard.main.CheckBoardForWinner();
        bool isWinner = winningPlayer > -1;
        if (isWinner || GameBoard.main.GetOpenSpaces().Count == 0) {
            GameOver(isWinner);
        }
        if (!gameOver) {
            xTurn = !xTurn;
            GameState.instance.StartTurn();
        }
    }

    public static void SelectPlayer(int playerNum, PLAYER_TYPE type, bool playSound = false) {
        if (playSound) SoundManager.PlayPlayerPlacementSound(playerNum);
        if (playerNum == 1) player1Type = type;
        else player2Type = type;
        int row = GetPlayerSelectRow(playerNum);
        GameBoard.ClearRow(row);
        GameBoard.InstantiateMarker(row,GetPlayerTypeColumn(type), playerNum);
    }

    static void GameOver(bool winner) {
        gameOver = true;
        gameEndTime = Time.time;
        if (winner) { instance.boardDisplay.GameWin(); }
        else instance.boardDisplay.GameTie();
    }

    public static int GetOppositePlayer(int player) {
        if (player == 1) {
            return 2;
        } 
        return 1;
    }

    public void StartTurn() {
        boardDisplay.StartTurn();
        if (IsCurrentPlayerAI()) {
            StartCoroutine(AIMove());
        }
    }

    IEnumerator AIMove()
    {
        int player = GetCurrentPlayer();
        AI aiToUse = new AIHard();
        if (GetCurrentPlayerType() == PLAYER_TYPE.AIEASY) {
            aiToUse = new AIEasy();
        }
        GameBoard boardCopy = new GameBoard();
        boardCopy.board = GameBoard.main.board;
        AIMove move = aiToUse.GetNextMove(boardCopy, player);
        yield return new WaitForSeconds(1.5f);
        GameBoard.PlaceMark(player, move.space.x, move.space.y);
    }

    void ResetGame() {
        SoundManager.PlayClickSound();
        boardDisplay.Reset();
        GameBoard.ClearAllSpaces();
        SelectPlayer(1, player1Type);
        SelectPlayer(2, player2Type);
        SetDefaultGameState();
    }

    public void StartGame() {
        SetRandomFirstTurn();
        SoundManager.PlayClickSound();
        boardDisplay.StartGame();
        GameBoard.ClearAllSpaces();
        gameStarted = true;
        StartTurn();
    }
    //Private Helpers
    static void SetRandomFirstTurn() {
        if (Random.Range(0, 2) == 1) xTurn = false;
        else xTurn = true;
    }
    static PLAYER_TYPE GetCurrentPlayerType() {
        if (xTurn) return player1Type;
        return player2Type;
    }
    static int GetPlayerTypeColumn(PLAYER_TYPE type) {
        return (int)type;
    }

    //public Helpers
    public static bool IsCurrentPlayerAI() {
        return (xTurn && player1Type != PLAYER_TYPE.PLAYER) || (!xTurn && player2Type != PLAYER_TYPE.PLAYER);
    }

    public static int GetCurrentPlayer() {
        if (xTurn) return 1;
        return 2;
    }
    //
    

   
}
