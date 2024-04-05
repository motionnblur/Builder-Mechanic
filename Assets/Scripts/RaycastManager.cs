using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private bool isOnCellDown = false;
    private void OnEnable()
    {
        EventManager.AddListener("OnCellDown", OnCellDown);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener("OnCellDown", OnCellDown);
    }
    void FixedUpdate()
    {
        if(!isOnCellDown) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            EventManager.TriggerEvent("OnRaycastHit", hit.point);
        }
    }

    private void OnCellDown() => isOnCellDown = true;
}
