using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDescription
{
    public interface IRepository<T>
    {
        void AddNewElement(T newElement);

        void DeleteAt(int elementIndex);
    }
}
