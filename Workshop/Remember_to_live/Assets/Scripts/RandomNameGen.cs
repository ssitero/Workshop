using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGen : MonoBehaviour
{
    [SerializeField]
    private string[] randomNameList;

    void Awake()
    {
        randomNameList = new string[]
        {
            "Adrian",
            "Brian",
            "Karen",
            "Melissa",
            "Frances",
            "Abcde",
            "Susan",
        };
    }
	
    public static string GetRandomName()
    {
        return randomNameList[Random.Range(0, randomNameList.Length)];
    }
}
