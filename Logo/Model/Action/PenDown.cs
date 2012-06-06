using System;

namespace LOGO.Model.Action
{
    [Serializable]
    public class PenDown : Action, IAction
    {
        #region variables
        #endregion variables


        #region constructors

        public PenDown()
        {
        }

        #endregion constructors


        #region methods

        public override bool Perform(ITurtle turtle)
        {
            if (turtle == null) return false;
            if (turtle.PenDown) return false;

            turtle.PenDown = true;
            return true;
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
