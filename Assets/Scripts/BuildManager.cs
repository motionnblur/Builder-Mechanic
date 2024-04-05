using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject cube;
    private GameObject objForHighlight;
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

    private void OnRaycastHit(Vector3 hitPoint)
    {
        HighLightObject(hitPoint);

        if (_lock) return;
        Instantiate(cube, hitPoint, Quaternion.identity);
        _lock = true;
    }
    private void HighLightObject(Vector3 hitPoint)
    {
        if(!objForHighlight.activeSelf) objForHighlight.SetActive(true);
        objForHighlight.transform.position = hitPoint;
    }
    private void OnMouseButtonDown()
    {
        if(Global.isOnCellDown) _lock = false;

        if(objForHighlight == null)
        {
            objForHighlight = Instantiate(cube);
            objForHighlight.SetActive(false);
            objForHighlight.GetComponent<Renderer>().material.color = new Color(0.4428177f, 0.61199f, 0.8773585f, 0.5f);
        }
    }
}
