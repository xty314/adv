using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Common 的摘要说明
/// </summary>
public class Common
{
    public static List<string> supportedFileTypes = new List<string>(new string[] { "jpg", "jpeg", "gif", "png", "bmp" });
    public static string[] GetCategories(string company)
    {
        string fullpath = HttpContext.Current.Server.MapPath("../"+company+"/config.txt");
        string  content = System.IO.File.ReadAllText(fullpath);
        string[] stringSeparators = new string[] { "\r\n" };
        string[] cat = content.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        //for(int i = 0; i < cat.Length; i++)
        //{
        //    cat[i] = cat[i].Replace("\\r", "");
        //}
        return cat;

    }
    public static string[] GetAllCategories()
    {
        string catrgoryPath = HttpContext.Current.Server.MapPath("./category");
  
        string[] categoryDirs = Directory.GetDirectories(catrgoryPath);
        return categoryDirs;
    }
    public static void DeleteDir(string srcPath)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            //FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
            dir.Delete(true);
  
            
        }
        catch (Exception e)
        {
            throw;
        }
    }
    public static string GetLastDir(string dirs)
    {
        string dirName = dirs.Substring(dirs.LastIndexOf("\\")+1);
        return dirName;
    }
    public static string GetImgSecond(string fileName)
    {

        string filename = fileName.Substring(0, fileName.LastIndexOf("."));

        string time = "10";
        // string result="";
        if (filename.LastIndexOf("_") != -1)
        {
            string suffix = filename.Substring(filename.LastIndexOf("_") + 1);

            if (Regex.IsMatch(suffix, @"^[+-]?\d*$"))
            {
                if (Convert.ToInt32(suffix) < 60)
                {
                    time = suffix;
                }
               
            }
        }
        return time;
    }
    public static string RandomName(int digits)
    {
        string _zimu = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopgrstuvwxyz1234567890";//要随机的字母
        Random _rand = new Random(Guid.NewGuid().GetHashCode()); //随机类
        string _result = "";
        for (int i = 0; i < digits; i++) //循环6次，生成6位数字，10位就循环10次
        {
            _result += _zimu[_rand.Next(62)]; //通过索引下标随机

        }
        return _result;
    }
}

public class AdminBasePage : System.Web.UI.Page
{

    protected virtual void Page_Init(object sender, EventArgs e)
    {
       
       
        if (Session["isLogin"]==null)
        {    
            Response.Redirect("index.aspx");
        }
        else

        {
            if (Session["isLogin"].ToString() !="true" )
            {
                Response.Redirect("index.aspx");
            }
        }
    }
}

public class PicName
{
    public string Order { get; set; }
    public string StartDate { get; set; }
    public string Id { get; set; }
    public string EndDate { get; set; }
    public string  Time { get; set; }
    public string Type { get; set; }
    public string  Name { get; set; }
    public  PicName (string name)
    {
        
  
        if (IsValid(name))
        {
            string[] stringSeparators = new string[] { "_" };
            string[] parts = name.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            Order = parts[0];
            StartDate = parts[1];
            Id = parts[2];
            EndDate = parts[3];
            Time = parts[4].Substring(0, parts[4].LastIndexOf("."));
            Type = parts[4].Substring(parts[4].LastIndexOf(".") + 1);
            Name = name;
           
        }
        else
        {
            Order = "0000";
            StartDate = "01-01-2020";
            Id = Common.RandomName(6);
            EndDate = "31-12-2099";
            Time = "10";
            Type = name.Substring(name.LastIndexOf(".") + 1);
            Name = name;
           
        }
       
 
    }
    public static bool IsValid(string name)
    {

        string[] names=name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
        if (names.Length != 2)
        {
            return false;
        }
        string type = names[1];
        if (!Common.supportedFileTypes.Contains(type.ToLower()))
        {
            return false;
        }
        string[] stringSeparators = new string[] { "_" };
        string[] parts = names[0].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 5)//5 个部分
        {
            return false;
        }
        if (!IsDate(parts[1]) || !IsDate(parts[3]))//第二个和第四个部分分别是开始时间和结束时间
        {
            return false;
        }
        if (parts[2].Length != 6)//第三个部分6位
        {
            return false;
        }
        if (!Regex.IsMatch(parts[4], @"^[+-]?\d*$"))//第五个部分数字
        {
            return false;
        }
        if (!Regex.IsMatch(parts[0], @"^[+-]?\d{4}$"))//第一个部分是个四位的数字
        {
            return false;
        }


        return true;
    }
    public static bool IsDate(string date)
    {
        try
        {
            DateTime dt = DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            return false;
        }
       
   
    }
    public bool IsStart()
    {
        
        DateTime dt = DateTime.ParseExact(this.StartDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (DateTime.Compare(dt, DateTime.Now)>0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public string ShowName()
    {
        if (IsValid(Name))
        {
            return Order + "_" + Id + "." + Type;
        }
        else
        {
            return Name;
        }
    }
    public bool IsEnd()
    {

        DateTime dt = DateTime.ParseExact(EndDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (DateTime.Compare(dt, DateTime.Now) > 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public bool IsShow()
    {
        if (IsStart() && !IsEnd())

        {
            return true;
        }
        else
        {
            return false;
        }
    }
}