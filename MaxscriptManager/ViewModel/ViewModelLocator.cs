/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MaxscriptManager.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MaxscriptManager.Model;
using System.Diagnostics.CodeAnalysis;
//using MaxscriptManager.Design;

namespace MaxscriptManager.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MMainVM>();
            SimpleIoc.Default.Register<MDataVM>();
            SimpleIoc.Default.Register<MCodeVM>();

        }

        /// <summary>s
        /// Gets the Main property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MMainVM Main => ServiceLocator.Current.GetInstance<MMainVM>();
        public MDataVM Data => ServiceLocator.Current.GetInstance<MDataVM>();
        public MCodeVM Code => ServiceLocator.Current.GetInstance<MCodeVM>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}