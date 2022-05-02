using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text username;
    public Text totalCount;
    public Text maxScore;
    public Button joinButton;
    
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

    public void SetRoomInform(string username, int totalCount, int maxScore)
    {
        this.username.text = username;
        this.totalCount.text ="总场数\n" + totalCount;
        this.maxScore.text = "最高得分\n" + maxScore;
    }
    
    public void SetRoomInform(string username, string totalCount, string maxScore)
    {
        this.username.text = username;
        this.totalCount.text ="总场数\n" + totalCount;
        this.maxScore.text = "最高得分\n" + maxScore;
    }

    private void OnJoinClick()
    {
        
    }
}
