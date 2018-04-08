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
        Debug.Log("AI looking for movables!");

        AIMovables.Clear();
        AIActionsList.Clear();

        for (int i = 0; i < board.GetNodeList().Count; ++i)
        {
            Node node = board.GetNodeList()[i];

            if (node.GetNodeTeam() == Node.NodeTeam.BLACK)
            {
                AIMovables.Add(node);
            }
        }

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

    public void AIMakeAction()
    {
        Debug.Log("AI is Doing Stuff!");

        bool isAvailable = false;

        while (!isAvailable)
        {
            chosenPieceMoves.Clear();
            previousNode = null;
            nodeToMove = null;

            int randomPiece = 0;
            int randomAction = 0;

            randomPiece = Random.Range(0, AIMovables.Count);
            previousNode = AIMovables[randomPiece]; //The randomly chosen chess piece.

            if (previousNode.GetNodesToCheck().Count > 0)
            {
                for (int i = 0; i < previousNode.GetNodesToCheck().Count; ++i)
                {
                    chosenPieceMoves.Add(previousNode.GetNodesToCheck()[i]);
                }

                randomAction = Random.Range(0, chosenPieceMoves.Count);
                nodeToMove = chosenPieceMoves[randomAction]; //The randomly chosen chess move.
                
                if (nodeToMove.GetNodeType() == Node.NodeType.KING)
                {
                    player.SetCheckmate(true);
                }

                //Updates to change for new node.
                nodeToMove.SetNodeType((int)previousNode.GetNodeType());
                nodeToMove.SetNodeTeam((int)previousNode.GetNodeTeam());

                nodeToMove.UpdateNode();
                nodeToMove.UpdateMoveCounter(previousNode.GetMoveCounter + 1);
                nodeToMove.UpdateChessWeight(previousNode.GetChessWeight);

                //Reset the previous node.
                previousNode.CheckEmpty();
                previousNode.GetNodesToCheck().Clear();

                for (int i = 0; i < board.GetNodeList().Count; ++i)
                {
                    board.GetNodeList()[i].UpdateNode();
                }

                isAvailable = true;
            }
            else
            {
                continue;
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
