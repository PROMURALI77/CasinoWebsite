using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Card
/// </summary>
public class Card //מחלקת קלפים
{
    private int value;
    private char sign;
    public Card() { }
    public Card(int value, char sign)//פעולה בונה
    {
        this.value = value;
        this.sign = sign;
    }
    //החזרת ערכים
    public int GetValue() { return value; }
    public char GetSign() { return sign; }

    public void SetValue(int value) { this.value = value; }

    public override string ToString()
    {
        string s = "Card Value:" + value;
        s += "\nCard Sign:";
        if (sign == 'h')
            s += "heart";
        else if (sign == 'c')
            s += "clover";
        else if (sign == 'd')
            s += "diamond";
        else if (sign == 'l')
            s += "leaf";
        return s;
    }
}