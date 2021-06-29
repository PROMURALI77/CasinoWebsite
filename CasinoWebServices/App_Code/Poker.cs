using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;


/// <summary>
/// Summary description for PokerFunctions
/// </summary>
public static class Poker
{

    public static Int64 CheckStraightFlush(Card[] deck)//בודק האם קיים צבע ורצף במערך של עד 7 קלפים ממוינים בסדר עולה ומחזיר מספר המייצג את היד
    {
        Card[] temp = DoubleCardArray(deck), flush;
        char[] signs = new char[4] { 'd', 'c', 'h', 'l' };
        for (int i = 0; i < signs.Length; i++)
        {
            flush = SignDeck(temp, signs[i]);
            if (flush != null)
            {
                if (flush.Length >= 5)
                {
                    Int64 num = CheckStraight(flush);
                    if (num != -1)
                        return 40000000000 + num;
                    return -1;
                }
            }
        }
        return -1;

    }
    public static Int64 CheckFourOfAKind(Card[] deck)//בודק האם קיימים ארבעה קלפים שערכם זהה במערך של עד 7 קלפים ממוינים בסדר עולה ומחזיר מספר המייצג את היד
    {
        if (deck.Length >= 4)
        {
            Card[] temp = DoubleCardArray(deck);
            int[] monim = Monim(temp);
            Int64 hand = 8;
            for (int i = 14; i >= 2; i--)
            {
                if (monim[i] == 4)
                {
                    if (deck.Length == 4)
                        for (int k = 0; k < 4; k++)
                            hand = hand * 100 + i;
                    else
                    {
                        temp = EraseValue(temp, i);
                        temp = SortDeck(temp);
                        if (i > temp[temp.Length - 1].GetValue())
                        {
                            for (int k = 0; k < 4; k++)
                                hand = hand * 100 + i;
                            hand = hand * 100 + temp[temp.Length - 1].GetValue();
                        }
                        else
                        {
                            hand = hand * 100 + temp[temp.Length - 1].GetValue();
                            for (int k = 0; k < 4; k++)
                                hand = hand * 100 + i;
                        }
                    }
                    return hand;
                }
            }
            return -1;
        }
        return -1;
    }
    public static Int64 CheckFullHouse(Card[] deck)//בודק האם קיים פול האוס במערך של עד 7 קלפים ממוינים בסדר עולה
    {
        if (deck.Length >= 5)
        {
            Card[] temp = DoubleCardArray(deck);
            int[] monim = Monim(temp);
            Int64 hand = 7;
            for (int i = 14; i >= 2; i--)
            {
                for (int k = i - 1; k >= 2; k--)
                {
                    if (monim[i] == 3 && monim[k] == 3)//i>k בהכרח
                    {
                        for (int j = 0; j < 3; j++)
                            hand = hand * 100 + i;
                        for (int j = 0; j < 2; j++)
                            hand = hand * 100 + k;
                        return hand;
                    }
                    else if (monim[i] == 3 && monim[k] == 2)
                    {
                        for (int j = 0; j < 3; j++)
                            hand = hand * 100 + i;
                        for (int j = 0; j < 2; j++)
                            hand = hand * 100 + k;
                        return hand;
                    }
                    else if (monim[i] == 2 && monim[k] == 3)
                    {
                        for (int j = 0; j < 2; j++)
                            hand = hand * 100 + i;
                        for (int j = 0; j < 3; j++)
                            hand = hand * 100 + k;
                        return hand;
                    }
                }
            }
            return -1;
        }
        return -1;
    }
    public static Int64 CheckFlush(Card[] deck)//בודק צבע במערך של קלפים ממוינים בסדר עולה ומחזיר מספר המייצג את היד
    {
        if (deck.Length >= 5)
        {
            int c = 0;
            char[] signs = new char[4] { 'd', 'c', 'h', 'l' };
            char sign = 'n';
            for (int i = 0; i < signs.Length; i++)//בודק האם קיים צבע ואם כן מוצא מהו הצבע
            {
                for (int k = 0; k < deck.Length; k++)
                {
                    if (deck[k].GetSign() == signs[i])
                        c++;
                    if (c >= 5)
                    {
                        sign = signs[i];
                    }
                }
                c = 0;
            }
            if (sign != 'n')//החזרת מספר מתאים
            {
                Int64 hand = 6;
                c = deck.Length - 1;
                while (hand < 60000000000)
                {
                    if (deck[c].GetSign() == sign)
                        hand = hand * 100 + deck[c].GetValue();
                    c--;
                }
                return hand;
            }
            else
                return -1;
        }
        return -1;
    }
    public static Int64 CheckStraight(Card[] deck)//בודק האם קיים רצף במערך של עד 7 קלפים ממוינים בסדר עולה ומחזיר מספר המייצג את היד
    {
        if (deck.Length >= 5)
        {
            int c = 1, last = -1;
            Card[] temp = EraseDoubleValues(deck);
            if (temp.Length >= 5)
            {
                for (int i = temp.Length - 1; i > 0; i--)//בודק האם קיים רצף ואם כן מוצא את המספר הגבוה ברצף
                {
                    if (temp[i].GetValue() - 1 == temp[i - 1].GetValue())
                    {
                        c++;
                    }
                    else
                    {
                        c = 1;
                    }
                    if (c == 5)
                    {
                        last = temp[i - 1].GetValue() + 4;
                        break;
                    }
                }


                if (last != -1 & c == 5)//החזרת מספר מתאים
                {
                    Int64 hand = 5;
                    c = 0;
                    for (int i = temp.Length - 1; i > 0; i--)
                    {
                        if (temp[i].GetValue() == last)
                        {
                            while (c < 5)
                            {
                                hand = hand * 100 + temp[i - c].GetValue();
                                c++;
                            }
                            return hand;
                        }
                    }
                }
                return CheckLowStraight(deck);
            }
            return CheckLowStraight(deck);
        }
        return -1;
    }
    public static Int64 CheckLowStraight(Card[] deck)//בודק האם קיים רצף מאחד עד חמש במערך של עד 7 קלפים ממוינים בסדר עולה ומחזיר מספר המייצג את היד
    {
        if (deck.Length >= 5)
        {
            Card[] temp = EraseDoubleValues(deck);
            if (temp[temp.Length - 1].GetValue() == 14 && temp.Length >= 5)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (temp[i].GetValue() != i + 2)
                        return -1;
                }
                return 50504030201;
            }
            else
                return -1;
        }
        return -1;
    }
    public static Int64 CheckThreeOfAKind(Card[] deck)//בודק האם קיימת שלישייה במערך של עד 7 קלפים ממוינים בסדר עולה ומחזיר מספר המייצג את היד
    {
        Card[] temp = DoubleCardArray(deck);
        int[] monim = Monim(temp);
        Int64 hand = 4;
        for (int i = 14; i >= 0; i--)
        {
            if (monim[i] == 3)
            {
                temp = EraseValue(temp, i);
                temp = SortDeck(temp);
                int c = deck.Length - 3;
                if (c > 2)
                    c = 2;
                bool inserted = false;
                for (int k = temp.Length - 1; k > temp.Length - c - 1; k--)
                {
                    if (inserted)
                        hand = hand * 100 + temp[k].GetValue();
                    else if (temp[k].GetValue() > i)
                        hand = hand * 100 + temp[k].GetValue();
                    else if (i > temp[k].GetValue())
                    {
                        for (int j = 0; j < 3; j++)
                            hand = hand * 100 + i;
                        hand = hand * 100 + temp[k].GetValue();
                        inserted = true;
                    }
                }
                if (!inserted)
                    for (int j = 0; j < 3; j++)
                        hand = hand * 100 + i;
                return hand;
            }
        }
        return -1;
    }
    public static Int64 CheckTwoPair(Card[] deck)// בודק האם קיימים שני זוגות במערך של עד 7 קלפים ממוינים בסדר עולה ומחזיר מספר המייצג את היד
    {
        Card[] temp = DoubleCardArray(deck);
        int[] monim = Monim(temp);
        Int64 hand = 3;
        for (int i = 14; i >= 2; i--)
        {
            for (int k = i - 1; k >= 2; k--)//i>k
            {
                if (monim[i] == 2 && monim[k] == 2)
                {
                    if (deck.Length > 4)
                    {
                        temp = EraseValue(temp, i);
                        temp = EraseValue(temp, k);
                        temp = SortDeck(temp);
                        if (temp[temp.Length - 1].GetValue() > i && temp[temp.Length - 1].GetValue() > k)
                        {
                            hand = hand * 100 + temp[temp.Length - 1].GetValue();
                            hand = hand * 10000 + i * 100 + i;
                            hand = hand * 10000 + k * 100 + k;
                            return hand;
                        }
                        else if (temp[temp.Length - 1].GetValue() < i && temp[temp.Length - 1].GetValue() > k)
                        {
                            hand = hand * 10000 + i * 100 + i;
                            hand = hand * 100 + temp[temp.Length - 1].GetValue();
                            hand = hand * 10000 + k * 100 + k;
                            return hand;
                        }
                        else
                        {
                            hand = hand * 10000 + i * 100 + i;
                            hand = hand * 10000 + k * 100 + k;
                            hand = hand * 100 + temp[temp.Length - 1].GetValue();
                            return hand;
                        }
                    }
                    else
                    {
                        hand = hand * 10000 + i * 100 + i;
                        hand = hand * 10000 + k * 100 + k;
                        return hand;
                    }
                }
            }
        }
        return -1;
    }
    public static Int64 CheckPair(Card[] deck)//בודק האם קיים זוג במערך של עד 7 קלפים מסודרים  בסדר עולה ומחזיר מספר המייצג את היד
    {
        Card[] temp = DoubleCardArray(deck);
        int[] monim = Monim(deck);
        Int64 hand = 2;
        for (int i = 14; i >= 2; i--)
        {
            if (monim[i] == 2)
            {
                temp = EraseValue(temp, i);
                temp = SortDeck(temp);
                bool inserted = false;
                int c = deck.Length - 2;
                if (c > 3)
                    c = 3;
                for (int k = temp.Length - 1; k > temp.Length - c - 1; k--)
                {
                    if (i > temp[k].GetValue() && !inserted)
                    {
                        hand = hand * 10000 + i * 100 + i;
                        inserted = true;
                        hand = hand * 100 + temp[k].GetValue();
                    }
                    else
                        hand = hand * 100 + temp[k].GetValue();
                }
                if (!inserted)
                    hand = hand * 10000 + i * 100 + i;
                return hand;
            }
        }
        return -1;
    }
    public static Int64 HighCard(Card[] deck)//  מחזיר מספר המייצג את עד 5 הקלפים הגבוהים במערך של קלפים ממוין בסדר עולה  
    {
        Int64 hand = 1;
        Card[] temp = DoubleCardArray(deck);
        if (temp.Length < 5)
            for (int i = temp.Length - 1; i >= 0; i--)
                hand = hand * 100 + temp[i].GetValue();
        else
            for (int i = temp.Length - 1; i > temp.Length - 6; i--)
                hand = hand * 100 + temp[i].GetValue();
        return hand;
    }

    public static Card[] DoubleCardArray(Card[] deck)//משכפל מערך של קלפים
    {
        Card[] temp = new Card[deck.Length];
        for (int i = 0; i < deck.Length; i++)//בונה מערך שניתן לשנות כדי לא לפגוע בערכי המערך המקורי
        {
            temp[i] = new Card(deck[i].GetValue(), deck[i].GetSign());
        }
        return temp;
    }
    public static Card[] SortDeck(Card[] deck)
    {
        Card temp;
        for (int i = 0; i < deck.Length; i++)
        {
            for (int k = 0; k < deck.Length - 1; k++)
            {
                if (deck[k].GetValue() > deck[k + 1].GetValue())
                {
                    temp = deck[k];
                    deck[k] = deck[k + 1];
                    deck[k + 1] = temp;
                }
            }
        }
        return deck;
    }//ממיין מערך של קלפים בכל אורך        
    public static Card[] EraseDoubleValues(Card[] deck)//מוחק ערכים כפולים במערך של קלפים ממוינים
    {
        Card[] temp = DoubleCardArray(deck);
        for (int i = 0; i < temp.Length - 1; i++)//מאפס ערכים שמופיעים פעמיים
        {
            if (temp[i].GetValue() == temp[i + 1].GetValue())
                temp[i].SetValue(0);
        }
        temp = SortDeck(temp);
        int c = 0;
        for (int i = 0; i < temp.Length; i++)//בודק כמה ערכים התאפסו
        {
            if (temp[i].GetValue() == 0)
                c++;
        }
        Card[] newDeck = new Card[deck.Length - c];
        for (int i = newDeck.Length - 1; i >= 0; i--)//מציב במערך קלפים חדש רק את הערכים שאינם אפס בסדר עולה
        {
            newDeck[i] = temp[i + c];
        }
        return newDeck;
    }
    public static Card[] EraseValue(Card[] deck, int num)//מוחק ממערך קלפים קלפים שערכם שווה לערך נתון
    {
        Card[] temp = DoubleCardArray(deck);
        int c = 0;
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].GetValue() == num)
            {
                temp[i].SetValue(0);
                c++;
            }
        }
        temp = SortDeck(temp);
        return temp;
    }
    public static int HowMuchTimeValueExist<T>(T value, T[] arr)//בודק כמה פעמים קיים ערך כלשהו במערך
    {
        int c = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].Equals(value))
                c++;
        }
        return c;
    }
    public static int[] Monim(Card[] deck)//מחזיר מערך מונים המייצג כמה פעמים מופיע ערך במערך של קלפים לא כולל 0 ו- 1
    {
        Card[] temp = DoubleCardArray(deck);
        int[] monim = new int[15];
        for (int i = 0; i < temp.Length; i++)
            if (temp[i].GetValue() > 1)
                monim[temp[i].GetValue()]++;
        return monim;
    }
    public static Card[] SignDeck(Card[] deck, char sign)// מחזיר מערך של קלפים בעלי אותו סימן מתוך מערך של קלפים
    {
        Card[] temp = DoubleCardArray(deck);
        int c = 0;
        for (int i = 0; i < temp.Length; i++)
            if (temp[i].GetSign() == sign)
                c++;
        if (c != 0)
        {
            int counter = 0;
            Card[] New = new Card[c];
            for (int i = 0; i < temp.Length; i++)
                if (temp[i].GetSign() == sign)
                {
                    New[counter] = new Card(temp[i].GetValue(), temp[i].GetSign());
                    counter++;
                }
            return New;
        }
        return null;
    }       

    public static Int64 GetDeckNumber(Card[] deck)
    {
        Int64 max=-1;
        deck = SortDeck(deck);
        Int64[] arr=new Int64[10] {CheckStraightFlush(deck),CheckFourOfAKind(deck),CheckFullHouse(deck)
            ,CheckFlush(deck),CheckStraight(deck),CheckLowStraight(deck),CheckThreeOfAKind(deck),CheckTwoPair(deck)
        ,CheckPair(deck),HighCard(deck)};
        for (int i = 0; i < arr.Length; i++)
        {
           max= Math.Max(arr[i], max);
        }
        return max;
    }//מחזיר מספר המייצג את רצף הקלפים
    public static string  GetDeckVariation(Int64 deckNum)//מחזיר את סוג רצף הקלפים
    {
        string[] variations = new string[9]{"high card","pair","two pair","three of a kind",
            "straight","flush","full house","four of a kind","straight flush"};
        return variations[deckNum / 10000000000 - 1];
    }

    /*public static Card[] BuildPack()//פעולה היוצרת חפיסת קלפים סטנדרטית
    {
        Card[] pack = new Card[52];
        char[] signs = new char[4];
        int c = 0;
        signs[0] = 'h'; signs[1] = 'c'; signs[2] = 'd'; signs[3] = 'l';
        for (int i = 0; i < signs.Length; i++)
        {
            for (int k = 1; k < 14; k++)
            {
                pack[c] = new Card(k, signs[i]);
                c++;
            }
        }
        return pack;
    }*/
    public static Card[] BuildPack()
    {
        char[] signs = new char[4] { 'h', 'c', 'd', 'l' };
        int num;
        char sign;
        Random rnd = new Random();
        Card[] pack = new Card[52];
        for (int i = 0; i < pack.Length; i++)
        {
            num = rnd.Next(2, 15);
            sign = signs[rnd.Next(0, 4)];
            if (!CheckCardExists(num, sign, pack))
                pack[i] = new Card(num, sign);
            else
                i--;
        }
        return pack;
    }
    public static bool CheckCardExists(int num, char sign, Card[] pack)
    {
        for (int i = 0; i < pack.Length; i++)
        {
            if (pack[i] != null)
            {
                if (pack[i].GetSign() == sign && pack[i].GetValue() == num)
                    return true;
            }
        }
        return false;
    }
    public static int RandomizeNum(List<int> randomized)// מגריל מספר שלא הוגרל בין אחד לחמישים ושתיים
    {
        
        Random rnd = new Random();
        int num = rnd.Next(1, 53);
        Thread.Sleep(15);
        num = rnd.Next(1, 53);
        Thread.Sleep(15);
        num = rnd.Next(1, 53);
        foreach (int number in randomized)
        {
            if (number == num)
                return RandomizeNum(randomized);
        }
        return num;
    }

}
