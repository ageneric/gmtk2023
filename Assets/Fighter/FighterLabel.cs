using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FighterLabel : MonoBehaviour
{
    private Fighter fighter;
    public TextMesh labelText;
    public Transform healthBarFill;

    // Start is called before the first frame update
    void Start()
    {
        fighter = gameObject.GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        string NewLine = System.Environment.NewLine;
        labelText.text = fighter.username;

        if (Input.GetButton("Enable Debug Button 1"))
        {
            labelText.text += NewLine + Mathf.Round(fighter.health).ToString() + "/" + fighter.maxHealth.ToString() + "HP"
                            + NewLine + "Hack = " + (fighter.hacks.Count > 0 ? fighter.hacks[0] : "")
                            + NewLine;
        }

        float healthBarScale = Mathf.Clamp01(fighter.health / fighter.maxHealth);
        healthBarFill.localScale = new Vector3(healthBarScale, 1f, 1f);
    }
}
