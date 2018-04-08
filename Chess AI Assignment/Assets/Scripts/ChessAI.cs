using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
    BoardManager board;
    PlayerInput player;

    [SerializeField] private List<Node> AIMovables;

    [SerializeField] int playerScore = 0;
    [SerializeField] int AIScore = 0;

    void Awake()
    {
        board = GetComponent<BoardManager>();
        player = GetComponent<PlayerInput>();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CheckScore()
    {
        AIScore = 0;
        playerScore = 0;

        for (int i = 0; i < board.GetNodeList().Count; ++i)
        {
            Node node = board.GetNodeList()[i];

            if (node.GetNodeTeam() == Node.NodeTeam.BLACK)
            {
                AIScore += node.GetChessWeight;
            }
            else if (node.GetNodeTeam() == Node.NodeTeam.WHITE)
            {
                playerScore += node.GetChessWeight;
            }
        }
    }

    public void GetAIMovables()
    {
        Debug.Log("AI looking for movables!");

        AIMovables.Clear();

        for (int i = 0; i < board.GetNodeList().Count; ++i)
        {
            Node node = board.GetNodeList()[i];

            if (node.GetNodeTeam() == Node.NodeTeam.BLACK)
            {
                AIMovables.Add(node);
            }
        }
    }

    public void AIAction()
    {
        Debug.Log("AI is Doing Stuff!");

        int randomPiece = 0;
        int randomAction = 0;
        
        for (int i = 0; i < AIMovables.Count; ++i)
        {
            Node node = AIMovables[i];

            for (int j = 0; j < node.GetNodesToCheck().Count; ++j)
            {
                randomPiece = Random.Range(0, AIMovables.Count);
                randomAction = Random.Range(0, node.GetNodesToCheck().Count);
            }
        }
    }

    public int GetAIWeight
    {
        get
        {
            return AIScore;
        }
    }

    public int GetPlayerWeight
    {
        get
        {
            return playerScore;
        }
    }
}
