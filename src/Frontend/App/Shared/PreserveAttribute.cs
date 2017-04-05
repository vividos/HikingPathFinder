using System;

namespace HikingPathFinder.App
{
    /// <summary>
    /// This attribute can be used to prevent that the Xamarin linker removes the method during
    /// linking (e.g. in Release mode, when using "Link all assemblies".
    /// See https://developer.xamarin.com/api/type/MonoTouch.Foundation.PreserveAttribute/
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class PreserveAttribute : Attribute
    {
        /// <summary>
        /// Constructor for attribute
        /// </summary>
        public PreserveAttribute()
        {
        }

        /// <summary>
        /// When the attribute is applied to a class and this property is set to true, then all
        /// members are kept by the linker.
        /// </summary>
        public bool AllMembers { get; set; }

        /// <summary>
        /// When true, the linker keeps the marked method or property only when the whole type is
        /// kept by the linker.
        /// </summary>
        public bool Conditional { get; set; }
    }
}
