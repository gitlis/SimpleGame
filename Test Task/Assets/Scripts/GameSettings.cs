using System.Collections.Generic;
using System.IO;
using System.Text;
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
        SourceFile = "Settings.txt";
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
            CreateLastingList(5, 15, 10);

            WriteSettingsToFile(Path);
        }
    }

    private void CreateLastingList(float minValue, float maxValue, int count)
    {
        while (SettingsGame.ListOfTimerLastings.Count < count)
        {
            var lasting = Random.Range(minValue, maxValue);
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
        ReadSettingsFromFile(fileName);
    }

    private void ReadSettingsFromFile(string fileName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(fileName);
        using (TextReader list = new StringReader(textAsset.text))
        {
            var lastings = SettingsGame.ListOfTimerLastings;
            var colors = SettingsGame.ListOfFigureColors;

            float lasting;
            Color color = new Color();

            while (list.Peek() != -1)
            {
                var stringLine = list.ReadLine();
                if (float.TryParse(stringLine, out lasting)) lastings.Add(lasting);
                else
                {
                    var colorComponents = stringLine.Split(';');
                    float.TryParse(colorComponents[0], out color.r);
                    float.TryParse(colorComponents[1], out color.g);
                    float.TryParse(colorComponents[2], out color.b);
                    float.TryParse(colorComponents[3], out color.a);

                    colors.Add(color);
                }
            }
        }
    }

    private void WriteSettingsToFile(string fileName)
    {
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            var lastings = SettingsGame.ListOfTimerLastings;
            var colors = SettingsGame.ListOfFigureColors;

            foreach (var lasting in lastings)
                sw.WriteLine(lasting);

            foreach (var color in colors)
                sw.WriteLine(color.r + ";" + color.g + ";" + color.b + ";" + color.a);
        }
    }
}
