using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using NoteDescription;

namespace DataBase
{
    public class NoteDataBase<Note>:IDataBase<Note>
    {
        private DataContractJsonSerializer Json { get; set; }

        public NoteDataBase()
        {
            Json = new DataContractJsonSerializer(typeof(Note));
        }
    
        public FileStream SetConnection(string fileName)
        {
            return new FileStream(fileName, mode: FileMode.OpenOrCreate);
        }

        public void WriteToDataBase(FileStream file, List<Note> collection)
        {
            Json.WriteObject(file, collection);
        }

        public List<Note> ReadFromDataBase(FileStream file)
        {
            List<Note> tempList = new List<Note>();

            try
            {
                tempList = (List<Note>)Json.ReadObject(file);
            }
            catch
            {
                return tempList;
            }
            
            return tempList;
        }
    }
}
