using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private const float TILE_SIZE = 1.0f;
    [SerializeField] private const float TILE_OFFSET = 0.5f;

    [SerializeField] private Vector2 selection = new Vector2(-1.0f, -1.0f);
    [SerializeField] private GameObject nodePrefab;
    
    [SerializeField] private int widthLine = 8;
    [SerializeField] private int heightLine = 8;
    [SerializeField] private Vector2 offSet = new Vector2(-4.0f, -4.0f);

    [SerializeField] private List<Node> nodeList;

    public static BoardManager Instance;

    ChessAI AICheck;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AICheck = GetComponent<ChessAI>();

        DrawChessBoard();

        AICheck.CheckScore();
    }

    void Update()
    {

    }

    private void DrawChessBoard()
    {
        int nameCounter = 0;
        for (int i = 0; i < heightLine; ++i)
        {
            for (int j = 0; j < widthLine; ++j)
            {
                GameObject GO = Instantiate(nodePrefab, new Vector2(j + offSet.y, i + offSet.x), Quaternion.identity, transform);
                SpriteRenderer sRend = GO.GetComponent<SpriteRenderer>();
                Node node = GO.GetComponent<Node>();

                GO.name = "Node (" + nameCounter + ")";
                nameCounter++;

                //Changing color of board node.
                if (i % 2 == 0 && j % 2 == 0)
                {
                    //Black
                    sRend.color = new Color(0.45f, 0.45f, 0.45f);
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    sRend.color = new Color(0.45f, 0.45f, 0.45f);
                }
                else
                {
                    //White
                    sRend.color = new Color(0.85f, 0.85f, 0.85f);
                }

                //Guideline for setting enum for nodes.
                //0 = None, 1 = Pawn, 2 = Bishop, 3 = Knight, 4 = Rook, 5 = Queen, 6 = King.
                //0 = None, 1 = White, 2 = Black.

                //Setting chess piece type based on counter position.

                //Team
                if (i == 0 || i == 1)
                {
                    node.SetNodeTeam(1);
                }
                else if (i == 6 || i == 7)
                {
                    node.SetNodeTeam(2);
                }

                //Pawn
                if (i == 1 || i == 6)
                {
                    node.SetNodeType(1);
                }

                if (i == 0 || i == 7)
                {
                    if (j == 2 || j == 5)
                    {
                        //Bishop
                        node.SetNodeType(2);
                    }
                    else if (j == 1 || j == 6)
                    {
                        //Knight
                        node.SetNodeType(3);
                    }
                    else if (j == 0 || j == 7)
                    {
                        //Rook
                        node.SetNodeType(4);
                    }
                    else if (j == 3)
                    {
                        //Queen
                        node.SetNodeType(5);
                    }
                    else if (j == 4)
                    {
                        //King
                        node.SetNodeType(6);
                    }
                }
                node.SetCurrentPos(j, i);
                nodeList.Add(node);
            }
        }

        for (int i = 0; i < nodeList.Count; ++i)
        {
            nodeList[i].UpdateNode();
            nodeList[i].StartChessWeight();
        }
    }

    public List<Node> GetNodeList()
    {
        return nodeList;
    }

    public int GetBoardSize
    {
        get
        {
            return widthLine;
        }
    }
}
