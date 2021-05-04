using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{

    [SerializeField]
    ScrollRect scrollRect;


    [SerializeField]
    VerticalLayoutGroup content;
    [SerializeField]
    TaskListItem prefabTaskListItem;

    public List<TaskListItem> items;
    public TextMeshProUGUI titleLabel, emptyListLabel;
    
    /// <summary>
    /// Updates the content shown in the list
    /// Should be called when adding or removing items
    /// </summary>
    void UpdateList()
    {
        bool isListEmpty = items.Count == 0;

        //if the list is empty show/hide the content and the empty label
        content.gameObject.SetActive(!isListEmpty);
        emptyListLabel.gameObject.SetActive(isListEmpty);
    }

    /// <summary>
    /// Add an item to the list
    /// </summary>
    /// <param name="itemText">Title of the added Item</param>
    public void AddListItem(string itemText)
    {
        TaskListItem newItem = Instantiate(prefabTaskListItem, content.transform);
        items.Add(newItem);
        newItem.text = itemText.Trim();
    

        //Gets the actual size of the message by iterating each lines of the text

        TextMeshProUGUI label = newItem.label;
        label.GetComponent<RectTransform>().sizeDelta = new Vector2(label.GetComponent<RectTransform>().sizeDelta.x, 0);

        float chatItemHeight = 0;
        foreach (var line in label.GetTextInfo(newItem.text).lineInfo)
        {
            chatItemHeight += line.lineHeight;
        }

        //Checks if the chatItemHeight is below the minimum of 5, if it ain't then we apply the new height) 
        chatItemHeight = chatItemHeight < 30 ? 30 : chatItemHeight;

        RectTransform chatItemRectTransform = label.GetComponent<RectTransform>();
        //SizeDelta.x is the Width of the RectTransform (this could be done in the ChatItem component)
        chatItemRectTransform.sizeDelta = new Vector2(chatItemRectTransform.sizeDelta.x, chatItemHeight);

        //Dynamically sets the height of the chat (this could be done in a ChatContent component)
        RectTransform chatContentRecTransform = content.GetComponent<RectTransform>();

        float finalHeight = 
            chatItemHeight + 
            chatContentRecTransform.GetComponent<VerticalLayoutGroup>().spacing + 
            chatContentRecTransform.GetComponent<VerticalLayoutGroup>().padding.top + 
            chatContentRecTransform.GetComponent<VerticalLayoutGroup>().padding.bottom;

        chatContentRecTransform.sizeDelta = new Vector2(chatContentRecTransform.sizeDelta.x, chatContentRecTransform.sizeDelta.y + finalHeight);

        UpdateList();
    }

    /// <summary>
    /// Remove an item to the list
    /// </summary>
    /// <param name="itemText">Title of the added Item</param>
    public void RemoveListItem(TaskListItem item)
    {
        items.Remove(item);

        foreach (var taskListItem in content.transform.GetComponentsInChildren<TaskListItem>())
        {
            if (taskListItem.Id == item.Id)
            {
                Destroy(taskListItem.gameObject);
                Debug.Log($"Removed {item.name}");
            }
        }

        UpdateList();
    }

    /// <summary>
    ///Edit an item from the list
    /// </summary>
    /// <param name="itemText">Title of the added Item</param>
    public void EditListItem(TaskListItem item)
    {
        items.Add(item);
    }

    /// <summary>
    /// Get an item from the list
    /// </summary>
    /// <param name="itemText">Title of the added Item</param>
    public TaskListItem GetListItem(TaskListItem item)
    {
        return items.Find(i => i == item);
    }

    /// <summary>
    /// Get an item from the list by Id
    /// </summary>
    /// <param name="itemText">Title of the added Item</param>
    public TaskListItem GetListItem(Guid id)
    {
        return items.Find(i => i.Id == id);
    }

    /// <summary>
    /// Generates a small test list
    /// </summary>
    public void CreateTestList()
    {
        AddListItem("Do the dishes.");
        AddListItem("Loot the dungeon.");
        AddListItem("remove this task...");
        AddListItem("Collect the blue cheese.");
    }
}
