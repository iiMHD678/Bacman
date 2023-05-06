using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //int x = 19;
    //int y = 22;
    static Node[,] map = new Node[19,22];

    public Map()
    {
        //string mapdata = @"
        //0000000000000000000
        //0111111110111111110
        //0100100010100010010
        //0100100010100010010
        //0111111111111111110
        //0100101000001010010
        //0111101110111011110
        //0000100010100010000
        //0000101111111010000
        //0000101000001010000
        //1111111000001111111
        //0000101000001010000
        //0000101111111010000
        //0000101000001010000
        //0111111110111111110
        //0100100010100010010
        //0110111111111110110
        //0010101000001010100
        //0111101110111011110
        //0100000010100000010
        //0111111111111111110
        //0000000000000000000
        //";

        //int i = 0;
        ////adds mapdata to map
        //for (int x = 0; x < map.GetLength(0); x++)
        //{
        //    for (int y = 0; y < map.GetLength(1); y++)
        //    {
        //        if (mapdata[i] == 0)
        //        {
        //            //map[x, y] = false;
        //            map[x, y] = new Node(false);
        //        }
        //        else if (mapdata[i] == 1)
        //        {
        //            //map[x, y] = true;
        //            map[x, y] = new Node(true);
        //        }
        //        i++;
        //    }
        //}
    }

    static Node[,] FillMap()
    {
        string mapdata = @"
        0000000000000000000
        0111111110111111110
        0100100010100010010
        0100100010100010010
        0111111111111111110
        0100101000001010010
        0111101110111011110
        0000100010100010000
        0000101111111010000
        0000101000001010000
        1111111000001111111
        0000101000001010000
        0000101111111010000
        0000101000001010000
        0111111110111111110
        0100100010100010010
        0110111111111110110
        0010101000001010100
        0111101110111011110
        0100000010100000010
        0111111111111111110
        0000000000000000000
        ";

        int i = 0;
        //adds mapdata to map
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (mapdata[i] == 0)
                {
                    //map[x, y] = false;
                    map[x, y] = new Node(false, x, y);
                }
                else if (mapdata[i] == 1)
                {
                    //map[x, y] = true;
                    map[x, y] = new Node(true, x, y);
                }
                i++;
            }
        }
        return map;
    }

    public static int PathFinding(int startX, int startY, int endX, int endY)
    {
        startX = Mathf.RoundToInt(startX - 0.5f);
        startY = Mathf.RoundToInt(-1*startY - 0.5f);
        endX = Mathf.RoundToInt(endX - 0.5f);
        endY = Mathf.RoundToInt(-1*endY - 0.5f);
        List<Node> Open = new List<Node>();
        List<Node> Closed = new List<Node>();
        Node[,] map = FillMap();
        Node current;

        Open.Add(map[startX, startY]);
        Open[0].Fcost = Mathf.Abs(startX - endX) + Mathf.Abs(startY - endY);


        while (true)
        {
            current = FindLowestFcost(Open);
            Open.Remove(current);
            Closed.Add(current);

            if (current.XPos == endX && current.YPos == endY)
            {
                break;
            }

            foreach (Node neighbor in FindNeighbors(current))
            {
                if (neighbor.isPath || Closed.Contains(neighbor))
                {
                    continue;
                }
                //if new path to neighbor is shorter OR 
                if (!Open.Contains(neighbor))
                {
                    neighbor.Fcost = Mathf.Abs(neighbor.XPos - endX) + Mathf.Abs(neighbor.YPos - endY) + Mathf.Abs(neighbor.XPos - startX) + Mathf.Abs(neighbor.YPos - startY);
                    neighbor.parent = current;
                    if (!Open.Contains(neighbor))
                    {
                        Open.Add(neighbor);
                    }
                }
            }
        }


        bool parentIsStartNode = false;
        if (current.parent.XPos == startX && current.parent.YPos == startY) // is parent startnode?
        {
            parentIsStartNode = true;
        }
        
        while (parentIsStartNode == false)
        {
            current = current.parent;
            if (current.parent.XPos == startX && current.parent.YPos == startY)
            {
                parentIsStartNode = true;
            }
        }
        if (current.parent.YPos - current.YPos == 1)//is current above parent?
        {
            return 1;
        }
        if (current.parent.XPos - current.XPos == -1) //is current right of parent?
        {
            return 2;
        }
        if (current.parent.YPos - current.YPos == -1)//is current below parent?
        {
            return 3;
        }
        if (current.parent.XPos - current.XPos == 1) //is current left of parent?
        {
            return 4;
        }
        return 69;
    }

    static Node FindLowestFcost(List<Node> Open)
    {
        Node lowest = Open[0];
        foreach (Node node in Open)
        {
            if (node.Fcost < lowest.Fcost)
            {
                lowest = node;
            }
        }
        return lowest;
    }

    static List<Node> FindNeighbors(Node mainNode)
    {
        List<Node> neighbors = new List<Node>();

        if (mainNode.XPos != 19)
        {
            neighbors.Add(map[mainNode.XPos + 1, mainNode.YPos]);
        }
        if (mainNode.XPos != 0)
        {
            neighbors.Add(map[mainNode.XPos - 1, mainNode.YPos]);
        }
        if (mainNode.YPos != 22)
        {
            neighbors.Add(map[mainNode.XPos, mainNode.YPos + 1]);
        }
        if (mainNode.YPos != 0)
        {
            neighbors.Add(map[mainNode.XPos, mainNode.YPos - 1]);
        }

        return neighbors;
    }
}

public class Node
{
    public int Fcost;
    public bool isPath;
    public int XPos;
    public int YPos;
    public Node parent;

    public Node(bool canWalk, int X, int Y)
    {
        isPath = canWalk;
        XPos = X;
        YPos = Y;
    }
}