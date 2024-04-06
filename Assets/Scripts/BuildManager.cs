using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject cube;
    private GameObject objForHighlight;
    private bool isOnCellDown = false;
    private bool instantiateLock = true;
    private int clickCount = 0;
    private void OnEnable()
    {
        EventManager.AddListener("OnRaycastHit", OnRaycastHit);
        EventManager.AddListener("OnMouseButtonDown", OnMouseButtonDown);
        EventManager.AddListener("OnCellDown", OnCellDown);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener("OnRaycastHit", OnRaycastHit);
        EventManager.RemoveListener("OnMouseButtonDown", OnMouseButtonDown);
        EventManager.RemoveListener("OnCellDown", OnCellDown);
    }
    private void Awake()
    {
        objForHighlight = Instantiate(cube);
        objForHighlight.SetActive(false);
        objForHighlight.GetComponent<Renderer>().material.color = new Color(0.4428177f, 0.61199f, 0.8773585f, 0.5f);
    }
    private void OnRaycastHit(Vector3 hitPoint)
    {
        HighLightObject(hitPoint);

        if(!instantiateLock)
        {
            Instantiate(cube, hitPoint, Quaternion.identity);
            instantiateLock = true;
        }
    }
    private void HighLightObject(Vector3 hitPoint)
    {
        if(!objForHighlight.activeSelf) objForHighlight.SetActive(true);
        objForHighlight.transform.position = hitPoint;
    }
    private void OnMouseButtonDown()
    {
        if(!isOnCellDown) return;
        
        if(clickCount == 1){
            instantiateLock = false;
        }else{
            clickCount++;
        }
    }
    private void OnCellDown()
    {
        isOnCellDown = true;
    }
}
