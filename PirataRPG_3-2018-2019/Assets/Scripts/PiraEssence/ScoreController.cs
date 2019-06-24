using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    Dictionary<string, int> essencesScores = new Dictionary<string, int>
    {
        {"Blue",0},
        {"Orange",0},
        {"Green",0},
        {"Yellow",0},
        {"Purple",0},
        {"Red",0}
    };

    public TextMesh Blue;
    public TextMesh Orange;
    public TextMesh Green;
    public TextMesh Yellow;
    public TextMesh Purple;
    public TextMesh Red;

    // Start is called before the first frame update
    void Start()
    {
        ResetTextScores();
    }

    void ResetTextScores()
    {
        Blue.text = essencesScores["Blue"].ToString();
        Orange.text = essencesScores["Orange"].ToString();
        Green.text = essencesScores["Green"].ToString();
        Yellow.text = essencesScores["Yellow"].ToString();
        Purple.text = essencesScores["Purple"].ToString();
        Red.text = essencesScores["Red"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEssence(string essenceTag)
    {
        essencesScores[essenceTag]++;

        switch (essenceTag)
        {
            case "Blue":
                Blue.text = essencesScores[essenceTag].ToString();
                break;
            case "Orange":
                Orange.text = essencesScores[essenceTag].ToString();
                break;
            case "Green":
                Green.text = essencesScores[essenceTag].ToString();
                break;
            case "Yellow":
                Yellow.text = essencesScores[essenceTag].ToString();
                break;
            case "Purple":
                Purple.text = essencesScores[essenceTag].ToString();
                break;
            case "Red":
                Red.text = essencesScores[essenceTag].ToString();
                break;
        }
    }
}
