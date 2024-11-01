using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseBehavior : BaseBehavior
{
    public GameObject Animation;
    public GameObject SuccessResultImage;
    // Start is called before the first frame update
    void Start()
    {
        base.OnStart();
        Animation.SetActive(false);
    }

    public override string GetFeedbackText()
    {
        return "Hello! Message received. The municipal government immediately ordered the commercial street management department to handle the situation, and the current handling is as follows:Upon investigation, the reported situation is true.We have organized forces to carry out noise decibel standard management at that location, and a new decibel measuring instrument has been installed.We request continued and close attention to the follow - up of this incident.\n This is the reply.";
    }

    public override bool HandlePlayerConfirmChoice()
    {
        PollutionManager.Instance.EventAvailable[(int)PollutionManager.POLLUTION_TYPE.NOISE] = false;
        NewsPaperManager.Instance.HandleUpdateImage(ResultImages[0]);
        SuccessResultImage.SetActive(true);
        return true;
    }

    public override void HandleBehaviorStart()
    {
        base.HandleBehaviorStart();
        Animation.SetActive(true);
        UI.GetComponentInChildren<Text>().text = "Dear Mayor,\r\n  the shop makes noise every night, and I have had a headache for a week! Building a beautiful motherland requires us young flowers!";
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
