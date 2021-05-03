using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListItem : MonoBehaviour
{
    public Guid Id;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    TextMeshProUGUI label;

    [SerializeField]
    Button saveIcon, editIcon, cancelIcon, deleteIcon;

    /// <summary>
    /// Item text
    /// </summary>
    public string text = "New Task";

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize the starting values and actions of a task item
    /// </summary>
    private void Initialize()
    {
        //Associates a method with the onclick behavior for each buttons
        saveIcon.onClick.AddListener(delegate { SaveEdit(); });
        editIcon.onClick.AddListener(delegate { StartEdit(); });
        cancelIcon.onClick.AddListener(delegate { CancelEdit(); });
        deleteIcon.onClick.AddListener(delegate { DeleteItem(); });

        Id = Guid.NewGuid();

        Show(editIcon);
        Show(deleteIcon);

        Hide(saveIcon);
        Hide(cancelIcon);

        inputField.gameObject.SetActive(false);
        label.gameObject.SetActive(true);
        inputField.text = text;
        label.text = text;
    }

    #region Actions
    private void DeleteItem()
    {
        TaskList parentList = GetComponentInParent<TaskList>();
        parentList.RemoveListItem(this);
    }

    private void Edit()
    {
        inputField.gameObject.SetActive(true);
        label.gameObject.SetActive(false);
    }

    private void Save()
    {
        inputField.gameObject.SetActive(false);
        label.gameObject.SetActive(true);

        text = inputField.text;
        label.text = text;
    }

    private void Cancel()
    {
        inputField.gameObject.SetActive(false);
        label.gameObject.SetActive(true);
        inputField.text = label.text;
    }
    #endregion


    #region Edit Actions
    public void StartEdit()
    {
        Show(cancelIcon);
        Show(saveIcon);
        Hide(editIcon);
        Hide(deleteIcon);
        Edit();
    }

    public void SaveEdit()
    {
        Show(editIcon);
        Show(deleteIcon);
        Hide(saveIcon);
        Hide(cancelIcon);
        Save();
    }

    public void CancelEdit()
    {
        Show(editIcon);
        Show(deleteIcon);
        Hide(saveIcon);
        Hide(cancelIcon);
        Cancel();
    }
    #endregion

    public void Hide(Button button)
    {
        button.gameObject.SetActive(false);
    }

    public void Show(Button button)
    {
        button.gameObject.SetActive(true);
    }
}
