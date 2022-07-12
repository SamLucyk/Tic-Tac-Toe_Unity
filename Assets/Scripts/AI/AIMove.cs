using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove {
    public Vector2Int space;
    public int score;
    public AIMove(int aScore)
    {
        score = aScore;
    }
}