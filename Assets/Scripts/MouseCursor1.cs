using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor1 : MonoBehaviour {
    int cursorSizeX = 32; // set to width of your cursor texture
    int cursorSizeY = 32; // set to height of your cursor texture

    bool showOriginal = true;
    public Texture2D[] originalCursor1;
    public Texture2D originalCursor;
    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        originalCursor = originalCursor1[0];
        //Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnGUI()
    {

        if (showOriginal == true)
        {
           GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorSizeX / 2 + cursorSizeX / 2, (Screen.height - Input.mousePosition.y) - cursorSizeY / 2 + cursorSizeY / 2, cursorSizeX, cursorSizeY), originalCursor);
        }
    }
}

