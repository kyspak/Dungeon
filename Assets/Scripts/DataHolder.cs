using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DataHolder
{
    public static string nickname;
    public static string currentProfileName=PlayerPrefs.GetString("nickname");
    public static int currentScore=0;
    public static int profileCount;
    public static int locations;
    public static int difficulty;
    public static float currentTime;
    //0.�����, 1.���� �� ���������, 2. ���� �� ���
    public static List<float> difficultySettings;
}
