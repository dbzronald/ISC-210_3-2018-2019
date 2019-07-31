using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Assets.Scripts.Entities;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;
    public GameObject F;
    public GameObject G;
    public GameObject H;
    public GameObject I;
    public GameObject J;
    public GameObject K;
    //public GameObject L;
    public GameObject M;
    public GameObject N;
    public GameObject O;
    public GameObject P;
    public GameObject Q;
    public GameObject R;
    public GameObject S;
    public GameObject T;
    public GameObject U;
    public GameObject V;
    public GameObject W;
    public GameObject X;
    public GameObject Y;
    public GameObject Z;
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    public GameObject e;
    public GameObject f;
    public GameObject g;
    XmlDocument xmlDoc;
    private const string xmlPath = "Level1";
    private GameObject newCell;
    public GameObject Player;
    public GameObject ChestBlue;
    public GameObject ChestOrange;
    public GameObject ChestGreen;
    public GameObject ChestYellow;
    public GameObject ChestPurple;
    public GameObject ChestRed;
    private GameEntity _newGameEntity;


    // Start is called before the first frame update
    private void Awake()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Resources.Load<TextAsset>(xmlPath).text);
    }

    private void Start()
    {
        LoadMap(0,73,0,40);
    }

    void LoadMap(int xFrom, int xTo, int yFrom, int yTo)
    {
        int xFromCopy = xFrom;
        var selectedNodes = xmlDoc.SelectNodes(string.Format("//map/row[position()>={0} and position()<={1}]",yFrom,yTo));

        foreach (XmlNode currentNode in selectedNodes)
        {
            for (xFrom = xFromCopy; xFrom <= xTo && xFrom < currentNode.InnerText.Length; xFrom++)
            {
                switch (currentNode.InnerText[xFrom])
                {
                    case 'A':
                        newCell = A;
                        break;
                    case 'B':
                        newCell = B;
                        break;
                    case 'C':
                        newCell = C;
                        break;
                    case 'D':
                        newCell = D;
                        break;
                    case 'E':
                        newCell = E;
                        break;
                    case 'F':
                        newCell = F;
                        break;
                    case 'G':
                        newCell = G;
                        break;
                    case 'H':
                        newCell = H;
                        break;
                    case 'I':
                        newCell = I;
                        break;
                    case 'J':
                        newCell = J;
                        break;
                    case 'K':
                        newCell = K;
                        break;
                    case 'M':
                        newCell = M;
                        break;
                    case 'N':
                        newCell = N;
                        break;
                    case 'O':
                        newCell = O;
                        break;
                    case 'Q':
                        newCell = Q;
                        break;
                    case 'R':
                        newCell = R;
                        break;
                    case 'S':
                        newCell = S;
                        break;
                    case 'T':
                        newCell = T;
                        break;
                    case 'U':
                        newCell = U;
                        break;
                    case 'V':
                        newCell = V;
                        break;
                    case 'W':
                        newCell = W;
                        break;
                    case 'X':
                        newCell = X;
                        break;
                    case 'Y':
                        newCell = Y;
                        break;
                    case 'Z':
                        newCell = Z;
                        break;
                    case 'a':
                        newCell = a;
                        break;
                    case 'b':
                        newCell = b;
                        break;
                    case 'c':
                        newCell = c;
                        break;
                    case 'd':
                        newCell = d;
                        break;
                    case 'e':
                        newCell = e;
                        break;
                    case 'f':
                        newCell = f;
                        break;
                    case 'g':
                        newCell = g;
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
            }

            _newGameEntity = new GameEntity(newCell, Convert.ToInt32(currentNode.Attributes["id"].Value),
                currentNode.Attributes["uniqueObjectName"].Value, currentNode.Attributes["prefabName"].Value, 
                Convert.ToSingle(currentNode.Attributes["posX"].Value),
                -Convert.ToSingle(currentNode.Attributes["posY"].Value), currentNode.Attributes["tag"].Value);

            Game.Instance().CurrentLevel.Entities.Add(_newGameEntity);

            if (newCell.tag == "Player")
            {
                Camera.main.transform.SetParent(newCell.transform);
                Camera.main.transform.localPosition = new Vector3(0, 0, -10);
            }
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
                Camera.main.transform.localPosition = new Vector3(0, 0, -10);
            }
        }


    }
}
