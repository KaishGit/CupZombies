using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instancia;

    //Musica
    [SerializeField] private AudioSource AudioSourceBackground;
    [SerializeField] private AudioClip[] ClipsBackground;
    //SFX
    [SerializeField] private AudioSource AudioSourceSFX;
    [SerializeField] private AudioClip ApertarEspaco;
    [SerializeField] private AudioClip AcertarIngrediente;
    [SerializeField] private AudioClip ErrarIngrediente;
    [SerializeField] private AudioClip PowerUp;
    [SerializeField] private AudioClip LancarShuricookie;
    [SerializeField] private AudioClip NinjaCakeMorte;
    [SerializeField] private AudioClip ZumbiMorte;
    [SerializeField] private AudioClip VitoriaFase;
    [SerializeField] private AudioClip DerrotaFase;
    [SerializeField] private AudioClip BotaoUi;

    private void Awake()
    {
        if(Instancia == null)
        {
            Instancia = this;
        }
    }

    private void Start()
    {
        AudioSourceBackground.clip = ObterClipBackground();
        AudioSourceBackground.Play();
    }

    private AudioClip ObterClipBackground()
    {
        return ClipsBackground[Random.Range(0, ClipsBackground.Length)];
    }

    public void PlaySfxApertarEspaco()
    {
        AudioSourceSFX.PlayOneShot(ApertarEspaco);
    }

    public void PlaySfxAcertarIngrediente()
    {
        AudioSourceSFX.PlayOneShot(AcertarIngrediente);
    }

    public void PlaySfxErrarIngrediente()
    {
        AudioSourceSFX.PlayOneShot(ErrarIngrediente);
    }

    public void PlaySfxPowerUp()
    {
        AudioSourceSFX.PlayOneShot(PowerUp);
    }

    public void PlaySfxLancarShuricookie()
    {
        AudioSourceSFX.PlayOneShot(LancarShuricookie);
    }

    public void PlaySfxNinjaCakeMorte()
    {
        AudioSourceSFX.PlayOneShot(NinjaCakeMorte);
    }

    public void PlaySfxZumbiMorte()
    {
        AudioSourceSFX.PlayOneShot(ZumbiMorte);
    }

    public void PlaySfxVitoriaFase()
    {
        AudioSourceSFX.PlayOneShot(VitoriaFase);
    }

    public void PlaySfxDerrotaFase()
    {
        AudioSourceSFX.PlayOneShot(DerrotaFase);
    }

    public void PlaySfxBotaoUi()
    {
        AudioSourceSFX.PlayOneShot(BotaoUi);
    }

    public void PausarMusica()
    {
        AudioSourceBackground.Pause();
    }

    public void DespausarMusica()
    {
        AudioSourceBackground.Play();
    }
}