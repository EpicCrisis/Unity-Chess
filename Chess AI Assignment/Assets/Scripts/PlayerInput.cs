using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isSelecting = false;

    [SerializeField] private Node previousNode;
    [SerializeField] private Node nodeToMove;

    //Automatically pause any input and show restart when checkmate.
    [SerializeField] private bool isCheckmate = false;
    //Need more thought on implementation.
    [SerializeField] private bool isCheck = false;

    [SerializeField] private int playerTurn = 1;

    [SerializeField] Node aiNodeMove;

    BoardManager board;
    ChessAI chessAI;
    UIManager UIManager;

    void Start()
    {
        board = GetComponent<BoardManager>();
        chessAI = GetComponent<ChessAI>();
        UIManager = GetComponent<UIManager>();
    }
    
    void Update()
    {
        if (isCheckmate)
        {
            //Debug.Log("The King Is Dead Stupid!");
            if (UIManager.GetRestartButton().activeSelf == false)
            {
                //Add function for restart.
                UIManager.GetRestartButton().SetActive(true);
            }
        }
        else
        {
            UpdateSelection();
        }
    }
    
    private void UpdateSelection()
    {
        if (!Camera.main)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Node"))
            {
                nodeToMove = hit.transform.GetComponent<Node>();

                if (playerTurn == 1 && nodeToMove.GetNodeTeam() == Node.NodeTeam.WHITE)
                {
                    if (!isSelecting)
                    {
                        if (nodeToMove.GetNodeType() != Node.NodeType.NONE)
                        {
                            previousNode = nodeToMove;
                        }

                        board.UpdateNodeNActionList();

                        nodeToMove.PaintFullSelected();

                        isSelecting = true;
                    }
                }
                //else if (playerTurn == 2 && nodeToMove.GetNodeTeam() == Node.NodeTeam.BLACK)
                //{
                //    if (!isSelecting)
                //    {
                //        if (nodeToMove.GetNodeType() != Node.NodeType.NONE)
                //        {
                //            previousNode = nodeToMove;
                //        }

                //        board.UpdateNodeNActionList();

                //        nodeToMove.PaintFullSelected();

                //        isSelecting = true;
                //    }
                //}
                //The phase where player chooses the node to move to.
                else if (isSelecting)
                {
                    if (previousNode.GetNodesToCheck().Contains(nodeToMove))
                    {
                        isSelecting = false;

                        UpdateInputs(nodeToMove, previousNode);

                        board.UpdateNodeNActionList();

                        playerTurn++;
                    }
                }
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            isSelecting = false;

            previousNode.UnPaintMovables();
        }
        else if (playerTurn == 2)
        {
            Debug.Log("Auto player turn is active!!!");

            chessAI.GetAIMovables();
            chessAI.AIMakeAction();

            board.UpdateNodeNActionList();

            playerTurn++;

            if (playerTurn > 2)
            {
                playerTurn = 1;
            }
        }
    }

    public void UpdateInputs(Node node, Node previousNode)
    {
        if (node.GetNodeType() == Node.NodeType.KING)
        {
            isCheckmate = true;
        }

        if (previousNode.GetNodeType() == Node.NodeType.PAWN)
        {
            if (previousNode.GetNodeTeam() == Node.NodeTeam.WHITE)
            {
                if (node.GetNodePosition().y == 7)
                {
                    previousNode.SetNodeType((int)Node.NodeType.QUEEN);
                }
            }
            else if (previousNode.GetNodeTeam() == Node.NodeTeam.BLACK)
            {
                if (node.GetNodePosition().y == 0)
                {
                    previousNode.SetNodeType((int)Node.NodeType.QUEEN);
                }
            }
        }

        //Updates to change for new node.
        node.SetNodeType((int)previousNode.GetNodeType());
        node.SetNodeTeam((int)previousNode.GetNodeTeam());

        node.UpdateNode();
        node.UpdateMoveCounter(previousNode.GetMoveCounter + 1);
        node.UpdateChessWeight(previousNode.GetChessWeight);

        CheckForKingCheck();

        //Reset the previous node.
        previousNode.CheckEmpty();
        previousNode.GetNodesToCheck().Clear();
    }

    public void CheckForKingCheck()
    {
        for (int i = 0; i < board.GetActionList().Count; ++i)
        {
            Node nodeAction = board.GetActionList()[i];

            if (nodeAction.GetNodeType() == Node.NodeType.KING)
            {
                Debug.Log("King is being checked!!!");

                isCheck = true;
            }
        }
    }

    public int GetPlayerTurn
    {
        get
        {
            return playerTurn;
        }
    }

    public void SetCheckmate(bool boolean)
    {
        isCheckmate = boolean;
    }

    public bool GetCheckmateState()
    {
        return isCheckmate;
    }
}
