using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptArchive : MonoBehaviour
{
    //===================================================================================
    //Player Input Script Archive
    //===================================================================================

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

    //===================================================================================
    //
    //===================================================================================
}
