using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QueueController : MonoBehaviour
{
    // Where the release node's index
    // Always rear == 0
    public int Front = 0;

    public Text MainText;
    public Text InputArea;

    public void OnEnQueue()
    {
        string getText = InputArea.text;
        if (Front > 9)
        {
            MainText.text = "Error : Out of Index, Queue is Full! Please Dequeue the Queue!";
            return;
        }
        
        GameManager.Instance.PushNode(Front,getText);
        Front++;
        MainText.text = "EnQueue! " + getText + " Remain Memory : " + (10 - Front).ToString() + "/10";
        
    }

    public void OnDequeue()
    {
        if (Front <=0)
        {
            MainText.text = "Queue is Empty! Please EnQueue the node";
            return;
        }
        MainText.text = "Dequeue! " + GameManager.Instance.GetRearQueue();

        Front--;
        GameManager.Instance.EraseLastQueue(Front);
    }
}
