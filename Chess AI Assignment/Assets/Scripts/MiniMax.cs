using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax : MonoBehaviour
{
    BoardManager board;
    ChessAI chessAI;
    PlayerInput player;

    [SerializeField] private List<Node> nextBoardPositions;

    [SerializeField] private int depthLimit = 4;

    void Start()
    {
        board = BoardManager.Instance;
        chessAI = GetComponent<ChessAI>();
        player = GetComponent<PlayerInput>();
    }

    public Node Minimax(Node node)
    {
        int highestScore = int.MinValue;
        int highestScoreIndex = -1;

        //getAllBoardPositions returns a list of next possible board positions, the boolean flag is to tell whether the current move is Max or Min
        nextBoardPositions = board.GetActionList();

        for (int i = 0; i < nextBoardPositions.Count; ++i)
        {
            Node boardPos = nextBoardPositions[i];

            int score = Min(boardPos, 0);

            if (score > highestScore)
            {
                highestScore = score;
                highestScoreIndex = i;
            }
        }

        //return the selected board position
        return nextBoardPositions[highestScoreIndex];
    }

    int Max(Node boardPos, int depth)
    {
        if (depth >= depthLimit || !player.GetCheckmateState())
        {
            return chessAI.GetTotalScore();
        }

        int highestScore = int.MinValue;

        nextBoardPositions = board.GetActionList();

        foreach (Node _boardPos in nextBoardPositions)
        {
            int score = Min(_boardPos, depth + 1);

            if (score > highestScore)
            {
                highestScore = score;
            }
        }

        return highestScore;
    }

    int Min(Node boardPos, int depth)
    {
        if (depth >= depthLimit || !player.GetCheckmateState())
        {
            return chessAI.GetTotalScore();
        }

        int lowestScore = int.MaxValue;

        nextBoardPositions = board.GetActionList();

        foreach (Node _boardPos in nextBoardPositions)
        {
            int score = Max(_boardPos, depth + 1);

            if (score < lowestScore)
            {
                lowestScore = score;
            }
        }

        return lowestScore;
    }
}
