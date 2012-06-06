using System;
using System.Drawing;
using System.Xml.Serialization;

namespace LOGO.Model.Action
{
    [Serializable]
    public class Draw : Action, IAction
    {
        #region variables

        protected Color _color;
        protected float _penWidth;

        #endregion variables


        #region constructors

        protected Draw()
        {
            _color = System.Drawing.Color.Transparent;
            _penWidth = -1f;
        }

        protected Draw(Color color, float penWidth)
        {
            _color = color;
            _penWidth = penWidth;
        }

        #endregion constructors


        #region methods
        #endregion methods


        #region properties

        [XmlAttribute("Color")]
        public string Color
        {
            get { return _color.ToString(); }
            set { _color = System.Drawing.Color.FromName(value); }
        }

        [XmlAttribute("PenWidth")]
        public float PenWidth
        {
            get { return _penWidth; }
            set { _penWidth = value; }
        }

        #endregion properties
    }
}
