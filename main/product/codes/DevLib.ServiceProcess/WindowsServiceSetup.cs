﻿//-----------------------------------------------------------------------
// <copyright file="WindowsServiceSetup.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.ServiceProcess
{
    using System;
    using System.Reflection;
    using System.ServiceProcess;
    using System.Xml.Serialization;

    /// <summary>
    /// Class WindowsServiceSetup.
    /// </summary>
    [Serializable]
    public class WindowsServiceSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsServiceSetup" /> class.
        /// </summary>
        public WindowsServiceSetup()
        {
            this.InitSetupInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsServiceSetup" /> class.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        public WindowsServiceSetup(string serviceName)
        {
            this.InitSetupInfo();

            this.ServiceName = serviceName;
            this.DisplayName = this.ServiceName;
            this.Description = this.ServiceName;
        }

        /// <summary>
        /// Gets or sets windows service assembly file path.
        /// </summary>
        public string ServiceAssemblyPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets windows service assembly.
        /// </summary>
        [XmlIgnore]
        public Assembly ServiceAssembly
        {
            get
            {
                if (string.IsNullOrEmpty(this.ServiceAssemblyPath))
                {
                    return Assembly.GetEntryAssembly();
                }
                else
                {
                    try
                    {
                        return Assembly.LoadFrom(this.ServiceAssemblyPath);
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler.Log(e);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the name used by the system to identify this service. This property must be identical to the <see cref="P:System.ServiceProcess.ServiceBase.ServiceName" /> of the service you want to install.
        /// </summary>
        public string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the friendly name that identifies the service to the user.
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description for the service.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the services that must be running for this service to run.
        /// </summary>
        public string[] ServicesDependedOn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets how and when this service is started.
        /// </summary>
        public ServiceStartMode StartType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to report Start, Stop, Pause, and Continue commands in the event log.
        /// </summary>
        public bool AutoLog
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the service can handle notifications of computer power status changes.
        /// </summary>
        public bool CanHandlePowerEvent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the service can handle session change events received from a Terminal Server session.
        /// </summary>
        public bool CanHandleSessionChangeEvent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the service can be paused and resumed.
        /// </summary>
        public bool CanPauseAndContinue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the service should be notified when the system is shutting down.
        /// </summary>
        public bool CanShutdown
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the service can be stopped once it has started.
        /// </summary>
        public bool CanStop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of account under which to run this service application.
        /// </summary>
        public ServiceAccount Account
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user account under which the service application will run.
        /// </summary>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password associated with the user account under which the service application runs.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Method InitSetupInfo.
        /// </summary>
        private void InitSetupInfo()
        {
            try
            {
                Assembly serviceAssembly = Assembly.GetEntryAssembly();

                if (serviceAssembly != null)
                {
                    this.ServiceAssemblyPath = serviceAssembly.Location;
                    this.ServiceName = serviceAssembly.GetName().Name;
                    this.DisplayName = this.ServiceName;
                    this.Description = serviceAssembly.FullName;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.Log(e);

                this.ServiceAssemblyPath = string.Empty;
                this.ServiceName = "DefaultServiceName";
                this.DisplayName = this.ServiceName;
                this.Description = string.Format("{0} {1}", this.ServiceName, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffffUTCzzz"));
            }

            this.StartType = ServiceStartMode.Automatic;
            this.AutoLog = true;
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
            this.Account = ServiceAccount.LocalSystem;
        }
    }
}