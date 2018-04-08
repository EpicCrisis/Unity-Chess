using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isSelecting = false;
    [SerializeField] private Node previousNode;

    //Automatically pause any input and show restart when checkmate.
    [SerializeField] private bool isCheckmate = false;
    //Need more thought on implementation.
    [SerializeField] private bool isCheck = false;

    [SerializeField] private int playerTurn = 1;

    BoardManager board;
    GameChecker gameChecker;
    ChessAI chessAI;

    void Start()
    {
        board = GetComponent<BoardManager>();
        gameChecker = GetComponent<GameChecker>();
        chessAI = GetComponent<ChessAI>();
    }
    
    void Update()
    {
        if (isCheckmate)
        {
            Debug.Log("The King Is Dead Stupid!");
            //Add function for restart.
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
                Node node = hit.transform.GetComponent<Node>();
                if (playerTurn == 1 && node.GetNodeTeam() == Node.NodeTeam.WHITE)
                {
                    if (!isSelecting)
                    {
                        if (node.GetNodeType() != Node.NodeType.NONE)
                        {
                            previousNode = node;
                        }

                        for (int i = 0; i < board.GetNodeList().Count; ++i)
                        {
                            board.GetNodeList()[i].UpdateNode();
                        }

                        //node.UpdateNode();

                        node.PaintSelected();
                        node.PaintMovables();
                        isSelecting = true;
                    }
                }
                //else if (playerTurn == 2 && node.GetNodeTeam() == Node.NodeTeam.BLACK)
                //{
                //    if (!isSelecting)
                //    {
                //        if (node.GetNodeType() != Node.NodeType.NONE)
                //        {
                //            previousNode = node;
                //        }

                //        for (int i = 0; i < board.GetNodeList().Count; ++i)
                //        {
                //            board.GetNodeList()[i].UpdateNode();
                //        }

                //        node.UpdateNode();

                //        node.PaintMovables();
                //        node.PaintSelected();
                //        isSelecting = true;
                //    }
                //}
                //The phase where player chooses the node to move to.
                else if (isSelecting)
                {
                    //if (previousNode.GetNodesToCheck().Contains(node))
                    //{
                    //    isSelecting = false;

                    //    UnityEditorInternal.ComponentUtility.CopyComponent(previousNode);
                    //    UnityEditorInternal.ComponentUtility.PasteComponentValues(node);

                    //    node.UpdateNode();

                    //    //Reset the previous node.
                    //    previousNode.CheckEmpty();
                    //    previousNode.GetNodesToCheck().Clear();

                    //    for (int i = 0; i < board.GetNodeList().Count; ++i)
                    //    {
                    //        board.GetNodeList()[i].UpdateNode();
                    //    }

                    //    playerTurn++;
                    //}

                    if (previousNode.GetNodesToCheck().Contains(node))
                    {
                        isSelecting = false;

                        if (node.GetNodeType() == Node.NodeType.KING)
                        {
                            isCheckmate = true;
                        }

                        //Updates to change for new node.
                        node.SetNodeType((int)previousNode.GetNodeType());
                        node.SetNodeTeam((int)previousNode.GetNodeTeam());

                        node.UpdateNode();
                        node.UpdateMoveCounter(previousNode.GetMoveCounter + 1);
                        node.UpdateChessWeight(previousNode.GetChessWeight);

                        //Reset the previous node.
                        previousNode.CheckEmpty();
                        previousNode.GetNodesToCheck().Clear();
                        
                        for (int i = 0; i < board.GetNodeList().Count; ++i)
                        {
                            board.GetNodeList()[i].UpdateNode();
                        }

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
            for (int i = 0; i < board.GetNodeList().Count; ++i)
            {
                board.GetNodeList()[i].UpdateNode();
            }

            chessAI.CheckScore();

            chessAI.GetAIMovables();

            chessAI.AIMakeAction();

            playerTurn++;

            if (playerTurn > 2)
            {
                playerTurn = 1;
            }
        }
    }

    //Check whether King is alive or not.
    //public void CheckForKing()
    //{
    //    for (int i = 0; i < board.GetNodeList().Count; ++i)
    //    {
    //        Node node = board.GetNodeList()[i];
    //        Node.NodeType nodeT = node.GetNodeType();

    //        if (board.GetNodeList().Contains(node))
    //        {

    //        }

    //        if (node.GetNodeTeam() == Node.NodeTeam.WHITE)
    //        {
    //            if (nodeT == Node.NodeType.KING)
    //            {
    //                isCheckmate = false;
    //            }
    //        }
    //        else if (node.GetNodeTeam() == Node.NodeTeam.BLACK)
    //        {
    //            if (nodeT == Node.NodeType.KING)
    //            {
    //                isCheckmate = false;
    //            }
    //        }
    //    }
    //}

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
}
