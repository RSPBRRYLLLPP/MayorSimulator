using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionManager : MonoBehaviour
{
    public static PollutionManager Instance;
    public float TimeIntervalTriggeringEvents;
    private float timer;
    public int currentActiveIndex;
    public enum POLLUTION_TYPE
    {
        ACID_RAIN,
        PM2_5,
        WATER_POLLUTION,
        NOISE,
        PLASTIC_POLLUTION,
    }

    public bool[] EventAvailable;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public BaseBehavior GetBehavior(POLLUTION_TYPE type)
    {
        switch(type)
        {
            case POLLUTION_TYPE.ACID_RAIN:
                return this.GetComponent<AcidRainBehavior>();
            case POLLUTION_TYPE.PM2_5:
                return this.GetComponent<PM2_5Behavior>();
            case POLLUTION_TYPE.WATER_POLLUTION:
                return this.GetComponent<WaterPollutionBehavior>();
            case POLLUTION_TYPE.NOISE:
                return this.GetComponent<NoiseBehavior>();
            case POLLUTION_TYPE.PLASTIC_POLLUTION:
                return this.GetComponent<PlasticPollutionBehavior>();
            default:
                return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        EventAvailable = new bool[System.Enum.GetValues(typeof(POLLUTION_TYPE)).Length];
        for(int i = 0;i<EventAvailable.Length;i++)
        {
            if(GetBehavior((POLLUTION_TYPE)i)!=null)
            {
                EventAvailable[i] = true;
            }
        }
        currentActiveIndex = -1;
    }

    public void HandlePlayerInput(int index)
    {
        GetBehavior((POLLUTION_TYPE)currentActiveIndex).HandlePlayerInput(index);
        FeedbackManager.Instance.UpdateLetterBody();
    }

    public void HandlePlayerFinalConfirm()
    {
        if(GetBehavior((POLLUTION_TYPE)currentActiveIndex).HandlePlayerConfirmChoice())
        {
            GameTimeManager.Instance.HandlePlayerMadeChoices(true);
        }
        else GameTimeManager.Instance.HandlePlayerMadeChoices(false);
    }


    public void HandleCurrentStatusEnd()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameTimeManager.Instance.IsGameEnd) return;
        timer += Time.deltaTime;
        if(timer >= TimeIntervalTriggeringEvents 
            && (currentActiveIndex == -1 ||(currentActiveIndex != -1 && GetBehavior((POLLUTION_TYPE)currentActiveIndex).CurrentStatus == BaseBehavior.BEHAVIOR_STATUS.END)))
        {
            int count = 0;
            //triger events here.
            while(count<=500)
            {
                count++;
                int randomIndex = Random.Range(0, EventAvailable.Length);
                if(EventAvailable[randomIndex])
                {
                    currentActiveIndex = randomIndex;
                    GetBehavior((POLLUTION_TYPE)randomIndex).HandleBehaviorStart();
                    FeedbackManager.Instance.HandleEventTriggered();
                    break;
                }
            }
            bool isAllComplete = true;
            for(int i = 0;i<EventAvailable.Length;i++)
            {
                if (EventAvailable[i]) isAllComplete = false;
            }
            if(isAllComplete)
            {
                GameTimeManager.Instance.HandleGameEnd();
            }
            timer = 0;
        }
    }
}
