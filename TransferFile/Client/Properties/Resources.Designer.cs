﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransferFile.Client.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TransferFile.Client.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connect Success..
        /// </summary>
        internal static string ConnectionMsg {
            get {
                return ResourceManager.GetString("ConnectionMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client disconnect from the server..
        /// </summary>
        internal static string DisconnectMsg {
            get {
                return ResourceManager.GetString("DisconnectMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please choose a path to save the file..
        /// </summary>
        internal static string EmptyPath {
            get {
                return ResourceManager.GetString("EmptyPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File received..
        /// </summary>
        internal static string FileReceivedDoneMsg {
            get {
                return ResourceManager.GetString("FileReceivedDoneMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The ip address is Invalid..
        /// </summary>
        internal static string InvalidAddressMsg {
            get {
                return ResourceManager.GetString("InvalidAddressMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t connect to the server..
        /// </summary>
        internal static string InvalidConnectionMsg {
            get {
                return ResourceManager.GetString("InvalidConnectionMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The port is Invalid..
        /// </summary>
        internal static string InvalidPortMsg {
            get {
                return ResourceManager.GetString("InvalidPortMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The address and port can&apos;t be empty..
        /// </summary>
        internal static string IsEmptyMsg {
            get {
                return ResourceManager.GetString("IsEmptyMsg", resourceCulture);
            }
        }
    }
}
