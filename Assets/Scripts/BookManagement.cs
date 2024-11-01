using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManagement : MonoBehaviour
{
    public int Index;
    public Image ImageNode;
    public Sprite[] Sprites;
    public GameObject LeftButton;
    public GameObject RightButton;
    // Start is called before the first frame update
    void Start()
    {
        Index = 0;
        LeftButton.SetActive(false);
    }

    public void HandleNextPage()
    {
        Index++;
        LeftButton.SetActive(true);
        if(Index>=Sprites.Length - 1)
        {
            Index = Sprites.Length - 1;
            RightButton.SetActive(false);
        }
        ImageNode.sprite = Sprites[Index];
    }

    public void HandlePrevPage()
    {
        Index--;
        RightButton.SetActive(true);
        if (Index <= 0)
        {
            Index = 0;
            LeftButton.SetActive(false);
        }
        ImageNode.sprite = Sprites[Index];
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
