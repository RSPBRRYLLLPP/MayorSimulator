using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavior : MonoBehaviour
{
    public GameTimeManager.GAME_TIME DefaultLastTime;
    public GameTimeManager.GAME_TIME DefaultBehaviorEndTime;
    public BEHAVIOR_STATUS CurrentStatus;
    public GameObject UI;
    public int PlayerChoice;
    public Sprite[] ResultImages;
    public enum BEHAVIOR_STATUS
    {
        NOT_START,
        STARTED,
        END,
    }

    public void HandlePlayerInput(int choice)
    {
        PlayerChoice = choice;
    }

    public virtual bool HandlePlayerConfirmChoice()
    {
        return false;
    }

    public virtual void HandleBehaviorStart()
    {
        CurrentStatus = BEHAVIOR_STATUS.STARTED;
        UI.SetActive(true);
        DefaultBehaviorEndTime = GameTimeManager.Instance.HandleAddTime(GameTimeManager.Instance.CurrentGameTime, DefaultLastTime);
    }

    public void OnUpdate()
    {
        if(GameTimeManager.Instance.IsTimeAEqualOrBiggerThanTimeB(GameTimeManager.Instance.CurrentGameTime,DefaultBehaviorEndTime)
            && CurrentStatus == BEHAVIOR_STATUS.STARTED)
        {
            PollutionManager.Instance.HandleCurrentStatusEnd();
            this.HandleBehaviorEnd();
        }
    }

    public virtual void HandleBehaviorEnd()
    {
        CurrentStatus = BEHAVIOR_STATUS.END;
        UI.SetActive(false);
    }

    public void OnStart()
    {
        UI.SetActive(false);
        PlayerChoice = -1;
        CurrentStatus = BEHAVIOR_STATUS.NOT_START;
    }

    public virtual string GetFeedbackText()
    {
        return null;
    }

    public virtual string GetChoiceText(int i)
    {
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
