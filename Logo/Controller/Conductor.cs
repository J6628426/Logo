using System;
using System.Collections.Generic;
using LOGO.Model;
using LOGO.View;

namespace LOGO.Controller
{
    public class Conductor
    {
        #region variables

        private static Conductor _instance = null;

        private List<IView> _listViews = null;
        private Turtle _turtle = null;

        #endregion variables


        #region constructors

        /// <summary>
        /// Default constructor. Must always instantiate object without exception
        /// </summary>
        private Conductor()
        {

        }

        #endregion constructors

        #region methods

        public void AddView(IView view)
        {
            if (!_listViews.Contains(view))
            {
                _listViews.Add(view);

                view.Closing += new ViewClosingEventHandler(ViewClosing);
                view.Input += new ViewInputEventHandler(ViewInput);
            }
        }

        /// <summary>
        /// Get the singleton object for this class
        /// </summary>
        /// <returns>ActionFactory object instance</returns>
        public static Conductor GetInstance()
        {
            if (_instance == null) _instance = new Conductor();
            return _instance;
        }

        public void Perform(string action, string[] arguments)
        {
        }

        private void ViewClosing(IView sender, ViewClosingEventArgs args)
        {
            if (_listViews.Contains(sender))
            {
                _listViews.Remove(sender);

                if (_listViews.Count < 1)                
            }
        }

        private void ViewInput(IView sender, ViewInputEventArgs args)
        {
            IAction action = ActionFactory.Create(args.Text);
            if (action != null)
            {
                action.Perform(_turtle);
            }
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
