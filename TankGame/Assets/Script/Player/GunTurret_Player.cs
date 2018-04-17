using UnityEngine;
using UnityEngine.UI;

public class GunTurret_Player : MonoBehaviour
{

    /// Object_Baseのキャッシュ（自身のステータス値）
    [SerializeField]
    ObjectBase Object_Base;
	/// Transformのキャッシュ
	Transform myTransform;
    /// 砲弾のオブジェクト
    [SerializeField]
    GameObject Bullet;
    /// マズルのTransform
    [SerializeField]
    Transform Muzzle;
    /// 砲身のTransform
    [SerializeField]
    Transform Barrel_Main;
    /// マズルフラッシュのパーティクル
    [SerializeField]
    ParticleSystem MuzzleFlash_Particle;
    [SerializeField]
    ParticleSystem MuzzleSmoke_Particle;
    [SerializeField]
    ParticleSystem MuzzleSmoke_Particle2;
    /// リロードのスライダーUI
    [SerializeField]
    Slider Reload_slider;
    /// リロードのテキストUI
    [SerializeField]
    Text Reload_text;
    /// AudioSourceのキャッシュ
    [SerializeField]
    AudioSource audioSource;
    /// 砲弾の発射音
    [SerializeField]
    AudioClip Shot_sound;

    /// マウス座標
    Vector3 m_position = new Vector3(-90, 0, 0);
    /// 砲塔の角度
    Quaternion gunturret_angle;
    /// 砲身の上下角度
    float mx = 0;
    /// 砲塔の上下角度の制限値
    const float angle_limit = 5;

    /// リロード時間
    [SerializeField]
    float interval = 3;
    /// リロードの経過時間
	float time = 0;
    /// 次の弾が撃てるかの判定
	bool next = true;

    /// 初期角度
    Quaternion InitTurretQuat;

    /// 初期化
    void Start()
    {
        // マウスカーソルの固定・非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        myTransform = transform;

        // 表示するUIを格納する
        GameObject[] obj = GameObject.FindGameObjectsWithTag("StatusUI");
        for (int ii = 0; ii < obj.Length; ii++)
        {
            if (obj[ii].name == "Reload_Slider")
                Reload_slider = obj[ii].GetComponent<Slider>();
            else if (obj[ii].name == "Reload_Text")
                Reload_text = obj[ii].GetComponent<Text>();
            //else if (obj[ii].name == "HP_Slider")
                //HP_slider = obj[ii].GetComponent<Slider>();
        }

        Reload_slider.maxValue = interval;
        Reload_slider.value = interval;

        InitTurretQuat = myTransform.rotation;
    }

    void Init()
    {
        next = true;
        gunturret_angle = Quaternion.identity;
        myTransform.rotation = InitTurretQuat;
        m_position = new Vector3(-90, 0, 0);
    }

    /// メインループ
    void Update()
    {
        if (GameManager.isPlay())
        {
            // 回転
            Rotation();

            // 砲撃
            Shelling();
        }
        else Init();
    }

    /// 砲塔の回転をする
    void Rotation()
    {
        // 上下角度の制限判定
        mx -= (Input.GetAxis("Mouse Y"));
        if (mx > angle_limit) mx = angle_limit; else if (mx < -angle_limit) mx = -angle_limit;
        // マウスの移動量を格納
        m_position += new Vector3(0, (Input.GetAxis("Mouse X")) * .3f, 0);
        // Vector3型 => Quaternion型に変換して、砲塔の角度として設定する
        gunturret_angle = Quaternion.Euler(m_position);
        myTransform.localRotation = gunturret_angle;
        Barrel_Main.localEulerAngles = new Vector3(mx, 0, 0);
    }

    /// マウスクリックで砲撃をする
    void Shelling()
    {
        // 砲弾が装填されていたら、砲撃をする
        if (Input.GetMouseButtonDown(0) && next)
        {
            GameStatus.ShotCount++;
            // 砲弾を生成してID登録をする
            var obj = Instantiate(Bullet, Muzzle.position, Quaternion.Euler(Barrel_Main.localEulerAngles.x, myTransform.eulerAngles.y, 0));
            obj.layer = LayerMask.NameToLayer("Player");
            // マズルフラッシュの再生
            MuzzleFlash_Particle.Play();
            MuzzleSmoke_Particle.Play();
            MuzzleSmoke_Particle2.Play();
            // 砲撃音の再生
            audioSource.PlayOneShot(Shot_sound);
            // 次弾装填の待ち処理の初期化
            next = false;
            time = 0;
            Reload_slider.value = 0;
            Reload_text.text = "装填中...";
        }
        // 次弾装填中
        if (!next)
        {
            // 時間経過
            time += Time.deltaTime;
            Reload_slider.value = time;
            // 装填完了
            if (time >= interval)
            {
                next = true;
                Reload_text.text = "装填完了";
            }
        }
    }

}
