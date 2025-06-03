using UnityEngine;

public class JumpScare : MonoBehaviour
{
   [SerializeField] GameObject jumpScare;


    private void OnTriggerEnter(Collider other)
    {
        jumpScare.SetActive(false);
    }

}
