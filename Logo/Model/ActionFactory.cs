using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;

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

        private static Assembly _assembly;
        private static Type _type;

        #endregion variables


        #region constructors

        /// <summary>
        /// Static constructor
        /// </summary>
        static ActionFactory()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _type = typeof(Action.Action);
        }

        #endregion constructors


        #region methods

        public static IAction Create(string name, Dictionary<string, object> attributes)
        {
            dynamic obj = null;

            try
            {
                obj = _assembly.CreateInstance(name);
            }
            catch (Exception ex)
            {
                obj = null;
            }

            if (obj == null)
            {
                try
                {
                    obj = _assembly.CreateInstance(string.Format("{0}.{1}", _type.Namespace, name));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            Type type = obj.GetType();
            PropertyInfo[] propertyList = type.GetProperties();

            foreach (string key in attributes.Keys)
            {
                object value = attributes[key];

                for (int index = 0; index < propertyList.Length; index++)
                {
                    PropertyInfo info = propertyList[index];
                    if (!info.Name.Equals(key.Trim(), StringComparison.InvariantCultureIgnoreCase)) continue;

                    if (value != null)
                    {
                        if (!info.PropertyType.Equals(value.GetType()))
                        {
                            value = Convert.ChangeType(value, info.PropertyType);
                        }
                    }

                    type.InvokeMember(info.Name, BindingFlags.SetProperty, null, obj, new object[] { value });
                    break;
                }
            }

            return obj;
        }

        public static IAction Create(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            Dictionary<string, object> attributes = new Dictionary<string, object>();

            string[] segments = text.Trim().Split(SEMI, 2);
            if (segments.Length == 2)
            {
                string[] list = segments[1].Trim().Split(COMMA, StringSplitOptions.RemoveEmptyEntries);

                for (int index = 0; index < list.Length; index++)
                {
                    string[] nameValue = list[index].Trim().Split(EQUALS);
                    attributes.Add(nameValue[0].Trim(), (nameValue.Length == 2 ? nameValue[1].Trim() : null));
                }
            }

            IAction action = Create(segments[0], attributes);

            attributes.Clear();
            attributes = null;

            return action;
        }

        public static IAction Create(XmlNode node)
        {
            if (node == null) return null;

            string name = node.Name;
            Dictionary<string, object> attributes = new Dictionary<string, object>();

            for (int index = 0; index < node.Attributes.Count; index++)
            {
                XmlAttribute attribute = node.Attributes[index];

                if (attribute.Name.Equals("name", StringComparison.InvariantCultureIgnoreCase)
                 || attribute.Name.EndsWith("type", StringComparison.InvariantCultureIgnoreCase)) name = attribute.Value;
                else attributes.Add(attribute.Name.Trim(), attribute.Value.Trim());
            }

            IAction action = Create(name, attributes);

            attributes.Clear();
            attributes = null;

            return action;
        }

        public static XmlNode Create(XmlDocument xmlDocument, IAction action)
        {
            if (action == null) return null;

            Type type = action.GetType();
            PropertyInfo[] propertyList = type.GetProperties();
            XmlNode node = xmlDocument.CreateElement(type.Name);

            for (int index = 0; index < propertyList.Length; index++)
            {
                PropertyInfo info = propertyList[index];

                XmlAttribute attribute = xmlDocument.CreateAttribute(info.Name);
                attribute.Value = type.InvokeMember(info.Name, BindingFlags.GetProperty, null, action, null).ToString();
                node.Attributes.Append(attribute);
            }

            return node;
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
