using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PM2_5Behavior : BaseBehavior
{
    public GameObject Animation;
    // Start is called before the first frame update
    void Start()
    {
        base.OnStart();
        Animation.SetActive(false);
    }

    public override string GetFeedbackText()
    {
        switch (PlayerChoice)
        {
            case 0:
            case 1:
            case 2:
                return "Hello! Message received. The municipal government has immediately initiated governance actions, and the current handling of the situation is as follows: Upon investigation, the reported situation is true.The traffic planning department has organized forces to carry out standard management at that location, requiring  " 
                    + "(" + GetChoiceText(PlayerChoice) + ")"+ " and continue to pay close attention to the follow-up of this incident. \n This is the reply.";
            default:
                return "Hello! Message received. The municipal government has immediately initiated governance actions, and the current handling of the situation is as follows: Upon investigation, the reported situation is true.The traffic planning department has organized forces to carry out standard management at that location, requiring  _______ and continue to pay close attention to the follow-up of this incident. \n This is the reply.";
        }
    }

    public override bool HandlePlayerConfirmChoice()
    {
        if (PlayerChoice == 2)
        {
            NewsPaperManager.Instance.HandleUpdateImage(ResultImages[0]);
            PollutionManager.Instance.EventAvailable[(int)PollutionManager.POLLUTION_TYPE.PM2_5] = false;
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
                return "prohibition of car travel";
            case 1:
                return "raising oil prices";
            case 2:
                return "use green energy";
            default:
                return null;
        }

    }

    public override void HandleBehaviorStart()
    {
        base.HandleBehaviorStart();
        Animation.SetActive(true);
        UI.GetComponentInChildren<Text>().text = "Dear Mayor:\r\nToday, due to the hazy weather, the air visibility has significantly decreased, resulting in increased road congestion and accidents. The root cause of the problem is believed to be the slow dispersion of motor vehicle exhaust emissions. We are reporting this to you and request that you issue an order to address the haze.\r\n\r\n¡ª¡ªTraffic Safety Department";
    }

    // Update is called once per frame
    void Update()
    {
        base.OnUpdate();
        if (CurrentStatus == BEHAVIOR_STATUS.END)
        {
            Animation.SetActive(false);
        }
    }
}
