
    function Update () {
        var screenPos : Vector3 = GetComponent.<Camera>().WorldToScreenPoint(Input.mousePosition);
        print ("target is " + screenPos.x + " pixels from the left");
    }