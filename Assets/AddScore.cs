using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour {
	public int tokuten =0; //各得点の変数
	private int score = 0; //スコア計算用変数
	private GameObject ScoreText;

	void Start() {
		score   = 0;
		//シーン中のScoreTextオブジェクトを取得
		this.ScoreText = GameObject.Find("ScoreText");
		SetScore(0);   //初期スコアを代入して表示
	}

	// Update is called once per frame
	void Update () {
	}


	//それぞれのタグごとに得点を加点
	void OnCollisionEnter( Collision collision ) {
		string yourTag  = collision.gameObject.tag;

		if (yourTag == "SmallStarTag") {
			tokuten = 10;
		} else if (yourTag == "LargeStarTag") {
			tokuten = 100;
		} else if (yourTag == "SmallCloudTag") {
			tokuten = 20;
		} else if (yourTag == "LargeCloudTag") {
			tokuten = 30;
		} else {
			tokuten = 0;
		}
		SetScore(tokuten);
	}


		void SetScore(int val){
		score += val;
		ScoreText.GetComponent<Text> ().text = "得点："+score;
		Debug.Log (score);
		}
	}