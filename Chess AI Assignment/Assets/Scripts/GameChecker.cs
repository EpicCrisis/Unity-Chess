using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChecker : MonoBehaviour
{
    //Checking for the condition of the king to restrict movement to protect king only.
    [SerializeField] private bool isCheck = false;
    [SerializeField] private bool isCheckmate = false;

    BoardManager board;

    void Start()
    {
        board = GetComponent<BoardManager>();
    }
    
    void Update()
    {
        CheckForKing();
    }

    //===============================================================================
    //Goes through the entire list of nodes,
    //then checks each node in the list that contains a King as a target of attack.
    //===============================================================================
    public void CheckForKing()
    {
        for (int i = 0; i < board.GetNodeList().Count; ++i)
        {
            Node node = board.GetNodeList()[i];

            for (int j = 0; j < node.GetNodesToCheck().Count; ++j)
            {
                Node nodeCheck = node.GetNodesToCheck()[j];

                if (nodeCheck.GetNodeType() == Node.NodeType.KING)
                {

                }
            }
        }
    }

    //How to check into a deeper level, where the king is unable
    //to move towards a node which is targeted by an enemy?

    //Need to tune node checking from checking the selected node
    //to checking all nodes everytime something happens.
}
