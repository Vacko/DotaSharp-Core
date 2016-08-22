using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DotaSharp.Hooking
{
    internal class DetourManager : Manager<Detour>
    {
        #region Internal Methods

        internal Detour Create(Delegate target, Delegate newTarget, string name)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (newTarget == null)
                throw new ArgumentNullException(nameof(newTarget));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (Applications.ContainsKey(name))
                throw new ArgumentException($"The {nameof(name)} detour already exists!");

            Detour d = new Detour(target, newTarget, name);
            Applications.Add(name, d);
            return d;
        }

        internal Detour CreateAndApply(Delegate target, Delegate newTarget, string name)
        {
            Detour ret = Create(target, newTarget, name);
            if (ret != null) ret.Apply();
            return ret;
        }

        #endregion
    }

    internal class Detour : IMemoryOperation
    {
        #region Fields

        private readonly IntPtr m_hook;

        private readonly Delegate m_hookDelegate;
        private readonly List<byte> m_new;
        private readonly List<byte> m_orginal;
        private readonly IntPtr m_target;
        private readonly Delegate m_targetDelegate;

        #endregion

        #region Constructors

        internal Detour(Delegate target, Delegate hook, string name)
        {
            Name = name;
            m_targetDelegate = target;
            m_target = Marshal.GetFunctionPointerForDelegate(target);
            m_hookDelegate = hook;
            m_hook = Marshal.GetFunctionPointerForDelegate(hook);

            //Orginal bytes
            m_orginal = new List<byte>();
            m_orginal.AddRange(HookMemory.Read(m_target, 6));

            //Detour bytes
            m_new = new List<byte> {0x68};
            byte[] tmp = BitConverter.GetBytes(m_hook.ToInt32());
            m_new.AddRange(tmp);
            m_new.Add(0xC3);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (IsApplied) Remove();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Implementation of IMemoryOperation

        public bool Apply()
        {
            try
            {
                HookMemory.Write(m_target, m_new.ToArray());
                IsApplied = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove()
        {
            try
            {
                HookMemory.Write(m_target, m_orginal.ToArray());
                IsApplied = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsApplied { get; private set; }
        public string Name { get; }

        #endregion

        #region Override of object

        ~Detour()
        {
            Dispose();
        }

        #endregion

        #region Internal Methods

        internal object CallOriginal(params object[] args)
        {
            Remove();
            object ret = m_targetDelegate.DynamicInvoke(args);
            Apply();
            return ret;
        }

        #endregion
    }
}