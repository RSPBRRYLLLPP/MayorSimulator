using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcidRainBehavior : BaseBehavior
{
    public GameObject Animation;
    public RealisticRainDrop RainDrop;
    // Start is called before the first frame update
    void Start()
    {
        base.OnStart();
        Animation.SetActive(false);
        RainDrop.enabled = false;
    }

    public override string GetFeedbackText()
    {
        switch(PlayerChoice)
        {
            case 0:
            case 1:
            case 2:
                return "Hello! Message received. \n  The municipal government has immediately issued an order to control the acid rain, and the current handling of the situation is as follows:\nUpon investigation, the reported situation is true. It is required that  " + "(" + GetChoiceText(PlayerChoice)+ ")" + " factory reduce the emission of acidic substances and continue to pay close attention to the follow-up of this incident.";
            default:
                return "Hello! Message received. \n  The municipal government has immediately issued an order to control the acid rain, and the current handling of the situation is as follows:\nUpon investigation, the reported situation is true. It is required that _______ factory reduce the emission of acidic substances and continue to pay close attention to the follow-up of this incident.";
        }
    }

    public override bool HandlePlayerConfirmChoice()
    {
        if (PlayerChoice == 0)
        {
            PollutionManager.Instance.EventAvailable[(int)PollutionManager.POLLUTION_TYPE.ACID_RAIN] = false;
            NewsPaperManager.Instance.HandleUpdateImage(ResultImages[0]);
            return true;
        }
        NewsPaperManager.Instance.HandleUpdateImage(ResultImages[1]);
        return false;
    }

    public override string GetChoiceText(int i)
    {
        switch (i)
        {
            case 0:
                return "chemical plant";
            case 1:
                return "textile factory";
            case 2:
                return "power plant";
            default:
                return null;
        }
        
    }

    public override void HandleBehaviorStart()
    {
        base.HandleBehaviorStart();
        Animation.SetActive(true);
        RainDrop.enabled = true;
        UI.GetComponentInChildren<Text>().text = "It's raining acid in the city. ¡®It stings so bad on my head!¡¯ It¡¯s like you can hear the plants¡¯ inner voices saying this.";
    }

    // Update is called once per frame
    void Update()
    {
        base.OnUpdate();
        if(CurrentStatus == BEHAVIOR_STATUS.END)
        {
            Animation.SetActive(false);
            RainDrop.enabled = false;
        }
    }
}
