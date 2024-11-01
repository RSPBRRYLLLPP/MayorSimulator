using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPollutionBehavior : BaseBehavior
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
        return "Hello! Message received. We have now provided the municipal government with our recommended plan. We hope that the factory construction will go smoothly and that the supervision and implementation of waste purification will be strengthened.\n This is the reply.";
    }

    public override string GetChoiceText(int i)
    {
        switch (i)
        {
            case 0:
                return "LEFT";
            case 1:
                return "MIDDLE";
            case 2:
                return "RIGHT";
            default:
                return null;
        }

    }

    public override void HandleBehaviorStart()
    {
        base.HandleBehaviorStart();
        Animation.SetActive(true);
        UI.GetComponentInChildren<Text>().text = "Dear Mayor,\r\n Our city is planning to build a new textile factory by the end of the month, which is expected to discharge dye waste and fiber materials. The location has not been finalized yet, and we would like to seek your opinion on this matter.";
    }

    public override bool HandlePlayerConfirmChoice()
    {
        if (PlayerChoice == 1)
        {
            NewsPaperManager.Instance.HandleUpdateImage(ResultImages[0]);
            PollutionManager.Instance.EventAvailable[(int)PollutionManager.POLLUTION_TYPE.WATER_POLLUTION] = false;
            return true;
        }
        NewsPaperManager.Instance.HandleUpdateImage(ResultImages[1]);
        return false;
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
