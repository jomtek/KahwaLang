using Kahwa.Parsing.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation.Memory
{
    internal abstract class Table<T>
    {
        protected List<T> _elements;

        public Table()
        {
            _elements = new List<T>();
        }

        public int GetIndex(T constant)
        {
            if (_elements.Contains(constant))
            {
                return _elements.IndexOf(constant);
            }
            else
            {
                _elements.Add(constant);
                return _elements.Count() - 1;
            }
        }

        public T GetValue(int index)
        {
            return _elements[index];
        }
    }
}
