using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard: MonoBehaviour
{
    public int[,] board = new int[3, 3] {
        {0, 0, 0},
        {0, 0, 0},
        {0, 0, 0}
    };

    public static GameBoard main = new GameBoard();
    public static GridSpace[,] spaces = new GridSpace[3,3];

    public static void ClearRow(int row) {
        for(int col = 0; col < 3; col++) {
            spaces[row, col].Clear();
            main.board[row, col] = 0; 
        }
    }

    public static void ClearAllSpaces() {
        for(int row = 0; row < 3; row++) {
            ClearRow(row);
        }
    }

    public static void PlaceMark(int mark, int row, int col) {
        if (main.isOpen(row, col)) {
            SoundManager.PlayPlayerPlacementSound(mark);
            main.board[row, col] = mark;
            InstantiateMarker(row, col, mark);
            GameState.CheckEndOfTurn();
        }
    }

    public static void InstantiateMarker(int row, int col, int playerNum) {
        spaces[row,col].InstantiateMarker(playerNum);
    }    

    public int CheckBoardForWinner() {
        for(int i = 0; i < 3; i++) {
            // Check Rows
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] &&  board[i, 1] == board[i, 2]) {
                return board[i, 0];
            } 
            // Check Columns
            if (board[0, i] != 0 && board[0, i] == board[1, i] &&  board[1, i] == board[2, i]) {
                return board[0, i];
            }
        }
        //Check Diagnol
        if (board[2, 0] != 0 && board[2, 0] == board[1, 1] &&  board[1, 1] == board[0, 2]) {
            return board[2, 0];
        }
        if (board[0, 0] != 0 && board[0, 0] == board[1, 1] &&  board[1, 1] == board[2, 2]) {
            return board[0, 0];
        }
        //Else
        return -1;
    }

    public bool isOpen(int row, int col) {
        return board[row,col] <= 0;
    }

    public List<Vector2Int> GetOpenSpaces() {
        List<Vector2Int> result = new List<Vector2Int>();
        for(int row = 0; row < 3; row++) {
            for(int col = 0; col < 3; col++) {
                if (isOpen(row, col)) {
                    result.Add(new Vector2Int(row, col));
                }
            }
        }
        return result;
    }

}
