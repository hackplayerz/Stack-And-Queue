using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Stack : MonoBehaviour
{
	public Text MainText;
	public Text GetText;
	public int LastIndex = 0;
	
	/// <summary>
	/// Push the element to Stack
	/// It will be save on Stack's top and you will be get element to Func Pop()
	/// </summary>
	public void Push()
	{
		if (LastIndex > 9)
		{
			MainText.text = "Overflow Stack! Plz Pop the element to Push new Element!";
			return;
		}
		string pushText = GetText.text;

		Debug.Log("Pushed " + pushText);
		GameManager.Instance.PushNode(LastIndex,pushText);
		LastIndex++;
		MainText.text = "Push ! " + pushText + " Remain Stack Memory : " + (10 - LastIndex).ToString() + " / 10";
	}
	/// <summary>
	/// Get last pushed element and reduce index 1
	/// </summary>
	public void Pop()
	{
		if (LastIndex <= 0)
		{
			LastIndex = 0;
			MainText.text = "Error ! Index is lower than 0 plz Push new Element!";
		}
		else
		{
			LastIndex--;
			MainText.text = "POP ! " + GameManager.Instance.PopNode(LastIndex);
		}
	}
}
