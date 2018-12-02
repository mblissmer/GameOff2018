using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GridReader : MonoBehaviour {

    public Vec2Variable LLLimits;
    public Vec2Variable URLimits;
    //public FloatArrayVariable xValues;
    //public FloatArrayVariable yValues;

    void Start () {
        GridGraph gg = AstarPath.active.data.gridGraph;
        LLLimits.Value = (Vector3)gg.nodes[0].position;
        URLimits.Value = (Vector3)gg.nodes[gg.nodes.Length - 1].position;
        //yValues.Values = new float[gg.depth];
        //xValues.Values = new float[gg.width];
        //for (int i = 0; i < gg.depth; i++) {
        //    yValues.Values[i] = gg.GetNode(0, i).position.y / 1000;
        //    //this is where a linecast would go if this were the pro version of AStar...
        //}
        //for (int i = 0; i < gg.width; i++) {
        //    xValues.Values[i] = gg.GetNode(i, 0).position.x / 1000;
        //}
    }

}
