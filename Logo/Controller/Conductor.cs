using System;
using System.Collections.Generic;
using LOGO.Model;
using LOGO.View;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LOGO.Controller
{
    public class Conductor : IDisposable
    {
        #region variables

        private static Conductor _instance = null;

        private List<IView> _listViews = null;
        private Turtle _turtle = null;
        private List<IAction> _history = null;

        #endregion variables


        #region constructors

        /// <summary>
        /// Default constructor. Must always instantiate object without exception
        /// </summary>
        private Conductor()
        {
            _listViews = new List<IView>();
            _turtle = null;
            _history = new List<IAction>(25);
        }

        #endregion constructors


        #region methods

        /// <summary>
        /// Add a view to the controller
        /// </summary>
        /// <param name="view"></param>
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
        /// Release all used memory
        /// </summary>
        public void Dispose()
        {
            if (_listViews != null)
            {
                while (_listViews.Count > 0)
                {
                    IView view = _listViews[0];
                    _listViews.RemoveAt(0);

                    view.Dispose();
                    view = null;
                }

                _listViews.Clear();
                _listViews = null;
            }

            if (_history != null)
            {
                _history.Clear();
                _history = null;
            }

            if (_turtle != null)
            {
                _turtle.Dispose();
                _turtle = null;
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

        /// <summary>
        /// Load an XML document from file
        /// </summary>
        /// <param name="filePath"></param>
        public void Load(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("File not found!", filePath);

            XmlDocument xmlDocument = null;

            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load XML document!");
            }

            XmlNode document = xmlDocument["Document"];
            if (document == null) throw new Exception("Root document element not found in file!");

            // clean up old memory if it exists
            if (_turtle != null)
            {
                _turtle.Dispose();
                _turtle = null;
            }

            if (_history != null)
            {
                _history.Clear();
                _history = null;
            }

            // create new turtle instance
            _turtle = new Turtle();
            _turtle.Surface.ToString();
            _turtle.Reset();
            _history = new List<IAction>();

            for (int index = 0; index < document.Attributes.Count; index++)
            {
                XmlAttribute attribute = document.Attributes[index];

                switch (attribute.Name.ToLower())
                {
                    case "backcolor":
                        _turtle.BackColor = Color.FromName(attribute.Value);
                        break;
                    case "forecolor":
                        _turtle.ForeColor = Color.FromName(attribute.Value);
                        break;
                    case "penwidth":
                        float penWidth = 0f;
                        if (float.TryParse(attribute.Value, out penWidth)) _turtle.PenWidth = penWidth;
                        break;
                    case "width":
                        int width = 0;
                        if (int.TryParse(attribute.Value, out width)) _turtle.Size = new Size(width, _turtle.Size.Height);
                        break;
                    case "height":
                        int height = 0;
                        if (int.TryParse(attribute.Value, out height)) _turtle.Size = new Size(_turtle.Size.Width, height);
                        break;
                }
            }

            for (int index = 0; index < document.ChildNodes.Count; index++)
            {
                XmlNode child = document.ChildNodes[index];

                // simply pass to ActionFactory and try perform the result...
                IAction action = ActionFactory.Create(child);
                if (action != null) Perform(action);
            }

            document = null;
            xmlDocument = null;
        }

        /// <summary>
        /// Performs the provided action against the turtle
        /// </summary>
        /// <param name="action">IAction - The action instance with which to perform on the turtle</param>
        public void Perform(IAction action)
        {
            if (action == null) return;
            if (_turtle == null) _turtle = new Turtle();

            bool success = action.Perform(_turtle);
            if (success)
            {
                _history.Add(action);

                for (int index = 0; index < _listViews.Count; index ++)
                {
                    _listViews[index].Draw(_turtle);
                    _listViews[index].Print(action.ToString());
                }
            }
        }

        /// <summary>
        /// Save current setup to new XML document
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveAs(string filePath)
        {
            if (_turtle == null) return;

            XmlDocument xmlDocument = new XmlDocument();
            XmlNode document = xmlDocument.CreateElement("Document");
            xmlDocument.AppendChild(document);

            XmlAttribute attribute = xmlDocument.CreateAttribute("BackColor");
            attribute.Value = _turtle.BackColor.Name;
            document.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("ForeColor");
            attribute.Value = _turtle.ForeColor.Name;
            document.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("PenWidth");
            attribute.Value = _turtle.PenWidth.ToString("N1");
            document.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("Width");
            attribute.Value = _turtle.Size.Width.ToString();
            document.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("Height");
            attribute.Value = _turtle.Size.Height.ToString();
            document.Attributes.Append(attribute);

            foreach (IAction action in _history)
            {
                XmlNode node = ActionFactory.Create(xmlDocument, action);
                if (node != null) document.AppendChild(node);
            }

            document = null;

            xmlDocument.Save(filePath);
            xmlDocument = null;
        }

        /// <summary>
        /// Show the application running
        /// </summary>
        /// <param name="filePath">string - A file path to load an XMl document</param>
        public void Show(string filePath)
        {
            foreach (IView view in _listViews)
            {
                view.Show();
            }

            if (_listViews.Count > 0) Application.Run((Form)_listViews[0]);
            else Application.Run();
        }

        /// <summary>
        /// Catches view Closing events to process responsibly
        /// </summary>
        /// <param name="sender">IView - The view that raised the close event</param>
        /// <param name="args">ViewClosingEventArgs - The event arguments to modify</param>
        private void ViewClosing(IView sender, ViewClosingEventArgs args)
        {
            if (_listViews.Contains(sender))
            {
                _listViews.Remove(sender);

                if (_listViews.Count < 1)
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// Catches view Input events
        /// </summary>
        /// <param name="sender">IView - The view that raise the Input event</param>
        /// <param name="args">ViewInputEventArgs - The event arguments process</param>
        private void ViewInput(IView sender, ViewInputEventArgs args)
        {
            Perform(ActionFactory.Create(args.Text));
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
