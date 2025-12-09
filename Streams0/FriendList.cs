using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Streams0
{
    public class FriendList
    {
        public  List<Friend> myFriends = new List<Friend>();
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

        /// <summary>
        /// Write the text representation of the instance of FriendList to a text file
        /// </summary>
        /// <param name="txtFileName">The text file to write to</param>
        /// <returns></returns>
        public string WriteToDisk(string txtFileName)
        {
            using(Stream s = new FileStream(fname(txtFileName), FileMode.Create))
            {
                var writer = new StreamWriter(s);
                writer.Write(this.ToString());
                writer.Flush();
            }
            return fname(txtFileName);
        }

        /// <summary>
        /// Write a ziped compressed of file the text representation of the instance of FriendList
        /// </summary>
        /// <param name="zipFileName">Zip file to write the compressed content to</param>
        /// <returns></returns>
        public string WriteToDiskCompressed(string zipFileName)
        {
            string textContent = this.ToString();
            string zipPath = fname(zipFileName);
            
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry entry = archive.CreateEntry("friends.txt");
                    using (StreamWriter writer = new StreamWriter(entry.Open()))
                    {
                        writer.Write(textContent);
                        writer.Flush();
                    }
                }
            }

            return zipPath;
        }

        /// <summary>
        /// Uncompresses the zipFile and writes the uncompressed content to a textfile
        /// </summary>
        /// <param name="zipFileName">Zip file to read and uncompress</param>
        /// <param name="txtFileName">Text file to write the uncompressed content</param>
        /// <returns></returns>
        public string UncompressToDisk(string zipFileName, string txtFileName)
        {
            string zipPath = fname(zipFileName);
            string txtPath = fname(txtFileName);
            
            //read from zip stream
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry = archive.Entries[0];
                    using (StreamReader reader = new StreamReader(entry.Open()))
                    {
                        string content = reader.ReadToEnd();
                        
                        //write to text stream
                        using (FileStream txtStream = new FileStream(txtPath, FileMode.Create))
                        {
                            using (StreamWriter writer = new StreamWriter(txtStream))
                            {
                                writer.Write(content);
                                writer.Flush();
                            }
                        }
                    }
                }
            }

            return txtPath;
        }

        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = Path.Combine(documentPath, "ADOP", "Streams");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
    }
}
