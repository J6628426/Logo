using System;


namespace LOGO.View
{
    public delegate void ViewInputEventHandler(IView sender, ViewInputEventArgs args);

    public class ViewInputEventArgs
    {
        #region variables

        private string _text = null;

        #endregion variables


        #region constructor

        public ViewInputEventArgs(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            _text = text;
        }

        #endregion constructor


        #region methods
        #endregion methods


        #region properties

        public string Text
        {
            get { return _text; }
        }

        #endregion properties
    }
}
