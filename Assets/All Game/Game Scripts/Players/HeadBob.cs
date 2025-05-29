using DG.Tweening;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public CharacterController playerController;  // asigna el CharacterController del jugador
    public float bobAmount = 0.05f;               // amplitud del movimiento
    public float bobDuration = 0.3f;              // duración de medio ciclo
    private Vector3 initialPosition;
    private Tween bobTween;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        bool isMoving = playerController.isGrounded && playerController.velocity.magnitude > 0.1f;

        if (isMoving && (bobTween == null || !bobTween.IsPlaying()))
        {
            StartHeadBob();
        }
        else if (!isMoving && bobTween != null && bobTween.IsPlaying())
        {
            StopHeadBob();
        }
    }

    void StartHeadBob()
    {
        // Movimiento en círculo (X e Y)
        bobTween = DOTween.To(() => 0f, t =>
        {
            float x = Mathf.Cos(t * Mathf.PI * 2) * bobAmount;
            float y = Mathf.Sin(t * Mathf.PI * 4) * bobAmount;
            transform.localPosition = initialPosition + new Vector3(x, y, 0);
        }, 1f, bobDuration)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);
    }

    void StopHeadBob()
    {
        bobTween.Kill();
        bobTween = null;
        transform.DOLocalMove(initialPosition, 0.2f);
    }
}
