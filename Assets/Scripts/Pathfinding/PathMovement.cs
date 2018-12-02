using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathMovement : MonoBehaviour {

    //public float fullSpeed = 10;
    //public float altSpeed = 5;
    //private Path path;
    //public float speed;
    //private int currentWaypoint = 0;
    //public bool completedPath;

    //private void Awake() {
    //    speed = fullSpeed;
    //}

    //public void NewPath(Path p) {
    //    completedPath = false;
    //    path = p;
    //    currentWaypoint = 0;
    //    Step();
    //}


    //void Step() {
    //    if (currentWaypoint <= path.vectorPath.Count - 1) {
    //        iTween.MoveTo(gameObject, iTween.Hash(
    //            "position", path.vectorPath[currentWaypoint],
    //            "speed", speed,
    //            "name", "move",
    //            "easetype", iTween.EaseType.linear,
    //            "oncomplete", "Step"));
    //        currentWaypoint++;
    //    }
    //    else {
    //        completedPath = true;
    //    }
    //}

    //public void Stop() {
    //    iTween.StopByName(gameObject,"move");
    //}

    //public void UseFullSpeed() {
    //    speed = fullSpeed;
    //}

    //public void UseAltSpeed() {
    //    speed = altSpeed;
    //}
}
