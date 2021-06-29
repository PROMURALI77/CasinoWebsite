using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_HomePage : System.Web.UI.Page
{
    int days = 15;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected int[] OurEarnings()
    {
        int[] arr = new int[days];
        arr[0] = 0;
        int num= 10000;
        for (int i = 1; i < arr.Length; i++)
        {
            arr[i] = arr[i - 1] +num*i;
        }
        return arr;
    }
    protected int[] UserEarnings()
    {
        int[] arr = new int[days];
        arr[days-1] = 0;       
        for (int i = arr.Length-2; i >=0; i--)
        {
            arr[i] = arr[i + 1] + 1000;
        }
        return arr;
    }
    protected string[] Dates()
    {
        string[] dates = new string[days];
        DateTime temp = DateTime.Now;
        for (int i = 0; i < dates.Length; i++)
        {
            dates[i] = temp.Date.Date.ToString("d/M/yy");
            temp=temp.AddDays(1);
        }
        return dates;
    }
}