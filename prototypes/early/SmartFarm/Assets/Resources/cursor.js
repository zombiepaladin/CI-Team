var crossHair : GameObject;
var layerMask = 1 << 8;
var disappear = new Vector3(0, 0, 0);
var hit : RaycastHit;

function Update () 
{
    Screen.showCursor = false; 
    ray = camera.ScreenPointToRay (Input.mousePosition);


    if (Physics.Raycast (ray, hit, Mathf.Infinity,layerMask)) 
    {
        crossHair.transform.rotation = Quaternion.FromToRotation (Vector3.up, hit.normal);
        crossHair.transform.position = hit.point + (hit.normal * 1.0);
    }	
    
    else 
    {
        crossHair.transform.position = disappear;
    }		
}