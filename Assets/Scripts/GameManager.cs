using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Core Controller of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    TaskList taskList;

    // Start is called before the first frame update
    void Start()
    {
        CreateList();
    }

    /// <summary>
    /// Test Method to Generate the list
    /// </summary>
    void CreateList()
    {
        taskList.titleLabel.text = "TASK LIST";
        taskList.CreateTestList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
