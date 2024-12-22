using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SimpleTouchController1 : MonoBehaviour {

	// PUBLIC
	public delegate void TouchDelegate(Vector2 value);
	public event TouchDelegate TouchEvent1;

	public delegate void TouchStateDelegate(bool touchPresent);
	public event TouchStateDelegate TouchStateEvent1;
	public CharacterMovement charmove;

	// PRIVATE
	[SerializeField]
	private RectTransform joystickArea;
	private bool touchPresent = false;
	public Vector2 movementVector1;


	public Vector2 GetTouchPosition
	{
		get { return movementVector1;}
	}


	public void BeginDrag()
	{
        charmove.fire = true;
        touchPresent = true;
        if (TouchStateEvent1 != null)
			TouchStateEvent1(touchPresent);
	}

	public void EndDrag()
	{
        charmove.fire = false;
        touchPresent = false;
        movementVector1 = joystickArea.anchoredPosition = Vector2.zero;
        if (TouchStateEvent1 != null)
			TouchStateEvent1(touchPresent);

	}

	public void OnValueChanged(Vector2 value)
	{
		if(touchPresent)
		{
            // convert the value between 1 0 to -1 +1
            movementVector1.x = ((1 - value.x) - 0.5f) * 2f;
			movementVector1.y = ((1 - value.y) - 0.5f) * 2f;

			if(TouchEvent1 != null)
			{
                TouchEvent1(movementVector1);
			}
		}

	}

}
