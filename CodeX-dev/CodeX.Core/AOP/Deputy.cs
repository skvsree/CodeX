using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Linq;
using CodeX.AOP.Attributes;
using CodeX.Data;

// ReSharper disable CheckNamespace
namespace CodeX.AOP
// ReSharper restore CheckNamespace
{

    /// <summary>
    /// Deputy Class
    /// </summary>
    public class Deputy<T> : RealProxy where T : IAOP
    {
        private readonly T _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deputy{T}"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public Deputy(T instance)
            : base(typeof(T))
        {
            _instance = instance;
        }

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static T Create(T instance)
        {
            return (T)new Deputy<T>(instance).GetTransparentProxy();
        }


        /// <summary>
        /// When overridden in a derived class, invokes the method that is specified in the provided <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> on the remote object that is represented by the current instance.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> that contains a <see cref="T:System.Collections.IDictionary" /> of information about the method call.</param>
        /// <returns>
        /// The message returned by the invoked method, containing the return value and any out or ref parameters.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        ///   </PermissionSet>
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var method = (MethodInfo)methodCall.MethodBase;
            MarshalByRefObject owner = GetUnwrappedServer();
            try
            {
                var mc = new MethodCallMessageWrapper((IMethodCallMessage)msg);
                var mi = (MethodInfo)mc.MethodBase;
                DoPreProcessors(mi, methodCall.Args);
                var result = method.Invoke(_instance, methodCall.InArgs);
                DoPostProcessors(mi, methodCall.Args);
                return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                if (e is TargetInvocationException && e.InnerException != null)
                {
                    return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
                }

                return new ReturnMessage(e, msg as IMethodCallMessage);
            }
        }



        /// <summary>
        /// Does the pre-processors.
        /// </summary>
        /// <param name="mi">The minimum.</param>
        private void DoPreProcessors(MethodInfo mi, object[] args)
        {
            bool IsGet = false;
            var pi = GetMethodProperty(mi, _instance, out IsGet); //if property 'pi' will be null
            if (pi != null)
            {
                DoPreProcessorForProperty(pi, args, IsGet);
            }
            else
            {
                DoPreProcessorForMethod(mi, args);
            }
        }

        /// <summary>
        /// Does the post processors.
        /// </summary>
        /// <param name="mi">The minimum.</param>
        private void DoPostProcessors(MethodInfo mi, object[] args)
        {
            bool IsGet = false;
            var pi = GetMethodProperty(mi, _instance, out IsGet); //if property 'pi' will be null
            if (pi != null)
            {
                DoPostProcessorForProperty(pi, args, IsGet);
            }
            else
            {
                DoPreProcessorForMethod(mi, args);
            }
        }

        /// <summary>
        /// Does the pre-processor for method.
        /// </summary>
        /// <param name="mi">The minimum.</param>
        private void DoPreProcessorForMethod(MethodInfo mi, object[] args)
        {
        }

        /// <summary>
        /// Does the post processor for method.
        /// </summary>
        /// <param name="mi">The minimum.</param>
        private void DoPostProcessorForMethod(MethodInfo mi, object[] args)
        {
        }

        /// <summary>
        /// Does the pre-processor for property.
        /// </summary>
        /// <param name="pi">The pi.</param>
        /// <param name="isget">if set to <c>true</c> [isget].</param>
        private void DoPreProcessorForProperty(PropertyInfo pi, object[] args, bool isget)
        {
        }

        /// <summary>
        /// Does the post processor for property.
        /// </summary>
        /// <param name="pi">The pi.</param>
        /// <param name="isget">if set to <c>true</c> [isget].</param>
        private void DoPostProcessorForProperty(PropertyInfo pi, object[] args, bool isget)
        {
            var shouldBeNotified = Attribute.GetCustomAttribute(pi, typeof(NotifyOnPropertyChangedAttribute)) != null;
            var shouldBeValidated = Attribute.GetCustomAttribute(pi, typeof(ValidateOnSetAttribute)) != null;
            if (shouldBeNotified)
            {
                var info = _instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                            .AsEnumerable()
                            .Where(f => f.FieldType == typeof(PropertyChangedEventHandler))
                            .FirstOrDefault();

                if (info != null)
                {
                    var evHandler = info.GetValue(_instance) as PropertyChangedEventHandler;
                    if (evHandler != null)
                        evHandler.Invoke(typeof(T), new PropertyChangedEventArgs(pi.Name));
                }
            }

            if (shouldBeValidated)
            {
                var results = _instance.Validate(args[0], pi.Name);

                if (results.Any())
                {
                    throw new Exception(string.Join(",", results.Select(x => x.ErrorMessage).ToArray()));
                }
            }
        }

        /// <summary>
        /// Gets the method property.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="IsGet">if set to <c>true</c> [is get].</param>
        /// <returns></returns>
        private static PropertyInfo GetMethodProperty(MethodInfo methodInfo, object owner, out bool IsGet)
        {
            foreach (PropertyInfo aProp in owner.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                MethodInfo mi = null;
                mi = aProp.GetGetMethod(true);
                if (mi != null && mi.Equals(methodInfo))
                {
                    IsGet = true;
                    return aProp;
                }
                mi = aProp.GetSetMethod(true);
                if (mi != null && mi.ToString().Equals(methodInfo.ToString()))
                {
                    IsGet = false;
                    return aProp;
                }
            }
            IsGet = false;
            return null;
        }
    }

}
