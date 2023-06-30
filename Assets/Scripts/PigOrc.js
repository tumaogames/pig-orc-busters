#pragma strict


function Start () {
	GetComponent.<Animation>().Play("Idle");
}

function OnTriggerEnter (other : Collider) {
    if(other.gameObject.tag == "Player"){
    	print('hmmmp');
    	GetComponent.<Animation>().Play("BackFlip");
    };
}
