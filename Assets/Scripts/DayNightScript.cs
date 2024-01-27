using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    bool day, night;
    float elapsedTime = 0.0f;
    [SerializeField] float MinX = -90.0f;
    [SerializeField] float MaxX = 270.0f;
    private float timeFormat = 192.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(elapsedTime < timeFormat)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / timeFormat;
            float Xrotation = Mathf.Lerp(MinX,MaxX,alpha);
            transform.rotation = Quaternion.Euler(Xrotation,transform.rotation.y,transform.rotation.z);
        }
        else
        {
            elapsedTime = 0.0f;
        }
        
    }
}
