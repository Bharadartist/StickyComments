using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject PlaceCube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Vector3 instantiatePosition = Camera.main.transform.position;
                GameObject.Instantiate(PlaceCube, instantiatePosition, Quaternion.identity);
            }
        }
    }
}
