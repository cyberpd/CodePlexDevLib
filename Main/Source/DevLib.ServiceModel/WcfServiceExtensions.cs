﻿//-----------------------------------------------------------------------
// <copyright file="WcfServiceExtensions.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.ServiceModel
{
    using System;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;

    /// <summary>
    /// WcfService extension methods.
    /// </summary>
    public static class WcfServiceExtensions
    {
        /// <summary>
        /// Field WildcardAction.
        /// </summary>
        private const string WildcardAction = "*";

        /// <summary>
        /// Field MessageDescriptionActionPropertyName.
        /// </summary>
        private const string MessageDescriptionActionPropertyName = "Action";

        /// <summary>
        /// Adds the endpoint behavior.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="endpointBehavior">The endpoint behavior.</param>
        /// <returns>The endpoint behavior to add.</returns>
        public static IEndpointBehavior AddEndpointBehavior(this ServiceHostBase source, IEndpointBehavior endpointBehavior)
        {
            foreach (var endpoint in source.Description.Endpoints)
            {
                endpoint.Behaviors.Add(endpointBehavior);
            }

            return endpointBehavior;
        }

        /// <summary>
        /// Configure the client binding.
        /// </summary>
        /// <param name="source">The source TChannel.</param>
        /// <param name="setBindingAction">A delegate to configure Binding.</param>
        public static void SetClientBinding(this object source, Action<Binding> setBindingAction)
        {
            IWcfClientBase wcfClient = source as IWcfClientBase;

            if (wcfClient != null)
            {
                wcfClient.SetBindingAction = setBindingAction;
            }
        }

        /// <summary>
        /// Configure the client credentials.
        /// </summary>
        /// <param name="source">The source TChannel.</param>
        /// <param name="setClientCredentialsAction">A delegate to configure ClientCredentials.</param>
        public static void SetClientCredentials(this object source, Action<ClientCredentials> setClientCredentialsAction)
        {
            IWcfClientBase wcfClient = source as IWcfClientBase;

            if (wcfClient != null)
            {
                wcfClient.SetClientCredentialsAction = setClientCredentialsAction;
            }
        }

        /// <summary>
        /// Configure DataContractResolver.
        /// </summary>
        /// <param name="source">The source TChannel.</param>
        /// <param name="setDataContractResolverAction">A delegate to configure DataContractSerializerOperationBehavior.</param>
        public static void SetClientDataContractResolver(this object source, Action<DataContractSerializerOperationBehavior> setDataContractResolverAction)
        {
            IWcfClientBase wcfClient = source as IWcfClientBase;

            if (wcfClient != null)
            {
                wcfClient.SetDataContractResolverAction = setDataContractResolverAction;
            }
        }

        /// <summary>
        /// Gets the WCF client base.
        /// </summary>
        /// <typeparam name="TChannel">The channel to be used to connect to the service.</typeparam>
        /// <param name="source">The source TChannel.</param>
        /// <returns>WcfClientBase{TChannel} instance.</returns>
        public static WcfClientBase<TChannel> AsWcfClientBase<TChannel>(this TChannel source) where TChannel : class
        {
            return source as WcfClientBase<TChannel>;
        }

        /// <summary>
        /// Gets the WCF client base interface.
        /// </summary>
        /// <param name="source">The source TChannel.</param>
        /// <returns>IWcfClientBase instance.</returns>
        public static IWcfClientBase AsIWcfClientBase(this object source)
        {
            return source as IWcfClientBase;
        }

        /// <summary>
        /// Gets the client base.
        /// </summary>
        /// <typeparam name="TChannel">The channel to be used to connect to the service.</typeparam>
        /// <param name="source">The source TChannel.</param>
        /// <returns>WcfClientBase{TChannel} instance.</returns>
        public static ClientBase<TChannel> AsClientBase<TChannel>(this TChannel source) where TChannel : class
        {
            return source as ClientBase<TChannel>;
        }

        /// <summary>
        /// Adds the endpoint behavior.
        /// </summary>
        /// <typeparam name="TChannel">The type of the channel.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="endpointBehavior">The endpoint behavior.</param>
        /// <returns>The endpoint behavior to add.</returns>
        public static IEndpointBehavior AddEndpointBehavior<TChannel>(this ClientBase<TChannel> source, IEndpointBehavior endpointBehavior) where TChannel : class
        {
            source.Endpoint.Behaviors.Add(endpointBehavior);

            return endpointBehavior;
        }

        /// <summary>
        /// Adds the endpoint behavior.
        /// </summary>
        /// <typeparam name="TChannel">The type of the channel.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="endpointBehavior">The endpoint behavior.</param>
        /// <returns>The endpoint behavior to add.</returns>
        public static IEndpointBehavior AddEndpointBehavior<TChannel>(this WcfClientBase<TChannel> source, IEndpointBehavior endpointBehavior) where TChannel : class
        {
            source.Endpoint.Behaviors.Add(endpointBehavior);

            return endpointBehavior;
        }

        /// <summary>
        /// Adds the endpoint behavior.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="endpointBehavior">The endpoint behavior.</param>
        /// <returns>The endpoint behavior to add.</returns>
        public static IEndpointBehavior AddEndpointBehavior(this IWcfClientBase source, IEndpointBehavior endpointBehavior)
        {
            source.Endpoint.Behaviors.Add(endpointBehavior);

            return endpointBehavior;
        }

        /// <summary>
        /// Adds the endpoint behavior.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="endpointBehavior">The endpoint behavior.</param>
        /// <returns>The endpoint behavior to add.</returns>
        public static IEndpointBehavior AddEndpointBehavior(this DynamicClientProxyBase source, IEndpointBehavior endpointBehavior)
        {
            source.Endpoint.Behaviors.Add(endpointBehavior);

            return endpointBehavior;
        }

        /// <summary>
        /// Removes OperationContractAttribute Action="*" or ReplyAction="*".
        /// </summary>
        /// <param name="source">The source.</param>
        public static void RemoveActionWildcard(this ServiceEndpoint source)
        {
            source.Contract.RemoveActionWildcard();
        }

        /// <summary>
        /// Removes OperationContractAttribute Action="*" or ReplyAction="*".
        /// </summary>
        /// <param name="source">The source.</param>
        public static void RemoveActionWildcard(this ContractDescription source)
        {
            foreach (OperationDescription operation in source.Operations)
            {
                operation.RemoveActionWildcard();
            }
        }

        /// <summary>
        /// Removes OperationContractAttribute Action="*" or ReplyAction="*".
        /// </summary>
        /// <param name="source">The source.</param>
        public static void RemoveActionWildcard(this OperationDescription source)
        {
            foreach (MessageDescription message in source.Messages)
            {
                message.RemoveActionWildcard();
            }
        }

        /// <summary>
        /// Removes OperationContractAttribute Action="*" or ReplyAction="*".
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>true if the source OperationContractAttribute has wildcard action; otherwise, false.</returns>
        public static bool RemoveActionWildcard(this MessageDescription source)
        {
            if (source != null && source.Action != null && source.Action == WildcardAction)
            {
                PropertyInfo propertyInfo = source.GetType().GetProperty(MessageDescriptionActionPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                propertyInfo.SetValue(source, string.Empty, null);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets OperationContractAttribute Action="*" or ReplyAction="*".
        /// </summary>
        /// <param name="source">The source.</param>
        public static void SetActionWildcard(this MessageDescription source)
        {
            if (source != null)
            {
                PropertyInfo propertyInfo = source.GetType().GetProperty(MessageDescriptionActionPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                propertyInfo.SetValue(source, WildcardAction, null);
            }
        }
    }
}
