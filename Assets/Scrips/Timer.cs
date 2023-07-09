using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

    private float TargetTime;
    public int TimeRn = 0;
    public EndGame eg;

    [SerializeField] 
    private TMP_Text _title;

    // Start is called before the first frame update
    void Start() {
        TargetTime = Time.time + 300;
        TimeRn = 0;
    }

    // Update is called once per frame
    void Update() {
        if (TimeRn < Time.time + 1) {
            _title.text = "" + Mathf.Floor(TargetTime - TimeRn);
            TimeRn += 1;
        }
        if(Time.time >= TargetTime)
        {
            eg.initEndgame();
        }
    }
}
