using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Son plus fort : il part de la caméra
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        //Son plus faible -> il part du bloc
        AudioSource.PlayClipAtPoint(breakSound, this.transform.position);
        Destroy(gameObject);
    }
}
