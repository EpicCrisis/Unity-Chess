using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
    BoardManager board;
    PlayerInput player;

    //MoveWeights moveWeights;

    [SerializeField] private List<Node> whiteMovables = new List<Node>();
    [SerializeField] private List<Node> whiteActions = new List<Node>();

    [SerializeField] private List<Node> blackMovables = new List<Node>();
    [SerializeField] private List<Node> blackActions = new List<Node>();

    [SerializeField] private List<Node> chosenPieceMoves = new List<Node>();

    [SerializeField] private Stack<Node> moveStack = new Stack<Node>();

    [SerializeField] int whiteScore = 0;
    [SerializeField] int blackScore = 0;
    [SerializeField] int totalScore = 0;

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

    //public int CalculateHeuristics(int depth, int alpha, int beta, bool max)
    //{
    //    GetBoardState();

    //    if (depth == 0)
    //    {
    //        return Evaluate();
    //    }
    //    if (max)
    //    {
    //        int score = -10000000;

    //        for (int i = 0; i < blackMovables.Count; ++i)
    //        {
    //            Node selectedBlack = blackMovables[i];

    //            for (int j = 0; j < selectedBlack.GetNodesToCheck().Count; ++j)
    //            {
    //                Node nextMove = selectedBlack.GetNodesToCheck()[j];
                    
    //                FakeMove(selectedBlack, nextMove);
                    
    //                score = CalculateHeuristics(depth - 1, alpha, beta, false);

    //                UndoFakeMove();
    //            }
    //        }

    //        List<Node> nodeMoves = blackActions;
    //        foreach (Node move in nodeMoves)
    //        {
    //            moveStack.Push(move);

    //            FakeMove(previousNode, nodeToMove);

    //            score = CalculateHeuristics(depth - 1, alpha, beta, false);

    //            _UndoFakeMove();

    //            if (score > alpha)
    //            {
    //                move.score = score;
    //                if (move.score > bestMove.score && depth == maxDepth)
    //                {
    //                    bestMove = move;
    //                }
    //                alpha = score;
    //            }
    //            if (score >= beta)
    //            {
    //                break;
    //            }
    //        }
    //        return alpha;
    //    }
    //    else
    //    {
    //        int score = 10000000;
    //        List<Move> allMoves = _GetMoves(Piece.playerColor.WHITE);
    //        foreach (Move move in allMoves)
    //        {
    //            moveStack.Push(move);

    //            _DoFakeMove(move.firstPosition, move.secondPosition);

    //            score = AB(depth - 1, alpha, beta, true);

    //            _UndoFakeMove();

    //            if (score < beta)
    //            {
    //                move.score = score;
    //                beta = score;
    //            }
    //            if (score <= alpha)
    //            {
    //                break;
    //            }
    //        }
    //        return beta;
    //    }
    //}

    //public int Evaluate()
    //{
    //    float pieceDifference = 0;
    //    float whiteWeight = 0;
    //    float blackWeight = 0;

    //    foreach (Node node in whiteMovables)
    //    {
    //        whiteWeight += moveWeights.GetBoardWeight(previousNode.GetNodeType(), previousNode.GetNodePosition(), Node.NodeTeam.WHITE);
    //    }
    //    foreach (Node node in blackMovables)
    //    {
    //        blackWeight += moveWeights.GetBoardWeight(previousNode.GetNodeType(), previousNode.GetNodePosition(), Node.NodeTeam.BLACK);
    //    }

    //    pieceDifference = (blackScore + (blackWeight / 100)) - (whiteScore + (whiteWeight / 100));

    //    return Mathf.RoundToInt(pieceDifference * 100);
    //}

    //public Move CreateMove(Tile tile, Tile move)
    //{
    //    Move tempMove = new Move();
    //    tempMove.firstPosition = tile;
    //    tempMove.pieceMoved = tile.CurrentPiece;
    //    tempMove.secondPosition = move;

    //    if (move.CurrentPiece != null)
    //    {
    //        tempMove.pieceKilled = move.CurrentPiece;
    //    }

    //    return tempMove;
    //}

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
    
    public void FakeMove(Node previousNode, Node targetNode)
    {
        SwapFakePieces(targetNode);
        previousNode = null;
    }

    public void SwapFakePieces(Node newNode)
    {
        previousNode = newNode;
    }
}
