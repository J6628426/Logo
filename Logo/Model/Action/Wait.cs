using System;
using System.Threading;
using System.Xml.Serialization;

namespace LOGO.Model.Action
{
    [Serializable]
    public class Wait : Action, IAction
    {
        #region variables

        private int _timeout;

        #endregion variables


        #region constructors

        public Wait()
        {
            _timeout = 1000;
        }

        public Wait(int timeout)
        {
            _timeout = timeout;
        }

        #endregion constructors


        #region methods

        public override bool Perform(ITurtle turtle)
        {
            Thread.Sleep(_timeout);
            return true;
        }

        #endregion methods


        #region properties

        [XmlAttribute("Timeout")]
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        #endregion properties
    }
}
