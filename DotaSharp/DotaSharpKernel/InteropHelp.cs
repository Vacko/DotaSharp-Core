using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel
{
    internal class InteropHelp
    {
        #region Static Methods

        public static TClass CastInterface<TClass>(IntPtr address) where TClass : INativeWrapper, new()
        {
            if (address == IntPtr.Zero)
                return default(TClass);

            TClass result = new TClass();
            result.SetupFunctions(address);

            return result;
        }

        #endregion

        #region Nested Types, Enums, Delegates

        public interface INativeWrapper
        {
            #region Public Methods

            void SetupFunctions(IntPtr objectAddress);

            #endregion
        }

        public abstract class NativeWrapper<TNativeFunctions> : INativeWrapper
        {
            #region Fields

            private readonly Dictionary<IntPtr, Delegate> m_functionCache = new Dictionary<IntPtr, Delegate>();

            protected TNativeFunctions Functions;
            protected IntPtr ObjectAddress;

            #endregion

            #region Properties

            public IntPtr Interface => ObjectAddress;

            #endregion

            #region Implementation of INativeWrapper

            public void SetupFunctions(IntPtr objectAddress)
            {
                ObjectAddress = objectAddress;

                IntPtr vtableptr = Marshal.ReadIntPtr(ObjectAddress);

                Functions = (TNativeFunctions) Marshal.PtrToStructure(
                    vtableptr, typeof(TNativeFunctions));
            }

            #endregion

            #region Override of object

            public override string ToString() =>
                $"[-- Interface<{typeof(TNativeFunctions)}> #{ObjectAddress.ToInt32():X8} --]";

            #endregion

            #region Protected Methods

            protected void Call<TDelegate>(IntPtr pointer, params object[] args)
            {
                GetDelegate<TDelegate>(pointer).DynamicInvoke(args);
            }

            protected TReturn Call<TReturn, TDelegate>(IntPtr pointer, params object[] args) =>
                (TReturn) GetDelegate<TDelegate>(pointer).DynamicInvoke(args);

            protected Delegate GetDelegate<TDelegate>(IntPtr pointer)
            {
                Delegate function;

                if (m_functionCache.ContainsKey(pointer) == false)
                {
                    function = Marshal.GetDelegateForFunctionPointer(pointer, typeof(TDelegate));
                    m_functionCache[pointer] = function;
                }
                else
                {
                    function = m_functionCache[pointer];
                }

                return function;
            }

            protected TDelegate GetFunction<TDelegate>(IntPtr pointer) where TDelegate : class =>
                (TDelegate) (object) GetDelegate<TDelegate>(pointer);

            #endregion
        }

        #endregion
    }
}