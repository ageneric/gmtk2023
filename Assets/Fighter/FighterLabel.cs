using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterLabel : MonoBehaviour
{
    public Fighter fighter;

    public Text labelText;

    // Start is called before the first frame update
    void Start()
    {
        fighter = gameObject.GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        labelText.text = fighter.health.ToString() + "/" + fighter.maxHealth.ToString();
    }
}
