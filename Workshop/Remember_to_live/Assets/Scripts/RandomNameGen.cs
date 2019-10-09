using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGen : MonoBehaviour
{
    [SerializeField]
    private string[26][5] randomNameList;

    private string playerName;

    void Start()
    {
        // A names
        randomNameList[0] = new string[]
        {
            "Adrian",
            "Abcde",
            "Alyssa",
            "Aaron",
            "Abel",
        };

        // B names
        randomNameList[1] = new string[]
        {
            "Bethany",
            "Bruce",
            "Barbara",
            "Bonnie",
            "Brian",
        };

        // C names
        randomNameList[2] = new string[]
        {
            "Caleb",
            "Chris",
            "Cara",
            "Calamari",
            "Caleb",
        };

        // D names
        randomNameList[3] = new string[]
        {
            "Dorian",
            "Deedee",
            "Dan",
            "David",
            "Diego",
        };

        // E names
        randomNameList[4] = new string[]
        {
            "Adrian",
            "Abcde",
            "Alyssa",
            "Aaron",
            "Abel",
        };
    }

    public string GetRandomName(string playerName)
    {
        int letter = (int)'a' - playerName[0];
        Debug.Log(letter);
        return randomNameList[letter][Random.Range(0, 5)];
    }

    private void Update()
    {
        Debug.Log(GetRandomName("Boooooo"));
    }
}