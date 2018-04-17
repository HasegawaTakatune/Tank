using System.Collections;
using System;
using UnityEngine;

public class Manager : MonoBehaviour {

    /// タイトル画面
    [SerializeField]
    GameObject TitleItems;
    /// リザルト画面
    [SerializeField]
    GameObject ResultItems;
    /// ゲームオーバー画面
    [SerializeField]
    GameObject GameOverItems;
    /// 仮ローディング画面
    [SerializeField]
    GameObject WaitItems;

    /// 初期化
	void Start () {
		
	}
	
    /// メインループ
	void Update () {
        // ゲームステータス遷移
        switch (GameManager.Status)
        {
                // タイトル
            case GameManager.Title:
                // タイトル画面の表示
                TitleItems.SetActive(true);
                // キー入力がされたら=>ロード画面=>ゲーム画面に遷移する
                if (Input.anyKeyDown)
                {
                    GameManager.Status = GameManager.Wait;
                    TitleItems.SetActive(false);
                    StartCoroutine(Delay(() =>
                    {
                        WaitItems.SetActive(false);
                        GameManager.Status = GameManager.Game;
                    }));

                }
                break;

                // ゲーム
            case GameManager.Game:

                break;

                // リザルト
            case GameManager.Result:
                // リザルト画面の表示
                ResultItems.SetActive(true);
                // キー入力がされたら=>ロード画面=>タイトル画面に遷移する
                if (Input.anyKeyDown)
                {
                    GameManager.Status = GameManager.Wait;
                    ResultItems.SetActive(false);
                    StartCoroutine(Delay(() =>
                    {
                        WaitItems.SetActive(false);
                        TitleItems.SetActive(true);
                        GameManager.Status = GameManager.Title;
                    }));
                    GameStatus.Init();
                }
                break;

            case GameManager.GameOver:
                // リザルト画面の表示
                GameOverItems.SetActive(true);
                // キー入力がされたら=>ロード画面=>タイトル画面に遷移する
                if (Input.anyKeyDown)
                {
                    GameManager.Status = GameManager.Wait;
                    GameOverItems.SetActive(false);
                    StartCoroutine(Delay(() =>
                    {
                        WaitItems.SetActive(false);
                        TitleItems.SetActive(true);
                        GameManager.Status = GameManager.Title;
                    }));
                    GameStatus.Init();
                }
                break;

                // 仮ロード時間みたいな
            case GameManager.Wait:
                WaitItems.SetActive(true);
                break;
        }
	}

    /// 遅延処理
    IEnumerator Delay(Action action) { yield return new WaitForSeconds(1f); action(); }
}
