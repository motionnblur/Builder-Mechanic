using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener("OnCellDown", OnCellDown);
        EventManager.AddListener("OnMouseButtonDown", OnMouseButtonDown);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener("OnCellDown", OnCellDown);
        EventManager.RemoveListener("OnMouseButtonDown", OnMouseButtonDown);
    }
    private void OnCellDown()
    {
        if(Global.pointerClickCount > 2) return;
        Global.pointerClickCount++;
    }
    private void OnMouseButtonDown()
    {
        if(Global.pointerClickCount > 2) return;
        Global.pointerClickCount++;
    }
}
