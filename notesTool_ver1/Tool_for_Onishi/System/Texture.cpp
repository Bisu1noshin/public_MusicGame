#define STB_IMAGE_IMPLEMENTATION
#include "../External/stb_image.h"
#include "Texture.h"
#include "GL/glew.h"

Texture::Texture() : texID(0), width(1), height(1) {}
Texture::~Texture() {
	Unload();
}
bool Texture::Load(const std::string& filename) {
	int ch = 0;
	unsigned char* image = stbi_load(filename.c_str(), &width, &height, &ch, 0);
	if (image == nullptr) {
		SDL_Log("Failed to load image %s : %s", filename.c_str(), stbi_failure_reason());
		return false;
	}
	int format = GL_RGB;
	if (ch == 4) {
		format = GL_RGBA;
	}
	glGenTextures(1, &texID);
	glBindTexture(GL_TEXTURE_2D, texID);

	glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, image);
	stbi_image_free(image);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	return true;
}
void Texture::Unload() const {
	glDeleteTextures(1, &texID);
}
void Texture::SetActive() const {
	glBindTexture(GL_TEXTURE_2D, texID);
}

void Texture::CreateFromSurface(SDL_Surface* surf) {
	width = surf->w;
	height = surf->h;
	glGenTextures(1, &texID);
	glBindTexture(GL_TEXTURE_2D, texID);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width, height, 0, GL_RGBA, GL_UNSIGNED_BYTE, surf->pixels);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	
}