using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public ParticleSystem explosionParticle; // Ссылка на объект частицы взрыва

    public bool isDead = false;

    private Animator animator;

    private SoundManager soundManager;

    private CubeJump cubeJump;

    private SaveManager saveManager;

    private void Start()
    {
        cubeJump = GetComponent<CubeJump>();
        animator = GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();

        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();
    }

    //public int hp = 2; 

    public void Die()
    {


        if (!isDead)
        {
            saveManager.GetDataLeaderboardScores();
            
            //YanCloud
            saveManager.MySave();

            cubeJump.canMove = false;

            soundManager.PlayDeathSound();

           isDead = true;

           animator.SetBool("Explode", true);



            // Активируем объект частицы взрыва
            explosionParticle.Play();
            // Здесь вы можете выполнить другие действия, связанные с смертью персонажа

        }

    }


}
