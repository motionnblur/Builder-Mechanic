using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject cube;
    private bool _lock = true;
    private void OnEnable()
    {
        EventManager.AddListener("OnRaycastHit", OnRaycastHit);
        EventManager.AddListener("OnMouseButtonDown", OnMouseButtonDown);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener("OnRaycastHit", OnRaycastHit);
        EventManager.RemoveListener("OnMouseButtonDown", OnMouseButtonDown);
    }

    private void OnRaycastHit(Vector3 hitPoing)
    {
        if (_lock) return;

        Instantiate(cube, hitPoing, Quaternion.identity);
        _lock = true;
    }
    private void OnMouseButtonDown()
    {
        _lock = false;
    }
}
