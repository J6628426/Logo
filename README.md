Logo
====

Logo Demo

Please ensure SDLdotNet is installed!
  - A beta install is included in this repository.
  - Or go to http://sourceforge.net/projects/cs-sdl/files/SDL.NET/ to download the latest libraries.


Using this LOGO Demo
--------------------

Commands can be entered in the input-box at the bottom of the main window.
The command syntax is as follows:
   <Command>[: [<Attribute[=<Value>[, ...]]]

Where the square brackets '[]' denote optional paramatures, and the less-than '<' and greater-than '>' signs denote required parameters.
   E.g.: Move: Right=320, Down=240, Color=Blue

Version 1.0 current commands include:
   - Clear:   Clears the current surface of any marks or drawings, without changing the turtle's location
   - Move:    Moves the turtle on the screen, drawing to the surface if the pen is down
      + Arguments:
         * Left    (integer) moves the turtle to the left by the number of pixels
         * Up      (integer) moves the turtle up by the number of pixels
         * Right   (integer) moves the turtle to the right by the number of pixels
         * Down    (integer) moves the turtle down by the number of pixels
         * Color   (string) draws the line (if pen is down) in the supplied colour for this movement only, if the colour can be interpreted or found.
   - PenDown: Puts the turtle into drawing mode
   - PenUp:   Puts the turtle into non-drawing mode
   - Reset:   Clears the current surface of any marks or drawings and resets turtle's location back to top left corner.
   - Wait:    Pauses the turtles movement for a period of time, default is 1 second.
      + Arguments:
         * Timeout (integer) period in milliseconds for the turtle to wait before continuing

