using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamenTouch : MonoBehaviour {
	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;
	public bool tapSide = true; //タップした側が左側（true）か右側（false）か

	//HingiJointコンポーネントを入れる
	private HingeJoint myHingeJoint;

	//初期の傾き
	private float defaultAngle = 20;
	//弾いた時の傾き
	private float flickAngle = -20;

	// Use this for initialization
	void Start () {
		//HingeJointコンポーネント取得
		this.myHingeJoint = GetComponent<HingeJoint>();

		//フリッパーの傾きを設定
		SetAngle(this.defaultAngle);
	}


	void Update() {

		// Track a single touch as a direction control.
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			switch (touch.phase) {
				//Began：パネルに指がついた状態
			case TouchPhase.Began:
				startPos = touch.position;
				directionChosen = false;
						//左側を押した時、左フリッパーを動かす
				   	if (startPos.x < Screen.width*0.5 && tag == "LeftFripperTag") {
						SetAngle (this.flickAngle);
						//右側を押した時、左フリッパーを動かさない
					} else if (startPos.x >= Screen.width*0.5 && tag == "LeftFripperTag") {
						SetAngle (this.defaultAngle);
					    //左側を押した時、右フリッパーを動かさない
					} else if (startPos.x < Screen.width*0.5 && tag == "RightFripperTag"){
						SetAngle (this.defaultAngle);
						//右側を押した時、右フリッパーを動かす				
					} else if (startPos.x >= Screen.width*0.5 && tag == "RightFripperTag"){
						SetAngle (this.flickAngle);
					}					
				break;

				// Moved：指が動いている状態
			case TouchPhase.Moved:
				direction = touch.position - startPos;
				break;

				// Stationary：パネルに指はあるけど、指が動いていない状態
			case TouchPhase.Stationary:
				direction = touch.position - startPos;
				if (Input.touchCount > 1) {
					SetAngle (this.flickAngle);	
				}
				break;

				// Ended：指がパネルから離れた状態
			case TouchPhase.Ended:
				directionChosen = true;
					SetAngle (this.defaultAngle);
				break;
			}
		}
			
	}


	//フリッパーの傾きを設定
	public void SetAngle (float angle){
		JointSpring jointSpr = this.myHingeJoint.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoint.spring = jointSpr;
	}

}