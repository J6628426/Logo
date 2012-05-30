using System;
using System.Drawing;

namespace LOGO.Model
{
    public class Turtle : ITurtle
    {
        #region variables

        private Color _backColor;
        private Color _foreColor;
        private Image _image;
        private Point _location;
        private float _penWidth;

        #endregion variables


        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Turtle()
        {

        }

        #endregion constructors

        #region methods

        #endregion methods


        #region properties

        public System.Drawing.Color BackColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Drawing.Color ForeColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Drawing.Image Image
        {
            get { throw new NotImplementedException(); }
        }

        public System.Drawing.Point Location
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float PenWidth
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion properties
    }
}
