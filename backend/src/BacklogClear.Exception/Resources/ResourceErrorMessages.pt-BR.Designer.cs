﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BacklogClear.Exception.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResourceErrorMessages_pt_BR {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceErrorMessages_pt_BR() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("BacklogClear.Exception.Resources.ResourceErrorMessages_pt_BR", typeof(ResourceErrorMessages_pt_BR).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string GENRE_REQUIRED {
            get {
                return ResourceManager.GetString("GENRE_REQUIRED", resourceCulture);
            }
        }
        
        internal static string PLATFORM_REQUIRED {
            get {
                return ResourceManager.GetString("PLATFORM_REQUIRED", resourceCulture);
            }
        }
        
        internal static string RELEASE_DATE_MUST_BE_IN_PAST {
            get {
                return ResourceManager.GetString("RELEASE_DATE_MUST_BE_IN_PAST", resourceCulture);
            }
        }
        
        internal static string STATUS_INVALID {
            get {
                return ResourceManager.GetString("STATUS_INVALID", resourceCulture);
            }
        }
        
        internal static string TITLE_REQUIRED {
            get {
                return ResourceManager.GetString("TITLE_REQUIRED", resourceCulture);
            }
        }
        
        internal static string UNKNOWN_ERROR {
            get {
                return ResourceManager.GetString("UNKNOWN_ERROR", resourceCulture);
            }
        }
        
        internal static string GAME_NOT_FOUND {
            get {
                return ResourceManager.GetString("GAME_NOT_FOUND", resourceCulture);
            }
        }
    }
}
