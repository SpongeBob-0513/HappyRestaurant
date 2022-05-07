using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text username;
    public Text totalCount;
    public Text maxScore;
    public Button joinButton;

    private int id;
    private RoomListPanel panel;
    
    // Start is called before the first frame update
    void Start()
    {
        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoomInform(int id, string username, int totalCount, int maxScore, RoomListPanel panel)
    {
        SetRoomInform(id, username, totalCount.ToString(), maxScore.ToString(), panel);
    }
    
    public void SetRoomInform(int id, string username, string totalCount, string maxScore, RoomListPanel panel)
    {
        this.id = id;
        this.username.text = username;
        this.totalCount.text ="总场数\n" + totalCount;
        this.maxScore.text = "最高得分\n" + maxScore;
        this.panel = panel;
    }

    private void OnJoinClick()
    {
        panel.OnJoinClick(id);
    }

    public void DestroySelf()
    {
        GameObject.Destroy(this.gameObject);
    }
}
