using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public enum NodeType
    {
        NONE = 0,
        PAWN = 1,
        BISHOP = 2,
        KNIGHT = 3,
        ROOK = 4,
        QUEEN = 5,
        KING = 6
    };

    public enum NodeTeam
    {
        NONE = 0,
        WHITE = 1,
        BLACK = 2
    };

    [SerializeField] private NodeType nodeType;
    [SerializeField] private NodeTeam nodeTeam;

    [SerializeField] private Vector2 currentPos;
    [SerializeField] private List<Node> nodesToCheck;

    [SerializeField] private GameObject pawnPrefab;
    [SerializeField] private GameObject bishopPrefab;
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject rookPrefab;
    [SerializeField] private GameObject queenPrefab;
    [SerializeField] private GameObject kingPrefab;

    [SerializeField] private GameObject currentPrefab;

    [SerializeField] private int pawnStep;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetCurrentPos(int x, int y)
    {
        currentPos.x = x;
        currentPos.y = y;
    }

    public NodeType GetNodeType()
    {
        return nodeType;
    }

    public void SetNodeType(int type)
    {
        nodeType = (NodeType)type;
    }

    public NodeTeam GetNodeTeam()
    {
        return nodeTeam;
    }

    public void SetNodeTeam(int team)
    {
        nodeTeam = (NodeTeam)team;
    }

    public void UpdateNode()
    {
        if (nodeType == NodeType.PAWN)
        {
            CheckPawn();
        }
        else if (nodeType == NodeType.BISHOP)
        {
            CheckBishop();
        }
        else if (nodeType == NodeType.KNIGHT)
        {
            CheckKnight();
        }
        else if (nodeType == NodeType.ROOK)
        {
            CheckRook();
        }
        else if (nodeType == NodeType.QUEEN)
        {
            CheckQueen();
        }
        else if (nodeType == NodeType.KING)
        {
            CheckKing();
        }
        else
        {
            CheckEmpty();
        }
    }

    public void CheckPawn()
    {
        currentPrefab = Instantiate(pawnPrefab, transform.position, Quaternion.identity, transform);
        SpriteRenderer sRend = currentPrefab.GetComponent<SpriteRenderer>();

        if (nodeTeam == NodeTeam.WHITE)
        {
            sRend.color = Color.white;
        }
        else if (nodeTeam == NodeTeam.BLACK)
        {
            sRend.color = Color.black;
        }
    }

    public void CheckPawnMovement()
    {
        nodesToCheck.Clear();

        BoardManager board = GetComponentInParent<BoardManager>();
        List<Node> nodeList = board.GetNodeList();

        for (int i = 0; i < nodeList.Count; ++i)
        {
            //Checking relevant positions for the pawn.
            Node node = nodeList[i];
            if (nodeTeam == NodeTeam.WHITE)
            {
                //Checking for movables, may add in a check for enemies who can attack it.
                if (node.currentPos.x == currentPos.x && node.currentPos.y == currentPos.y + 1)
                {
                    if (node.nodeType == NodeType.NONE)
                    {
                        nodesToCheck.Add(node);
                    }
                }
                if (pawnStep == 0)
                {
                    if (node.currentPos.x == currentPos.x && node.currentPos.y == currentPos.y + 2)
                    {
                        if (node.nodeType == NodeType.NONE)
                        {
                            nodesToCheck.Add(node);
                        }
                    }
                }
                //Checking for attackables
                if (node.currentPos.x == currentPos.x - 1 && node.currentPos.y == currentPos.y + 1)
                {
                    if (node.nodeTeam == NodeTeam.BLACK)
                    {
                        Debug.Log("There's some enemy to attack on the left!");
                    }
                }
                if (node.currentPos.x == currentPos.x + 1 && node.currentPos.y == currentPos.y + 1)
                {
                    if (node.nodeTeam == NodeTeam.BLACK)
                    {
                        Debug.Log("There's some enemy to attack on the right!");
                    }
                }
            }
            else if (nodeTeam == NodeTeam.BLACK)
            {
                if (node.currentPos.x == currentPos.x && node.currentPos.y == currentPos.y - 1)
                {
                    nodesToCheck.Add(node);
                }
                if (pawnStep == 0)
                {
                    if (node.currentPos.x == currentPos.x && node.currentPos.y == currentPos.y - 2)
                    {
                        nodesToCheck.Add(node);
                    }
                }
            }
        }

        for (int i = 0; i < nodesToCheck.Count; ++i)
        {
            Node node = nodesToCheck[i];

            SpriteRenderer sRend = node.GetComponent<SpriteRenderer>();
            sRend.color = Color.green;
        }
    }

    public void CheckBishop()
    {
        currentPrefab = Instantiate(bishopPrefab, transform.position, Quaternion.identity, transform);
        SpriteRenderer sRend = currentPrefab.GetComponent<SpriteRenderer>();

        if (nodeTeam == NodeTeam.WHITE)
        {
            sRend.color = Color.white;
        }
        else if (nodeTeam == NodeTeam.BLACK)
        {
            sRend.color = Color.black;
        }
    }

    public void CheckKnight()
    {
        currentPrefab = Instantiate(knightPrefab, transform.position, Quaternion.identity, transform);
        SpriteRenderer sRend = currentPrefab.GetComponent<SpriteRenderer>();

        if (nodeTeam == NodeTeam.WHITE)
        {
            sRend.color = Color.white;
        }
        else if (nodeTeam == NodeTeam.BLACK)
        {
            sRend.color = Color.black;
        }
    }

    public void CheckRook()
    {
        currentPrefab = Instantiate(rookPrefab, transform.position, Quaternion.identity, transform);
        SpriteRenderer sRend = currentPrefab.GetComponent<SpriteRenderer>();

        if (nodeTeam == NodeTeam.WHITE)
        {
            sRend.color = Color.white;
        }
        else if (nodeTeam == NodeTeam.BLACK)
        {
            sRend.color = Color.black;
        }
    }

    public void CheckQueen()
    {
        currentPrefab = Instantiate(queenPrefab, transform.position, Quaternion.identity, transform);
        SpriteRenderer sRend = currentPrefab.GetComponent<SpriteRenderer>();

        if (nodeTeam == NodeTeam.WHITE)
        {
            sRend.color = Color.white;
        }
        else if (nodeTeam == NodeTeam.BLACK)
        {
            sRend.color = Color.black;
        }
    }

    public void CheckKing()
    {
        currentPrefab = Instantiate(kingPrefab, transform.position, Quaternion.identity, transform);
        SpriteRenderer sRend = currentPrefab.GetComponent<SpriteRenderer>();

        if (nodeTeam == NodeTeam.WHITE)
        {
            sRend.color = Color.white;
        }
        else if (nodeTeam == NodeTeam.BLACK)
        {
            sRend.color = Color.black;
        }
    }

    public void CheckEmpty()
    {
        nodeType = NodeType.NONE;
        nodeTeam = NodeTeam.NONE;

        for (int i = 0; i < nodesToCheck.Count; ++i)
        {

        }
        
        Destroy(currentPrefab);
    }

    public Vector2 GetNodePosition()
    {
        return currentPos;
    }

    public List<Node> GetNodesToCheck()
    {
        return nodesToCheck;
    }
}
