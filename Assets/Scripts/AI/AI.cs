using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AI{
    AIMove GetNextMove(GameBoard gameBoard, int thisPlayer);
}