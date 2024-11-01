using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    public static StampManager Instance;
    public GameObject[] Stamps;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandlePlayAnimation()
    {
        StartCoroutine(playAni());
    }

    IEnumerator playAni()
    {
        Stamps[0].SetActive(true);
        Stamps[0].GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(0.5f/100);
        Stamps[0].SetActive(false);
        Stamps[1].SetActive(true);
        yield return new WaitForSeconds(0.01f);
        Stamps[1].GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(0.01f);
        Stamps[1].SetActive(false);
    }
}
