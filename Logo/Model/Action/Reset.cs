using System;

namespace LOGO.Model.Action
{
    [Serializable]
    public class Reset : Action, IAction
    {
        #region variables
        #endregion variables


        #region constructors

        public Reset()
        {
        }

        #endregion constructors


        #region methods

        public override bool Perform(ITurtle turtle)
        {
            if (turtle == null) return false;

            turtle.Reset();
            return true;
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
