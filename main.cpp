#include <cstdlib>
#include <direct.h> // for getcwd
#include <SDL/SDL.h>
#include <SDL/SDL_image.h>
#include "navi.h"
#include "Timer.h"
#include "SDL_Func.h"
#include "background.h"

const int frameRate = 60;  //constant for game frame rate
const int pixelMovement = (8 * frameRate)/24; //constant for ammount of pixels the navi moves per frame
const int pixelDiagonalMovement = (4 * frameRate)/24; //constant for ammount of pixels the navi moves per frame when moving digonally
/*
 * MMBNO Main
 *
 * Created: 3/18/2007 at 2:00 PM by Nare
 *
 * Edited: 6/16/2007 at 1:16 AM by Nare
 *
 * -Fixed the walking glitch from walking diagonally
 *
 * Edited: 6/26/2007 at 11:20 PM by Nare
 *
 * -Changed the old map for the background object, after a little time the image becomes clumsy
 *
 * Edited: 6/28/2007 at 07:46 PM by Nare
 *
 * -Added the backnextframe variable to make the background animation
 *
 * Edited: 6/30/2007 at 07:44 PM by Nare
 *
 * -Minor addition: now the program starts centered in the map
 *
 * Edited: 8/17/2007 at 08:15 PM by Nare
 *
 * -Added constants for frame rate and navi desplacement
 *
 * -Added a smoothing framerate for background and Navis
 *
 */
int main ( int argc, char** argv )
{
    // initialize SDL video
printf("started\n");
 if ( SDL_Init( SDL_INIT_VIDEO ) < 0 )
    {
        printf( "Unable to init SDL: %s\n", SDL_GetError() );
        return 1;
    }

    // make sure SDL cleans up before exit
    atexit(SDL_Quit);

    // create a new window
    SDL_Surface* screen = SDL_SetVideoMode(800, 600, 32,
                                           SDL_HWSURFACE|SDL_DOUBLEBUF);
    if ( !screen )
    {
        printf("Unable to set 640x480 video: %s\n", SDL_GetError());
        return 1;
    }

    //This 4 lines are for getting the program path
    char buffer[_MAX_PATH]; //make an array of chars to save the program path
    string programPath; //where the path WILL be saved
    getcwd(buffer, _MAX_PATH); //gets the program path
    programPath=buffer; //saves the path in the string

    // load a navi
printf(programPath.c_str());
    Navi a(programPath + "\\skin.txt");
printf("Loaded navi\n");
    Background backy(programPath + "\\map.txt", screen);
    bool backnextframe=true; //sets if the background frame changes
printf("Loaded navi\n");
    bool done = false; //this says that it has to do
    int offsetx=0; //nuff said
    int offsety=0; //nuff said
    int direction=0; //0 facing, 1 right-down 2 right, 3 right-up, 4 back
    Timer fps; //This little thingy lets me keep track of framerating
    bool standing=true; //nuff said

    a.move((screen->w - a.naviWidth)/2,(screen->h - 2*a.naviHeight)/2,0,true, true); //puts our little guy STANDING on the center of the screen
    backy.move(backy.backWidth/2-(screen->w/2), backy.backHeight/2-(screen->h/2),false); //centers in the map
    // program main loop
    unsigned char frameCounter=0;
    while (!done)
    {
        fps.start(); //strat the counter for framerating

        // message processing loop
        SDL_Event event;
        while (SDL_PollEvent(&event))
        {
            // check for messages
            switch (event.type)
            {
                // exit if the window is closed
            case SDL_QUIT:
                done = true;
                break;

                // check for keypresses
            case SDL_KEYDOWN:

                    // exit if ESCAPE is pressed
                    switch(event.key.keysym.sym)
                    {
                        case SDLK_DOWN:standing=false; offsety= pixelMovement;break;
                        case SDLK_UP:standing=false; offsety=-pixelMovement;break;
                        case SDLK_LEFT:standing=false; offsetx=-pixelMovement;break;
                        case SDLK_RIGHT:standing=false; offsetx=pixelMovement;break;
                        case SDLK_ESCAPE:standing=false; done = true;break;
                    }
                break;
            case SDL_KEYUP:

                    switch(event.key.keysym.sym)
                    {
                        case SDLK_DOWN: offsety=0;break;
                        case SDLK_UP: offsety=0;break;
                        case SDLK_LEFT: offsetx=0;break;
                        case SDLK_RIGHT: offsetx=0;break;
                    }
            break;
            } // end switch
        } // end of message processing

      if(offsetx==0){
          switch(offsety){
            case 0: standing=true;break; //isn't moving
            case 4: offsety=pixelMovement; direction=0;break; //changed from going diagonally to going sright down
            case 8: direction=0;break; //is going down
            case -4: offsety=-pixelMovement; direction=4;break; //changed from going diagonally to going sright up
            case -8: direction=4;break; //is going up
          }
      }
      else{
        if(offsetx==8){ //goes right
            switch(offsety){
                case 8:offsety=pixelDiagonalMovement;direction=1;break; //is going right-down
                case 0:direction=2;break; //is going right
                case -8:offsety=-pixelDiagonalMovement;direction=3;break; //is going right-up
            }
        }
        else{ //goes left
            switch(offsety){
                case 8:offsety=pixelDiagonalMovement;direction=5;break; //is going left-down
                case 0:direction=6;break; //is going left
                case -8:offsety=-pixelDiagonalMovement;direction=7;break; //is going left-up
            }
        }
      }


        // clear screen
//        SDL_BlitSurface(map, 0, screen, &background); //draw the map, that would CLEAR THE SCREEN
        if((frameCounter % (frameRate/6))==0){ backnextframe= true;} else {backnextframe= false;}

        backy.move(offsetx,offsety,backnextframe);

        backy.draw(screen);
        printf("drew background\n");
        a.move(0,0,direction,true, standing); //we move the navi
        a.draw(screen); //we DRAW the navi on the screen
        // DRAWING ENDS HERE

        SDL_Flip(screen);

        //Limit the framerate
            while( fps.get_ticks() < 1000 / frameRate )
            {
                 //wait...
            }
            if(frameCounter==frameRate-1){frameCounter=0;}else{frameCounter++;}


        // finally, update the screen :)
    } // end main loop

    // all is well ;)
    printf("Exited cleanly\n");
    return 0;
}
