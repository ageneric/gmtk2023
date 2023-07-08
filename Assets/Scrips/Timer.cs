using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

    private float TargetTime;
    private int TimeRn = 0;

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
            _title.text = "" + (TargetTime - TimeRn);
            TimeRn += 1;
        }
        
    }
}
