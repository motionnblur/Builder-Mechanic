using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject cube, cubeGhost;
    [SerializeField] GameObject magnet;
    private GameObject objForHighlight;
    private bool isOnCellDown = false;
    private bool instantiateLock = true;
    private int clickCount = 0;
    private bool isMagnetTouched = false;
    private void OnEnable()
    {
        EventManager.AddListener("OnRaycastHit", OnRaycastHit);
        EventManager.AddListener("OnMouseButtonDown", OnMouseButtonDown);
        EventManager.AddListener("OnCellDown", OnCellDown);
        EventManager.AddListener("OnMagnetTouched", OnMagnetTouched);
        EventManager.AddListener("OffMagnetTouched", OffMagnetTouched);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener("OnRaycastHit", OnRaycastHit);
        EventManager.RemoveListener("OnMouseButtonDown", OnMouseButtonDown);
        EventManager.RemoveListener("OnCellDown", OnCellDown);
        EventManager.RemoveListener("OnMagnetTouched", OnMagnetTouched);
        EventManager.RemoveListener("OffMagnetTouched", OffMagnetTouched);
    }
    private void Awake()
    {
        objForHighlight = Instantiate(cubeGhost);
        objForHighlight.SetActive(false);
        objForHighlight.GetComponent<Renderer>().material.color = new Color(0.4428177f, 0.61199f, 0.8773585f, 0.5f);

        magnet = Instantiate(magnet, Vector3.zero, Quaternion.identity);
    }
    private void OnRaycastHit(Vector3 hitPoint)
    {
        magnet.transform.position = hitPoint;

        if(!isMagnetTouched)
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
    private void OnMagnetTouched()
    {
        isMagnetTouched = true;

        Vector3 obj1Pos = Global.obj1.transform.position;
        Vector3 obj2Pos = Global.obj2.transform.position;
        Vector3 dist = (obj2Pos - obj1Pos).normalized;

        float dot = Mathf.Abs(Vector3.Dot(dist, Global.obj1.transform.forward));
        if(dot >= 0.95f && dot <= 1.0f){
            Debug.Log("up-down");
        }else if(dot >= 0f && dot <= 0.1f){
            Debug.Log("left-right");
        }
    }
    private void OffMagnetTouched()
    {
        isMagnetTouched = false;
    }
}
