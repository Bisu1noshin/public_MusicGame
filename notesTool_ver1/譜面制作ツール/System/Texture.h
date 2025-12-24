#pragma once
#include <string>
#include "GL/glew.h"
#include "SDL3/SDL.h"
#include "SDL3_image/SDL_image.h"

class Texture {
public:
	Texture();
	~Texture();
	bool Load(const std::string& filename);
	void Unload() const;
	void SetActive() const;
	void CreateFromSurface(SDL_Surface* surf);

	int GetWidth() const { return width; }
	int GetHeight() const { return height; }
private:
	unsigned int texID;
	int width, height;
};
