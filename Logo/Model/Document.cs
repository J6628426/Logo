using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using LOGO.Model.Action;
using System.IO;

namespace LOGO.Model
{
    [Serializable]
    public class Document
    {
        private Color _backColor;
        private Color _foreColor;
        private float _penWidth;
        private int _width;
        private int _height;
        private List<Action.Action> _actions;


        public Document()
        {
            //_backColor = Color.White;
            //_foreColor = Color.Black;
            //_penWidth = 4F;
            //_width = 640;
            //_height = 480;

            //_actions = new List<Action.Action>();
            //_actions.Add(new Action.Reset());
            //_actions.Add(new Action.Clear());
            //_actions.Add(new Action.PenUp());
            //_actions.Add(new Action.Move());
            //_actions.Add(new Action.PenDown());

            //_actions.Add(new Action.Move(Color.Blue, 1.5f, 0, 0, 640, 480));

            //_actions.Add(new Action.Move(Color.Red, 1.5f, 0, 480, 0, 0));

            //_actions.Add(new Action.Move(Color.Green, 1.5f, 640, 0, 0, 480));
        }




        [XmlAttribute("BackColor")]
        public string BackColor
        {
            get { return _backColor.ToString(); }
            set { _backColor = Color.FromName(value); }
        }

        [XmlAttribute("ForeColor")]
        public String ForeColor
        {
            get { return _foreColor.ToString(); }
            set { _foreColor = Color.FromName(value); }
        }

        [XmlAttribute("PenWidth")]
        public float PenWidth
        {
            get { return _penWidth; }
            set { _penWidth = value; }
        }

        [XmlAttribute("Width")]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        [XmlAttribute("Height")]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        [XmlElement("Action")]
        public Action.Action[] Action
        {
            get { return _actions.ToArray(); }
            set { _actions = new List<Action.Action>(value); }
        }
    }
}
