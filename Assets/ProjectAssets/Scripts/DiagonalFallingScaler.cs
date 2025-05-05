using UnityEngine;

public class DiagonalFallingScaler : MonoBehaviour
{
    public Vector2 direction = new Vector2(1f, -1f); // Dirección diagonal
    public float speed = 2f;                         // Velocidad de movimiento

    public float growDuration = 2f;                  // Tiempo de crecimiento
    public float shrinkDuration = 2f;                // Tiempo de reducción
    public float maxScale = 2f;                      // Tamaño máximo

    private Vector3 originalScale;
    private float timer = 0f;
    private bool isGrowing = true;

    void Start()
    {
        direction.Normalize();
        originalScale = transform.localScale;

        // Destruir el objeto después de 5 segundos
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        // Movimiento diagonal
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Escalado
        timer += Time.deltaTime;

        if (isGrowing)
        {
            float t = Mathf.Clamp01(timer / growDuration);
            transform.localScale = Vector3.Lerp(originalScale, originalScale * maxScale, t);

            if (t >= 1f)
            {
                isGrowing = false;
                timer = 0f;
            }
        }
        else
        {
            float t = Mathf.Clamp01(timer / shrinkDuration);
            transform.localScale = Vector3.Lerp(originalScale * maxScale, originalScale, t);
        }
    }
}
