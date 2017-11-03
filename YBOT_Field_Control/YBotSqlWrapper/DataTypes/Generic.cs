using System;
using System.Linq;
using System.Collections.Generic;

namespace YBotSqlWrapper
{
    public class Generic<T> : IEnumerable<T>
    {
        protected List<T> objectList;

        public int Count {
            get {
                return objectList.Count;
            }
        }

        public Generic () {
            objectList = new List<T> ();
        }

        public Generic (IEnumerable<T> items) : this () {
            foreach (var i in items) {
                objectList.Add (i);
            }
        }

        public void Add (T item) {
            objectList.Add (item);
        }

        public int IndexOf (T item) {
            return objectList.IndexOf (item);
        }

        public void ForEach (Action<T> action) {
            objectList.ForEach (action);
        }

        public IEnumerator<T> GetEnumerator () {
            return objectList.GetEnumerator ();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
            return GetEnumerator ();
        }
    }
}
