using Common;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMakeFood : MonoBehaviour
{
    public GameObject dishFoodPrefab;
    private Animator anim;
    private Transform handTrans;
    private Vector3 shootDir;
    private PlayerManager playerMng;

    private void Start()
    {
        anim = GetComponent<Animator>();
        handTrans = transform.Find("HandPos");
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(EventSystem.current.IsPointerOverGameObject()) return;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit);
                if (isCollider)
                {
                    Vector3 targetPoint = hit.point;
                    targetPoint.y = transform.position.y;
                    shootDir = targetPoint - transform.position;
                    transform.rotation = Quaternion.LookRotation(-shootDir);
                    anim.SetTrigger("MakeFood");
                    if (gameObject.GetComponent<PlayerInfo>().roleType == RoleType.Chef1)
                    {
                        GameFacade.Instance.PlayNormalSound(AudioManager.SoundCut);
                    }
                    else
                    {
                        GameFacade.Instance.PlayNormalSound(AudioManager.SoundFry);
                    }
                    Invoke(nameof(ShootFood), 5f);
                }
            }
        }
    }

    public void SetPlayMng(PlayerManager playMng)
    {
        this.playerMng = playMng;
    }

    private void ShootFood()
    {
        playerMng.MakeFood(dishFoodPrefab, handTrans.position, Quaternion.LookRotation(shootDir));
    }
}