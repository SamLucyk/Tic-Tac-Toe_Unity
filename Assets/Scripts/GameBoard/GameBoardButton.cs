using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameBoardButton : MonoBehaviour
{
    public UnityEvent onClick;
    public Animator anim;
    public string animTriggerOnClick;

     void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            anim.SetTrigger(animTriggerOnClick);
            onClick.Invoke();
        }
    }
}
