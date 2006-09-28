First of all please note that I am using C# and an IDE called SharpDevelop
so some parts may not compile with C++. I suggest you download SharpDevelop
and the .NET framework with SDK and use it because that is what I understand.
If C++ works that is cool but I want both of us to be able to work on this 
and I have no idea how to make things other than Consol Apps in C++.

Here is a list of the main things I have done:

- It displays a map
- It displays the navi's sprite
- The map can be scrolled (NumPad 2,4,6,8)
- The navi can be moved
- The navi is somewhat animated

Before I detail what I would like you to help with, here is some general
information about how the navi sprites work:

I am going to use a skin file (like the included skin.txt) that includes
a picture and the height and width of the navi as well as some other things
(one example I was thinking of is some sort of hash to make sure people don't
just alter their navis without approval).

The image included in the skin file (right now it is included in the resource
file of the project and hardcoded into the game) is made by standardizing the
height and width of each animation frame and putting them together. Look at
the included image to see exactly how the frames are arranged.

Here is what I would like you to work on:

- Better graphics (I really have no idea how graphics are supposed to be done
  and I could really use some help making it look good and work fast)
- Horizontal fliping of images. Just figure it out because I couldn't.
- Make the animation timed (when the key is held) instead of when the key
  is pressed.
- Make the map scroll when you move.
- Collision with the map.
- Skin support (shouldn't be too hard). Make it get the FileName, Height, and
  width from the skin file and use them. Don't worry about the other stuff for
  now.

And anything else you think that might be helpful.

Also I could use some help on the website. There are several main things that
I want to implement.

- A system for submitting custom navis for the site (this means storing it to
  a database)
- A system so users can submit sprites (not custom but from the game)
- A system so users can make chip folders for use in the forum RPG or just
  to display which one they use in the game. For this they should be able to
  choose what game of BN they want to use chips from first. Give them the
  largest amount of regular memory and follow the rules from the game. I think
  the wiki has chiplists for almost all of the games.

The site is coded entirely in PHP and the database is MySQL. If you need access
to it I get it to you.

Also the forums:

- A new skin would be nice. I don't know how savvy you are at web design but
  it really would be nice. If you are going to do this, do it on a test forum
  first and then show me.
- Organize the code a bit. By this I mean the stuff in the headers and footers.

I'm not sure if you are interested in helping out with the forums at all but
if you are I can set you up as an Admin.

Thanks in advance this will be incredibly helpful.

~ Spike