using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsPaperManager : MonoBehaviour
{
    public static NewsPaperManager Instance;
    public GameObject ImageNode;
    public Sprite[] InitialImages;
    private bool isInitImage;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isInitImage = true;
    }

    public void HandleUpdateImage(Sprite sprite)
    {
        ImageNode.GetComponent<Image>().sprite = sprite;
        isInitImage = false;
    }

    public void HandleActiveOrDeactiveImageNode()
    {
        if(ImageNode.activeSelf)
        {
            ImageNode.SetActive(false);
            GameTimeManager.Instance.GameTimeSpeed = 1f;
        }
        else
        {
            ImageNode.SetActive(true);
            if(isInitImage)
            {
                ImageNode.GetComponent<Image>().sprite = InitialImages[Random.Range(0, InitialImages.Length)];
            }
            GameTimeManager.Instance.GameTimeSpeed = 0.01f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
