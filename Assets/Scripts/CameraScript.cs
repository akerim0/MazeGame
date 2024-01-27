using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]float mouseSpeed = 30.0f;
    [SerializeField]float UpAngle = 300.0f;
    [SerializeField]float DownAngle = 30.0f;
    bool canTurn;
    Quaternion originalCamPos;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        canTurn = false;
        StartCoroutine(waitFor(3.0f));
        originalCamPos = this.GetComponent<Transform>().rotation;
        //transform.rotation = Quaternion.Euler(21.0f, 0, 0);
        Debug.Log("Camera position : " + originalCamPos.eulerAngles.x);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate with target
        //Quaternion targetAngle = Quaternion.Euler (transform.rotation.eulerAngles.x,target.rotation.eulerAngles.y,0.0f);
        //transform.rotation = Quaternion.Slerp(transform.rotation,targetAngle,0.3f);
        if (canTurn)
        {
            transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime);
        }
        //Up and Down View 
        float xAng = transform.rotation.eulerAngles.x;
        if(xAng > DownAngle && xAng < 180 )
        {
            transform.rotation = Quaternion.Euler(DownAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); ;
        }
        if (xAng < UpAngle && xAng > 180)
        {
            transform.rotation = Quaternion.Euler(UpAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }



    }
    IEnumerator waitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canTurn = true;
    }
   
}
