using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Reference of number
    public int number;

    // Game object
    GameObject gameObj, input, inputField;

    // Reference of vector3
    Vector3 position;

    // Reference of the input text
    string inputText;

    // Reference of boolean
    bool ifSave, ifLoad;

    // GUI style
    GUIStyle guiStyle;

    // Reference of buttons
    Button saveBt, loadBt, enterBt;

    // Reference of input field
    InputField inputFieldPlace;

    // Awake
    void Awake()
    {
        // get component
        gameObj = GameObject.Find("Object");
        input = GameObject.Find("Input");
        inputField = GameObject.Find("InputField");

        position = gameObj.transform.position;

        // buttons
        saveBt = GameObject.Find("saveButton").GetComponent<Button>();
        loadBt = GameObject.Find("loadButton").GetComponent<Button>();
        enterBt = GameObject.Find("Enter").GetComponent<Button>();

        // input field place
        inputFieldPlace = input.GetComponent<InputField>();

        // add function
        saveBt.onClick.AddListener(Save);
        loadBt.onClick.AddListener(Load);

        // GUI style
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 18;
        guiStyle.normal.textColor = Color.white;

        // initialize the scene
        inputFieldPlace.onEndEdit.AddListener(EndEdit);
        enterBt.onClick.AddListener(Enter);
        inputField.SetActive(false);
    }

    // Update
    void Update()
    {
        gameObj.transform.position = position;
    }

    // GUI
    void OnGUI()
    {
        // label
        GUI.Label(new Rect(0, 50, 200, 30), "Number: " + number, guiStyle);

        // buttons
        if (GUI.Button(new Rect(0, 100, 200, 30), "Plus 100"))
        {
            number += 100;
        }
        if (GUI.Button(new Rect(0, 150, 200, 30), "Minus 100"))
        {
            number -= 100;
        }
        if (GUI.Button(new Rect(0, 200, 200, 30), "Left"))
        {
            position.x += 1;
        }
        if (GUI.Button(new Rect(0, 250, 200, 30), "Right"))
        {
            position.x -= 1;
        }
    }

    // Save
    void Save()
    {
        // show input field
        inputField.SetActive(true);

        // change status
        ifSave = true;
    }


    // Load
    void Load()
    {
        // show input field
        inputField.SetActive(true);

        // change status
        ifLoad = true;
    }

    void EndEdit(string input_text)
    {
        inputText = input_text;
    }

    // Confirm
    void Enter()
    {
        // if save
        if (ifSave)
        {
            // create a file
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + inputText + ".dat");
            SystemFile systemFile = new SystemFile();
            systemFile.Number = number;
            systemFile.Position = position;

            binaryFormatter.Serialize(file, systemFile);
            file.Close();

            Debug.Log("File is saved!");

            // close the input field
            ifSave = false;
            inputField.SetActive(false);
        }
        // if load file
        if (ifLoad)
        {
            if (File.Exists(Application.persistentDataPath + "/" + inputText + ".dat"))
            {
                // load a file
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + inputText + ".dat", FileMode.Open);
                SystemFile systemFile = (SystemFile)binaryFormatter.Deserialize(file);
                file.Close();

                number = systemFile.Number;
                position = systemFile.Position;
            }
            else
            {
                Debug.Log("File does not exit!");
            }

            // close the input field
            ifLoad = false;
            inputField.SetActive(false);
        }
    }
}
