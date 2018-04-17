using UnityEngine;

public class FirstPerson_Camera : MonoBehaviour {

    /// カメラコンポーネント
    Camera cam;
    /// アップ/ルーズの速度
    public float spd = 2;

    /// 初期化
	void Start () {
        cam = GetComponent<Camera>();
	}
    void Init()
    {
        cam.fieldOfView = Mathf.Clamp(value: 60, min: 20f, max: 60f);
    }
	
    /// メインループ
	void Update () {
        if (GameManager.isPlay())
        {
            // マウスのスクロールホイールの移動量を求め
            // カメラの視野角を設定する
            float scroll = Input.GetAxis("Mouse ScrollWheel") * spd;
            float view = cam.fieldOfView - scroll;
            cam.fieldOfView = Mathf.Clamp(value: view, min: 20f, max: 60f);
        }
        else Init();
	}
}
