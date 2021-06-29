using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StringFunctions
/// </summary>
public static class StringFunctions
{
    public static string DeleteIdExtention(string id)
    {
        Char[] idArr = id.ToCharArray();
        string s = "";
        for (int i = 0; i < idArr.Length; i++)
        {
            if (idArr[i] == '_')
                break;
            s += idArr[i];
        }
        return s;
    }//מוחק תוספת של הזהות של אלמנט
    public static string GetIdExtention(string id)
    {
        Char[] idArr = id.ToCharArray();
        string s = "";
        bool flag = false;
        for (int i = 0; i < idArr.Length; i++)
        {
            if (flag)
                s += idArr[i];
            if (idArr[i] == '_')
                flag = true;
        }
        return s;
    }//מחזיר תוספת של אלמנט 
    public static string GetUrlExtension(string url)
    {
        string extension = "";
        bool flag = false;
        char[] chars = url.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (flag)
                extension += chars[i];
            if (chars[i] == '?')
                flag = true;
        }
        if (flag == false)
            return "";
        return extension;
    }//מחזיר את התוספת של כתֹובת
    public static string CheckPrimaryKey(string primaryKey)
    {
        char[] arr = primaryKey.ToCharArray();
        foreach (char c in arr)
            if (c == '@')
                return "Email";
        return "UserName";
    }//בודק האם המפתח הראשי הוא מייל או שם משתמש
    public static string GetEqual(string equal, string extension)
    {
        List<string> equalsNames = new List<string>(), equals = new List<string>();
        List<char> temp = new List<char>();
        char[] arr = extension.ToCharArray();
        for (int i = 0; i < extension.Length; i++)
        {
            if (arr[i] == '=')
            {
                equalsNames.Add(ListToString(temp));
                temp = new List<char>();
            }
            else if (arr[i] == ',')
            {
                equals.Add(ListToString(temp));
                temp = new List<char>();
            }
            else
            {
                temp.Add(arr[i]);
            }
        }
        equals.Add(ListToString(temp));
        string[] equalsNamesArr = equalsNames.ToArray(), equalsArr = equals.ToArray();
        for (int i = 0; i < equalsNamesArr.Length; i++)
        {
            if (equalsNamesArr[i] == equal && equalsArr[i] != null)
                return equalsArr[i];
        }
        return "";
    }//מחזיר את הטקסטו בין שווה לפסיק
    public static string ListToString(List<char> list)
    {
        string value = "";
        foreach (char c in list)
        {
            value += c;
        }
        return value;
    }//הופך רשימה של תווים למחרוזת
}