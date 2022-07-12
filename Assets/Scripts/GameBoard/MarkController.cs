using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkController : MonoBehaviour
{
    public void DestroyOnAnimationComplete() {
        Destroy(this.gameObject);
    }
}
