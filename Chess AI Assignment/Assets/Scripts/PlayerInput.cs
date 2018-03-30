using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isSelecting = false;
    [SerializeField] private Node previousNode;

    [SerializeField] private int playerTurn = 1;

    GameChecker gameChecker;
    ChessAI chessAI;

    void Start()
    {
        gameChecker = GetComponent<GameChecker>();
        chessAI = GetComponent<ChessAI>();
    }
    
    void Update()
    {
        if (playerTurn > 2)
        {
            playerTurn = 1;
        }
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
                Debug.Log("You just clicked on this: " + hit.transform);

                Node node = hit.transform.GetComponent<Node>();
                if (playerTurn == 1)
                {
                    if (!isSelecting)
                    {
                        if (node.GetNodeTeam() == Node.NodeTeam.WHITE)
                        {
                            if (node.GetNodeType() != Node.NodeType.NONE)
                            {
                                previousNode = node;
                            }
                            if (node.GetNodeType() == Node.NodeType.PAWN)
                            {
                                node.CheckPawnMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.BISHOP)
                            {
                                node.CheckBishopMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.KNIGHT)
                            {
                                node.CheckKnightMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.ROOK)
                            {
                                node.CheckRookMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.QUEEN)
                            {
                                node.CheckQueenMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.KING)
                            {
                                node.CheckKingMovement();
                            }
                            node.PaintSelected();
                            isSelecting = true;
                        }
                    }
                    //The phase where player chooses the node to move to.
                    else if (isSelecting)
                    {
                        //Check for the correct node
                        if (previousNode.GetNodesToCheck().Contains(node))
                        {
                            isSelecting = false;

                            node.SetNodeType((int)previousNode.GetNodeType());
                            node.SetNodeTeam((int)previousNode.GetNodeTeam());

                            node.UpdateNode();
                            node.UpdateMoveCounter(previousNode.GetMoveCounter + 1);

                            //Reset the previous node.
                            previousNode.CheckEmpty();
                            previousNode.GetNodesToCheck().Clear();

                            playerTurn++;
                        }
                    }
                }
                else if (playerTurn == 2)
                {
                    if (!isSelecting)
                    {
                        if (node.GetNodeTeam() == Node.NodeTeam.BLACK)
                        {
                            if (node.GetNodeType() != Node.NodeType.NONE)
                            {
                                previousNode = node;
                            }
                            if (node.GetNodeType() == Node.NodeType.PAWN)
                            {
                                node.CheckPawnMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.BISHOP)
                            {
                                node.CheckBishopMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.KNIGHT)
                            {
                                node.CheckKnightMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.ROOK)
                            {
                                node.CheckRookMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.QUEEN)
                            {
                                node.CheckQueenMovement();
                            }
                            if (node.GetNodeType() == Node.NodeType.KING)
                            {
                                node.CheckKingMovement();
                            }
                            node.PaintSelected();
                            isSelecting = true;
                        }
                    }
                    //The phase where player chooses the node to move to.
                    else if (isSelecting)
                    {
                        //Check for the correct node
                        if (previousNode.GetNodesToCheck().Contains(node))
                        {
                            isSelecting = false;

                            node.SetNodeType((int)previousNode.GetNodeType());
                            node.SetNodeTeam((int)previousNode.GetNodeTeam());

                            node.UpdateNode();
                            node.UpdateMoveCounter(previousNode.GetMoveCounter + 1);

                            //Reset the previous node.
                            previousNode.CheckEmpty();
                            previousNode.GetNodesToCheck().Clear();

                            playerTurn++;
                        }
                    }
                }
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            isSelecting = false;

            previousNode.UnPaintMovables();
        }
    }

    
}
