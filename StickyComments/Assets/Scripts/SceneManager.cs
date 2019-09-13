using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private GameObject stickerPrefab;
    private GameObject stickerCanvas;
    private GameObject sticker = null;
    private Touch touch_first, touch_second, touch_third;
    private Vector3 startPosition = Vector3.zero;
    private float scaleMagnitude = 0f;
    public float moveFactor = .001f;
    public float scaleFactor = .1f;

    // Start is called before the first frame update
    void Start()
    {
        stickerCanvas = GameObject.FindGameObjectWithTag("StickerCanvas");
        stickerPrefab = Resources.Load("Prefabs/StickerPrefab") as GameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Vector3 instantiatePosition = Camera.main.transform.position;
                GameObject.Instantiate(PlaceCube, instantiatePosition, Quaternion.identity);
            }
        }*/
        if(Input.touchCount == 1)
        {
            touch_first = Input.GetTouch(0);
            if(touch_first.phase == TouchPhase.Began)
            {
                TouchDown();
            }
            else if(touch_first.phase == TouchPhase.Moved)
            {
                Touched();
            }
            else if(touch_first.phase == TouchPhase.Ended)
            {

            }

        }
        else if(Input.touchCount == 2)
        {
            touch_first = Input.GetTouch(0);
            touch_second = Input.GetTouch(1);
            if(touch_second.phase == TouchPhase.Moved)
            {
                if (sticker != null)
                {
                    ScaleTouch();
                }
                
            }
        }
        else if(Input.touchCount == 3)
        {
            touch_first = Input.GetTouch(0);
            touch_second = Input.GetTouch(1);
            touch_third = Input.GetTouch(2);
            if (touch_third.phase == TouchPhase.Began)
            {
                if (sticker != null)
                {
                    sticker.transform.Rotate(0f, 180f, 0f);
                }
            }            
        }

        if (Input.GetMouseButton(0))
        {
            
            if (Input.GetMouseButtonDown(0))
            {                
                //PressedDown(); 
            }

            if (sticker != null)
            {
                //Scaling();
                //sticker.transform.Rotate(0f, 180f, 0f);
                //Pressed();
            }
            else
            {
                //print("Null");
            }                     
        }

        if (Input.GetMouseButtonUp(0))
        {
            PressedUp();
        }
    }

    public void CreateSticker(Sprite imageSprite)
    {
        GameObject bird = Instantiate(stickerPrefab, stickerCanvas.transform);
        bird.GetComponent<Image>().sprite = imageSprite;
    }

    public void TouchDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touch_first.position);

        Physics.Raycast(ray, out hit, 50.0f);
        if (hit.collider != null)
        {
            sticker = hit.collider.gameObject;
        }
        Debug.Log("Down");
        startPosition = touch_first.position;
        Debug.Log("StartPosition: " + startPosition);
    }

    public void Touched()
    {
        //Debug.Log(sticker.gameObject.name);
        Vector3 movePosition = touch_first.position;
        movePosition-= startPosition;
        Debug.Log("MovePosition: " + movePosition);
        startPosition = touch_first.position;
        Debug.Log("StartPosition: " + startPosition);
        movePosition.z = 0;
        sticker.transform.localPosition = sticker.transform.localPosition + (movePosition * moveFactor);
        Debug.Log("StickerPosition: " + sticker.transform.localPosition);
    }

    public void TouchUp()
    {
        sticker = null;
        Debug.Log("Up");
        startPosition = Vector3.zero;
        scaleMagnitude = 0;
    }

    public void PressedDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, 50.0f);
        if (hit.collider != null)
        {
            sticker = hit.collider.gameObject;
        }
        Debug.Log("Down");

        startPosition = Input.mousePosition;
        
        Debug.Log("StartPosition: " + startPosition);
    }

    public void Pressed()
    {
        //Debug.Log(sticker.gameObject.name);
        Vector3 movePosition = Input.mousePosition - startPosition;
        Debug.Log("MovePosition: " + movePosition);
        startPosition = Input.mousePosition;
        Debug.Log("StartPosition: " + startPosition);
        movePosition.z = 0;
        sticker.transform.localPosition = sticker.transform.localPosition + (movePosition * moveFactor);
        Debug.Log("StickerPosition: " + sticker.transform.localPosition);
    }

    public void PressedUp()
    {
        //sticker = null;
        Debug.Log("Up");
        //startPosition = Vector3.zero;
        scaleMagnitude = 0;
    }

    public void Scaling()
    {
        Vector3 scaleVector = Input.mousePosition - startPosition;
        Vector3 multiplyFactor = new Vector3(1f, 1f, 0f);
        Vector3 transformScaleValue = Vector3.zero;
        Debug.Log("ScaleVector: " + scaleVector);
        float scaleValue = scaleVector.magnitude * scaleFactor;
        Debug.Log("ScaleValue: " + scaleValue);
        Debug.Log("ScaleMagnitude: " + scaleMagnitude);
        if (scaleMagnitude < scaleValue)
        {
            transformScaleValue = sticker.transform.localScale + (multiplyFactor * scaleValue);
        }
        else if(scaleMagnitude > scaleValue)
        {
            transformScaleValue = sticker.transform.localScale - (multiplyFactor * scaleValue);
        }
        if (transformScaleValue.x > 0.0f && transformScaleValue.x < 20.0f)
        {
            sticker.transform.localScale = transformScaleValue;
            scaleMagnitude = scaleValue;
        }
        Debug.Log("LocalScale: " + sticker.transform.localScale);
    }

    public void ScaleTouch()
    {
        Vector3 scaleVector = touch_first.position - touch_second.position;
        Vector3 multiplyFactor = new Vector3(1f, 1f, 0f);
        Vector3 transformScaleValue = Vector3.zero;
        Debug.Log("ScaleVector: " + scaleVector);
        float scaleValue = scaleVector.magnitude * scaleFactor;
        Debug.Log("ScaleValue: " + scaleValue);
        Debug.Log("ScaleMagnitude: " + scaleMagnitude);        
        if (scaleMagnitude < scaleValue)
        {
            transformScaleValue = sticker.transform.localScale + (multiplyFactor * scaleValue);
        }
        else if (scaleMagnitude > scaleValue)
        {
            transformScaleValue = sticker.transform.localScale - (multiplyFactor * scaleValue);
        }
        if (transformScaleValue.x > 0.0f && transformScaleValue.x < 20.0f)
        {
            sticker.transform.localScale = transformScaleValue;
            scaleMagnitude = scaleValue;
        }
        Debug.Log("LocalScale: " + sticker.transform.localScale);
    }
}
