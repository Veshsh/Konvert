/*using Newtonsoft.Json;*/
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Konvert.TextStructed;
using static System.Net.Mime.MediaTypeNames;

namespace Konvert;
public class Text
{
    public string Name { get; set; }
    public int Long1 { get; set; }
    public int Long2 { get; set; }
    public Text(string name, int long1, int ljng2)
    {
        Name = name;
        Long1 = long1;
        Long2 = ljng2;
    }
    public Text()
    {
    }
}
internal class TextStructed
{
    public string FilePath;
    public TextStructed(string filePath)
    {
        FilePath=filePath;
    }
    public string[] FileOpen()
    {
        var text = new Text("",0,0);
        string[] myText= new string[3];
        if (FilePath.Substring(FilePath.LastIndexOf(".")) == ".txt")
            myText = File.ReadAllText(FilePath).Split('\n');
        else if (FilePath.Substring(FilePath.LastIndexOf(".")) == ".json")
        {
            string jsontext = File.ReadAllText(FilePath);
            text = JsonSerializer.Deserialize<Text>(jsontext);
            myText[0]= text.Name;
            myText[1] = Convert.ToString(text.Long1);
            myText[2] = Convert.ToString(text.Long2);
        }
        else if (FilePath.Substring(FilePath.LastIndexOf(".")) == ".Xml")
        {

            XmlSerializer xml = new XmlSerializer(typeof(Text));
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                text=(Text)xml.Deserialize(fs);
            }
            myText[0] = text.Name;
            myText[1] = Convert.ToString(text.Long1);
            myText[2] = Convert.ToString(text.Long2);
        }
        return  myText;
    }
    public void FileClose(string[] myText)
    {
        for (int i = 0; i < myText.Length; i++)
            if(i%3!=0)
                myText[i]=Convert.ToString(Convert.ToInt32(myText[i]));

        if (FilePath.Substring(FilePath.LastIndexOf(".")) == ".txt")
            File.WriteAllText((FilePath), myText[0] + "\n" + Convert.ToInt32(myText[1]) + "\n" + Convert.ToInt32(myText[2]));
        else if (FilePath.Substring(FilePath.LastIndexOf(".")) == ".json")
        {
            var text = new Text(myText[0], Convert.ToInt32(myText[1].Replace(" ", "")), Convert.ToInt32(myText[2].Replace(" ", "")));
            string jsontext = JsonSerializer.Serialize(text);
            File.WriteAllText(FilePath, jsontext);
        }
        else if (FilePath.Substring(FilePath.LastIndexOf(".")) == ".Xml")
        {
            var text = new Text(myText[0], Convert.ToInt32(myText[1].Replace(" ", "")), Convert.ToInt32(myText[2].Replace(" ", "")));
            XmlSerializer xml = new XmlSerializer(typeof(Text));
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                xml.Serialize(fs,text);
            }
        }
    }
}