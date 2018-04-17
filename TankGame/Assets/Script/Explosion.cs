using UnityEngine;

public class Explosion : MonoBehaviour
{

    /// パーティクル
    [SerializeField]
    ParticleSystem particle;
    /// オーディオソース
    [SerializeField]
    AudioSource audioSource;
    /// 爆破音
    [SerializeField]
    AudioClip[] Explosion_sound;

    /// 初期化
	void Start()
    {
        // ランダムで音を再生
        int ii = Random.Range(0, Explosion_sound.Length);
        audioSource.PlayOneShot(Explosion_sound[ii]);
    }

    /// メインループ
	void Update()
    {
        // エフェクトが終わったら削除
        if (!particle.isPlaying)
            Destroy(gameObject);
    }

}
