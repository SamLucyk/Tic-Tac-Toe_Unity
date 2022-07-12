using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHard: AI {
    public AIMove GetNextMove (GameBoard gameBoard, int player) {
        return MiniMax(gameBoard, player, player);
    }

    public static AIMove MiniMax(GameBoard gameBoard, int player, int currentPlayer) {
        List<Vector2Int> openSpaces = gameBoard.GetOpenSpaces();
        AIMove result = GetTerminalState(gameBoard, openSpaces, player);
        if (result != null) return result;
        List<AIMove> moves = GetAllPossibleMoves(gameBoard, openSpaces, player, currentPlayer);
        return GetBestMove(moves, player, currentPlayer);
    }

    static AIMove GetTerminalState(GameBoard gameBoard, List<Vector2Int> openSpaces, int thisPlayer) {
        int winner = gameBoard.CheckBoardForWinner();
        if (winner > 0 && winner == thisPlayer) {
            return new AIMove(10);
        }
        if (winner > 0) {
            return new AIMove(-10);
        }
        if (openSpaces.Count == 0) {
            return new AIMove(0);
        }
        return null;
    }

    static List<AIMove> GetAllPossibleMoves(GameBoard gameBoard, List<Vector2Int> openSpaces, int thisPlayer, int currentPlayer) {
        List<AIMove> moves = new List<AIMove>();
        foreach (Vector2Int openSpace in openSpaces) {
            gameBoard.board[openSpace.x, openSpace.y] = currentPlayer;
            AIMove move = MiniMax(gameBoard, thisPlayer, GameState.GetOppositePlayer(currentPlayer));
            move.space = openSpace;
            gameBoard.board[openSpace.x, openSpace.y] = 0;
            moves.Add(move);
        }
        return moves;
    }

    static AIMove GetBestMove(List<AIMove> moves, int thisPlayer, int currentPlayer) {
        AIMove bestMove = new AIMove(-10000);
        bool isCurrentPlayer = (thisPlayer == currentPlayer);
        if (!isCurrentPlayer) bestMove.score = 10000;
        foreach(AIMove move in moves)
        {
            if (isBestMoveForPlayer(isCurrentPlayer, move, bestMove)) {
                bestMove.score = move.score;
                bestMove.space = move.space;
            }
        }
        return bestMove;
    }

    static bool isBestMoveForPlayer(bool isCurrentPlayer, AIMove move, AIMove bestMove) {
        bool scoreIsBetter = (isCurrentPlayer && (move.score > bestMove.score)) || (!isCurrentPlayer && (move.score < bestMove.score));
        if (scoreIsBetter) return true;
        bool shouldReplaceSameScore = Random.Range(0, 2) == 1;
        return shouldReplaceSameScore && move.score == bestMove.score;
    }
}