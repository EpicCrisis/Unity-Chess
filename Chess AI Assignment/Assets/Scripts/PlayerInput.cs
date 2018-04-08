using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isSelecting = false;
    [SerializeField] private Node previousNode;

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
        UpdateSelection();
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
                    isSelecting = false;

                    //Check for the correct node
                    for (int i = 0; i < previousNode.GetNodesToCheck().Count; ++i)
                    {
                        Node toNode = previousNode.GetNodesToCheck()[i];

                        if (node == toNode)
                        {
                            node = toNode;

                            node.UpdateNode();
                        }
                    }
                    //Reset the previous node.
                    previousNode.CheckEmpty();
                    previousNode.GetNodesToCheck().Clear();

                    for (int i = 0; i < board.GetNodeList().Count; ++i)
                    {
                        board.GetNodeList()[i].UpdateNode();
                    }

                    playerTurn++;

                    //if (previousNode.GetNodesToCheck().Contains(node))
                    //{
                    //    isSelecting = false;

                    //    node = previousNode;

                    //    //node.SetNodeType((int)previousNode.GetNodeType());
                    //    //node.SetNodeTeam((int)previousNode.GetNodeTeam());

                    //    node.UpdateNode();
                    //    //node.UpdateMoveCounter(previousNode.GetMoveCounter + 1);

                    //    //Reset the previous node.
                    //    previousNode.CheckEmpty();
                    //    previousNode.GetNodesToCheck().Clear();

                    //    for (int i = 0; i < board.GetNodeList().Count; ++i)
                    //    {
                    //        board.GetNodeList()[i].UpdateNode();
                    //    }

                    //    playerTurn++;
                    //}
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

            playerTurn++;
            if (playerTurn > 2)
            {
                playerTurn = 1;
            }
        }
    }

    void CheckPlayerAction(int playerturn)
    {
        if (playerturn == 1)
        {
            
        }
        else if (playerturn == 2)
        {

        }
    }

    public int GetPlayerTurn
    {
        get
        {
            return playerTurn;
        }
    }
}
