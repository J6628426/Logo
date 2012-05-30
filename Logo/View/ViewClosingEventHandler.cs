using System;

namespace LOGO.View
{
    public delegate void ViewClosingEventHandler(IView sender, ViewClosingEventArgs args);

    public class ViewClosingEventArgs
    {
        #region variables

        private bool _cancel;

        #endregion variables


        #region constructors

        public ViewClosingEventArgs()
        {
            _cancel = false;
        }

        #endregion constructors


        #region methods
        #endregion methods


        #region properties

        /// <summary>
        /// Cancel public accessor
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        #endregion proeprties
    }
}
