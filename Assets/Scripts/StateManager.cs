using UnityEngine;

public class StateManager : MonoBehaviour
{
    private bool isOnCellDownFirst = false;
    private bool isOnCellDown = false;
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
        if(!isOnCellDownFirst)
        {
            isOnCellDownFirst = true;
            return;
        }
        isOnCellDown = true;
    }
    private void OnMouseButtonDown()
    {
        if(isOnCellDown) return;

        Global.isOnCellDown = true;
    }
}
