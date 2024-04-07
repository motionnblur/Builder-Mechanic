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
    private Vector3 magnetPoint = Vector3.zero;
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
            Instantiate(cube, isMagnetTouched ? magnetPoint : hitPoint, Quaternion.identity);
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
        Vector3 obj1Pos = Global.obj1.transform.position;
        Vector3 obj2Pos = Global.obj2.transform.position;
        Vector3 dist = (obj2Pos - obj1Pos).normalized;

        float dot = Mathf.Abs(Vector3.Dot(dist, Global.obj1.transform.forward));
        bool isLeftRight = false;
        bool isUpDown = false;

        if(obj1Pos.x > obj2Pos.x) isLeftRight = true;
        if(obj1Pos.y > obj2Pos.y) isUpDown = true;
        
        if(dot >= 0.97f && dot <= 1.0f){
            isMagnetTouched = true;
            
            objForHighlight.transform.position = new Vector3(
                Global.obj1.transform.position.x,
                objForHighlight.transform.position.y,
                Global.obj1.transform.position.z + (isUpDown ? Global.obj1.transform.localScale.z: -Global.obj1.transform.localScale.z)
            );
            
            magnetPoint = objForHighlight.transform.position;
        }else if(dot >= 0f && dot <= 0.15f){
            isMagnetTouched = true;

            objForHighlight.transform.position = new Vector3(
                Global.obj1.transform.position.x + (isLeftRight ? -Global.obj1.transform.localScale.x : Global.obj1.transform.localScale.x),
                objForHighlight.transform.position.y,
                Global.obj1.transform.position.z
            );

            magnetPoint = objForHighlight.transform.position;
        }
    }
    private void OffMagnetTouched()
    {
        isMagnetTouched = false;
        objForHighlight.transform.position = Vector3.zero;
    }
}
