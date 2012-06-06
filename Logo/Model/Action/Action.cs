using System;

namespace LOGO.Model.Action
{
    /// <summary>
    /// Action base class
    /// </summary>
    public class Action : IAction
    {
        #region variables
        #endregion variables


        #region constructors
        #endregion constructors


        #region methods

        public virtual bool Perform(ITurtle turtle)
        {
            return false;
        }

        public override string ToString()
        {
            return GetType().Name;
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
