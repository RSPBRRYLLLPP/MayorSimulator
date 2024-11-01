using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager Instance;
    public bool IsGameEnd;
    [Serializable]
    public struct GAME_TIME
    {
        public int hour;
        public int minute;
        public int day;
    }
    // The Ratio is used to transfer real world time to game time.
    // Seconds in Real world/Minutes in game time = ratio
    public float TimePassSpeedRatio;
    public GAME_TIME CurrentGameTime;
    public GAME_TIME GameStartTime;
    public float GameTimeSpeed;
    public GAME_TIME GameEndTime;

    public GameObject UI;
    public int SuccessCount;
    public int FailCount;

    public GameObject GameEndUI;
    public Sprite[] Sprites;
    public enum GAME_ENDING
    {
        DO_NOTHING,
        MULTI_FAILURE,
        FEW_FAILURE,
        PERFECT,
    }

    public GAME_ENDING GameResult;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FailCount = 0;
        SuccessCount = 0;
    }
    public void HandlePlayerMadeChoices(bool success)
    {
        if (success) SuccessCount++;
        else FailCount++;
    }

    public void HandleGameEnd()
    {
        int finalScore = SuccessCount * 20 - FailCount * 10;
        IsGameEnd = true;
        GameEndUI.SetActive(true);
        Sprite spriteToShow = Sprites[0];
        if (finalScore == 100)
        {
            GameResult = GAME_ENDING.PERFECT;
        }
        else if(finalScore<=0)
        {
            spriteToShow = Sprites[3];
            GameResult = GAME_ENDING.DO_NOTHING;
        }
        else if(finalScore>=60)
        {
            spriteToShow = Sprites[1];
            GameResult = GAME_ENDING.FEW_FAILURE;
        }
        else
        {
            spriteToShow = Sprites[2];
            GameResult = GAME_ENDING.MULTI_FAILURE;
        }
        GameEndUI.GetComponent<SpriteRenderer>().sprite = spriteToShow;
        print("Game is End, final score is: " + finalScore + " Result is: " + GameResult);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameEnd) return;
        Time.timeScale = GameTimeSpeed;
        int totalMinutes = (int)(Time.time / TimePassSpeedRatio) + GameStartTime.minute + GameStartTime.hour * 60;
        int minute = totalMinutes % 60;
        int totalHours = (totalMinutes - minute) / 60;
        int hour = totalHours % 24;
        int day = (totalHours - hour) / 24;
        CurrentGameTime.hour = hour;
        CurrentGameTime.minute = minute;
        CurrentGameTime.day = day;
        UI.GetComponent<Text>().text = "Day:" + CurrentGameTime.day + " Hour:" + CurrentGameTime.hour + " Minute:" + CurrentGameTime.minute;
        if(IsTimeABiggerThanTimeB(CurrentGameTime, GameEndTime))
        {
            HandleGameEnd();
        }
    }

    public bool IsTimeABiggerThanTimeB(GAME_TIME A, GAME_TIME B)
    {
        return ((A.day > B.day) 
            || (A.day == B.day && A.hour > B.hour)
            ||(A.day == B.day && A.hour == B.hour && A.minute>B.minute));
    }
   
    public bool IsTimeAEqualOrBiggerThanTimeB(GAME_TIME A, GAME_TIME B)
    {
        return IsTimeAEqualsToTimeB(A, B) || IsTimeABiggerThanTimeB(A, B);
    }

    public bool IsTimeAEqualsToTimeB(GAME_TIME A, GAME_TIME B)
    {
        return A.day==B.day && A.hour == B.hour && A.minute == B.minute;
    }
   
    public GAME_TIME HandleAddTime(GAME_TIME A, GAME_TIME B)
    {
        GAME_TIME result;
        result.day = A.day + B.day;
        int hour= A.hour + B.hour;
        int minute = A.minute + B.minute;
        if(minute>=60)
        {
            hour++;
            minute -= 60;
        }
        else if(minute<0)
        {
            hour--;
            minute = 60 + minute;
        }
        if(hour >= 24)
        {
            result.day++;
            hour -= 24;
        }
        else if(hour<0)
        {
            result.day--;
            hour += 24;
        }
        result.hour = hour;
        result.minute = minute;
        return result;
    }
   
   // //A-B
   // public GAME_TIME HandleSubTimeBFromA(GAME_TIME A, GAME_TIME B)
   // {
   //     B.hour = -B.hour;
   //     B.minute = -B.minute;
   //     return HandleAddTime(A, B);
   // }


}
