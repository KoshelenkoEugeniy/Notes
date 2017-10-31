using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDescription
{
   
    public abstract class Repository<T> : IRepository<T>
    {
       
        public List<T> collection { get; set; }

        public abstract List<T> FindUsing(string keyForSearch);

        public abstract void ChangeElementAt(int index, string status, string text);

        public abstract string GetCurrentNotesStatusAt(int index);

        public abstract string GetCurrentNotesTextAt(int index);



        public void AddNewElement(T newElement)
        {
            collection.Add(newElement);
        }

        public void DeleteAt(int elementIndex)
        {
            collection.RemoveAt(elementIndex);
        }

        public override string ToString()
        {
            string outputText = "";

            foreach (var element in collection)
            {
                if (outputText == "")
                {
                    outputText = element.ToString() + "\n \n";
                }
                else
                {
                    outputText = outputText + element.ToString() + "\n \n";
                }
            }
            return outputText;
        }
    }
}
