using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private GameObject stickerPrefab;
    private GameObject stickerCanvas;

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
    }

    public void CreateSticker(Sprite imageSprite)
    {
        GameObject bird = Instantiate(stickerPrefab, stickerCanvas.transform);
        bird.GetComponent<Image>().sprite = imageSprite;
    }
}
