using Common;
using Manager;
using UnityEngine;

public class DishFood : MonoBehaviour
{
    public RoleType roleType;
    public int speed = 5;
    public bool isLocal = false;
    private Rigidbody rgd;

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rgd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Table") || other.CompareTag($"Wall"))
        {
            if (other.CompareTag($"Table"))
            {
                if (isLocal)
                {
                    GameFacade.Instance.SendScore(Random.Range(10, 20));
                }
                GameFacade.Instance.PlayNormalSound(AudioManager.SoundScore);
            }
            GameObject.Destroy(this.gameObject);
        }
    }
}
