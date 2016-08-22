using System;
using System.Collections.Generic;

namespace DotaSharp.Hooking
{
    internal interface IMemoryOperation : IDisposable
    {
        #region Properties

        bool IsApplied { get; }

        string Name { get; }

        #endregion

        #region Public Methods

        bool Apply();

        bool Remove();

        #endregion
    }

    internal abstract class Manager<T> where T : IMemoryOperation
    {
        #region Fields

        protected readonly Dictionary<string, T> Applications = new Dictionary<string, T>();

        #endregion

        #region Properties

        public virtual T this[string name] => Applications[name];

        #endregion

        #region Public Methods

        public virtual void ApplyAll()
        {
            foreach (KeyValuePair<string, T> dictionary in Applications)
                dictionary.Value.Apply();
        }

        public virtual void Delete(string name)
        {
            if (!Applications.ContainsKey(name)) return;

            Applications[name].Dispose();
            Applications.Remove(name);
        }

        public virtual void DeleteAll()
        {
            foreach (KeyValuePair<string, T> dictionary in Applications)
                dictionary.Value.Dispose();

            Applications.Clear();
        }

        public virtual void RemoveAll()
        {
            foreach (KeyValuePair<string, T> dictionary in Applications)
                dictionary.Value.Remove();
        }

        #endregion
    }
}