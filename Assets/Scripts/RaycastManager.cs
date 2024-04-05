using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private bool isMouseButtonDown = false;
    private void OnEnable()
    {
        EventManager.AddListener("OnMouseButtonDown", OnMouseButtonDown);
        EventManager.AddListener("OnMouseButtonUp", OnMouseButtonUp);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener("OnMouseButtonDown", OnMouseButtonDown);
        EventManager.RemoveListener("OnMouseButtonUp", OnMouseButtonUp);
    }
    void FixedUpdate()
    {
        if(!isMouseButtonDown) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            EventManager.TriggerEvent("OnRaycastHit", hit.point);
        }
    }
    private void OnMouseButtonDown()
    {
        isMouseButtonDown = true;
    }
    private async void OnMouseButtonUp()
    {
        await System.Threading.Tasks.Task.Delay(100);
        isMouseButtonDown = false;
    }
}
