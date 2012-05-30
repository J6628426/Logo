using System.Drawing;

namespace LOGO.Model
{
    interface IAction
    {
        void Perform(ITurtle turtle);

        Color Color
        {
            get;
            set;
        }

        float PenWidth
        {
            get;
            set;
        }
    }
}
