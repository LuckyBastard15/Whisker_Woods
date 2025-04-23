using UnityEngine;

public class PlayerDies : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject enemy4;
    [SerializeField] private Transform player3D;

    public float followSpeed = 3f;         // Velocidad actual
    public float speedIncreaseRate = 0.5f; // Qué tanto sube por segundo
    public float maxSpeed = 5f;           // Límite de velocidad

    private bool isFollowing = false;

    public void PlayerDiesActive()
    {
        Debug.Log("PlayerDiesActive");
        SetEnemysActive();
        isFollowing = true;
    }

    public void SetEnemysActive()
    {
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);
        enemy4.SetActive(true);
    }

    void Update()
    {
        if (isFollowing)
        {
            // Aumentar la velocidad gradualmente
            followSpeed += speedIncreaseRate * Time.deltaTime;
            followSpeed = Mathf.Clamp(followSpeed, 0f, maxSpeed); // no pasar del límite

            // Seguir al jugador
            FollowAndLookAtPlayer(enemy1);
            FollowAndLookAtPlayer(enemy2);
            FollowAndLookAtPlayer(enemy3);
            FollowAndLookAtPlayer(enemy4);
        }
    }

    public void KillPlayerNow()
    {
        Time.timeScale = 0;
    }

    private void FollowAndLookAtPlayer(GameObject enemy)
    {
        Vector3 direction = (player3D.position - enemy.transform.position).normalized;
        enemy.transform.position += direction * followSpeed * Time.deltaTime;

        // Mirar al jugador
        Vector3 lookDirection = player3D.position - enemy.transform.position;
        lookDirection.y = 0f;
        if (lookDirection != Vector3.zero)
            enemy.transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
