using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
    BoardManager board;
    PlayerInput player;

    [SerializeField] private List<Node> AIMovables = null;
    [SerializeField] private List<Node> AIActionsList = null;
    [SerializeField] private List<Node> chosenPieceMoves = null;

    [SerializeField] int playerScore = 0;
    [SerializeField] int AIScore = 0;

    [SerializeField] Node previousNode;
    [SerializeField] Node nodeToMove;

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
        //Debug.Log("AI looking for movables!");

        //Update score before checking.
        CheckScore();

        GetBlackMoves();

        GetBlackActions();
    }

    public void AIMakeAction()
    {
        //Debug.Log("AI is doing stuff!");

        bool isAvailable = false;

        while (!isAvailable)
        {
            chosenPieceMoves.Clear();
            previousNode = null;
            nodeToMove = null;

            int randomPiece = 0;
            int randomAction = 0;

            //Random choose a piece to use.
            randomPiece = Random.Range(0, AIMovables.Count);
            previousNode = AIMovables[randomPiece]; 

            //Checks if chosen pieces has valid moves.
            if (previousNode.GetNodesToCheck().Count > 0)
            {
                for (int i = 0; i < previousNode.GetNodesToCheck().Count; ++i)
                {
                    chosenPieceMoves.Add(previousNode.GetNodesToCheck()[i]);
                }

                //Chooses a random valid node to move to, base on chosen chess piece.
                randomAction = Random.Range(0, chosenPieceMoves.Count);
                nodeToMove = chosenPieceMoves[randomAction];

                player.UpdateInputs(nodeToMove, previousNode);

                //Update score after action.
                CheckScore();

                isAvailable = true;
            }
            else
            {
                continue;
            }
        }
    }

    public void CalculateHeuristics()
    {

    }

    public void GetBlackMoves()
    {
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

    public void GetBlackActions()
    {
        AIActionsList.Clear();

        for (int i = 0; i < AIMovables.Count; ++i)
        {
            Node node = AIMovables[i];

            for (int j = 0; j < node.GetNodesToCheck().Count; ++j)
            {
                Node actionNode = node.GetNodesToCheck()[j];

                AIActionsList.Add(actionNode);
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
