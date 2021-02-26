using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Common 的摘要说明
/// </summary>
public class Common
{
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
        string _zimu = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";//要随机的字母
        Random _rand = new Random(Guid.NewGuid().GetHashCode()); //随机类
        string _result = "";
        for (int i = 0; i < digits; i++) //循环6次，生成6位数字，10位就循环10次
        {
            _result += _zimu[_rand.Next(26)]; //通过索引下标随机

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
            //Response.Redirect("index.aspx");
        }
        else

        {
            if (Session["isLogin"].ToString() !="true" )
            {
                //Response.Redirect("index.aspx");
            }
        }
    }
}