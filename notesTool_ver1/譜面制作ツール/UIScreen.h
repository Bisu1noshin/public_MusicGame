#pragma once
#include <cstdint>
#include <string>
#include <vector>
#include "System/Math.h"

class UIScreen {
public:
	UIScreen(class Game* game);
	virtual ~UIScreen();
	virtual void Update();
	virtual void ProcessInput(const uint8_t* keys);
	virtual void HandleKeyPress(int key);

	enum UIState { Active, Closing };
	void Close();
	UIState GetState() const { return mState; }

	void SetTitle(const std::string& text, const Color& color = Color(), int pointSize = 40);

protected:
	void DrawTexture(class Shader* shader, class Texture* texture, const Vector2& offset = Vector2(), float scale = 1.f);
	class Game* game;
	class Font* mFont;
	class Texture* title;
	Vector2 titlePos;
	UIState mState;
};