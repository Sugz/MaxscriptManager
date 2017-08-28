using GalaSoft.MvvmLight;
using MaxscriptManager.Model;
using System.Windows;

namespace MaxscriptManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MMainVM : ViewModelBase
    {

        #region Fields


        private bool _StatusPanelIsOpen = true;                                     // The status panel opening state
        private string _Status = string.Empty;                                      // The string displayed in the status panel
        private Visibility _ProgressBarVisibility = Visibility.Collapsed;           // The progressbar visibility


        #endregion Fields



        #region Properties


        /// <summary>
        /// Get or set the status panel opening state
        /// </summary>
        public bool StatusPanelIsOpen
        {
            get => _StatusPanelIsOpen;
            set => Set(ref _StatusPanelIsOpen, value);
        }

        /// <summary>
        /// Get or set the string displayed in the status panel
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        /// <summary>
        /// Get the progressbar visibility
        /// </summary>
        public Visibility ProgressBarVisibility
        {
            get => _ProgressBarVisibility;
            private set => Set(ref _ProgressBarVisibility, value);
        }


        #endregion Properties



        #region Constructor


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MMainVM()
        {

        } 


        #endregion Constructor


    }
}