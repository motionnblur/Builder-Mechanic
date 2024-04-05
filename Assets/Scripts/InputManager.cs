using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EventManager.TriggerEvent("OnMouseButtonDown");
        }else if(Input.GetMouseButtonUp(0)){
            EventManager.TriggerEvent("OnMouseButtonUp");
        }
    }
}
