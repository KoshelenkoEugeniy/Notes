using System.Collections.Generic;
using System.IO;

namespace DataBase
{
    interface IDataBase<T>
    {
        FileStream SetConnection(string fileName);

        void WriteToDataBase(FileStream file, List<T> collection);

        List<T> ReadFromDataBase(FileStream file);
    }
}
