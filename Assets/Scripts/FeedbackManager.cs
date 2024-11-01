using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager Instance;
    public GameObject FeedbackWindow;
    public GameObject FinalConfirmPic;
    public Text TitleText;
    public GameObject[] Buttons;
    public GameObject FinalConfirm;
    public GameObject MiniMap;
    /// <summary>
    /// 
    /// </summary>
    public GameObject Book;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HandleActiveOrDeactiveBook()
    {
        if(Book.activeSelf)
        {
            GameTimeManager.Instance.GameTimeSpeed = 1f;
            Book.SetActive(false);
        }
        else
        {
            GameTimeManager.Instance.GameTimeSpeed = 0.01f;
            Book.SetActive(true);
        }
    }

    public void HandleActiveOrDeActiveFeedbackWindow()
    {
        if (FeedbackWindow.activeSelf)
        {
            GameTimeManager.Instance.GameTimeSpeed = 1f;
            FeedbackWindow.SetActive(false);
        }
        else
        {
            GameTimeManager.Instance.GameTimeSpeed = 0.01f;
            FeedbackWindow.SetActive(true);
            StampManager.Instance.Stamps[1].transform.localPosition = Vector3.zero;
        }
    }

    public void UpdateLetterBody()
    {
        TitleText.text = PollutionManager.Instance.GetBehavior((PollutionManager.POLLUTION_TYPE)PollutionManager.Instance.currentActiveIndex).GetFeedbackText();
    }

    public void HandlePlayerChooseConfirm()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
        StampManager.Instance.HandlePlayAnimation();
        PollutionManager.Instance.HandlePlayerFinalConfirm();
        FinalConfirm.SetActive(false);
        StartCoroutine(waitAndSetFinalPic());
    }

    IEnumerator waitAndSetFinalPic()
    {
        yield return new WaitForSeconds(0.025f);
        FinalConfirmPic.SetActive(true);
    }

    public void HandleEventTriggered()
    {
        FinalConfirmPic.SetActive(false);
        FinalConfirm.SetActive(true);
        TitleText.text = PollutionManager.Instance.GetBehavior((PollutionManager.POLLUTION_TYPE)PollutionManager.Instance.currentActiveIndex).GetFeedbackText();
        if(PollutionManager.Instance.currentActiveIndex == (int)PollutionManager.POLLUTION_TYPE.WATER_POLLUTION)
        {
            MiniMap.SetActive(true);
        }
        else
        {
            MiniMap.SetActive(false);
        }
        for(int i = 0;i< Buttons.Length;i++)
        {
            string buttonText = PollutionManager.Instance.GetBehavior((PollutionManager.POLLUTION_TYPE)PollutionManager.Instance.currentActiveIndex).GetChoiceText(i);
            if(buttonText!=null)
            {
                Buttons[i].SetActive(true);
                Buttons[i].GetComponentInChildren<Text>().text = buttonText;
            }
            else
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
