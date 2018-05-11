using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
    BoardManager board;
    PlayerInput player;

    MoveWeights moveWeights = new MoveWeights();

    [SerializeField] private List<Node> whiteMovables = new List<Node>();
    [SerializeField] private List<Node> whiteActions = new List<Node>();

    [SerializeField] private List<Node> blackMovables = new List<Node>();
    [SerializeField] private List<Node> blackActions = new List<Node>();

    [SerializeField] private List<Node> chosenPieceMoves = new List<Node>();

    [SerializeField] int whiteScore = 0;
    [SerializeField] int blackScore = 0;
    [SerializeField] int totalScore = 0;
    [SerializeField] int maxDepth = 4;

    [SerializeField] Node previousNode;
    [SerializeField] Node nodeToMove;
    [SerializeField] Node bestMove;
    [SerializeField] Node tempNode;

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
        blackScore = 0;
        whiteScore = 0;

        for (int i = 0; i < board.GetNodeList().Count; ++i)
        {
            Node node = board.GetNodeList()[i];

            if (node.GetNodeTeam() == Node.NodeTeam.BLACK)
            {
                blackScore += node.GetChessWeight;
            }
            else if (node.GetNodeTeam() == Node.NodeTeam.WHITE)
            {
                whiteScore += node.GetChessWeight;
            }
        }

        GetTotalScore();
    }

    public int GetTotalScore()
    {
        totalScore = blackScore - whiteScore;

        return totalScore;
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
            randomPiece = Random.Range(0, blackMovables.Count);
            previousNode = blackMovables[randomPiece]; 

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

    public int CalculateMinMax(int depth, int alpha, int beta, bool max)
    {
        GetBoardState();

        if (depth == 0)
        {
            return Evaluate();
        }
        if (max)
        {
            int score = -1000000;
            for (int i = 0; i < blackMovables.Count; ++i)
            {
                Node selectedNode = blackMovables[i];

                for (int j = 0; j < selectedNode.GetNodesToCheck().Count; ++j)
                {
                    Node selectedAction = selectedNode.GetNodesToCheck()[i];
                    tempNode = selectedAction;

                    DoFakeMove(selectedNode, selectedAction);
                    
                    score = CalculateMinMax(depth - 1, alpha, beta, false);

                    UndoFakeMove(selectedNode, selectedAction, tempNode);

                    if (score > alpha)
                    {
                        selectedNode.SetScore(score);
                        if (selectedNode.GetScore > selectedAction.GetScore && depth == maxDepth)
                        {
                            bestMove = selectedNode;
                        }
                        alpha = score;
                    }
                    if (score >= beta)
                    {
                        break;
                    }
                }
            }
            return alpha;
        }
        else
        {
            int score = 1000000;
            for (int i = 0; i < whiteMovables.Count; ++i)
            {
                Node selectedNode = blackMovables[i];

                for (int j = 0; j < selectedNode.GetNodesToCheck().Count; ++j)
                {
                    Node selectedAction = selectedNode.GetNodesToCheck()[i];
                    tempNode = selectedAction;

                    DoFakeMove(selectedNode, selectedAction);

                    score = CalculateMinMax(depth - 1, alpha, beta, false);

                    UndoFakeMove(selectedNode, selectedAction, tempNode);

                    if (score > alpha)
                    {
                        selectedNode.SetScore(score);
                        if (selectedNode.GetScore > selectedAction.GetScore && depth == maxDepth)
                        {
                            bestMove = selectedNode;
                        }
                        alpha = score;
                    }
                    if (score >= beta)
                    {
                        break;
                    }
                }
            }
            return beta;
        }
    }

    int Evaluate()
    {
        float pieceDifference = 0;
        float whiteWeight = 0;
        float blackWeight = 0;

        for (int i = 0; i < whiteMovables.Count; ++i)
        {
            Node node = whiteMovables[i];

            whiteWeight += moveWeights.GetBoardWeight(node.GetNodeType(), node.GetNodePosition(), Node.NodeTeam.WHITE);
        }
        for (int i = 0; i < blackMovables.Count; ++i)
        {
            Node node = blackMovables[i];

            blackWeight += moveWeights.GetBoardWeight(node.GetNodeType(), node.GetNodePosition(), Node.NodeTeam.BLACK);
        }

        pieceDifference = (blackScore + (blackWeight / 100)) - (whiteScore + (whiteWeight / 100));
        return Mathf.RoundToInt(pieceDifference * 100);
    }

    public void DoFakeMove(Node selectedNode, Node targetNode)
    {
        targetNode = selectedNode;
        selectedNode = null;
    }

    public void UndoFakeMove(Node selectedNode, Node targetNode, Node tempNode)
    {
        selectedNode = targetNode;
        targetNode = tempNode;
        tempNode = null;
    }

    List<Move> GetMoves(Node.NodeTeam nodeTeam)
    {
        List<Move> turnMove = new List<Move>();
        List<Node> pieces = new List<Node>();

        if (nodeTeam == Node.NodeTeam.BLACK)
        {
            pieces = blackMovables;
        }
        else
        {
            pieces = whiteMovables;
        }

        for (int i = 0; i < pieces.Count; ++i)
        {
            Node selected = pieces[i];

            for (int j = 0; j < selected.GetNodesToCheck().Count; ++j)
            {
                Node action = selected.GetNodesToCheck()[j];
                

            }
        }
        return turnMove;
    }

    Move CreateMove(Node selectedNode, Node nodeToMove)
    {
        Move tempMove = new Move
        {
            firstPosition = selectedNode,
            pieceMoved = selectedNode,
            secondPosition = nodeToMove
        };

        if (nodeToMove != null)
        {
            tempMove.pieceKilled = nodeToMove;
        }

        return tempMove;
    }

    public void GetBoardState()
    {
        blackMovables.Clear();
        whiteMovables.Clear();
        blackScore = 0;
        whiteScore = 0;

        GetBlackMoves();
        GetWhiteMoves();
    }

    public void GetBlackMoves()
    {
        blackMovables.Clear();

        for (int i = 0; i < board.GetNodeList().Count; ++i)
        {
            Node node = board.GetNodeList()[i];

            if (node.GetNodeTeam() == Node.NodeTeam.BLACK)
            {
                blackMovables.Add(node);
            }
        }
    }

    public void GetWhiteMoves()
    {
        whiteMovables.Clear();

        for (int i = 0; i < board.GetNodeList().Count; ++i)
        {
            Node node = board.GetNodeList()[i];

            if (node.GetNodeTeam() == Node.NodeTeam.WHITE)
            {
                whiteMovables.Add(node);
            }
        }
    }

    public void GetBlackActions()
    {
        blackActions.Clear();

        for (int i = 0; i < blackMovables.Count; ++i)
        {
            Node node = blackMovables[i];

            for (int j = 0; j < node.GetNodesToCheck().Count; ++j)
            {
                Node actionNode = node.GetNodesToCheck()[j];

                blackActions.Add(actionNode);
            }
        }
    }

    public void GetWhiteActions()
    {
        whiteActions.Clear();

        for (int i = 0; i < whiteMovables.Count; ++i)
        {
            Node node = whiteMovables[i];

            for (int j = 0; j < node.GetNodesToCheck().Count; ++j)
            {
                Node actionNode = node.GetNodesToCheck()[j];

                whiteActions.Add(actionNode);
            }
        }
    }

    public int GetAIWeight
    {
        get
        {
            return blackScore;
        }
    }

    public int GetPlayerWeight
    {
        get
        {
            return whiteScore;
        }
    }
    
}
