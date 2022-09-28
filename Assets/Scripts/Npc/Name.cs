using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basically a randomizer to make sure each NPC's has a unique name
/// based on it sex.
/// <see cref="Npc"/>
/// </summary>
public class Name: MonoBehaviour
{
    private static Name instance;
    public static Name Instance { get { return instance; } }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private string[] _maleNames =
    {
        "Alan",
        "Adam",
        "Alfred",
        "Arnold",
        "Arthur",
        "Andrew",
        "Anthony",
        "Benjamin",
        "Chris",
        "Connor",
        "Charles",
        "Christian",
        "Daniel",
        "David",
        "Damian",
        "Dustin",
        "Dominic",
        "Elijah",
        "Ethan",
        "George",
        "Harry",
        "Han",
        "Jason",
        "John",
        "James",
        "Jonathan",
        "Jacob",
        "Jack",
        "Liam",
        "Lucas",
        "Luke",
        "Max",
        "Michael",
        "Matthew",
        "Noah",
        "Nicholas",
        "Oliver",
        "Owen",
        "Thomas",
        "William"
    };

    private string[] _femaleNames =
    {
        "Ava",
        "Audrey",
        "Alice",
        "Allison",
        "Amelia",
        "Brooklyn",
        "Bella",
        "Caroline",
        "Charlotte",
        "Camila",
        "Chloe",
        "Claire",
        "Emma",
        "Evelyn",
        "Eva",
        "Emily",
        "Elizabeth",
        "Eleanor",
        "Grace",
        "Lily",
        "Lucy",
        "Mia",
        "Maya",
        "Natalie",
        "Naomi",
        "Olivia",
        "Sadie",
        "Scarlett",
        "Sophie",
        "Riley",
        "Ruby",
        "Victoria",
        "Zoey",
    };

    private string[] _surnames =
    {
        "Adams",
        "Allen",
        "Anderson",
        "Brown",
        "Bennet",
        "Baker",
        "Clark",
        "Cruz",
        "Collins",
        "Carter",
        "Cooper",
        "Davis",
        "Evans",
        "Harris",
        "Hill",
        "Hall",
        "Howard",
        "Jackson",
        "Jones",
        "Johnson",
        "Long",
        "King",
        "Kelly",
        "Lee",
        "Lewis",
        "Mitchell",
        "Miller",
        "Murphy",
        "Moore",
        "Martinez",
        "Nelson",
        "Phillips",
        "Peterson",
        "Price",
        "Smith",
        "Sanchez",
        "Stewart",
        "Scott",
        "Thomas",
        "Thompson",
        "Taylor",
        "Torres",
        "Turner",
        "Williams",
        "White",
        "Wright",
        "Walker",
        "Willson",
        "Watson",
        "Young",
    };

    /// <summary>
    /// <see cref="Npc.AssignName"/>
    /// </summary>
    /// <returns>
    /// a string symbolizing name that is chosen
    /// based on NPC's sex
    /// </returns>
    public string GetRandomName(string sex)
    {
        string firstName;
        string lastName;

        if (sex == Constants.MALE)
        {
            firstName = _maleNames[Random.Range(0, _maleNames.Length)];
        } else
        {
            firstName = _femaleNames[Random.Range(0, _femaleNames.Length)];
        }

        lastName = _surnames[Random.Range(0, _surnames.Length)];

        string name = $"{firstName} {lastName}";

        return name;
    }
}
