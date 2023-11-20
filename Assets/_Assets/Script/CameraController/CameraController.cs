using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    private Transform obstruction;
    
    private void Start()
    {
        obstruction = target.transform;
    }

    private void Update()
    {
       // UpdateZoomController(); da riscrivere per farla funzionare con chinemachine camera
    }

    private void UpdateZoomController(){

        RaycastHit hit;
        var maxDistance = transform.position - target.transform.position;
        //var layerMask = 4.5f;
        if(Physics.Raycast(transform.position, maxDistance, out hit, 4.5f)){
            
        //zoomSpeed = 2f;

            if(hit.collider.TryGetComponent(out Player Player)){
        
                if( Vector3.Distance(target.transform.position, transform.position) < 4.5f)
                {
                    transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                }
            } else{
                
                obstruction = hit.transform;
                if(Vector3.Distance(obstruction.position, transform.position) >= 3f && Vector3.Distance(target.transform.position, transform.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                }
            
            }
        }
    }
}
