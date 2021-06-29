using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Player
/// </summary>
public class Player//מחלקת שחקנים
{
    private int id;
    private string userName;
    private int cash;

    public Player(int id, string userName, int cash)
    {
        this.id = id;
        this.userName = userName;
        this.cash = cash;
    }
}