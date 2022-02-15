using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoredProcedureExecutor.Common
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var obervableCollection = new ObservableCollection<T>();
            foreach (var item in list)
            {
                obervableCollection.Add(item);
            }
            return obervableCollection;
        }

        public static ObservableCollection<T> FillObservableCollection<T>(this ObservableCollection<T> collections, IEnumerable<T>? data)
        {
            if (data == null) return collections;
            foreach (var item in data)
            {
                collections.Add(item);
            }
            return collections;
        }
    }
}
