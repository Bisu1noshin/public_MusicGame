#pragma once
#include "SDL3_ttf/SDL_ttf.h"
#include <string>
#include "System/Math.h"
#include <unordered_map>
#include <iostream>
#include "System/Texture.h"

class Font {
public:
	Font();
	~Font();
	bool Load(const std::string& fileName);
	void Unload();
	Texture* RenderText(const std::string& text, const Color& color = Color(1, 1, 1, 1), int pointSize = 30);
private:
	std::unordered_map<int, TTF_Font*> mFontData;
};