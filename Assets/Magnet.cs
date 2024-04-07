using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            Global.obj1 = other.gameObject;
            Global.obj2 = gameObject;
            EventManager.TriggerEvent("OnMagnetTouched");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            EventManager.TriggerEvent("OffMagnetTouched");
        }
    }
}
