#ifndef NAVI_H_INCLUDED
#define NAVI_H_INCLUDED
#include <SDL/SDL.h>
#include <SDL/SDL_image.h>
#include <string>
#include <fstream>
#include <map>
#include "SDL_Func.h"
/*
 * MMBNO navi class
 *
 * Created: 10/6/2006 at 9:04 PM by Spikeman
 *
 */
/*
 * Edited: 10/10/2006 at 2:15 AM by Nare
 *
 * Changed the contructor to parse the skin file automatically por the navi data
 *
 * Edited: 3/18/2007 at 2:05 PM by Nare
 *
 * Migrated to C++
 *
 * Edited: 5/14/2007 at 2:05 PM by Nare
 *
 * finished migration to C++/SDL
 - ONLY THING LEFT TO DO IS THE H FLIPPING

 * Edited: 6/14/2007 at 10:00 PM by Nare
 *
 * Flipping Works!!

	/// <summary>
	/// The class for navis.
	/// </summary>
*/
using namespace std;

	class Navi
	{
		private:
            int naviX; //X position in the map
            int naviY; //Y position in the map
            int naviDir; //Direction the navi is facing
            int naviFrame; //number of frame that should be shown
            bool naviHFlip; //Stores if the navi need HFlipping
            int naviNumFrames; //amount of frames in the spritesheet
            int naviStandingFrames; //1 if stnads still, ammount of frames if moves while standing
            SDL_Surface* naviImage; //the spritesheet itself
            SDL_Surface* todraw; //the frame to draw


public:
  bool loadNavi(string filename);
  Navi(string filename); //full filename
  ~Navi(void);
            int naviWidth;
            int naviHeight;
  void draw(SDL_Surface* g); //This is the
  bool IsFlipped(void); //Returns if the navi uses the flipping process or is stored in spritesheet
  void FlipH(void); //Changes the naviHflip variable
  void move(int x, int y, int direction, bool nextframe, bool stand);

};



bool Navi::IsFlipped(void)
{
			return naviHFlip;
}

void Navi::FlipH(void)
{
    naviHFlip = !naviHFlip;
}

Navi::Navi(string filename) //full filename
{
    naviX=0;
    naviY=0;
    naviDir=0;
    naviFrame=0;
	if(!loadNavi(filename)){/*	throw new System.Exception("Invalid skin");*/}
}

Navi::~Navi(void)
{
    SDL_free(todraw);
    SDL_free(naviImage);

}

bool Navi::loadNavi(string filename)
{
			//parses file into a dictionary so anything later added to skins can be added very easily

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

	string path = filename.substr(0,filename.find_last_of("\\"));

	naviWidth = atoi(vals["width"].c_str());
	naviHeight = atoi(vals["height"].c_str());
	naviNumFrames = atoi(vals["frames"].c_str());
	naviHFlip = atoi(vals["hflip"].c_str());
	path = path + "\\" + vals["filename"].c_str();
	naviImage = IMG_Load(path.c_str());
	if(vals.find("standingframes")!=vals.end()){naviStandingFrames = atoi(vals["standingframes"].c_str());} else{naviStandingFrames = 1;}; //default 1
	return true; //success
}

void Navi::draw(SDL_Surface* g)		{
	SDL_Rect rectscr; //where it goes in the screen
	SDL_Rect rectnavi; //where it has to take the frame from in the sprite sheet
	SDL_Rect rectframe; //to blit the into todraw

	rectscr.x=naviX;
	rectscr.y=naviY;
	rectscr.w=naviWidth;
	rectscr.h=naviHeight;

	rectnavi.x=naviFrame*naviWidth;
	rectnavi.w=naviWidth;
	rectnavi.h=naviHeight;

	rectframe.x=0;
	rectframe.y=0;
	rectframe.w=naviWidth;
	rectframe.h=naviHeight;
	//if the Hflip is on, then we must flip the navi before drawing
        //I initialize the cropped navi image
    SDL_free(todraw);
	todraw = SDL_CreateRGBSurface(naviImage->flags, rectscr.w, rectscr.h, naviImage->format->BitsPerPixel, naviImage->format->Rmask, naviImage->format->Gmask, naviImage->format->Bmask, 0);
	SDL_SetColorKey(todraw, SDL_SRCCOLORKEY,naviImage->format->colorkey);

	if((naviDir>4) && naviHFlip){
		rectnavi.y=(naviDir-4)*naviHeight; //I get the correct row
		SDL_BlitSurface(naviImage,&rectnavi,todraw,&rectframe); //cut the one to flip
		todraw=flip_surface(todraw, FLIP_HORIZONTAL); //and flip
	}
        else{
		rectnavi.y=naviDir*naviHeight; //I get the correct row
                SDL_BlitSurface(naviImage,&rectnavi,todraw,&rectframe); //if no need to flip, I just cut it
	}
	SDL_BlitSurface(todraw, &rectframe, g, &rectscr); //image,rect,frame*width,dir*height,width,height,GraphicsUnit.Pixel);
}

void Navi::move(int x, int y, int direction,bool nextframe, bool stand){
    naviX+=x;
    naviY+=y;
    naviDir=direction;
    if(nextframe){
        naviFrame++;
        if(naviFrame>naviNumFrames){naviFrame=0;}
    }
    if(stand){naviFrame=0;}
}
#endif // NEW_H_INCLUDED
