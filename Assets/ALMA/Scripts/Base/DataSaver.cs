using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PopupDataWrapper
{
    public List<PopupData> PopupDataList;
}

public class DataSaver
{
    public void SaveToJson(PopupData data, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PopupDataWrapper popupDataListWrapper = JsonUtility.FromJson<PopupDataWrapper>(json);
            popupDataListWrapper.PopupDataList.Add(data);
            string updatedJson = JsonUtility.ToJson(popupDataListWrapper, true);
            File.WriteAllText(path, updatedJson);
        }
        else
        {
            PopupDataWrapper popupDataListWrapper = new PopupDataWrapper {PopupDataList = new List<PopupData> { data } };
            string json = JsonUtility.ToJson(popupDataListWrapper, true);
            File.WriteAllText(path, json);
        }

        Debug.Log("Data saved to " + path);
    }

    public PopupData LoadDataById(string popupId, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PopupDataWrapper popupDataWrapper = JsonUtility.FromJson<PopupDataWrapper>(json);
            return popupDataWrapper.PopupDataList.Find(data => data.Id == popupId);
        }

        return null;
    }

    public List<PopupData> LoadAllData(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PopupDataWrapper popupDataWrapper = JsonUtility.FromJson<PopupDataWrapper>(json);
            return popupDataWrapper.PopupDataList;
        }

        return new List<PopupData>();
    }

    public void UpdateDataById(PopupData data, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PopupDataWrapper popupDataListWrapper = JsonUtility.FromJson<PopupDataWrapper>(json);

            int index = popupDataListWrapper.PopupDataList.FindIndex(item => item.Id == data.Id);
            Debug.Log(index);
            if (index != -1)
            {
                popupDataListWrapper.PopupDataList[index] = data;
                string updatedJson = JsonUtility.ToJson(popupDataListWrapper, true);
                File.WriteAllText(path, updatedJson);
                Debug.Log("Data updated for ID: " + data.Id);
            }
            else
            {
                Debug.LogWarning("Data with ID " + data.Id + " not found.");
            }
        }
        else
        {
            Debug.LogWarning("JSON file does not exist: " + path);
        }
    }

    public void ClearJsonFile(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.json");

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("JSON file cleared: " + path);
        }
        else
        {
            Debug.LogWarning("JSON file does not exist: " + path);
        }
    }
}
