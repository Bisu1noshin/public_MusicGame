#pragma once
#include "SDL3/SDL.h"
#include "Math.h"

enum ButtonState {
	Off, Down, On, Up
};
class KeyboardState {
public:
	friend class InputSystem;
	KeyboardState() : mCurrState(nullptr), mPrevState{} {}

	//キーが押されているかだけを判別
	bool GetKeyValue(SDL_Scancode keyCode) const;

	//ひとつ前のフレームと現在のフレームから、キーの状態を判別
	ButtonState GetKeyState(SDL_Scancode keyCode) const;

private:
	int keyboardLength = SDL_SCANCODE_COUNT;
	//bool mCurrState[SDL_SCANCODE_COUNT];
	const bool* mCurrState;
	bool mPrevState[SDL_SCANCODE_COUNT];
};

class MouseState {
public:
	MouseState() : mMousePos(), mCurrButtons(0), mPrevButtons(0), isRelative(false), mScrollWheel() {}
	friend class InputSystem;

	const Vector2& GetPosition() const { return mMousePos; }
	bool GetButtonValue(int button) const;
	ButtonState GetButtonState(int button) const;
	Vector2 GetScrollWheel() const { return mScrollWheel; }

private:
	Vector2 mMousePos;
	Uint32 mCurrButtons;
	Uint32 mPrevButtons;
	bool isRelative;
	Vector2 mScrollWheel;
};

class InputState {
public:
	KeyboardState keyboard;
	MouseState mouse;
};

class InputSystem {
public:
	InputSystem(class Game* owner_) : owner(owner_) { Initialize(); }
	bool Initialize();
	void Shutdown();

	void PrepareForUpdate();
	void Update();

	const InputState& GetState() const { return state; }
	ButtonState GetKeyStates(SDL_Scancode keyCode) const { return state.keyboard.GetKeyState(keyCode); }
	void ProcessEvent(SDL_Event& event);
private:
	void SetRelativeMouseMode(SDL_Window* window, bool value);

	class Game* owner;
	InputState state;
};

