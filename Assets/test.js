var prefab : GameObject;
var collider : Collider;
var activated : boolean = false;
var endPoint : Vector3;
var duration : float = 1.0;
private var startPoint : Vector3;
private var startTime : float;
var objectHolding : boolean = false;

     
function Start() {
    startTime = Time.time;	
}

function Update () {
		
    }
    
    
function Lerping(){
		while (objectHolding == false) {
		var hit: RaycastHit;
 		var movetarget : float;
 		var startpoint = hit.point;
        var ray : Ray = GetComponent.<Camera>().ScreenPointToRay (Input.mousePosition );
        Debug.DrawRay (ray.origin, ray.direction * 100, Color.yellow);
        if (Physics.Raycast(ray, hit)) {
        print(hit.distance);
        movetarget = hit.distance;
        if(activated == false){
        //Instantiate(prefab, hit.point, transform.rotation);
        activated = true;
        }
        startPoint = hit.point;
   		}
        //prefab.transform.Translate(jamjam);
		prefab.transform.position = Vector3.Lerp(startPoint, hit.point, (Time.time - startTime) / duration);
		yield;
		}
};