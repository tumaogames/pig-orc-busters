using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour {

    string bagName = "Set a name for the bag";
    string bagBtnName = "Bag button name here";
    Vector2 posBagBtn = new Vector2(70,70);//position of the bag button.
    float slotSize = 60;
    float inventorySlotSize = 40;
    float spacingBetweenSlots = 10;
    public Texture defaultTextureSlot;
    float inventorySpace = 0.0f;
    GUIStyle bagBtn;
    GUIStyle bagSlotBtn;
    GUIStyle boxStyle;
    int numSlot = 16;
    int offsetBag = 0;//this is how much are we going to move the index in the parent's bag array
    bool showBag = false;//shows or not this bag
    Vector2 bagPos;
    int indexOnController;
    Texture tmpTexture;
    PositionController ctrl;
    public MouseCursor1 cursor;
    public test test;

    // Use this for initialization
    void Start()
    {
        ctrl = transform.parent.GetComponent<PositionController>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnGUI()
    {
        
        if (GUI.Button(new Rect(Screen.width - posBagBtn.x, posBagBtn.y - slotSize, slotSize, slotSize), bagBtnName))
        {
            test.Lerping = true;
           cursor.originalCursor = cursor.originalCursor1[1];

        }



        // Constrain all drawing to be within a 800x600 pixel area centered on the screen.
        GUI.BeginGroup(new Rect(Screen.width - (posBagBtn.x + 520), posBagBtn.y - slotSize, 510, 60));

        // Draw a box in the new coordinate space defined by the BeginGroup.
        // Notice how (0,0) has now been moved on-screen
        GUI.Box(new Rect(0, 0, 510, 60), "");
        for (var i = 0; i < 10; i++)
        {
            if (GUI.Button(new Rect(inventorySlotSize * i + spacingBetweenSlots * (i + 1), spacingBetweenSlots, 40, 40), "jamjam"))
            {

            }
        }
        // We need to match all BeginGroup calls with an EndGroup
        GUI.EndGroup();
    }
}





