using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace LOGO.Model.Action
{
    [Serializable]
    public class Move : Draw, IAction
    {
        #region variables

        private int _left;
        private int _up;
        private int _right;
        private int _down;

        #endregion variables


        #region constructors

        public Move()
        {
            _left = 0;
            _up = 0;
            _right = 0;
            _down = 0;
        }

        public Move(int left, int up, int right, int down)
        {
            _left = left;
            _up = up;
            _right = right;
            _down = down;
        }

        public Move(Color color, float penWidth, int left, int up, int right, int down)
            : base(color, penWidth)
        {
            _left = left;
            _up = up;
            _right = right;
            _down = down;
        }

        #endregion constructors


        #region methods

        private Point CalculateValidEndPoint(ITurtle turtle)
        {
            Point end = new Point((turtle.Location.X - _left) + _right, (turtle.Location.Y - _up) + _down);

            // The equation to find the angle of a line is m = (y2 - y1) / (x2 - x1)
            // Therefore if we have the angle, origin and a desired ending X or Y coordinate, we could find the corresponding X or Y coordinate.
            // So, for example: m = (-24 - 120) / (259 - 160) = -144 / 99 = -1.4545454545454545454545454545455
            //                  Angle(m) = tan-1(-1.4545454545454545454545454545455) = -55.491477012331598683770360648605
            // Therefore, if we have our desired end-point at y-axiz ?, we need to calculate x2
            //                  x2 = ((y2 - y1) / m) + x1
            //
            // Therefore, if we have our desired end-point at x-axiz ?, we need to calculate y2
            //                  y2 = (m * (x2 - x1)) + y1

            // Find special cases where line extends further than surface
            // NOTE: may have to test by longest side first, will have to test
            float m = ((float)end.Y - (float)turtle.Location.Y) / ((float)end.X - (float)turtle.Location.X);

            if ((end.X < 0) || (end.X > turtle.Size.Width))
            {
                end.X = (end.X < 0 ? 0 : (end.X > turtle.Size.Width ? turtle.Size.Width : end.X));
                end.Y = (int)((m * ((float)end.X - (float)turtle.Location.X)) + (float)turtle.Location.Y);
            }
            if ((end.Y < 0) || (end.Y > turtle.Size.Height))
            {
                end.Y = (end.Y < 0 ? 0 : (end.Y > turtle.Size.Height ? turtle.Size.Height : end.Y));
                end.X = (int)((((float)end.Y - (float)turtle.Location.Y) / m) + (float)turtle.Location.X);
            }            

            return end;
        }

        public override bool Perform(ITurtle turtle)
        {
            if (turtle == null) return false;

            try
            {
                Point point1 = turtle.Location;

                // find if / where line intersects a border, and set end point (point 2) to the intersection.
                Point point2 = CalculateValidEndPoint(turtle);

                // if the turtle's pen is down, then perform draw...
                if (turtle.PenDown)
                {
                    SdlDotNet.Graphics.Primitives.Line line = new SdlDotNet.Graphics.Primitives.Line(point1, point2);
                    //if (_penWidth > 0f) turtle.Surface.Draw()
                    line.Draw(turtle.Surface, (_color.A > 0 ? _color : turtle.ForeColor), true, true);
                }
                turtle.Location = point2;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (_left != 0) builder.AppendFormat("Left={0}", _left);
            if (_up != 0) builder.AppendFormat(builder.Length > 0? ", Up={0}" : "Up={0}", _up);
            if (_right != 0) builder.AppendFormat(builder.Length > 0 ? ", Right={0}" : "Right={0}", _right);
            if (_down != 0) builder.AppendFormat(builder.Length > 0 ? ", Down={0}" : "Down={0}", _down);
            if (_color.A > 0) builder.AppendFormat(builder.Length > 0 ? ", Color={0}" : "Color={0}", _color.Name);
            if (_penWidth > 0F) builder.AppendFormat(builder.Length > 0 ? ", PanWidth={0}" : "PanWidth={0}", _penWidth);

            return "Move: " + builder.ToString();
        }

        #endregion methods


        #region properties

        [XmlAttribute("Left")]
        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        [XmlAttribute("Up")]
        public int Up
        {
            get { return _up; }
            set { _up = value; }
        }

        [XmlAttribute("Right")]
        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }

        [XmlAttribute("Down")]
        public int Down
        {
            get { return _down; }
            set { _down = value; }
        }

        #endregion properties

    }
}
