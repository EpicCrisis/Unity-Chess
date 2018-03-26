using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isSelecting = false;
    [SerializeField] private Node selectedNode;

    void Start()
    {

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
                Debug.Log("You just clicked on this: " + hit.transform);
                
                Node node = hit.transform.GetComponent<Node>();
                if (!isSelecting)
                {
                    if (node.GetNodeType() == Node.NodeType.NONE)
                    {
                        return;
                    }
                    if (node.GetNodeType() == Node.NodeType.PAWN)
                    {
                        selectedNode = node;
                        node.CheckPawnMovement();
                        isSelecting = true;
                    }
                }

                if (isSelecting)
                {
                    for (int i = 0; i < node.GetNodesToCheck().Count; ++i)
                    {
                        //Count from the list of available moves.
                    }

                    if (node.GetNodeType() == Node.NodeType.NONE)
                    {
                        node.SetNodeType((int)selectedNode.GetNodeType());
                        node.SetNodeTeam((int)selectedNode.GetNodeTeam());

                        //Reset the previous node.
                        selectedNode.CheckEmpty();

                        isSelecting = false;
                        node.UpdateNode();
                    }
                }
            }
        }
    }

    
}
