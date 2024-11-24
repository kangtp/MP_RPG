using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum QuestState {
    Startable,
    Progressing,
    Completable,
    Complete
}

[CreateAssetMenu(fileName = "New Quest", menuName = "RPG/Quest")]
public class Quest : ScriptableObject {
    public QuestState state = QuestState.Startable;

    public string title;

    [TextArea(2, 6)]
    public string content, completeDialog;

    public CollectObjective[] collectObjectives;

    public bool IsCompleteObjectives {
        get {
            foreach(var o in collectObjectives) {
                if(!o.IsComplete)
                    return false;
            }
            return true;
        }
    }

    public Rewards rewards;
}

[Serializable]
public abstract class Objective {
    public Item item;
    public int amount;
    public int currentAmount { get; set; }

    public bool IsComplete { get { return currentAmount >= amount; } }
}

[Serializable]
public class CollectObjective : Objective {

    public void UpdateItemCount() {
        currentAmount = Inventory.Instance.GetItemCount(item);
    }
}

[Serializable]
public class Rewards {
    public Item ItemReward;
    public int ItemRewardCount;
    public float EXPReward;
    public bool Reward() {
        bool addable = true;
        if(ItemReward != null)
            addable = Inventory.Instance.AddMultiple(ItemReward, ItemRewardCount);
        if (ItemReward == null)
        {
            if (ItemRewardCount == -1)   //용왕 가는 문 오픈
                GameObject.FindObjectOfType<Door>().OpenDoor();
            else if (ItemRewardCount == 0)   //토끼 보스 맵 이동
                SceneManager.LoadScene("Game2 1");
            else if (ItemRewardCount == -2)
                SceneManager.LoadScene("king"); //용왕 보스 맵 이동
        }
        if(addable) {
            GameManager.Instance.player.IncreaseExp(EXPReward);
            if(ItemReward != null)
                NotificationManager.Instance.Generate_GetItem(ItemReward.name, ItemRewardCount);
        }
        else
            NotificationManager.Instance.Generate_InventoryIsFull();
        return addable;
    }
}