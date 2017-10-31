using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using RepositoryDescription;
using NoteDescription;
using DataBase;
using System.Runtime.Serialization;

namespace ModelDescription
{
    [DataContract]
    [KnownType(typeof(Note))]

    public class Model:Repository<Note>
    {
        public int counter { get; set; }

        private string dbName = "../localDB/DataBase.json";

        private FileStream file { get; set; }

        private NoteDataBase<Note> localDB { get; set; }

        private Note tempNote { get; set; }


        public Model()
        {
            counter = 0; // counter of notes

            localDB = new NoteDataBase<Note>();

            file = localDB.SetConnection(dbName);

            collection = new List<Note>();
        }


        public override List<Note> FindUsing(string keyForSearch)
        {
            if (keyForSearch == "")
            {
                return collection;
            }
            else
            {
                List<Note> tempList = new List<Note>();

                foreach (var element in collection)
                {
                    if (element.Title.Contains(keyForSearch))
                    {
                        tempList.Add(element);
                    }
                }

                return tempList;
            }
        }
        
        public override void ChangeElementAt(int index, string status, string text)
        {
            if (status == "Current")
            {
                //collection[index].Status = NoteStatus.Current;
                collection[index].Status = "Current";
            }
            else
            {
                //collection[index].Status = NoteStatus.Finished;
                collection[index].Status = "Finished";
            }

            collection[index].Title = text;

            collection[index].DateOfCreation = DateTime.Now;
        }




        public override string GetCurrentNotesStatusAt(int index)
        {
            //return $"{collection[index].Status}";
            return collection[index].Status;
        }




        public override string GetCurrentNotesTextAt(int index)
        {
            return collection[index].Title;
        }




        public string ToDo(string task, string withInfo, string withInfo2 = "", string withInfo3 = "")
        {
            List<Note> tempList;

            int index = 0;

            string AllNotes = "";

            string tempText = "";


            if (counter == 0)
            {
                counter = GetCollectionSize();
            }
            
            switch (task)
            {
                case "Create" :
                    tempNote = new Note(withInfo,counter);
                    counter += 1;
                    try
                    {
                        AddNewElement(tempNote);
                        using (file)
                        {
                            localDB.WriteToDataBase(file, collection);
                        }
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    return "ok";

                case "Search":
                    
                    try 
                    {
                        using (file)
                        {
                            collection = localDB.ReadFromDataBase(file);
                        }
                        
                        tempList = FindUsing(withInfo);
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }

                    foreach (var element in tempList)
                    {
                        AllNotes += element.ToString();
                    }

                    return  AllNotes;

                case "GetStatus":
                case "GetNote":
                case "Change":
                case "Delete":

                    index = int.Parse(withInfo);

                    if (index > counter)
                    {
                        return "Wrong index. Please retype.";
                    }
                    else
                    {
                        try
                        {
                            if (task == "GetStatus")
                            {
                                tempText = GetCurrentNotesStatusAt(index);
                            }
                            else if (task == "GetNote")
                            {
                                tempText = GetCurrentNotesTextAt(index);
                            }
                            else if (task == "Change")
                            {
                                using (file)
                                {
                                    collection = localDB.ReadFromDataBase(file);
                                }

                                ChangeElementAt(index, withInfo2, withInfo3);

                                using (file)
                                {
                                    localDB.WriteToDataBase(file, collection);
                                }
                            }
                            else
                            {
                                DeleteAt(index);
                                using (file)
                                {
                                    localDB.WriteToDataBase(file, collection);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }

                        if (task == "Change" || task == "Delete")
                        {
                            return "ok";
                        }
                        else
                        {
                            return tempText;
                        }
                    }
                default:
                    return "";
            }
        }


        private int GetCollectionSize()
        {
            List<Note> tempList;
            using (file)
            {
                tempList = localDB.ReadFromDataBase(file);
                return tempList.Count;
            }
        }
    }
}
