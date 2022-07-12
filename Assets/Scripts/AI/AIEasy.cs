using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasy: AI {
    public AIMove GetNextMove (GameBoard gameBoard, int thisPlayer) {
        List<Vector2Int> openSpaces = gameBoard.GetOpenSpaces();
        Vector2Int space = openSpaces[Random.Range(0, openSpaces.Count)];
        AIMove move = new AIMove(10);
        move.space = space;
        return move;
    }
}