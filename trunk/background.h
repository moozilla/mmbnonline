#ifndef background_H_INCLUDED
#define background_H_INCLUDED
#include <SDL/SDL.h>
#include <SDL/SDL_image.h>
#include <string>
#include <fstream>
#include <map>
#include "SDL_Func.h"
/*
 * MMBNO background class
 *
 * Created: 6/23/2007 at 11:00 PM by Nare
 *
 * Edited: 6/26/2007 at 11:20 PM by Nare
 *
 * - Started the draw function
 *
 * Edited: 6/28/2007 at 11:20 PM by Nare
 *
 * - modified the move function, it now thakes a bool to know if to move to the next frame
 * - modified the draw function, it now creates the bacgkground correctly
 */

using namespace std;

	class Background
	{
		private:
            int backFrame; //number of frame that should be shown
            int backNumFrames; //amount of frames in the spritesheet
            int backY; //position of the background in the screen
            int backX; //position of the background in the screen
            SDL_Surface* backImage; //the spritesheet itself


public:
  bool loadBackground(string filename, SDL_Surface* screen);
  Background(string filename, SDL_Surface* screen); //full filename
  ~Background(void);
  int backWidth;
  int backHeight;
  void draw(SDL_Surface* g); //This is the
  void move(int x, int y, bool nextframe);
};

Background::Background(string filename, SDL_Surface* screen) //full filename
{
	if(!loadBackground(filename, screen)){/*	throw new System.Exception("Invalid skin");*/}
}

Background::~Background(void)
{
    SDL_free(backImage);
}

bool Background::loadBackground(string filename, SDL_Surface* screen)
{
			//parses file into a dictionary so anything later added to skins can be added very easily


    SDL_Surface* tempmap; //this will be the temporary image for the map
    SDL_Surface* spriteframes; //temp for the sprites of the background
    int tempWidth;
    int tempHeight;

	ifstream archivo;
	string line;
	string aux;
	map<string, string> vals; //store data from file in dictionary
        archivo.open(filename.c_str());
        getline(archivo,line);
        int i;
	while (!(archivo.eof())) {
		if(line.find("//")!=0) //only gets lines that aren't comments
		{
			int t = line.find("=");
			aux=line.substr(0,t);
//THIS FOR IS HERE BECAUSE THE DARNED GCC COMPILER HAS A BUG WITH THE NORMAL WAY RO CONVERT A STRING TO LOWER CASE
			for(i=0; i<aux.length();i++){aux[i]=tolower(aux[i]);}
			string key = aux; //key name is on left of equal sign
			aux=line.substr(t+1);
			for(i=0; i<aux.length();i++){aux[i]=tolower(aux[i]);}
			string val = aux; //value name on right of equal sign
			vals[key]=val;
		}
		getline(archivo,line);
	}
	archivo.close();

	string path = filename.substr(0,filename.find_last_of("\\")); //recover the file path
	string tempname; //temporary string to load the images

	tempWidth = atoi(vals["mapwidth"].c_str()); //loads the width of the map
	tempHeight = atoi(vals["mapheight"].c_str()); //loads the height of the map

    //loads the amount of frames there is of the map; total frames = frames per row * ammount of rows
	backNumFrames = atoi(vals["spritesperrow"].c_str()) * atoi(vals["spritesrows"].c_str());
    tempname = path + "\\" + vals["backfile"].c_str(); //recover the path to the background sprites
	spriteframes = IMG_Load(tempname.c_str()); //load the sprites for background animation

    backWidth = tempWidth + (screen->w); //this way, the map can be displayed with the Navi always centered
    backHeight = tempHeight + (screen->h); //this way, the map can be displayed with the Navi always centered

	//Create the blank background
	backImage = SDL_CreateRGBSurface(screen->flags, backWidth*backNumFrames,backHeight, screen->format->BitsPerPixel, screen->format->Rmask, screen->format->Gmask, screen->format->Bmask,0);

	tempWidth = atoi(vals["spritewidth"].c_str()); //loads the width of the frames
	tempHeight = atoi(vals["spriteheight"].c_str()); //loads the height of the frames

	SDL_Rect cutplace; //the rectangle to cut
	SDL_Rect pasteplace; //where to blit it
	int offsetX=0;
	int offsetY=0;
    int row=0;
    int col=0;

    //the cut and paste sizes will be the same size as the sprites
    cutplace.w=tempWidth;
    cutplace.h=tempHeight;
    pasteplace.w=tempWidth;
    pasteplace.h=tempHeight;

	for (i=0; i<backNumFrames; i++){
	    cutplace.x=col*tempWidth; //calculates wich sprite from the row to take
	    cutplace.y=row*tempHeight; //claculates wich row to take
        while(offsetY<backWidth){
            pasteplace.y=offsetY;
            while(offsetX<backWidth){ //while I haven't filled a frame of the background
                pasteplace.x=(i*backWidth)+offsetX;
                SDL_BlitSurface(spriteframes, &cutplace, backImage, &pasteplace);
                offsetX+=tempWidth;
            }
            offsetX=0;
            offsetY+=tempHeight;
        }
        offsetY=0;
        col++;
        if(col==atoi(vals["spritesperrow"].c_str())){
            col=0;
            row++;
        }
    }
    SDL_free(spriteframes);

    tempname = path + "\\" + vals["filename"].c_str();
    tempmap = IMG_Load(tempname.c_str()); //loads the map

    //we are going to paste the whole map
    cutplace.x=0;
    cutplace.y=0;
    cutplace.w=tempmap->w;
    cutplace.h=tempmap->h;

    pasteplace.y=(screen->h)/2; //center horizontally
    pasteplace.w=atoi(vals["mapwidth"].c_str());
    pasteplace.h=atoi(vals["mapheight"].c_str());
    for (i=0; i<backNumFrames; i++){
        pasteplace.x=i*backWidth + ((screen->w)/2); //center vertically on it's frame
        SDL_BlitSurface(tempmap, &cutplace, backImage, &pasteplace); //blit
    }

    SDL_free(tempmap);

	backX=0;
	backY=0;
	backFrame=0;
	return true; //success
}

void Background::draw(SDL_Surface* g)		{
	SDL_Rect rectscr; //where it goes in the screen
	SDL_Rect rectback; //where it has to take the frame from in the sprite sheet
	SDL_Rect rectframe; //to blit the into todraw

	rectscr.x=0;
	rectscr.y=0;
	rectscr.w=g->w;
	rectscr.h=g->h;

	rectback.x=(backFrame*backWidth)+backX; //I get the place where I have to start cutting
	rectback.y=backY;//I get the place where I have to start cutting
	rectback.w=g->w; //The size of the image to cut is the size of the screen
	rectback.h=g->h; //The size of the image to cut is the size of the screen
	//if the Hflip is on, then we must flip the back before drawing
        //I initialize the cropped back image

   // SDL_BlitSurface(backImage,&rectback,todraw,&rectframe); //if no need to flip, I just cut it

	SDL_BlitSurface(backImage, &rectback, g, &rectscr); //image,rect,frame*width,dir*height,width,height,GraphicsUnit.Pixel);
}

void Background::move(int x, int y, bool nextframe){
    backX+=x;
    backY+=y;
    if(nextframe){
        backFrame++;
        if(backFrame==backNumFrames){backFrame=0;}
    }
}


#endif // NEW_H_INCLUDED
