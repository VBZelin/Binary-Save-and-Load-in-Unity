using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using ObjectAttribute;

public class UI : MonoBehaviour
{
    // Reference of number
    public int number;

    public Voltage volt;

    // Game object
    GameObject gameObj1, gameObj2, input, inputField;

    // Reference of vector3
    Vector3 position1, position2;

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
        gameObj1 = GameObject.Find("Object1");
        gameObj2 = GameObject.Find("Object2");
        input = GameObject.Find("Input");
        inputField = GameObject.Find("InputField");

        position1 = gameObj1.transform.position;
        position2 = gameObj2.transform.position;

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
        gameObj1.transform.position = position1;
        gameObj2.transform.position = position2;
    }

    // GUI
    void OnGUI()
    {
        // label
        GUI.Label(new Rect(0, 50, 200, 30), "Number: " + number, guiStyle);

        GUI.Label(new Rect(0, 75, 200, 30), "Voltage: " + volt.ToString(), guiStyle);

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
            position1.x += 1;
            position2.x += 1;
        }
        if (GUI.Button(new Rect(0, 250, 200, 30), "Right"))
        {
            position1.x -= 1;
            position2.x -= 1;
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
            systemFile.Position1 = position1;
            systemFile.Position2 = position2;
            systemFile.Volt = Voltage.Positive;

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
                position1 = systemFile.Position1;
                position2 = systemFile.Position2;
                volt = systemFile.Volt;
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
