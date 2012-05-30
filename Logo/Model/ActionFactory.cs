using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LOGO.Model
{
    /// <summary>
    /// Action Factory singleton class
    /// </summary>
    public class ActionFactory
    {
        #region variables

        private static readonly char[] SEMI = new char[] { ':' };
        private static readonly char[] COMMA = new char[] { ',' };
        private static readonly char[] EQUALS = new char[] { '=' };

        private static ActionFactory _instance = null;

        #endregion variables


        #region constructors

        /// <summary>
        /// Default constructor. Must always instantiate object without exception
        /// </summary>
        private ActionFactory()
        {

        }

        #endregion constructors

        #region methods

        /// <summary>
        /// Get the singleton object for this class
        /// </summary>
        /// <returns>ActionFactory object instance</returns>
        public static ActionFactory GetInstance()
        {
            if (_instance == null) _instance = new ActionFactory();
            return _instance;
        }

        public static IAction Create(string name, Dictionary<string, object> attributes)
        {



            return null;
        }

        public static IAction Create(string text)
        {
            IAction action = null;

            string[] segments = text.Split(SEMI, 2);

            Dictionary<string, object> attributes = new Dictionary<string, object>();

            if (segments.Length == 2)
            {
                string[] list = segments[1].Split(COMMA, StringSplitOptions.RemoveEmptyEntries);

                for (int index = 0; index < list.Length; index++)
                {
                    string[] nameValue = list[index].Split(EQUALS);
                    attributes.Add(nameValue[0], (nameValue.Length == 2 ? nameValue[1] : null));
                }
            }

            action = Create(segments[0], attributes);

            attributes.Clear();
            attributes = null;

            return action;
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
