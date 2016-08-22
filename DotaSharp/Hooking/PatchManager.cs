using System;
using System.Linq;

namespace DotaSharp.Hooking
{
    internal class PatchManager : Manager<Patch>
    {
        #region Override of Manager<Patch>

        public override void ApplyAll()
        {
            foreach (Patch patch in Applications.Values)
                if (patch.Enabled && !patch.IsApplied)
                    patch.Apply();
        }

        public override void RemoveAll()
        {
            foreach (Patch patch in Applications.Values)
                if (patch.IsApplied)
                    patch.Remove();
        }

        #endregion

        #region Public Methods

        public Patch Create(IntPtr address, byte[] patchWith, string name)
        {
            if (!Applications.ContainsKey(name))
            {
                Patch p = new Patch(address, patchWith, name);
                Applications.Add(name, p);
                return p;
            }

            return Applications[name];
        }

        public Patch CreateAndApply(IntPtr address, byte[] patchWith, string name)
        {
            Patch p = Create(address, patchWith, name);
            if (p != null)
                p.Apply();

            return p;
        }

        #endregion
    }

    internal class Patch : IMemoryOperation
    {
        #region Fields

        private readonly IntPtr m_address;
        private readonly byte[] m_originalBytes;
        private readonly byte[] m_patchBytes;

        #endregion

        #region Constructors

        internal Patch(IntPtr address, byte[] patchWith, string name)
        {
            Name = name;
            m_address = address;
            m_patchBytes = patchWith;
            m_originalBytes = HookMemory.Read(address, patchWith.Length);
        }

        #endregion

        #region Properties

        public bool Enabled { get; set; }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (IsApplied)
                Remove();

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Implementation of IMemoryOperation

        public bool Apply()
        {
            try
            {
                HookMemory.Write(m_address, m_patchBytes);
                return true;
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public bool Remove()
        {
            try
            {
                HookMemory.Write(m_address, m_originalBytes);
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public bool IsApplied => HookMemory.Read(m_address, m_patchBytes.Length).SequenceEqual(m_patchBytes);

        public string Name { get; }

        #endregion

        #region Override of object

        ~Patch()
        {
            Dispose();
        }

        #endregion
    }
}