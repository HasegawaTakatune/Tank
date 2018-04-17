using UnityEngine;

public class TargetAiming_Enemy : MonoBehaviour {

    /// Transformのキャッシュ
    Transform myTrns;
    /// ターゲット
    Transform target;
    /// ターゲットアングル
    float target_angle;
    [SerializeField]
    Transform parent;

    /// Object_Baseのキャッシュ（自身のステータス値）
    [SerializeField]
    ObjectBase Object_Base;
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
    /// AudioSourceのキャッシュ
    [SerializeField]
    AudioSource audioSource;
    /// 砲弾の発射音
    [SerializeField]
    AudioClip Shot_sound;

    /// リロード時間
    float interval = 6;
    /// リロードの経過時間
	float time = 0;
    /// 次の弾が撃てるかの判定
	bool next = false;

    [SerializeField]
    float aaa;
    
    /// 初期化
    void Start () {
        myTrns = transform;
	}
    void Init()
    {

    }

    /// メインループ
    void Update()
    {
        if (GameManager.isPlay())
        {
            LookAt();

            Shot();
        }
        else Init();
    }

    /// ターゲットに向く
    void LookAt()
    {
        if (target == null)
            target = GameStatus.PlayerTransform;

        target_angle = GetDegree(myTrns.position, target.position);
        myTrns.eulerAngles = new Vector3(-90, -target_angle + aaa, 0);
    }

    /// 座標XとZの値から、2点間の角度を求める
    float GetDegree(Vector3 pp1, Vector3 pp2)
    {
        float xx = pp2.x - pp1.x;
        float zz = pp2.z - pp1.z;
        float rad = Mathf.Atan2(zz, xx);
        return rad * Mathf.Rad2Deg;
    }

    void Shot()
    {
        // 砲弾が装填されていたら、砲撃をする
        if (next)
        {
            // 砲弾を生成してID登録をする
            var obj = Instantiate(Bullet, Muzzle.position, Quaternion.Euler(0, Barrel_Main.eulerAngles.y, 0));
            obj.layer = LayerMask.NameToLayer("Enemy");
            // マズルフラッシュの再生
            MuzzleFlash_Particle.Play();
            MuzzleSmoke_Particle.Play();
            // 砲撃音の再生
            audioSource.PlayOneShot(Shot_sound);
            // 次弾装填の待ち処理の初期化
            next = false;
            time = 0;
        }
        // 次弾装填中
        if (!next)
        {
            // 時間経過
            time += Time.deltaTime;
            // 装填完了
            if (time >= interval)
            {
                next = true;
            }
        }
    }
}
