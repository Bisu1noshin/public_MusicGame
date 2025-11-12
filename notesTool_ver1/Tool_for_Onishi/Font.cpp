#include "Font.h"
bool Font::Load(const std::string& fileName) {
	std::vector<int> fontSizes = {
		8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28,
		30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 52, 56,
		60, 64, 68, 72
	};
	for (auto size : fontSizes) {
		TTF_Font* font = TTF_OpenFont(fileName.c_str(), size);
		if (font == nullptr) {
			std::cerr << "フォント " << fileName << " サイズ " << size << " のロードに失敗";
			return false;
		}
		mFontData.emplace(size, font);
	}
	return true;
}

Texture* Font::RenderText(const std::string& text, const Color& color, int pointSize) {
	Texture* texture = nullptr;
	SDL_Color sdlColor;
	sdlColor.r = static_cast<Uint8>(color.r * 255);
	sdlColor.g = static_cast<Uint8>(color.g * 255);
	sdlColor.b = static_cast<Uint8>(color.b * 255);
	sdlColor.a = static_cast<Uint8>(color.a * 255);

	auto it = mFontData.find(pointSize);
	if (it != mFontData.end()) {
		TTF_Font* font = it->second;
		SDL_Surface* surf = TTF_RenderText_Blended(font, text.c_str(), 0, sdlColor);
		if (surf != nullptr) {
			texture = new Texture();
			texture->CreateFromSurface(surf);
			SDL_DestroySurface(surf);
		}
	}
	else {
		std::cout << "未対応のサイズ : " << pointSize << std::endl;
	}
	return texture;
}