using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGen : MonoBehaviour
{
    List<List<string>> nameList = new List<List<string>>();

    [SerializeField] public string playerName;

    void Start()
    {
        for (int i = 0; i < 26; i++)
        {
            nameList.Add(new List<string>());
        }

        // A names
        nameList[0].Add("Adrian");
        nameList[0].Add("Abcde");
        nameList[0].Add("Albuquerque");
        nameList[0].Add("Aaron");
        nameList[0].Add("Asparagus");


        // B names
        nameList[1].Add("Bethany");
        nameList[1].Add("Bagel");
        nameList[1].Add("Barbara");
        nameList[1].Add("Bananas");
        nameList[1].Add("Bacteria");

        // C names
        nameList[2].Add("Caleb");
        nameList[2].Add("Chris");
        nameList[2].Add("Cranberry Sauce");
        nameList[2].Add("Calamari");
        nameList[2].Add("Caleb");

        // D names
        nameList[3].Add("Dorian");
        nameList[3].Add("Deedee");
        nameList[3].Add("Dan");
        nameList[3].Add("Deli Cheese");
        nameList[3].Add("Dijon Mustard");

        // E names
        nameList[4].Add("Eric");
        nameList[4].Add("Eggplant");
        nameList[4].Add("Ear");
        nameList[4].Add("Edgy");
        nameList[4].Add("Emile");

        // F names
        nameList[5].Add("Fabian");
        nameList[5].Add("Florida");
        nameList[5].Add("Frankie");
        nameList[5].Add("Flour");
        nameList[5].Add("Flounder");

        // G names
        nameList[6].Add("Gloria");
        nameList[6].Add("Grapes");
        nameList[6].Add("Guy");
        nameList[6].Add("Guava");
        nameList[6].Add("Greenie");

        // H names
        nameList[7].Add("Henry");
        nameList[7].Add("Hazlenut");
        nameList[7].Add("Ham");
        nameList[7].Add("Horseradish");
        nameList[7].Add("Harper");


        // I names
        nameList[8].Add("Igor");
        nameList[8].Add("Ian");
        nameList[8].Add("Iceberg Lettuce");
        nameList[8].Add("Internet");
        nameList[8].Add("Intern");

        // J names
        nameList[9].Add("Jello");
        nameList[9].Add("Jam");
        nameList[9].Add("John");
        nameList[9].Add("Jane");
        nameList[9].Add("Junior");

        // K names
        nameList[10].Add("Karen");
        nameList[10].Add("Kay");
        nameList[10].Add("Kale");
        nameList[10].Add("Kiwi");
        nameList[10].Add("Kidney");

        // L names
        nameList[11].Add("Louise");
        nameList[11].Add("Lily");
        nameList[11].Add("Liam");
        nameList[11].Add("Lemon");
        nameList[11].Add("Low Fat Canola Oil");

        // M names
        nameList[12].Add("Malt Vinegar");
        nameList[12].Add("Mango");
        nameList[12].Add("Mila");
        nameList[12].Add("Molly");
        nameList[12].Add("Marinade");

        // N names
        nameList[13].Add("Nachos");
        nameList[13].Add("Nina");
        nameList[13].Add("Nadia");
        nameList[13].Add("Noodles");
        nameList[13].Add("Natalie");

        // O names
        nameList[14].Add("Oscar");
        nameList[14].Add("Owen");
        nameList[14].Add("Okra");
        nameList[14].Add("Oprah");
        nameList[14].Add("Olive Oil");

        // P names
        nameList[15].Add("Potatoes");
        nameList[15].Add("Pepperoni");
        nameList[15].Add("Patrick");
        nameList[15].Add("Penelope");
        nameList[15].Add("Patricia");

        // Q names
        nameList[16].Add("Quinn");
        nameList[16].Add("Quesadilla");
        nameList[16].Add("Quiche");
        nameList[16].Add("Queso");
        nameList[16].Add("Name That Starts With Q");

        // R names
        nameList[17].Add("Ratatouille");
        nameList[17].Add("Roast Beef");
        nameList[17].Add("Romeo");
        nameList[17].Add("Rachel");
        nameList[17].Add("Ryan");

        // S names
        nameList[18].Add("Steven");
        nameList[18].Add("Samoa");
        nameList[18].Add("Smoothie");
        nameList[18].Add("Salad");
        nameList[18].Add("Sophie");

        // T names
        nameList[19].Add("Tyrone");
        nameList[19].Add("Timmy");
        nameList[19].Add("Taco");
        nameList[19].Add("Tartare Sauce");
        nameList[19].Add("Tempura");

        // U names
        nameList[20].Add("Uriel");
        nameList[20].Add("Udon");
        nameList[20].Add("Unagi");
        nameList[20].Add("Ukelele");
        nameList[20].Add("Udders");

        // V names
        nameList[21].Add("Vera");
        nameList[21].Add("Vanessa");
        nameList[21].Add("Vinegar");
        nameList[21].Add("Vermicelli");
        nameList[21].Add("Valerie");

        // W names
        nameList[22].Add("Wasabi");
        nameList[22].Add("Wheat Bread");
        nameList[22].Add("Walter");
        nameList[22].Add("Wilson");
        nameList[22].Add("Walnut");

        // X names
        nameList[23].Add("Xavier");
        nameList[23].Add("Xander");
        nameList[23].Add("Xylophone");
        nameList[23].Add("Xeroses");
        nameList[23].Add("Xenon");

        // Y names
        nameList[24].Add("Yasmine");
        nameList[24].Add("Yemen");
        nameList[24].Add("Yogurt");
        nameList[24].Add("Yams");
        nameList[24].Add("Yolk");

        // Y names
        nameList[25].Add("Zack");
        nameList[25].Add("Zeke");
        nameList[25].Add("Zucchini");
        nameList[25].Add("Zooplankton");
        nameList[25].Add("Zygote");
    }

    // Given player name, returns randomly selected name from list
    public string GetRandomName(string playerName)
    {
        char firstLetter = char.ToLower(playerName[0]);
        int letter = (int)firstLetter - (int)'a';
        return nameList[letter][Random.Range(0, nameList[0].Count)];
    }

    private void Update()
    {
        Debug.Log(GetRandomName(playerName));
    }
}