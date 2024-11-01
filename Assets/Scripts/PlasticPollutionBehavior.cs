using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlasticPollutionBehavior : BaseBehavior
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
        return "Hello! We have contacted the relevant garbage disposal department to inspect and repair the garbage disposal facilities around the residential area and find out where the problem is. We will definitely clean up the garbage this week and not disturb your work. Thank you for your letter, and please feel free to contact us if you encounter any problems in the future.\n This is the reply.";
    }
    public override void HandleBehaviorStart()
    {
        base.HandleBehaviorStart();
        Animation.SetActive(true);
        UI.GetComponentInChildren<Text>().text = "Mayor£¬we are farmers who grow wheat. This morning, when we got up and went to the fields, we saw a lot of white plastic bags. This will make it hard for the plants to grow. What should we do? They said we could write a letter to you, so here we are. Will it be useful?";
    }

    public override bool HandlePlayerConfirmChoice()
    {
        NewsPaperManager.Instance.HandleUpdateImage(ResultImages[0]);
        PollutionManager.Instance.EventAvailable[(int)PollutionManager.POLLUTION_TYPE.PLASTIC_POLLUTION] = false;
        return true;
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
