using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamMouseLook : MonoBehaviour
{
	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
	public bool mouseMove = true;
	public bool zoom = false;

	GameObject character;
	
    // Start is called before the first frame update
    void Start(){
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update() 
    {
    	Application.targetFrameRate = 70;
    	
    	if((Cursor.lockState == CursorLockMode.Locked) && (mouseMove == true)){
	        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
			
	        if(zoom == false){
				md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
	        }else if(zoom == true){
				md = Vector2.Scale(md, new Vector2((sensitivity / 4) * smoothing, (sensitivity / 4) * smoothing));
	        }

			smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;
			mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
			
			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
			character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
		}
	}
}
