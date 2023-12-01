using UnityEngine;

public class randomDirection : MonoBehaviour
{
    public float vitesse = 35f;
    private Vector3 deplacement;

void Start()
{
      deplacement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
}
    void Update()
    {
        transform.Translate(deplacement * vitesse * Time.deltaTime);
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        if (position.x < 0 || position.x > 1 || position.y < 0 || position.y > 1)
        {
            position.x = Mathf.Repeat(position.x, 1.0f);
            position.y = Mathf.Repeat(position.y, 1.0f);
            transform.position = Camera.main.ViewportToWorldPoint(position);
        }
    }
}