using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4f;

    public GameObject dialogBox;

    private float timerDisplay;

    private void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1f;
    }
    private void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;

            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);  
    }
}
