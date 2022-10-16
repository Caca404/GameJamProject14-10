using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // public Transform target_player;
    // public Transform target_boss;
	// public float damping = 1;
	// public float lookAheadFactor = 3;
	// public float lookAheadReturnSpeed = 0.5f;
	// public float lookAheadMoveThreshold = 0.1f;
	// public float yPosRestriction = -1;
	// float offsetZ;
	// Vector3 lastTargetPosition;
	// Vector3 currentVelocity;
	// Vector3 lookAheadPos;

	// float nextTimeToSearch = 0;
    // Vector3 target_position;
	
	// // Use this for initialization
	// void Start () {
    //     if(target_boss.position.x > target_player.position.x){
    //         target_position = new Vector3(
    //             target_player.position.x + (target_boss.position - target_player.position).x,
    //             0,
    //             0
    //         );
    //     }
    //     else{
    //         target_position = new Vector3(
    //             target_boss.position.x + (target_player.position.x - target_boss.position.x),
    //             0,
    //             0
    //         );
    //     }
	// 	lastTargetPosition = target_position;
	// 	offsetZ = (transform.position - target_position).z;
	// 	transform.parent = null;
	// }
	
	// // Update is called once per frame
	// void Update () {

	// 	if (target_player == null) {
	// 		FindPlayer ();
	// 		return;
	// 	}

    //     if(target_boss.position.x > target_player.position.x){
    //         target_position = new Vector3(
    //             target_player.position.x + (target_boss.position - target_player.position).x,
    //             0,
    //             0
    //         );
    //     }
    //     else{
    //         target_position = new Vector3(
    //             target_boss.position.x + (target_player.position.x - target_boss.position.x),
    //             0,
    //             0
    //         );
    //     }

	// 	// only update lookahead pos if accelerating or changed direction
	// 	float xMoveDelta = (target_position - lastTargetPosition).x;

	//     bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

	// 	if (updateLookAheadTarget) {
	// 		lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
	// 	} else {
	// 		lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
	// 	}
		
	// 	// Vector3 aheadTargetPos = target_position + lookAheadPos + Vector3.forward * offsetZ;
        
	// 	Vector3 newPos = Vector3.SmoothDamp(transform.position, new Vector3(target_position.x, target_position.y, -10), ref currentVelocity, damping);

	// 	newPos = new Vector3 (newPos.x, Mathf.Clamp (newPos.y, yPosRestriction, Mathf.Infinity), newPos.z);

    //     Debug.Log(target_position);
	// 	transform.position = newPos;
		
	// 	lastTargetPosition = target_position;
	// }

	// void FindPlayer () {
	// 	if (nextTimeToSearch <= Time.time) {
	// 		GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
	// 		if (searchResult != null)
	// 			target_player = searchResult.transform;
	// 		nextTimeToSearch = Time.time + 0.5f;
	// 	}
	// }
}
