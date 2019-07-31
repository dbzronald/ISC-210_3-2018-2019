using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Assets.Scripts.Entities;
using UnityEngine;

public class P_MapManager : MonoBehaviour
{


    public GameObject N1;
    public GameObject N2;
    public GameObject N7;
    public GameObject N9;
    public GameObject N10;

    XmlDocument xmlDoc;
    private const string xmlPath = "Level2";
    private GameObject newCell;
    public GameObject Player;
    public GameObject ChestBlue;
    public GameObject ChestOrange;
    public GameObject ChestGreen;
    public GameObject ChestYellow;
    public GameObject ChestPurple;
    public GameObject ChestRed;
    public GameObject Door;
    private GameEntity _newGameEntity;


    // Start is called before the first frame update
    private void Awake()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Resources.Load<TextAsset>(xmlPath).text);
    }

    private void Start()
    {
        LoadMap(0, 73, 0, 40);
    }

  
    void LoadMap(int xFrom, int xTo, int yFrom, int yTo)
    {
        int xFromCopy = xFrom;
        var selectedNodes = xmlDoc.SelectNodes(string.Format("//map/row[position()>={0} and position()<={1}]", yFrom, yTo));

        foreach (XmlNode currentNode in selectedNodes)
        {
            for (xFrom = xFromCopy; xFrom <= xTo && xFrom < currentNode.InnerText.Length; xFrom++)
            {
                switch (currentNode.InnerText[xFrom])
                {
                    case '1':
                        newCell = N1;
                        break;
                    case '2':
                        newCell = N2;
                        break;
                    case '7':
                        newCell = N7;
                        break;
                    case '9':
                        newCell = N9;
                        break;
                    case '0':
                        newCell = N10;
                        break;

                }

                Instantiate(newCell, new Vector3(xFrom, -yFrom), Quaternion.identity);
            }

            yFrom++;
        }

        selectedNodes = xmlDoc.SelectNodes(string.Format("//level/characters/character"));

        foreach (XmlNode currentNode in selectedNodes) // For every character...
        {
            switch (currentNode.Attributes["prefabName"].Value)
            {
                case "Player":
                    newCell = Player;
                    break;
                case "door":
                    newCell = Door;
                    break;
            }

            _newGameEntity = new GameEntity(newCell, Convert.ToInt32(currentNode.Attributes["id"].Value),
                currentNode.Attributes["uniqueObjectName"].Value, currentNode.Attributes["prefabName"].Value,
                Convert.ToSingle(currentNode.Attributes["posX"].Value),
                -Convert.ToSingle(currentNode.Attributes["posY"].Value), currentNode.Attributes["tag"].Value);

            //Game.Instance().CurrentLevel.Entities.Add(_newGameEntity);

           /* if (newCell.tag == "Player")
            {
                Camera.main.transform.SetParent(newCell.transform);
                Camera.main.transform.localPosition = new Vector3(0, 0, -10);
            }*/
        }

        selectedNodes = xmlDoc.SelectNodes(string.Format("//level/items/item"));

        foreach (XmlNode currentNode in selectedNodes) // For every character...
        {
            switch (currentNode.Attributes["prefabName"].Value)
            {
                case "ChestBlue":
                    newCell = ChestBlue;
                    break;
                case "ChestRed":
                    newCell = ChestRed;
                    break;
                case "ChestYellow":
                    newCell = ChestYellow;
                    break;
                case "ChestOrange":
                    newCell = ChestOrange;
                    break;
                case "ChestPurple":
                    newCell = ChestPurple;
                    break;
                case "ChestGreen":
                    newCell = ChestGreen;
                    break;
            }
            newCell = Instantiate(newCell, new Vector3(Convert.ToSingle(currentNode.Attributes["posX"].Value), -Convert.ToSingle(currentNode.Attributes["posY"].Value)), Quaternion.identity);
            newCell.name = currentNode.Attributes["uniqueObjectName"].Value;
            newCell.tag = currentNode.Attributes["tag"].Value;
            if (newCell.tag == "Player")
            {
                Camera.main.transform.SetParent(newCell.transform);
                Camera.main.transform.localPosition = new Vector3(50, -1, 0);
            }
        }


    }
}


