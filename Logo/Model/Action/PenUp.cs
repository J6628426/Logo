using System;

namespace LOGO.Model.Action
{
    [Serializable]
    public class PenUp : Action, IAction
    {
        #region variables
        #endregion variables


        #region constructors

        public PenUp()
        {
        }

        #endregion constructors


        #region methods

        public override bool Perform(ITurtle turtle)
        {
            if (turtle == null) return false;
            if (!turtle.PenDown) return false;

            turtle.PenDown = false;
            return true;
        }

        #endregion methods


        #region properties
        #endregion properties
    }
}
