using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public RandomSfxPlayer sfxPlayer;

    public int health = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            sfxPlayer.PlayDamage();
            health--;

            if (health <= 0)
            {
                print("Player Defeated");
                // Reload scene, MAKE sure scene index exists
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }


        }
    }


}
