﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BacklogClear.Domain.Reports {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceReportGenerationMessages {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceReportGenerationMessages() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("BacklogClear.Domain.Reports.ResourceReportGenerationMessages", typeof(ResourceReportGenerationMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string GAME {
            get {
                return ResourceManager.GetString("GAME", resourceCulture);
            }
        }
        
        public static string PLATFORM {
            get {
                return ResourceManager.GetString("PLATFORM", resourceCulture);
            }
        }
        
        public static string RELEASE_DATE {
            get {
                return ResourceManager.GetString("RELEASE_DATE", resourceCulture);
            }
        }
        
        public static string STATUS {
            get {
                return ResourceManager.GetString("STATUS", resourceCulture);
            }
        }
        
        public static string STATUS_BACKLOG {
            get {
                return ResourceManager.GetString("STATUS_BACKLOG", resourceCulture);
            }
        }
        
        public static string STATUS_PLAYING {
            get {
                return ResourceManager.GetString("STATUS_PLAYING", resourceCulture);
            }
        }
        
        public static string STATUS_COMPLETED {
            get {
                return ResourceManager.GetString("STATUS_COMPLETED", resourceCulture);
            }
        }
        
        public static string STATUS_DROPPED {
            get {
                return ResourceManager.GetString("STATUS_DROPPED", resourceCulture);
            }
        }
        
        public static string GAME_REPORT {
            get {
                return ResourceManager.GetString("GAME_REPORT", resourceCulture);
            }
        }
        
        public static string STARTED_PLAYING_DATE {
            get {
                return ResourceManager.GetString("STARTED_PLAYING_DATE", resourceCulture);
            }
        }
        
        public static string FINISHED_PLAYING_DATE {
            get {
                return ResourceManager.GetString("FINISHED_PLAYING_DATE", resourceCulture);
            }
        }
        
        public static string NOT_FINISHED {
            get {
                return ResourceManager.GetString("NOT_FINISHED", resourceCulture);
            }
        }
        
        public static string TOTAL_PLAYED_IN {
            get {
                return ResourceManager.GetString("TOTAL_PLAYED_IN", resourceCulture);
            }
        }
    }
}
