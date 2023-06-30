/*#pragma strict
#pragma implicit
#pragma downcast

var bagName : String = "Set a name for the bag";
var bagBtnName : String = "Bag button name here";
var posBagBtn : Vector2 = Vector2(70,70);//position of the bag button.
var slotSize : float = 60;
var inventorySlotSize : float = 40;
var spacingBetweenSlots : float = 10;
var defaultTextureSlot : Texture;
var inventorySpace : float = 0;
var bagBtn : GUIStyle;
var bagSlotBtn : GUIStyle;
var boxStyle : GUIStyle;
private var numSlot = 16;
private var offsetBag : int;//this is how much are we going to move the index in the parent's bag array
private var showBag : boolean = false;//shows or not this bag
private var bagPos : Vector2;
private var indexOnController : int;
private var tmpTexture : Texture;
private var ctrl : PositionController;
var test : test;
var cursor : MouseCursor1;

function Start(){
	ctrl = transform.parent.GetComponent(PositionController);
}

function OnGUI() {
	if(GUI.Button(Rect(Screen.width - posBagBtn.x, posBagBtn.y - slotSize, slotSize, slotSize),bagBtnName)){
		test.Lerping();
		test.prefab.gameObject.GetComponent.<Renderer>().material.color.a = 0.5;
		var animationObj = test.prefab.gameObject.GetComponentInChildren(Animation);
		animationObj.Play("wall_rotation");
		cursor.originalCursor = cursor.originalCursor1[1];
		
	}

	
	
	// Constrain all drawing to be within a 800x600 pixel area centered on the screen.
	GUI.BeginGroup (new Rect (Screen.width - (posBagBtn.x + 520), posBagBtn.y - slotSize, 510, 60));
    
	// Draw a box in the new coordinate space defined by the BeginGroup.
	// Notice how (0,0) has now been moved on-screen
	GUI.Box (new Rect (0,0,510,60),"");
    for (var i = 0; i < 10; i++){
    	if(GUI.Button(Rect(inventorySlotSize*i + spacingBetweenSlots*(i+1), spacingBetweenSlots,40,40),"jamjam")){
    	
    	}
    }
	// We need to match all BeginGroup calls with an EndGroup
	GUI.EndGroup ();
	
	
}
*/