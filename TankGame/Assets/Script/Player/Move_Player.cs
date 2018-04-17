using UnityEngine;

public class Move_Player : MonoBehaviour
{
    /// AudioSourceのキャッシュ
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource audioSource2;
    [SerializeField]
    AudioClip Engine_departure;
    [SerializeField]
    AudioClip Catepillar;
    bool doOnce_audio = false;
    
    /// Transformのキャッシュ
    Transform myTransform;
    Vector3 InitPos;
    Quaternion InitQuat;

    /// 移動速度
    [SerializeField]
    float speed = 0;
    /// 加速度
    [SerializeField]
    float accele = .001f;
    /// 最大加速度
    [SerializeField]
    float max_speed = .3f;
    /// 旋回速度
    [SerializeField]
    float turning_speed = .5f;
    
    /// 初期化
    void Start()
    {
        myTransform = transform;
        InitPos = myTransform.position;
        InitQuat = myTransform.rotation;
    }

    void Init()
    {
        myTransform.position = InitPos;
        myTransform.rotation = InitQuat;
        speed = 0;
    }

    /// メインループ
    void Update()
    {
        if (GameManager.isPlay())
        {
            // キー入力処理
            Key_control();
        }else Init();
    }

    /// キー入力でオブジェクトの移動・旋回をする
    void Key_control()
    {

        // キー入力時の戻り値を保存
        // Vertical : 上下(1 ～ -1)　　Horizontal : 左右(1 ～ -1)
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // 移動
        if (vertical != 0)
        {

            // どの方向を向いているかを取得
            float direc = Mathf.Sign(vertical);
            if (Mathf.Sign(speed) != direc)
                speed += (accele * direc) * 3;  // 急ブレーキをして別方向にアクセル全開
            else
                speed += accele * direc;        // 加速させる

            // 最大速度以上であれば、最大速度にとどめる
            if (Mathf.Abs(speed) > max_speed)
                speed = max_speed * direc;
            if (!audioSource.isPlaying) audioSource.PlayOneShot(Catepillar);
            if (!doOnce_audio) { audioSource2.PlayOneShot(Engine_departure); doOnce_audio = true; }
        }
        else
        {
            // キー入力がされていない場合、減速　→　停止する
            if (Mathf.Abs(speed) > accele)
            {
                speed += (speed * -1) / 3;
                doOnce_audio = false;
            }
            else
            {
                speed = 0;
                audioSource.Stop();                
            }
        }

        // 座標の更新
        myTransform.position += myTransform.forward * speed;

        // 旋回
        if (horizontal != 0)
        {
            myTransform.Rotate(0, turning_speed * Mathf.Sign(horizontal), 0);
            if (!audioSource.isPlaying) audioSource.PlayOneShot(Catepillar);
            if (!doOnce_audio) { audioSource2.PlayOneShot(Engine_departure); doOnce_audio = true; }
        }
    }
}
