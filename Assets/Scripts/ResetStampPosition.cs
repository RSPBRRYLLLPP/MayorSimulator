using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStampPosition : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.localPosition = Vector3.zero;
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
