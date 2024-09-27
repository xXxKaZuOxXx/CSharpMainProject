using Model;
using System.Collections;
using System.Collections.Generic;
using UnitBrains.Pathfinding;
using UnityEngine;
using Utilities;

public class A_zvezdochka : BaseUnitPath
{
    private Vector2Int _startPoint;
    private Vector2Int _endPoint;
    public A_zvezdochka(IReadOnlyRuntimeModel runtimeModel, Vector2Int startPoint, Vector2Int endPoint) :base( runtimeModel,  startPoint,  endPoint)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
    }


    protected override void Calculate()
    {
        List<Nodee> path1 = FindPath();
        if (path1 != null)
        {
            return;
        }
        // Nodee nextpos = path1[1];
        path1.RemoveAt(0);
        List<Vector2Int> finalka = new List<Vector2Int>();
        while (path1 != null)
        {
            finalka.Add(path1[0].Point);
            path1.RemoveAt(0);
        }
        path = finalka.ToArray();
    }
    private List<Nodee> FindPath()
    {
        Nodee startNode = new Nodee(_startPoint);
        Nodee targetNode = new Nodee(_endPoint);

        List<Nodee> openList = new List<Nodee> { startNode };
        List<Nodee> closedList = new List<Nodee>();
        
        while (openList.Count > 0)
        {
            Nodee currentNode = openList[0];
            foreach(var node in openList)
            {
                if(node.Value < currentNode.Value)
                {
                    currentNode = node;
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            if(currentNode.Point.x == targetNode.Point.x && currentNode.Point.y == targetNode.Point.y)
            {
                List<Nodee> path = new List<Nodee>();
                while (currentNode != null)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.Parent;
                }
                path.Reverse();
                return path;
            }
            var diff = _endPoint - _startPoint;
            var stepDiff = diff.SignOrZero();
            var nextStep = _startPoint + stepDiff;

            if (runtimeModel.IsTileWalkable(nextStep))
            {
                Nodee neighbor = new Nodee(nextStep);
                if (closedList.Contains(neighbor))
                    continue;
                neighbor.Parent = currentNode;
                neighbor.CalculateEstimate(targetNode.Point);
                neighbor.CalculateValue();

                openList.Add(neighbor);
            }

        }
        return null;
    }
}
