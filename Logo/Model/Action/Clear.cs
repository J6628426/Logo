using System;

namespace LOGO.Model.Action
{
    [Serializable]
    public class Clear : Action, IAction
    {
        #region variables
        #endregion variables


        #region constructors

        public Clear()
        {
        }

        #endregion constructors


        #region methods

        public override bool Perform(ITurtle turtle)
        {
            if (turtle == null) return false;

            turtle.Clear();
            return true;
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
