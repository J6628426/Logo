using System;

namespace LOGO.Model
{
    public interface IAction
    {
        bool Perform(ITurtle turtle);
    }
}
