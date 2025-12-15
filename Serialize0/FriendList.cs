using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;

namespace Serialization0
{
    public class FriendList
    {
        //Note myFriends { get; set; } - it needs to be a public property for Json serialization
        public List<Friend> myFriends { get; set; } = new List<Friend>();
        public Friend this[int idx]=> myFriends[idx];

        public override string ToString()
        {
            string sRet = "";
            foreach (var item in myFriends)
            {
                sRet += item.ToString() + "\n";
            }
            return sRet;
        }

        public static class Factory
        {
            public static FriendList CreateRandom(int NrOfItems)
            {

                var myList = new FriendList();
                for (int i = 0; i < NrOfItems; i++)
                {
                    var afriend = Friend.Factory.CreateRandom();
                    myList.myFriends.Add(afriend);
                }
                return myList;
            }
        }

        public void SerializeXml(string xmlFileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Friend>));
            using (Stream s = File.Create(fname(xmlFileName)))
            {
                xs.Serialize(s, myFriends);
            }
        }
        public static FriendList DeSerializeXml(string xmlFileName)
        {
            var xs = new XmlSerializer(typeof(List<Friend>));
            using (Stream s = File.OpenRead(fname(xmlFileName)))
            {
                var friends = (List<Friend>)xs.Deserialize(s);
                var friendList = new FriendList();
                friendList.myFriends = friends;
                return friendList;
            }
        }
        public void SerializeJson(string jsonFileName)
        {
            var option = new JsonSerializerOptions
            {
              WriteIndented = true  
            };
            var sjson = JsonSerializer.Serialize(this, option);
            File.WriteAllText(fname(jsonFileName), sjson);
            
        }
        public static FriendList DeSerializeJson(string jsonFileName)
        {
            //Your Code
            return null;
        }

        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = Path.Combine(documentPath, "ADOP", "Serialization");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
    }
}
