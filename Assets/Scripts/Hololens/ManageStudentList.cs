using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ManageStudentList : MonoBehaviour
{
    [SerializeField] private Transform studentListInMenu;
    [SerializeField] private TextMeshPro studentCounterLabel;
    [SerializeField] private Transform studentNamePrefab;
    private int studentCounter = 0;

    public void UpdateStudentList()
    {
        List<string> studentList = NetworkManager.Singleton.GetComponent<StartLesson>().GetStudentList();

        DeleteStudentList();

        foreach (var studentName in studentList)
        {
            UpdateStudentListSpecific(studentName);
        }
    }


    public void UpdateStudentListSpecific(string studentName)
    {
        studentCounter++;
        studentCounterLabel.text = "Studenti collegati: " + studentCounter;

        studentNamePrefab.GetComponent<TextMeshPro>().text = studentName;
        Instantiate(studentNamePrefab, studentListInMenu);
        studentListInMenu.GetComponent<GridObjectCollection>().UpdateCollection();
    }


    public void DeleteStudentList()
    {
        studentCounter = 0;
        foreach (Transform child in studentListInMenu)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }

        Debug.Log("DeleteStudentList() " + studentListInMenu.childCount);
    }

    public void RemoveStudentSpecific(string studentName)
    {
        Debug.Log("5. Start RemoveStudentSpecific: " + studentListInMenu);
        foreach (Transform student in studentListInMenu)
        {
            if (student.gameObject.GetComponent<TextMeshPro>().text.Equals(studentName))
            {
                Destroy(student.gameObject);
                studentCounter--;
                studentCounterLabel.text = "Studenti collegati: " + studentCounter;
            }
        }
        studentListInMenu.GetComponent<GridObjectCollection>().UpdateCollection();
        Debug.Log("6. Finish RemoveStudentSpecific: " + studentListInMenu);
    }
}