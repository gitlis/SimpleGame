using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;


public class GameSettings
{
    public class Settings
    {
        public List<float> ListOfTimerLastings { get; private set; }
        public List<Color> ListOfFigureColors { get; private set; }

        public Settings()
        {
            ListOfFigureColors = new List<Color>();
            ListOfTimerLastings = new List<float>();
        }
    }

    public Settings SettingsGame { get; set; }
    public readonly string SourceFile;
    public readonly string Path;

    public GameSettings()
    {
        SourceFile = "Settings.xml";
        Path = Application.dataPath.ToString() + "/Resources/" + SourceFile;
        SettingsGame = new Settings();
        SetSettings();
    }

    private void SetSettings()
    {
        if (File.Exists(Path)) ReadSettingsForGame(SourceFile.Split('.')[0]);
        else
        {
            CreateColorList(10);
            CreateLastingList(20, 10);

            XmlSerializer settingsSerialiser = new XmlSerializer(typeof(Settings));
            WriteSettingsToFile(settingsSerialiser, Path);
        }
    }

    private void CreateLastingList(float maxValue, int count)
    {
        while (SettingsGame.ListOfTimerLastings.Count < count)
        {
            var lasting = Random.Range(1, maxValue);
            if (!SettingsGame.ListOfTimerLastings.Contains(lasting)) SettingsGame.ListOfTimerLastings.Add(lasting);
        }
    }

    private void CreateColorList(int count)
    {

        while (SettingsGame.ListOfFigureColors.Count < count)
        {
            var color = Random.ColorHSV();
            if (!SettingsGame.ListOfFigureColors.Contains(color)) SettingsGame.ListOfFigureColors.Add(color);
        }
    }

    private void ReadSettingsForGame(string fileName)
    {
        XmlSerializer settingsSerialiser = new XmlSerializer(typeof(Settings));
        ReadSettingsFromFile(settingsSerialiser, fileName);
    }

    private void ReadSettingsFromFile(XmlSerializer formatter, string fileName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(fileName);
            using (TextReader list = new StringReader(textAsset.text))
            {
                SettingsGame = (Settings)formatter.Deserialize(list);
            }
    }

    private void WriteSettingsToFile(XmlSerializer formatter, string fileName)
    {
        var path = Path;
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, SettingsGame);
        }


    }


}

//TextAsset textAssetColors = (TextAsset)Resources.Load("Colors");
//TextAsset textAssetLastings = (TextAsset)Resources.Load("Lastings");
// var listColors = list.Re
// var colorComponents = new float[4];
