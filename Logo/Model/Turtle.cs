using System;
using System.Drawing;
using System.Collections.Generic;

namespace LOGO.Model
{
    public class Turtle : ITurtle
    {
        #region variables

        private Color _backColor;
        private Color _foreColor;
        private Image _image;
        private Point _location;
        private bool _penDown;
        private float _penWidth;
        private Size _size;
        private SdlDotNet.Graphics.Surface _surface;

        #endregion variables


        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Turtle()
        {
            _backColor = Color.White;
            _foreColor = Color.Black;
            _image = null;
            _location = new Point(0, 0);
            _penDown = true;
            _penWidth = 1F;
            _size = new Size(640, 480);
            _surface = null;
        }

        #endregion constructors


        #region methods

        public void Clear()
        {
            if (_surface != null)
            {
                _surface.Fill(_backColor);
            }
        }

        public void Dispose()
        {
            if (_surface != null)
            {
                _surface.Dispose();
                _surface = null;
            }
        }

        public void Redraw()
        {

        }

        public void Reset()
        {
            _location = new Point(0, 0);
            Clear();
        }

        #endregion methods


        #region properties

        /// <summary>
        /// Gets or sets the background color for the resulting image
        /// </summary>
        public Color BackColor
        {
            get { return _backColor; }
            set
            {
                if (_backColor.Equals(value)) return;

                _backColor = value;
                Redraw();
            }
        }

        /// <summary>
        /// gets or sets the foreground pen color for the resulting image
        /// </summary>
        public Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                if (_foreColor.Equals(value)) return;

                _foreColor = value;
                Redraw();
            }
        }

        /// <summary>
        /// Gets a bitmap image object representing the result
        /// </summary>
        public Image Image
        {
            get { return (_surface == null ? null : _surface.Bitmap); }
        }

        /// <summary>
        /// Gets or sets the turtle location within the image
        /// </summary>
        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }

        /// <summary>
        /// Gets or sets the pen status
        /// </summary>
        public bool PenDown
        {
            get { return _penDown; }
            set { _penDown = value; }
        }

        /// <summary>
        /// Gets or sets the turtle's default pen width in the resuting image
        /// </summary>
        public float PenWidth
        {
            get { return _penWidth; }
            set
            {
                if (_penWidth.Equals(value)) return;

                _penWidth = value;
                Redraw();
            }
        }

        /// <summary>
        /// The size of the sandbox in which the turtle can move
        /// </summary>
        public Size Size
        {
            get { return _size; }
            set
            {
                if (_size.Equals(value)) return;

                _size = value;
                Redraw();
            }

        }

        /// <summary>
        /// The drawing surface of the turtle
        /// </summary>
        public SdlDotNet.Graphics.Surface Surface
        {
            get
            {
                if (_surface == null)
                {
                    _surface = new SdlDotNet.Graphics.Surface(_size.Width, _size.Height);
                    Clear();
                }
                return _surface;
            }
        }

        #endregion properties
    }
}
