using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyLibraryApp
{
    public class XStorage
    {
        internal static string SerializeXML<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        internal static Task<bool> WriteStorageFile<T>(string data, string localFileName)
        {
            try
            {
                File.WriteAllText(GetLocalPath(localFileName), data);
                return Task.FromResult<bool>(true);
            }
            catch (Exception)
            {
                return Task.FromResult<bool>(false);
            }
        }

        private static string GetLocalPath(string localFileName)
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var localPath = Path.Combine(documentPath, localFileName);
            return localPath;
        }

        internal static Task<T> DeserializeXML<T>(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                using (StringReader stringReader = new StringReader(data))
                {
                    T appData = (T)serializer.Deserialize(stringReader);

                    return Task.FromResult<T>(appData);
                }
            }
            catch
            {
                return default(Task<T>);
            }
        }

        internal static Task<string> ReadStorageFile(string fileName)
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var localPath = Path.Combine(documentPath, fileName);
            try
            {
                var data = File.ReadAllText(GetLocalPath(fileName));
                return Task.FromResult<string>(data);
            }
            catch (Exception)
            {
                return Task.FromResult<string>("Error");
            }
        }

        internal static Task<T> GetEmbeddedXML<T>(string file)
        {
            Assembly assembly = typeof(MyLibraryApp.App).GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var filePath = (from f in resources where f.Contains(file) select f).FirstOrDefault();
            Stream stream = assembly.GetManifestResourceStream(filePath);

            try
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return Task.FromResult<T>((T)serializer.Deserialize(reader));
                }
            }
            catch (Exception)
            {
                return default(Task<T>);
            }
        }
    }
}
