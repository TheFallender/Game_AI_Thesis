using UnityEngine;
using System.Collections.Generic;

//DynamicCounter: Will store the records of different sets of info
public class DynamicCounter : MonoBehaviour {
    //Instance
    public static DynamicCounter Instance;

    //Text to add the info
    private UnityEngine.UI.Text unity_text;

    //Dictionary with CounterInfo
    private Dictionary<string, CounterInfo> records = new Dictionary<string, CounterInfo>();

    //Awake method
    private void Awake () {
        //Singleton
        if (Instance == null) {
            unity_text = GetComponent<UnityEngine.UI.Text>();
            Instance = this;
        } else
            Destroy(this);
    }

    //Add record
    public void AddRecord (string key, string textInfo) {
        if (!records.ContainsKey(key)) {
            records.Add(
                key,
                new CounterInfo {
                    infoString = textInfo,
                    counter = 0
                }
            );
            OnRecordsChanged();
        }
    }

    //Delete record
    public void RemoveRecord (string key) {
        if (records.ContainsKey(key)) {
            records.Remove(key);
            OnRecordsChanged();
        }
    }

    //Add one to value
    public void TickRecord (string key) {
        if (records.TryGetValue(key, out CounterInfo temp)) {
            temp.counter++;
            records[key] = temp;
            OnRecordsChanged();
        }
    }

    //Reset record to zero
    public void ResetRecord (string key) {
        if (records.TryGetValue(key, out CounterInfo temp)) {
            temp.counter = 0;
            records[key] = temp;
            OnRecordsChanged();
        }
    }

    //OnRecordsChanged
    private void OnRecordsChanged () {
        string textToDisplay = "";
        foreach (var set in records) {
            textToDisplay +=
                set.Value.infoString +
                ": " +
                set.Value.counter +
                "\n";
        }
        UpdateText(textToDisplay);
    }

    //Update text
    private void UpdateText (string newText) {
        unity_text.text = newText;
    }
}

//Struct that holds the records info
public struct CounterInfo {
    public string infoString;
    public uint counter;
}